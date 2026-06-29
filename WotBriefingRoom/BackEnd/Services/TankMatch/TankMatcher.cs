using DTO.TanksModels;
using MongoDB.Driver;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using BackEnd.Services.MongoDb;

/*The class is tested in an separete projcet. 
 * Right now same tanks dont matches the api's names.
 * When the TankMatcher state is reaches the 80% matches then added to this project
 */

namespace BackEnd.Services.TankMatch
{
    public class TankMatcher
    {
        private readonly IMongoCollection<TankData> _tankCollection;
        private readonly ILogger<TankMatcher> _logger;

        public TankMatcher(MongoDbContext context, ILogger<TankMatcher> logger)
        {
            _tankCollection = context.Tanks;
            _logger = logger;
        }

        public Dictionary<string, string> MatchFilesToTanks(string folderPath)
        {
            var fileNames = Directory.GetFiles(folderPath)
                                     .Select(Path.GetFileNameWithoutExtension)
                                     .ToList();

            var tanks = _tankCollection.Find(FilterDefinition<TankData>.Empty).ToList();

            // Normalizáljuk a tank neveket
            var normalizedTanks = new Dictionary<string, List<string>>();
            foreach (var tank in tanks)
            {
                var key = NormalizeName(tank.Name);
                if (!normalizedTanks.ContainsKey(key))
                    normalizedTanks[key] = new List<string>();
                normalizedTanks[key].Add(tank.Name);
            }

            var matches = new Dictionary<string, string>();

            foreach (var fileName in fileNames)
            {
                var normalizedFileName = NormalizeName(fileName);
                var fileTokens = normalizedFileName.Split(' ');

                // Token-alapú direkt egyezés
                var directMatches = normalizedTanks.Where(kvp =>
                {
                    var tankTokens = kvp.Key.Split(' ');
                    return tankTokens.All(t => fileTokens.Contains(t));
                }).ToList();

                if (directMatches.Count == 1)
                {
                    matches[fileName] = directMatches[0].Value.First();
                }
                else if (directMatches.Count > 1)
                {
                    // Tie-breaker: smallest Levenshtein distance
                    var best = directMatches
                        .OrderBy(kvp => LevenshteinDistance(normalizedFileName, kvp.Key))
                        .First();

                    _logger.LogInformation($"[AMBIGUOUS → PICKED] '{fileName}' több egyezés: {string.Join(", ", directMatches.Select(k => k.Value.First()))} => választott: {best.Value.First()}");
                    matches[fileName] = best.Value.First();
                }
                else
                {
                    var fuzzyMatch = FindBestMatch(normalizedFileName, normalizedTanks, threshold: 4);
                    if (fuzzyMatch != null)
                    {
                        _logger.LogInformation($"[FUZZY] '{fileName}' -> '{fuzzyMatch}'");
                        matches[fileName] = fuzzyMatch;
                    }
                }

            }

            return matches;
        }

        private string NormalizeName(string name)
        {
            name = name.ToLowerInvariant();
            name = RemoveDiacritics(name);

            name = Regex.Replace(name, @"^[a-z]{1,2}\d{1,3}_", "");  // prefix eltávolítás
            name = Regex.Replace(name, @"\b(kpz|pz|obj|object|version|fallout|grandfinal|gup|captured|prototype|ter|bis|shxxi|sh)\b", "", RegexOptions.IgnoreCase);

            name = name.Replace(".", "");
            name = name.Replace("_", " ");
            name = name.Replace("-", " ");
            name = Regex.Replace(name, @"[()\[\]{}]", "");

            name = Regex.Replace(name, @"\s+", " ").Trim();

            name = Regex.Replace(name, @"_siege_mode$", "");
            return name;
        }

        private string RemoveDiacritics(string text)
        {
            var normalized = text.Normalize(NormalizationForm.FormD);
            var builder = new StringBuilder();
            foreach (var c in normalized)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    builder.Append(c);
            }
            return builder.ToString().Normalize(NormalizationForm.FormC);
        }

        private string FindBestMatch(string fileNorm, Dictionary<string, List<string>> normalizedTanks, int threshold = 3)
        {
            string bestMatch = null;
            int bestDistance = int.MaxValue;

            foreach (var kv in normalizedTanks)
            {
                int distance = LevenshteinDistance(fileNorm, kv.Key);
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    bestMatch = kv.Value.First();
                }
                else if (distance == bestDistance)
                {
                    // Ha két egyenlő távolságú találat van, ambiguitás
                    bestMatch = null;
                }
            }

            return bestDistance <= threshold ? bestMatch : null;
        }

        private int LevenshteinDistance(string s, string t)
        {
            int[,] d = new int[s.Length + 1, t.Length + 1];

            for (int i = 0; i <= s.Length; i++) d[i, 0] = i;
            for (int j = 0; j <= t.Length; j++) d[0, j] = j;

            for (int i = 1; i <= s.Length; i++)
            {
                for (int j = 1; j <= t.Length; j++)
                {
                    int cost = s[i - 1] == t[j - 1] ? 0 : 1;
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost
                    );
                }
            }
            return d[s.Length, t.Length];
        }
    }
}

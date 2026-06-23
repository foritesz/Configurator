using DTO.TanksModels;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace BackEnd.Services.Xml
{
    public class XmlParser
    {
        private readonly ILogger<XmlParser> _logger;

        public List<string> ProcessFolder(string rootFolder)
        {
            var outputs = new List<string>();

            if (!Directory.Exists(rootFolder))
                return outputs;  // Üres lista, ha nem létezik a mappa

            string[] xmlFiles = Directory.GetFiles(rootFolder, "*.xml", SearchOption.AllDirectories);

            foreach (var file in xmlFiles)
            {
                var sb = new StringBuilder();
                sb.AppendLine($"=== Fájl: {file} ===");

                try
                {
                    XDocument doc = XDocument.Load(file);
                    XElement root = doc.Root;

                    sb.Append(PrintSection(root, "crew"));
                    sb.Append(PrintSection(root, "postProgressionTree"));
                    sb.Append(PrintSection(root, "speedLimits"));
                    sb.Append(PrintSection(root, "invisibility", new List<string> { "moving", "still", "camouflageBonus" , "firePenalty" }));
                    sb.Append(PrintSection(root, "hull", new List<string> { "armor", "primaryArmor", "weight", "ammoBayHealth" }));
                    sb.Append(PrintFirstNodeWithChildren(root, "chassis", new List<string> { "armor", "weight", "maxLoad", "terrainResistance", "rotationSpeed", "shotDispersionFactors" }));
                    sb.Append(PrintFirstNodeWithChildren(root, "turrets0", new List<string> { "armor", "turretRotatorHealth", "surveyingDeviceHealth" }));
                    sb.Append(PrintFirstNodeWithChildren(root, "guns", new List<string> { "armor", "turretYawLimits", "pitchLimits", "reloadTime", "aimingTime", "shotDispersionFactors", "invisibilityFactorAtShot", "shotDispersionRadius", "dualGun" }));
                    sb.Append(PrintFirstNodeWithChildren(root, "engines"));
                    sb.Append(PrintFirstNodeWithChildren(root, "fuelTanks"));
                    sb.Append(PrintFirstNodeWithChildren(root, "radios"));
                    sb.Append(PrintFirstNodeWithChildren(root, "fuelTank"));
                    sb.Append(PrintSection(root, "siege_mode"));
                }
                catch (Exception ex)
                {
                    sb.AppendLine($"Hiba: {ex.Message}");
                }

                outputs.Add(sb.ToString());
            }

            return outputs;
        }

        public string PrintSection(XElement root, string tagName, List<string> filterChildren = null)
        {
            var node = root.Descendants(tagName).FirstOrDefault();
            if (node == null) return string.Empty;

            var sb = new StringBuilder();
            sb.AppendLine($"<{tagName}>");

            foreach (var child in node.Elements())
            {
                if (filterChildren == null || filterChildren.Contains(child.Name.LocalName))
                {
                    sb.Append(PrintNodeRecursive(child, "  "));
                }
                else if (!string.IsNullOrWhiteSpace(child.Value) && !child.HasElements)
                {
                    sb.AppendLine($"  <{child.Name.LocalName}>{child.Value.Trim()}</{child.Name.LocalName}>");
                }
            }

            sb.AppendLine($"</{tagName}>");
            return sb.ToString();
        }

        public string PrintFirstNodeWithChildren(XElement root, string parentTag, List<string> filterChildren = null)
        {
            var parent = root.Descendants(parentTag).FirstOrDefault();
            if (parent == null || !parent.HasElements) return string.Empty;

            var sb = new StringBuilder();
            sb.AppendLine($"<{parent.Name.LocalName}>");

            foreach (var element in parent.Elements())
            {
                sb.AppendLine($"  <{element.Name.LocalName}>");

                foreach (var child in element.Elements())
                {
                    if (filterChildren == null || filterChildren.Contains(child.Name.LocalName))
                    {
                        sb.Append(PrintNodeRecursive(child, "    "));
                    }
                    else if (!string.IsNullOrWhiteSpace(child.Value) && !child.HasElements)
                    {
                        sb.AppendLine($"    <{child.Name.LocalName}>{child.Value.Trim()}</{child.Name.LocalName}>");
                    }
                }

                sb.AppendLine($"  </{element.Name.LocalName}>");
            }

            sb.AppendLine($"</{parent.Name.LocalName}>");
            return sb.ToString();
        }

        public string PrintNodeRecursive(XElement node, string indent)
        {
            var sb = new StringBuilder();

            if (!node.HasElements)
            {
                sb.AppendLine($"{indent}<{node.Name.LocalName}>{node.Value.Trim()}</{node.Name.LocalName}>");
                return sb.ToString();
            }

            sb.AppendLine($"{indent}<{node.Name.LocalName}>");

            foreach (var child in node.Elements())
            {
                sb.Append(PrintNodeRecursive(child, indent + "  "));
            }

            sb.AppendLine($"{indent}</{node.Name.LocalName}>");
            return sb.ToString();
        }

       public static (float? forward, float? backward) GetTankSpeeds(string xmlData)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(xmlData)) return (null, null);

                string xmlContent = xmlData.Split("=== Fájl:").LastOrDefault();
                if (string.IsNullOrWhiteSpace(xmlContent)) return (null, null);

                var xml = XElement.Parse(xmlContent.Trim());

                var forwardStr = xml.Element("speedLimits")?.Element("forward")?.Value;
                var backwardStr = xml.Element("speedLimits")?.Element("backward")?.Value;

                float? forward = float.TryParse(forwardStr, out float f) ? f : null;
                float? backward = float.TryParse(backwardStr, out float b) ? b : null;

                return (forward, backward);
            }
            catch (Exception ex)
            {
                // Cseréld le loggolásra
                Console.WriteLine($"[GetTankSpeeds] XML parse error: {ex.Message}");
                return (null, null);
            }
        }


        public Dictionary<string, BsonDocument> ParseXmlFile(string filePath)
        {
            var result = new Dictionary<string, BsonDocument>();
            var doc = XDocument.Load(filePath);
            var root = doc.Root;

            void AddSection(string name, List<string> filter = null)
            {
                var node = root.Descendants(name).FirstOrDefault();
                if (node != null)
                {
                    result[name] = XElementToBson(node, filter);
                }
            }

            void AddFirstChildSection(string name, List<string> filter = null)
            {
                var parent = root.Descendants(name).FirstOrDefault();
                if (parent != null && parent.HasElements)
                {
                    var innerDoc = new BsonDocument();

                    foreach (var element in parent.Elements())
                    {
                        innerDoc[element.Name.LocalName] = XElementToBson(element, filter);
                    }

                    result[name] = innerDoc;
                }
            }

            AddSection("crew");
            AddSection("postProgressionTree");
            AddSection("speedLimits");
            AddSection("invisibility", new List<string> { "moving", "still", "camouflageBonus", "firePenalty" });
            AddSection("hull", new List<string> { "armor", "primaryArmor", "weight", "ammoBayHealth" });
            AddSection("siege_mode");

            // Több belső elemű (mint pl. "guns", "chassis" stb.)
            AddFirstChildSection("chassis", new List<string> { "armor", "weight", "maxLoad", "terrainResistance", "rotationSpeed", "shotDispersionFactors" });
            AddFirstChildSection("turrets0", new List<string> { "armor", "turretRotatorHealth", "surveyingDeviceHealth" , "circularVisionRadius" });
            AddFirstChildSection("guns", new List<string> { "armor", "turretYawLimits", "pitchLimits", "reloadTime", "aimingTime", "shotDispersionFactors", "invisibilityFactorAtShot", "shotDispersionRadius",});
            AddFirstChildSection("engines");
            AddFirstChildSection("fuelTanks");
            AddFirstChildSection("fuelTank");
            AddFirstChildSection("radios");


            return result;
        }

        private BsonDocument XElementToBson(XElement element, List<string> filter = null)
        {
            var doc = new BsonDocument();

            foreach (var child in element.Elements())
            {
                string tagName = child.Name.LocalName;

                // Ha armor blokkban vagyunk, a crew tageket hagyjuk ki
                if (element.Name.LocalName == "armor")
                {
                    if (tagName == "commander" ||
                        tagName == "driver" ||
                        tagName == "loader" ||
                        tagName == "radioman" ||
                        tagName == "gunner" ||
                        tagName == "surveyingDevice" ||
                        tagName.StartsWith("gunner_", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                }

                if (filter == null || filter.Contains(tagName))
                {
                    if (child.HasElements)
                    {
                        doc[tagName] = XElementToBson(child);
                    }
                    else
                    {
                        doc[tagName] = child.Value.Trim();
                    }
                }
            }

            return doc;
        }
    }

}

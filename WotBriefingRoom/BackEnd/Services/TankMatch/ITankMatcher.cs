namespace BackEnd.Services.TankMatch
{
    public interface ITankMatcher
    {
        Dictionary<string, string> MatchFilesToTanks(string folderPath);
    }
}

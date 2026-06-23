using MongoDB.Bson;

namespace BackEnd.Services.Xml
{
    public interface IXmlParser
    {
        Dictionary<string, BsonDocument> ParseXmlFile(string filePath);
    }
}

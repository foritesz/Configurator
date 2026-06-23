using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CrewData
    {
        [BsonElement("commander")]
        public string Commander { get; set; } = string.Empty;

        [BsonElement("gunner")]
        public string Gunner { get; set; } = string.Empty;

        [BsonElement("driver")]
        public string Driver { get; set; } = string.Empty;

        [BsonElement("radioman")]
        public string Radioman { get; set; } = string.Empty;

        [BsonElement("loader")]
        public string Loader { get; set; } = string.Empty;
    }
}

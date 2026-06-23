using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SpeedLimitsData
    {
        [BsonElement("forward")]
        public string Forward { get; set; } = string.Empty;

        [BsonElement("backward")]
        public string Backward { get; set; } = string.Empty;
    }
}

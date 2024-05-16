using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoriesLayer.Models
{
    public class PhoneNumber
    {
        [BsonElement("number")]
        public string Number { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }
    }
}

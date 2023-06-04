using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccessTest.Models
{
    public class CarData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("cars")]
        public List<Car> Cars { get; set; }
    }

    public class Car
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}

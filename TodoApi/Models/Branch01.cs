using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

public class Branch01
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; }
        [Column("age_restriction")]
        public string Age { get; set; }
        [Column("name")]
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }
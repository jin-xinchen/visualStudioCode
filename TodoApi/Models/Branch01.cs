using Newtonsoft.Json;

public class Branch01
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; }
    }
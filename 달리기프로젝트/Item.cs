using Newtonsoft.Json;
public record class Item
    {
        [JsonProperty("LABEL")]
        public int LABEL { get; set; }

        [JsonProperty("NAME")]
        public string NAME { get; set; }

        [JsonProperty("STAT")]
        public string STAT { get; set; }

        [JsonProperty("VALUE")]
        public int VALUE { get; set; }

        [JsonProperty("PRICE")]
        public int PRICE { get; set; }
    }

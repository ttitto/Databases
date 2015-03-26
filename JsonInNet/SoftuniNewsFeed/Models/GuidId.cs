namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;
    public class GuidId
    {
        [JsonProperty("#text")]
        public int Id { get; set; }

        [JsonProperty("@isPermalink")]
        public bool IsPermalink { get; set; }
    }
}

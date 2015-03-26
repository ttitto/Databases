namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class Item
    {
        public GuidId Guid { get; set; }

        public string Link { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [JsonProperty("a10:updated")]
        public DateTime Updated { get; set; }
    }
}

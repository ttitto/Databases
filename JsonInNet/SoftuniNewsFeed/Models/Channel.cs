namespace Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Channel
    {
        public Channel()
        {
            this.NewsItems = new List<Item>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Link { get; set; }

        public DateTime LastBuildDate { get; set; }

        public IList<Item> NewsItems { get; set; }
    }
}

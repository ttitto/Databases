namespace SoftuniNewsFeed.Client
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    public class JsonParser
    {
        public static string ConvertXmlToJsonNews(string xmlFilePath)
        {
            XDocument xmlDoc = XDocument.Load(xmlFilePath);
            string jsonNews = JsonConvert.SerializeXNode(xmlDoc, Formatting.Indented);
            return jsonNews;
        }

        public static IEnumerable<JToken> ExtractNewsNames(string xmlFilePath)
        {
            JObject jObj = JObject.Parse(ConvertXmlToJsonNews(xmlFilePath));
            var titles = jObj["rss"]["channel"]["item"].Select(it => it["title"]).ToList();
            return titles;
        }
    }
}

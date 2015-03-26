namespace SoftuniNewsFeed.Client
{
    using Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    public class Program
    {
        static void Main(string[] args)
        {
            string url = "https://softuni.bg/Feed/News";
            string xmlOutputPath = @"../../SoftuniNews.xml";

            Console.OutputEncoding = Encoding.UTF8;

            WebClient rssDownloadClient = new WebClient();
            try
            {
                rssDownloadClient.DownloadFile(url, xmlOutputPath);
                Console.WriteLine("File downloaded successfully.");
            }
            catch (WebException)
            {
                Console.WriteLine("An error occurred while downloading data. The file is not found in the requested url");
            }

            string jsonNews = JsonParser.ConvertXmlToJsonNews(xmlOutputPath);
            if (jsonNews != string.Empty)
            {
                Console.WriteLine("News were successfully extracted into json.");
            }

            try
            {
                IEnumerable<JToken> titles = JsonParser.ExtractNewsNames(xmlOutputPath);
                foreach (JToken title in titles)
                {
                    Console.WriteLine(title);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Message: {0}, Inner: {1}", ex.Message, ex.InnerException.Message));
            }

            // Task 4:
            JObject newsItemsJson = JObject.Parse(jsonNews);
            JToken channel = newsItemsJson["rss"]["channel"];
            List<JToken> newsItems = newsItemsJson["rss"]["channel"]["item"].Children().ToList();

            Channel channelPoco = JsonConvert.DeserializeObject<Channel>(channel.ToString());
            foreach (var item in newsItems)
            {
                JToken guid = item["guid"];
                GuidId guidPoco = JsonConvert.DeserializeObject<GuidId>(guid.ToString());
                var itemToAdd = JsonConvert.DeserializeObject<Item>(item.ToString());
                itemToAdd.Guid = guidPoco;

                channelPoco.NewsItems.Add(itemToAdd);
            }

            // Task 5:
            string htmlFilePath = @"..\..\SoftuniNews.html";
            StringBuilder htmlString = new StringBuilder();
            htmlString.Append(@"<!DOCTYPE html>

                            <html lang=""en"" xmlns=""http://www.w3.org/1999/xhtml"">
                            <head>
                                <meta charset=""utf-8"" />
                                <title>News Feed of SoftUni</title>
                            </head>
                            <body>");
            htmlString.AppendLine(string.Format("<h2>{0}</h2>", channelPoco.Title));
            foreach (var item in channelPoco.NewsItems)
            {
                htmlString.AppendLine(string.Format("<a href=\"{0}\"  title=\"{2}\">{1}</a><br>",
                WebUtility.HtmlDecode(item.Link),
                 WebUtility.HtmlDecode(item.Title),
                 WebUtility.HtmlEncode(item.Description)));
            }

            htmlString.AppendLine("</body></html>");

            File.WriteAllText(htmlFilePath, htmlString.ToString());

            Process.Start(htmlFilePath);
        }
    }
}

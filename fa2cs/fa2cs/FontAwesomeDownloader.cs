using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace fa2cs
{
    public class FontAwesomeDownloader
    {
        public const string Endpoint = "https://fontawesome.com/cheatsheet";

        public async Task<List<FontAwesomeIcon>> DownloadIconCodes()
        {
            var client = new HttpClient();

            Console.WriteLine("Downloading: " + Endpoint);

            var htmlContent = await client.GetStringAsync(Endpoint);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlContent);

            var nodes = htmlDocument.DocumentNode.SelectNodes("//script[@data-prerender='keep']");

            HtmlNode node = null;
            foreach (var n in nodes)
            {
                if (n.InnerText.Contains("window.__inline_data__"))
                {
                    node = n;
                    break;
                }
            }

            if (node == null)
            {
                throw new Exception("Could not find json data to build font awesome codes");
            }

            var jsonContent = node.InnerText;

            var jsonStart = jsonContent.IndexOf("[{"); 
            var jsonEnd = jsonContent.LastIndexOf("}]");

            jsonContent = jsonContent.Substring(jsonStart, jsonEnd - jsonStart + "}]".Length);

            var fontAwesome = QuickType.FontAwesome.FromJson(jsonContent);

            var result = new List<FontAwesomeIcon>();

            foreach (var fa in fontAwesome)
            {
                var data = fa.Data;

                foreach (var datum in fa.Data)
                {
                    if (datum.Type == QuickType.TypeEnum.Icon)
                    {
                        result.Add(new FontAwesomeIcon(datum));
                    }
                }
            }

            Console.WriteLine("Discovered " + result.Count + " icons");

            return result;
        }
    }
}

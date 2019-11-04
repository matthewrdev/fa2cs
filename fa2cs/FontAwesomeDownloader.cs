using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace fa2cs
{
    public class FontAwesomeDownloader
    {
        public async Task<List<FontAwesomeIcon>> DownloadIconCodes(string endpoint)
        {
            var client = new HttpClient();

            Console.WriteLine("Downloading: " + endpoint);

            var htmlContent = await client.GetStringAsync(endpoint);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlContent);

            var node = htmlDocument.DocumentNode.SelectNodes("//script[contains(text(),'window.__inline')]").SingleOrDefault();
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

            Console.WriteLine("Discovered " + result.Count + " icons from " + endpoint);

            return result;
        }
    }
}

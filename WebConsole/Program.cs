using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            StartCrawlerAsync();
            Console.Read();
        }

        private static async Task StartCrawlerAsync()
        {
            var url = "http://www.automobile.tn/neuf/bmw.3/";
            var httpClient = new HttpClient();
        var html= await httpClient.GetStringAsync(url);
           var htmlDocument = new HtmlDocument(); 

            htmlDocument.LoadHtml(html);

            var divs =  htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("Class", "").Equals("article_new_car article_last_modele")).ToList();
            var cars = new List<Car>();
            foreach (var div in divs)
            {
                var car = new Car()
                {
                     Model  = div.Descendants("h2").FirstOrDefault().InnerText,
    Prix = div.Descendants("span").FirstOrDefault().InnerText,
     ImageUrl = div.Descendants("img").FirstOrDefault().ChildAttributes("src").FirstOrDefault().Value,
    Link= div.Descendants("a").FirstOrDefault().ChildAttributes("href").FirstOrDefault().Value,
                };
                 cars.Add(car);   
            }


        }
    }
    [DebuggerDisplay("{Model},{Prix}")]
    class Car 
    {
        public string Model { get; set; }
        public string Prix { get; set; }

        public string ImageUrl { get; set; }
        public string Link { get; set; }
    }

}

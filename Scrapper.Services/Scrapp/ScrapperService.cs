using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrapper.Services.Scrapp
{
    public class ScrapperService : IScrapperService
    {
        public ScrapperService()
        {
        }

        public async Task RunAsync()
        {
            Console.WriteLine("START SCRAPPING");
        }
    }
}

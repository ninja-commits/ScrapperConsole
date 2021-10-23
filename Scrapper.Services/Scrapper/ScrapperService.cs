using Microsoft.Extensions.Options;
using Scrapper.Configuration.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrapper.Services.Scrapp
{
    public class ScrapperService : IScrapperService
    {
        private readonly Position _options;

        public ScrapperService(IOptions<Position> options)
        {
            _options = options.Value;
        }

        public async Task RunAsync()
        {
            Console.WriteLine("START SCRAPPING");
            Console.WriteLine(_options.Name);
        }
    }
}

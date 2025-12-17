using Google.Apis.Sheets.v4;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiberiaApp.Classes
{
    class GoogleSheetReader
    {
        private readonly SheetsService _sheetsService;
        private readonly ILogger<GoogleSheetReader> _logger;

        public GoogleSheetReader(SheetsService sheetsService, ILogger<GoogleSheetReader> logger = null)
        {
            _sheetsService = sheetsService;
            _logger = logger;
        }


    }
}

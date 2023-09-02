using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Gateway.Common.DTOs
{
    public class MessageDto
    {
        public string code { get; set; } = string.Empty;

        public string description { get; set; } = string.Empty;
        
        public string help { get; set; } = string.Empty;

        public string logError { get; set; } = string.Empty;
    }
}

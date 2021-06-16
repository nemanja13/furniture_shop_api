using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class LogDto : DtoBase
    {
        public DateTime CreatedAt { get; set; }
        public string UseCaseName { get; set; }
        public string Data { get; set; }
        public string Actor { get; set; }
    }
}

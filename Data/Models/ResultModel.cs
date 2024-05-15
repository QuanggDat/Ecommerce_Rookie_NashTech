using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ResultModel
    {
        public string? errorMessage { get; set; }
        public object? Data { get; set; }
        public bool succeed { get; set; } = false;
    }
    public class ResponeResultModel
    {
        public string? errorMessage { get; set; }
    }

    public class PagingModel
    {
        public object? data { get; set; }
        public int total { get; set; } = 0;
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HepsiTools.Entities
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string Data { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string StackTrace { get; set; }
    }
}

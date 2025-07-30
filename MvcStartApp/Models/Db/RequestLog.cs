using System.ComponentModel.DataAnnotations.Schema;

namespace MvcStartApp.Models.Db
{
    [Table("RequestLogs")]
    public class RequestLog
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }
    }
}

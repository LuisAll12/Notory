using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notory.Models
{
    public class CalendarPost
    {
        public string MongoId {  get; set; }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string BackgroundColor { get; set; }
        public int Duration
        {
            get
            {
                if (DateTime.TryParse(TimeFrom, out var from) && DateTime.TryParse(TimeTo, out var to))
                {
                    return (int)(to - from).TotalMinutes;
                }
                return 0;
            }
        }

        public bool HasData => !string.IsNullOrEmpty(Title) || !string.IsNullOrEmpty(Subtitle);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuickPillApp.Library.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public IEnumerable<TimeOnly> Time { get; set; }
        public int Period { get; set; }
        public int Frequency { get; set; }
        public int SlotId { get; set; }
        [JsonIgnore]
        public string TimeString
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                foreach (var item in Time)
                {
                    sb.Append($"{item:HH:mm}, ");
                }
                sb.Remove(sb.Length - 2, 2);
                return sb.ToString(); ;
            }
        }

        [JsonIgnore]
        public string PillName { get; set; }
    }
}

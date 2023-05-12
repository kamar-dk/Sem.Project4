using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FA_DB.Models.TraningTypes;

namespace FA_DB.Models
{
    public class TraningData
    {
        public long Id { get; set; }
        public string TrainingType { get; set; } = "";
        public DateTime SessionDate { get; set; }
        public float Distance { get; set; }
        public int SessionHourTime { get; set; }
        public int SessionMinuteTime { get; set; }
        public int SessionSecondTime { get; set; }
        //public TimeSpan
        public int Calories { get; set; }
        public int MaxHeartRate { get; set; }
        public int MinHeartRate { get; set; }
        public int AvgHeartRate { get; set; }
        public float Vo2Max { get; set; }
        [ForeignKey("Id")]
        public string UserId { get; set; }
        public User User { get; set; } = new User();
                
        //public ICollection<RunningSession>? RunningSessions { get; set; }
        //public ICollection<BikeSession>? BikeSessions { get; set; }
    }
}

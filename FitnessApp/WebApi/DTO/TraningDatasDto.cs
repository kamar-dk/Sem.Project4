using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Models;
using WebApi.Data;

namespace WebApi.DTO
{
    public class TraningDatasDto
    {
        public long Id { get; set; }

        public string UserId { get; set; }

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

        




        //public User User { get; set; }
        
    }


}

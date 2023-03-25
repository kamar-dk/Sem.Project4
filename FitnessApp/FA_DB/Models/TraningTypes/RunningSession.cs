using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA_DB.Models.TraningTypes
{
    public class RunningSession
    {
        [Key]
        public int SessionId { get; set; }
        public DateTime Date { get; set; }
        public float Durration { get; set; }
        public float Distance { get; set; }
        public float AvgSpeed { get; set; }
        public string Note { get; set; }

        public TraningData traningData { get; set; }
    }
}

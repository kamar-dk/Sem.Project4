﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Models.TraningTypes;

namespace WebApi.Models
{
    public class TraningData
    {
        public string Email { get; set; }
        public User User { get; set; }
        public ICollection<RunningSession>? RunningSessions { get; set; }
        public ICollection<BikeSession>? BikeSessions { get; set; }
    }
}
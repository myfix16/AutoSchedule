﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoSchedule.Core.Models;

namespace AutoSchedule.UI.Services
{
    public class AppDataService
    {
        public IEnumerable<IEnumerable<Session>> AvailableSessions { get; set; }
        public IEnumerable<string> AvailableClasses { get; set; }
    }
}
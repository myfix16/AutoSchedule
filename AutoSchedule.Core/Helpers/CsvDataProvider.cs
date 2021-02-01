using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using AutoSchedule.Core.Models;
using CsvHelper;

namespace AutoSchedule.Core.Helpers
{
    public class CsvDataProvider : IDataProvider<IEnumerable<Session>>
    {
        private readonly string csvDirectionPath;

        public CsvDataProvider(string csvPath) => csvDirectionPath = csvPath;

        public IEnumerable<Session> GetSessions()
        {
            return ReadSessions(csvDirectionPath);
        }

        [Obsolete("This is actually a fake async method using await new Task.")]
        public async Task<IEnumerable<Session>> GetSessionsAsync()
        {
            return await new Task<IEnumerable<Session>>(() => ReadSessions(csvDirectionPath));
        }

        private IEnumerable<Session> ReadSessions(string csvPath)
        {
            var sessions = new List<Session>();
            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Read();
            csv.ReadHeader();
            while (csv.Read())
            {
                var codeField = csv.GetField("Code");
                var name = $"{csv.GetField("Name").Split('-')[0]} {codeField[0..3]}";
                var sessionType = codeField[4..7];
                var instructor = csv.GetField("Instructor");
                var timesField = csv.GetField("Time").Split(';');
                var sessionTimes = new List<SessionTime>();
                foreach (var timeString in timesField)
                {
                    var splittedTime = timeString.Split(' ');
                    // Do something.
                    for (int i = 0; i < splittedTime[0].Length; i += 2)
                    {
                        var dayOfWeek = splittedTime[0][i..(i + 2)] switch
                        {
                            "Mo" => DayOfWeek.Monday,
                            "Tu" => DayOfWeek.Tuesday,
                            "We" => DayOfWeek.Wednesday,
                            "Th" => DayOfWeek.Thursday,
                            "Fr" => DayOfWeek.Friday,
                            _ => throw new NotImplementedException("No such weekday.")
                        };
                        sessionTimes.Add(new SessionTime(dayOfWeek, new Time(splittedTime[1]), new Time(splittedTime[3])));
                    }
                }
                // 2. split each item such as WeFr 10:00-11:00
                sessions.Add(new Session(sessionType, name, codeField[8..12], instructor, sessionTimes));
            }
            return sessions;
        }
    }
}

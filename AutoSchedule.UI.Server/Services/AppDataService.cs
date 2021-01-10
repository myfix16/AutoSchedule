using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoSchedule.Core.Models;
using AutoSchedule.Core.Helpers;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;

namespace AutoSchedule.UI.Server.Services
{
    public class AppDataService
    {
        public IEnumerable<IEnumerable<Session>> AvailableSessions { get; set; } = new List<List<Session>>();
        public IEnumerable<string> AvailableClasses { get; set; } = new List<string>();
        public string Version { get; set; } = "1.0.0 Preview";
        public Schedule CurrentSchedule { get; set; } = new();
        public ObservableCollection<string> FilteredClasses { get; set; } = new();
        public ObservableCollection<string> SelectedClasses { get; set; } = new();
        public ObservableCollection<Schedule> AvailableSchedules { get; set; } = new();

        public async Task InitializeAsync(HttpClient Http, string path)
        {
            if (AvailableSessions == null || !AvailableSessions.Any())
            {
                #region
                /*AvailableSessions = new List<List<Session>>
                {
                // FIN 3080
                new List<Session>
                {
                    new Session
                        ("Lecture","FIN3080","1369","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Thursday,new Time(8,30),new Time(10,20)),
                            new SessionTime(DayOfWeek.Friday,new Time(8,30),new Time(9,20)),
                        }),
                    new Session
                        ("Lecture","FIN3080","1370","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Thursday,new Time(10,30),new Time(12,20)),
                            new SessionTime(DayOfWeek.Friday,new Time(10,30),new Time(11,20)),
                        }),
                    new Session
                        ("Lecture","FIN3080","1371","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Thursday,new Time(13,30),new Time(15,20)),
                            new SessionTime(DayOfWeek.Friday,new Time(13,30),new Time(14,20)),
                        }),
                },
                // FIN 4110
                new List<Session>
                {
                    new Session
                        ("Lecture","FIN4110","1384","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Wednesday,new Time(10,30),new Time(11,50)),
                            new SessionTime(DayOfWeek.Friday,new Time(9,00),new Time(10,20)),
                        }),
                    new Session
                        ("Lecture","FIN4110","1385","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Wednesday,new Time(13,30),new Time(14,50)),
                            new SessionTime(DayOfWeek.Friday,new Time(10,30),new Time(11,50)),
                        }),
                },
                // FIN 4210
                new List<Session>
                {
                    new Session
                        ("Lecture","FIN4210","1778","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Tuesday,new Time(10,30),new Time(11,50)),
                            new SessionTime(DayOfWeek.Thursday,new Time(10,30),new Time(11,50)),
                        }),
                },
                // MAT 3007
                new List<Session>
                {
                    new Session
                        ("Lecture","MAT3007","1445","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Wednesday,new Time(14,00),new Time(15,20)),
                            new SessionTime(DayOfWeek.Friday,new Time(14,00),new Time(15,20)),
                        }),
                },
                // MAT 2002
                new List<Session>
                {
                    new Session
                        ("Lecture","MAT2002","1426","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Tuesday,new Time(10,30),new Time(11,50)),
                            new SessionTime(DayOfWeek.Thursday,new Time(10,30),new Time(11,50)),
                        }),
                    new Session
                        ("Lecture","MAT2002","1427","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Tuesday,new Time(13,30),new Time(14,50)),
                            new SessionTime(DayOfWeek.Thursday,new Time(13,30),new Time(14,50)),
                        }),
                },
                // FTE 4560
                new List<Session>
                {
                    new Session
                        ("Lecture","FTE4560","2062","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Tuesday,new Time(8,30),new Time(9,50)),
                            new SessionTime(DayOfWeek.Thursday,new Time(8,30),new Time(9,50)),
                        }),
                },
                // DMS 3003
                new List<Session>
                {
                    new Session
                        ("Lecture","DMS3003","1936","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Tuesday,new Time(8,30),new Time(9,50)),
                            new SessionTime(DayOfWeek.Thursday,new Time(8,30),new Time(9,50)),
                        }),
                    new Session
                        ("Lecture","DMS3003","1937","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Tuesday,new Time(10,30),new Time(11,50)),
                            new SessionTime(DayOfWeek.Thursday,new Time(10,30),new Time(11,50)),
                        }),
                },
                // GEB 2404
                new List<Session>
                {
                    new Session
                        ("Lecture","GEB2404","2111","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Friday,new Time(8,30),new Time(10,20)),
                        }),
                },
                // GEB 2404 TUT
                new List<Session>
                {
                    new Session
                        ("Lecture","GEB2404T","2112","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Wednesday,new Time(8,30),new Time(10,20)),
                        }),
                    new Session
                        ("Lecture","GEB2404T","2113","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Wednesday,new Time(10,30),new Time(12,20)),
                        }),
                    new Session
                        ("Lecture","GEB2404T","2114","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Wednesday,new Time(15,30),new Time(17,20)),
                        }),
                },
                // DDA 4230
                new List<Session>
                {
                    new Session
                        ("Lecture","DDA4230","2059","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Monday,new Time(13,30),new Time(14,50)),
                            new SessionTime(DayOfWeek.Wednesday,new Time(13,30),new Time(14,50)),
                        }),
                },
                // CSC 3170
                new List<Session>
                {
                    new Session
                        ("Lecture","CSC3170","1616","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Monday,new Time(10,30),new Time(11,50)),
                            new SessionTime(DayOfWeek.Wednesday,new Time(10,30),new Time(11,50)),
                        }),
                    new Session
                        ("Lecture","CSC3170","1617","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Monday,new Time(13,30),new Time(14,50)),
                            new SessionTime(DayOfWeek.Wednesday,new Time(13,30),new Time(14,50)),
                        }),
                },
                // RMS 4060
                new List<Session>
                {
                    new Session
                        ("Lecture","RMS4060","1737","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Monday,new Time(15,30),new Time(16,50)),
                            new SessionTime(DayOfWeek.Wednesday,new Time(15,30),new Time(16,50)),
                        }),
                },
                // CSC4020
                new List<Session>
                {
                    new Session
                        ("Lecture","CSC4020","1642","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Monday,new Time(8,30),new Time(9,50)),
                            new SessionTime(DayOfWeek.Wednesday,new Time(8,30),new Time(9,50)),
                        }),
                },
                // ERG3020
                new List<Session>
                {
                    new Session
                        ("Lecture","ERG3020","1744","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Monday,new Time(10,30),new Time(11,50)),
                            new SessionTime(DayOfWeek.Wednesday,new Time(10,30),new Time(11,50)),
                        }),
                },
                // STA3010
                new List<Session>
                {
                    new Session
                        ("Lecture","STA3010","1690","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Monday,new Time(9,00),new Time(10,20)),
                            new SessionTime(DayOfWeek.Wednesday,new Time(9,00),new Time(10,20)),
                        }),
                    new Session
                        ("Lecture","STA3010","1691","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Monday,new Time(13,30),new Time(14,50)),
                            new SessionTime(DayOfWeek.Wednesday,new Time(13,30),new Time(14,50)),
                        }),
                },
                // CSC 3050
                new List<Session>
                {
                    new Session
                        ("Lecture","CSC3050","1607","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Tuesday,new Time(13,30),new Time(14,50)),
                            new SessionTime(DayOfWeek.Thursday,new Time(13,30),new Time(14,50)),
                        }),
                    new Session
                        ("Lecture","CSC3050","1608","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Tuesday,new Time(10,30),new Time(11,50)),
                            new SessionTime(DayOfWeek.Thursday,new Time(10,30),new Time(11,50)),
                        }),

                },
                // EIE 2810
                new List<Session>
                {
                    new Session
                        ("Lecture","EIE2810","1781","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Thursday,new Time(15,00),new Time(17,50)),
                        }),
                    new Session
                        ("Lecture","EIE2810","1782","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Friday,new Time(9,00),new Time(11,50)),
                        }),

                },
                // GEB 2405
                new List<Session>
                {
                    new Session
                        ("Lecture","GEB2405","1969","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Friday,new Time(8,30),new Time(9,20)),
                        }),
                    new Session
                        ("Lecture","GEB2405","1970","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Friday,new Time(10,30),new Time(11,20)),
                        }),
                    new Session
                        ("Lecture","GEB2405","1971","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Friday,new Time(14,30),new Time(14,50)),
                        }),
                },
                // ENG2002S
                new List<Session>
                {
                    new Session
                        ("Lecture","ENG2002S","1821","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Monday,new Time(9,00),new Time(10,20)),
                            new SessionTime(DayOfWeek.Wednesday,new Time(9,00),new Time(10,20)),
                        }),
                    new Session
                        ("Lecture","ENG2002S","1691","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Monday,new Time(13,30),new Time(14,50)),
                            new SessionTime(DayOfWeek.Wednesday,new Time(13,30),new Time(14,50)),
                        }),
                },
                // ENG2002B
                new List<Session>
                    {
                        new Session
                            ("Lecture","ENG2002B","????","staff",
                            new List<SessionTime>
                            {
                                new SessionTime(DayOfWeek.Tuesday,new Time(15,30),new Time(16,50)),
                                new SessionTime(DayOfWeek.Thursday,new Time(15,30),new Time(16,50)),
                            }),
                    },
            };*/
                #endregion
                await GetSessionsFromDB();
                AvailableClasses = AvailableSessions
                    .Select(l => l.First().Name).Distinct().OrderBy(s => s);
                foreach (var item in AvailableClasses) FilteredClasses.Add(item);
            }
        }

        private async Task GetSessionsFromDB()
        {
            var cosmosClient = new CosmosClientBuilder
                (DataProvider.GetDBConnectionString("AzureCosmosDB-ConnectionString-ReadOnly", Environment.GetEnvironmentVariable("VaultUri")))
               .WithSerializerOptions(new CosmosSerializationOptions { Indented = true })
               .Build();
            var container = cosmosClient.GetDatabase("SessionsData").GetContainer("SessionsContainer");

            var sqlQueryText = "SELECT * FROM c";
            var queryIterator = container.GetItemQueryIterator<Session>(new QueryDefinition(sqlQueryText));

            // Fetch session data from data base.
            List<Session> sessions = new();
            {
                //Asynchronous query execution
                while (queryIterator.HasMoreResults)
                {
                    foreach (var item in await queryIterator.ReadNextAsync()) sessions.Add(item);
                }
            }

            // reorder the data into IEnumerable<IEnumerable<Session>>.
            AvailableSessions = sessions.GroupBy(s => s.Name);
        }
    }
}

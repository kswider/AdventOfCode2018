using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            using(StreamReader sr = new StreamReader("input.txt"))
            {
                var lines = sr.ReadToEnd().Split("\n").OrderBy(x => x);
                
                var idRegex = new Regex(@"#(?<id>\d+)");
                int asleepMinute = 0;
                int wakeUpMinute = 0;
                var asleepRegex = new Regex(@":(?<minute>\d+).*falls asleep");
                var wakeUpRegex = new Regex(@":(?<minute>\d+).*wakes up");
                string currentId = "";
                var dict = new Dictionary<string,Sleep>();
                foreach(var line in lines)
                {
                    var matches = idRegex.Match(line);
                    if(matches.Success)
                    {
                        currentId = matches.Groups["id"].Value;
                    }
                    else
                    {
                        var sleepMatches = asleepRegex.Match(line);
                        if(sleepMatches.Success)
                        {
                            asleepMinute = int.Parse(sleepMatches.Groups["minute"].Value);
                        }
                        else
                        {
                            var wakeUpMatches = wakeUpRegex.Match(line);
                            wakeUpMinute = int.Parse(wakeUpMatches.Groups["minute"].Value);
                            var sleepTime = wakeUpMinute - asleepMinute;
                            if(dict.ContainsKey(currentId))
                            {
                                var sleep = dict[currentId];
                                sleep.SleepTime += sleepTime;
                                for(int i = asleepMinute;i < wakeUpMinute; i++)
                                {
                                    sleep.SleepMinutes[i]++;
                                }
                            }
                                
                            else
                            {
                                var sleepMinutes = new int[60];
                                for(int i = asleepMinute;i < wakeUpMinute; i++)
                                {
                                    sleepMinutes[i]++;
                                }
                                dict.Add(currentId,new Sleep(sleepTime,sleepMinutes));
                            }
                        }
                    }
                }
                
                var foundGuard = dict.OrderBy(x => x.Value.SleepTime).Last();
                int max = foundGuard.Value.SleepMinutes.Max();
                int minuteMostAsleep = foundGuard.Value.SleepMinutes.ToList().FindIndex(x => x == max ); 
                Console.WriteLine($"Guard {foundGuard.Key}, sleep time {foundGuard.Value.SleepTime}, minute {minuteMostAsleep}");
                Console.WriteLine($"Answer 1 = {int.Parse(foundGuard.Key)*minuteMostAsleep}");

                //excercise 2
                var guard2 = dict.OrderBy(x => x.Value.SleepMinutes.Max()).Last();
                max = guard2.Value.SleepMinutes.Max();
                minuteMostAsleep = guard2.Value.SleepMinutes.ToList().FindIndex(x => x == max ); 
                Console.WriteLine($"Guard {guard2.Key}, sleep time {guard2.Value.SleepTime}, minute {minuteMostAsleep}");
                Console.WriteLine($"Answer 2 = {int.Parse(guard2.Key)*minuteMostAsleep}");

                
            }
        }
    }


}

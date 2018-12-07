using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day7
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var sr = new StreamReader("input.txt"))
            {
                var lines = sr.ReadToEnd().Split("\n");
                var regex = new Regex(@"Step (?<prequisite>\w) .* step (?<step_name>\w)");
                var steps = new Dictionary<string,Prequisites>();
                var allPossibleStepNames = new HashSet<string>();
                foreach(var line in lines)
                {
                    var match = regex.Match(line);
                    var prequisite = match.Groups["prequisite"].Value;
                    var stepName = match.Groups["step_name"].Value;
                    allPossibleStepNames.Add(prequisite);
                    if(steps.ContainsKey(stepName))
                    {
                        steps[stepName].ListOfPrequisites.Add(prequisite);
                    }
                    else
                    {
                        var prequisites = new Prequisites();
                        prequisites.ListOfPrequisites.Add(prequisite);
                        steps.Add(stepName,prequisites);
                    }
                    
                }
                foreach(var entry in allPossibleStepNames)
                {
                    if(!steps.ContainsKey(entry))
                    {
                        steps.Add(entry,new Prequisites());
                    }
                }

                //Excercise1(steps);
                Excercise2(steps);
            }
            
            
        }

        static void Excercise1(Dictionary<string,Prequisites> steps)
        {
            var orderedListOfCommands = new List<string>();
            var allowedSteps = new HashSet<string>();
            while(steps.Count > 0)
            {
                foreach(var entry in steps)
                {
                    entry.Value.SwitchStatus();
                    if(entry.Value.CanBeExecuted)
                    {
                        allowedSteps.Add(entry.Key);
                    }
                }

                var currentStep = allowedSteps.OrderBy(x => x).First();
                allowedSteps.Remove(currentStep);
                steps.Remove(currentStep);
                orderedListOfCommands.Add(currentStep);

                foreach(var entry in steps)
                {
                    entry.Value.ListOfPrequisites.Remove(currentStep);
                }
            }

            Console.WriteLine("Excercise 1 solution:");
            foreach(var command in orderedListOfCommands)
            {
                Console.Write(command);
            }
        }

        static void Excercise2(Dictionary<string,Prequisites> steps)
        {
            var workers = new List<Worker>(){new Worker(1),new Worker(2),new Worker(3),new Worker(4),new Worker(5)};

            var jobsToPick = new HashSet<string>();
            int time;
            for(time = 0;; time++)
            {
                if(workers.All(x => x.IsAvailable) && steps.Count == 0)
                    break;
                foreach(var entry in steps)
                {
                    entry.Value.SwitchStatus();
                    if(entry.Value.CanBeExecuted)
                    {
                        jobsToPick.Add(entry.Key);
                    }
                }

                var jobsToRemove = new List<String>();
                foreach(var job in jobsToPick)
                {
                    var freeWorker = workers.FirstOrDefault(x => x.IsAvailable);
                    if(freeWorker != null)
                    {
                        freeWorker.TakeJob(job);
                        Console.WriteLine($"Worker nr {freeWorker.WorkerNumber} taking job {job} at {time} seconds");
                        jobsToRemove.Add(job);
                    }
                    
                }

                jobsToRemove.ForEach(x => 
                {
                    jobsToPick.Remove(x);
                    steps.Remove(x);
                });

                foreach(var busyWorker in workers.Where(x => !x.IsAvailable))
                {
                    if(busyWorker.Tick())
                    {
                        foreach(var entry in steps)
                        {
                            entry.Value.ListOfPrequisites.Remove(busyWorker.StepBeingDone);
                            
                        }
                    }
                }
                
            }

            Console.WriteLine($"Excercise 2 solution: {time}");
        }
    }
}

using System;

namespace Day7
{
    public class Worker
    {
        public bool IsAvailable { get; set; } = true;
        public int SecondsLeft { get; set; }
        public string StepBeingDone { get; set; }
        public int WorkerNumber { get; }

        public Worker(int workerNumber)
        {
            WorkerNumber = workerNumber;
        }

        public void TakeJob(string job)
        {
            IsAvailable = false;
            StepBeingDone = job;
            SecondsLeft = 61 + (Convert.ToChar(job) - 'A');
        }
        public bool Tick()
        {
            SecondsLeft--;
            if(SecondsLeft == 0)
            {
                IsAvailable = true;
                return true;
            }
            return false;
            
        }

    }
}
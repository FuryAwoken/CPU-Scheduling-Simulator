using System.Runtime.InteropServices;
using CpuSchedulerConsole.Models;

namespace CpuSchedulerConsole.Algorithms;

public static class SchedulingAlgorithms
{
    //
    //FCFS Algorithm (First Come First Serve)
    //
    public static List<SchedulingResult> RunFCFS(List<ProcessData> processes)
    {
        List<SchedulingResult> results = new();
        int currentTime = 0;

        var sortedProcesses = processes
            .OrderBy(p => p.ArrivalTime)
            .ToList();

        foreach (var process in sortedProcesses)
        {
            int startTime = Math.Max(currentTime, process.ArrivalTime);
            int finishTime = startTime + process.BurstTime;
            int waitingTime = startTime - process.ArrivalTime;
            int turnaroundTime = finishTime - process.ArrivalTime;


            results.Add(new SchedulingResult
            {
                ProcessID = process.ProcessID,
                ArrivalTime = process.ArrivalTime,
                BurstTime = process.BurstTime,
                StartTime = startTime,
                FinishTime = finishTime,
                WaitingTime = waitingTime,
                TurnaroundTime = turnaroundTime
            });


            currentTime = finishTime;
        }

        return results;
    }

    //
    //SJF Algorithm (Shortest Job First)
    //
    public static List<SchedulingResult> RunSJF(List<ProcessData> processes)
    {
         var results = new List<SchedulingResult>();
            var currentTime = 0;
            var remainingProcesses = processes.ToList();
            
            while (remainingProcesses.Count > 0)
            {
                // Get processes that have arrived by current time
                var availableProcesses = remainingProcesses.Where(p => p.ArrivalTime <= currentTime).ToList();
                
                if (availableProcesses.Count == 0)
                {
                    // No process has arrived yet, jump to next arrival time
                    currentTime = remainingProcesses.Min(p => p.ArrivalTime);
                    continue;
                }
                
                // Select process with shortest burst time
                var nextProcess = availableProcesses.OrderBy(p => p.BurstTime).ThenBy(p => p.ArrivalTime).First();
                
                var startTime = Math.Max(currentTime, nextProcess.ArrivalTime);
                var finishTime = startTime + nextProcess.BurstTime;
                var waitingTime = startTime - nextProcess.ArrivalTime;
                var turnaroundTime = finishTime - nextProcess.ArrivalTime;
                
                results.Add(new SchedulingResult
                {
                    ProcessID = nextProcess.ProcessID,
                    ArrivalTime = nextProcess.ArrivalTime,
                    BurstTime = nextProcess.BurstTime,
                    StartTime = startTime,
                    FinishTime = finishTime,
                    WaitingTime = waitingTime,
                    TurnaroundTime = turnaroundTime
                });
                
                currentTime = finishTime;
                remainingProcesses.Remove(nextProcess);
            }
            
            return results.OrderBy(r => r.StartTime).ToList();
    }
    
    /// 
    /// Priority Algorithm
    /// 
    public static List<SchedulingResult> RunPriority(List<ProcessData> processes)
    {
       var results = new List<SchedulingResult>();
            var currentTime = 0;
            var remainingProcesses = processes.ToList();
            
            while (remainingProcesses.Count > 0)
            {
                // Get processes that have arrived by current time
                var availableProcesses = remainingProcesses.Where(p => p.ArrivalTime <= currentTime).ToList();
                
                if (availableProcesses.Count == 0)
                {
                    // No process has arrived yet, jump to next arrival time
                    currentTime = remainingProcesses.Min(p => p.ArrivalTime);
                    continue;
                }
                
                // Select process with highest priority (highest number)
                var nextProcess = availableProcesses.OrderByDescending(p => p.Priority).ThenBy(p => p.ArrivalTime).First();
                
                var startTime = Math.Max(currentTime, nextProcess.ArrivalTime);
                var finishTime = startTime + nextProcess.BurstTime;
                var waitingTime = startTime - nextProcess.ArrivalTime;
                var turnaroundTime = finishTime - nextProcess.ArrivalTime;
                
                results.Add(new SchedulingResult
                {
                    ProcessID = nextProcess.ProcessID,
                    ArrivalTime = nextProcess.ArrivalTime,
                    BurstTime = nextProcess.BurstTime,
                    StartTime = startTime,
                    FinishTime = finishTime,
                    WaitingTime = waitingTime,
                    TurnaroundTime = turnaroundTime
                });
                
                currentTime = finishTime;
                remainingProcesses.Remove(nextProcess);
            }
            
            return results.OrderBy(r => r.StartTime).ToList(); 
    }
    
    ///
    /// Round Robin Algorithm
    /// 
    public static List<SchedulingResult> RunRoundRobin(List<ProcessData> processes, int quantumTime = 4)
    {
        var results = new List<SchedulingResult>();
            var currentTime = 0;
            var processQueue = new Queue<ProcessData>();
            var processResults = new Dictionary<string, SchedulingResult>();
            var remainingBurstTimes = new Dictionary<string, int>();
            
            // Initialize remaining burst times and results
            foreach (var process in processes)
            {
                remainingBurstTimes[process.ProcessID] = process.BurstTime;
                processResults[process.ProcessID] = new SchedulingResult
                {
                    ProcessID = process.ProcessID,
                    ArrivalTime = process.ArrivalTime,
                    BurstTime = process.BurstTime,
                    StartTime = -1, // Will be set on first execution
                    FinishTime = 0,
                    WaitingTime = 0,
                    TurnaroundTime = 0
                };
            }
            
            // Add processes that arrive at time 0
            foreach (var process in processes.Where(p => p.ArrivalTime <= currentTime).OrderBy(p => p.ArrivalTime))
            {
                processQueue.Enqueue(process);
            }
            
            var processesNotInQueue = processes.Where(p => p.ArrivalTime > currentTime).OrderBy(p => p.ArrivalTime).ToList();
            
            while (processQueue.Count > 0 || processesNotInQueue.Count > 0)
            {
                // Add any processes that have now arrived
                while (processesNotInQueue.Count > 0 && processesNotInQueue[0].ArrivalTime <= currentTime)
                {
                    processQueue.Enqueue(processesNotInQueue[0]);
                    processesNotInQueue.RemoveAt(0);
                }
                
                if (processQueue.Count == 0)
                {
                    // No processes in queue, jump to next arrival
                    currentTime = processesNotInQueue[0].ArrivalTime;
                    continue;
                }
                
                var currentProcess = processQueue.Dequeue();
                var result = processResults[currentProcess.ProcessID];
                
                // Set start time if this is the first execution
                if (result.StartTime == -1)
                {
                    result.StartTime = currentTime;
                }
                
                // Execute for quantum time or remaining burst time, whichever is smaller
                var executionTime = Math.Min(quantumTime, remainingBurstTimes[currentProcess.ProcessID]);
                currentTime += executionTime;
                remainingBurstTimes[currentProcess.ProcessID] -= executionTime;
                
                // Add any processes that arrived during this execution
                while (processesNotInQueue.Count > 0 && processesNotInQueue[0].ArrivalTime <= currentTime)
                {
                    processQueue.Enqueue(processesNotInQueue[0]);
                    processesNotInQueue.RemoveAt(0);
                }
                
                // Check if process is completed
                if (remainingBurstTimes[currentProcess.ProcessID] == 0)
                {
                    result.FinishTime = currentTime;
                    result.TurnaroundTime = result.FinishTime - result.ArrivalTime;
                    result.WaitingTime = result.TurnaroundTime - result.BurstTime;
                }
                else
                {
                    // Process not completed, add back to queue
                    processQueue.Enqueue(currentProcess);
                }
            }
            return processResults.Values.OrderBy(r => r.StartTime).ToList();
    }
    ///
    /// SRTF(Shortest Remaining Time First)
    /// 
    public static List<SchedulingResult> RunSRTF(List<ProcessData> processes)
    {
       var results = new List<SchedulingResult>();

       var remainingTime = processes.ToDictionary(
        p => p.ProcessID,
        p => p.BurstTime
       );

       int currentTime = 0;
       int completed = 0;

       while (completed < processes.Count)
        {
            var availableProcesses = processes
                .Where(p => p.ArrivalTime <= currentTime && remainingTime[p.ProcessID] > 0)
                .ToList();

            if (availableProcesses.Count == 0)
            {
                currentTime++;
                continue;
            }
            
            var currentProcess = availableProcesses
            .OrderBy(p => remainingTime[p.ProcessID])
            .First();

            if (!results.Any(r => r.ProcessID == currentProcess.ProcessID))
            {
                results.Add(new SchedulingResult
                {
                    ProcessID = currentProcess.ProcessID,
                    ArrivalTime = currentProcess.ArrivalTime,
                    BurstTime = currentProcess.BurstTime,
                    StartTime = currentTime
                });
            }
            
            remainingTime[currentProcess.ProcessID]--;
            currentTime++;
            
            if (remainingTime[currentProcess.ProcessID] == 0)
            {
                var result = results
                .First(r => r.ProcessID == currentProcess.ProcessID);
                
                    result.FinishTime = currentTime;
                    result.TurnaroundTime = result.FinishTime - result.ArrivalTime;
                    result.WaitingTime = result.TurnaroundTime - result.BurstTime;
                
                    completed++;
            }
        }
        return results;
    }
        
    ///
    /// HRRN(Highest Response Ratio Next)
    /// 
    public static List<SchedulingResult> RunHRRN(List<ProcessData> processes)
    {
        List<SchedulingResult> results = new();
        int currentTime = 0;
        var remainingProcesses = processes.ToList();
        
        while(remainingProcesses.Count > 0)
        {
            var availableProcesses = remainingProcesses
                .Where(p => p.ArrivalTime <= currentTime)
                .ToList();
    
            if(availableProcesses.Count == 0)
                {
                    currentTime = remainingProcesses.Min(p => p.ArrivalTime);
                    continue;
                }
            
            var nextProcess = availableProcesses
                .OrderByDescending(p =>
                {
                    Double waitingTime = currentTime - p.ArrivalTime;
    
                    return (waitingTime + p.BurstTime) / (double)p.BurstTime;
                })
                .First();
    
        int startTime = Math.Max(currentTime, nextProcess.ArrivalTime);
        int finishTime = startTime + nextProcess.BurstTime;
        int waitingTime = startTime - nextProcess.ArrivalTime;
        int turnaroundTime = finishTime - nextProcess.ArrivalTime;

        results.Add(new SchedulingResult
        {
            ProcessID = nextProcess.ProcessID,
            ArrivalTime = nextProcess.ArrivalTime,
            BurstTime = nextProcess.BurstTime,
            StartTime = startTime,
            FinishTime = finishTime,
            WaitingTime = waitingTime,
            TurnaroundTime = turnaroundTime
        });

        currentTime = finishTime;
        remainingProcesses.Remove(nextProcess);
        }
    return results;
    }
}
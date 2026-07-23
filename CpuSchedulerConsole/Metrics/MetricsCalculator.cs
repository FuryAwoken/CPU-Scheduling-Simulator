using CpuSchedulerConsole.Models;

namespace CpuSchedulerConsole.Metrics;

public static class MetricsCalculator
{
    public static PerformanceMetrics Calculate(List<SchedulingResult> results)
    {
        double totalWaiting = 0;
        double totalTurnaround = 0;
        double totalBurst = 0;

        foreach(var result in results)
        {
            totalWaiting += result.WaitingTime;
            totalTurnaround += result.TurnaroundTime;
            totalBurst += result.BurstTime;
        }

        double totalTime = results.Max(r => r.FinishTime);

        return new PerformanceMetrics
        {
            AverageWaitingTime = totalWaiting / results.Count,
            AverageTurnaroundTime = totalTurnaround / results.Count,
            CpuUtilization = (totalBurst / totalTime) * 100,
            Throughput = results.Count / totalTime
        };
    }
}
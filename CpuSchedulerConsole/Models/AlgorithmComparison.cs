namespace CpuSchedulerConsole.Models;

public class AlgorithmComparison
{
    public string AlgorithmName { get; set; } = "";

    public double AverageWaitingTime { get; set; }

    public double AverageTurnaroundTime { get; set; }

    public double CpuUtilization { get; set; }

    public double Throughput { get; set; }
}
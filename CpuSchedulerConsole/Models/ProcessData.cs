namespace CpuSchedulerConsole.Models;

public class ProcessData
{
    public string ProcessID{ get; set; } = "";
    public int ArrivalTime{ get; set; }
    public int BurstTime{ get; set; }
    public int Priority{ get; set; }
}

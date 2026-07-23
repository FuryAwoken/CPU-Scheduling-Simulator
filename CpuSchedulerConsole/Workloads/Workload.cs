using CpuSchedulerConsole.Models;

namespace CpuSchedulerConsole.Workloads;

public class Workload
{
    public string Name {get; set; } = "";
    public string Type {get; set; } = "";
    public string Size {get; set; } = "";
    public List<ProcessData> Processes {get; set; } = new();
    
}
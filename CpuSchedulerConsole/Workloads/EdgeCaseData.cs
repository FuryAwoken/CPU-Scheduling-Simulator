using CpuSchedulerConsole.Models;

namespace CpuSchedulerConsole.Workloads;

public static class EdgeCaseData
{
    public static Workload AllArriveAtZero()
    {
        return new Workload
        {
            Name = "All Processes Arrive at Time 0",
            Type = "Edge Case",
            Size = "Small",

            Processes = new List<ProcessData>
            {
                new ProcessData
                {
                    ProcessID = "P1",
                    ArrivalTime = 0,
                    BurstTime = 8,
                    Priority = 3
                },

                new ProcessData
                {
                    ProcessID = "P2",
                    ArrivalTime = 0,
                    BurstTime = 4,
                    Priority = 1
                },

                new ProcessData
                {
                    ProcessID = "P3",
                    ArrivalTime = 0,
                    BurstTime = 9,
                    Priority = 4
                },

                new ProcessData
                {
                    ProcessID = "P4",
                    ArrivalTime = 0,
                    BurstTime = 5,
                    Priority = 2
                },

                new ProcessData
                {
                    ProcessID = "P5",
                    ArrivalTime = 0,
                    BurstTime = 2,
                    Priority = 5
                }
            }
        };
    }

    public static Workload IdenticalBurstTimes()
    {
        return new Workload
        {
            Name = "Processes With Identical Burst Times",
            Type = "Edge Case",
            Size = "Small",

            Processes = new List<ProcessData>
            {
                new ProcessData
                {
                    ProcessID = "P1",
                    ArrivalTime = 0,
                    BurstTime = 6,
                    Priority = 3
                },

                new ProcessData
                {
                    ProcessID = "P2",
                    ArrivalTime = 1,
                    BurstTime = 6,
                    Priority = 1
                },

                new ProcessData
                {
                    ProcessID = "P3",
                    ArrivalTime = 2,
                    BurstTime = 6,
                    Priority = 4
                },

                new ProcessData
                {
                    ProcessID = "P4",
                    ArrivalTime = 3,
                    BurstTime = 6,
                    Priority = 2
                },

                new ProcessData
                {
                    ProcessID = "P5",
                    ArrivalTime = 4,
                    BurstTime = 6,
                    Priority = 5
                }
            }
        };
    }

    public static Workload ExtremeBurstTimes()
    {
        return new Workload
        {
            Name = "Very Short and Very Long Burst Times",
            Type = "Edge Case",
            Size = "Small",

            Processes = new List<ProcessData>
            {
                new ProcessData
                {
                    ProcessID = "P1",
                    ArrivalTime = 0,
                    BurstTime = 120,
                    Priority = 3
                },

                new ProcessData
                {
                    ProcessID = "P2",
                    ArrivalTime = 1,
                    BurstTime = 1,
                    Priority = 1
                },

                new ProcessData
                {
                    ProcessID = "P3",
                    ArrivalTime = 2,
                    BurstTime = 2,
                    Priority = 4
                },

                new ProcessData
                {
                    ProcessID = "P4",
                    ArrivalTime = 3,
                    BurstTime = 105,
                    Priority = 2
                },

                new ProcessData
                {
                    ProcessID = "P5",
                    ArrivalTime = 4,
                    BurstTime = 1,
                    Priority = 5
                },

                new ProcessData
                {
                    ProcessID = "P6",
                    ArrivalTime = 5,
                    BurstTime = 2,
                    Priority = 2
                }
            }
        };
    }

    public static Workload PriorityStarvation()
    {
        return new Workload
        {
            Name = "Priority Starvation Scenario",
            Type = "Edge Case",
            Size = "Small",

            Processes = new List<ProcessData>
            {
                new ProcessData
                {
                    ProcessID = "P1",
                    ArrivalTime = 0,
                    BurstTime = 12,
                    Priority = 5
                },

                new ProcessData
                {
                    ProcessID = "P2",
                    ArrivalTime = 0,
                    BurstTime = 4,
                    Priority = 1
                },

                new ProcessData
                {
                    ProcessID = "P3",
                    ArrivalTime = 2,
                    BurstTime = 4,
                    Priority = 1
                },

                new ProcessData
                {
                    ProcessID = "P4",
                    ArrivalTime = 4,
                    BurstTime = 4,
                    Priority = 1
                },

                new ProcessData
                {
                    ProcessID = "P5",
                    ArrivalTime = 6,
                    BurstTime = 4,
                    Priority = 1
                },

                new ProcessData
                {
                    ProcessID = "P6",
                    ArrivalTime = 8,
                    BurstTime = 4,
                    Priority = 1
                }
            }
        };
    }

    public static List<Workload> GetAllEdgeCases()
    {
        return new List<Workload>
        {
            AllArriveAtZero(),
            IdenticalBurstTimes(),
            ExtremeBurstTimes(),
            PriorityStarvation()
        };
    }
}
using CpuSchedulerConsole.Models;

namespace CpuSchedulerConsole.Workloads;

public static class WorkloadData
{
    public static List<Workload> GetAllWorkloads()
    {
        return new List<Workload>
        {
            new Workload
            {
                Name = "CPU-Bound Small",
                Type = "CPU-Bound",
                Size = "Small",
                Processes = CpuBoundSmall()
            },

            new Workload
            {
                Name = "CPU-Bound Medium",
                Type = "CPU-Bound",
                Size = "Medium",
                Processes = GenerateCpuBound(30, 101)
            },

            new Workload
            {
                Name = "CPU-Bound Large",
                Type = "CPU-Bound",
                Size = "Large",
                Processes = GenerateCpuBound(120, 102)
            },

            new Workload
            {
                Name = "I/O-Bound Small",
                Type = "I/O-Bound",
                Size = "Small",
                Processes = IoBoundSmall()
            },

            new Workload
            {
                Name = "I/O-Bound Medium",
                Type = "I/O-Bound",
                Size = "Medium",
                Processes = GenerateIoBound(30, 201)
            },

            new Workload
            {
                Name = "I/O-Bound Large",
                Type = "I/O-Bound",
                Size = "Large",
                Processes = GenerateIoBound(120, 202)
            },

            new Workload
            {
                Name = "Mixed Small",
                Type = "Mixed",
                Size = "Small",
                Processes = MixedSmall()
            },

            new Workload
            {
                Name = "Mixed Medium",
                Type = "Mixed",
                Size = "Medium",
                Processes = GenerateMixed(30, 301)
            },

            new Workload
            {
                Name = "Mixed Large",
                Type = "Mixed",
                Size = "Large",
                Processes = GenerateMixed(120, 302)
            }
        };
    }

    public static List<ProcessData> CpuBoundSmall()
    {
        return new List<ProcessData>
        {
            new ProcessData
            {
                ProcessID = "P1",
                ArrivalTime = 0,
                BurstTime = 18,
                Priority = 3
            },

            new ProcessData
            {
                ProcessID = "P2",
                ArrivalTime = 1,
                BurstTime = 22,
                Priority = 1
            },

            new ProcessData
            {
                ProcessID = "P3",
                ArrivalTime = 2,
                BurstTime = 15,
                Priority = 4
            },

            new ProcessData
            {
                ProcessID = "P4",
                ArrivalTime = 3,
                BurstTime = 26,
                Priority = 2
            },

            new ProcessData
            {
                ProcessID = "P5",
                ArrivalTime = 5,
                BurstTime = 20,
                Priority = 5
            },

            new ProcessData
            {
                ProcessID = "P6",
                ArrivalTime = 6,
                BurstTime = 17,
                Priority = 2
            }
        };
    }

    public static List<ProcessData> IoBoundSmall()
    {
        return new List<ProcessData>
        {
            new ProcessData
            {
                ProcessID = "P1",
                ArrivalTime = 0,
                BurstTime = 3,
                Priority = 3
            },

            new ProcessData
            {
                ProcessID = "P2",
                ArrivalTime = 1,
                BurstTime = 2,
                Priority = 1
            },

            new ProcessData
            {
                ProcessID = "P3",
                ArrivalTime = 2,
                BurstTime = 5,
                Priority = 4
            },

            new ProcessData
            {
                ProcessID = "P4",
                ArrivalTime = 3,
                BurstTime = 1,
                Priority = 2
            },

            new ProcessData
            {
                ProcessID = "P5",
                ArrivalTime = 4,
                BurstTime = 4,
                Priority = 5
            },

            new ProcessData
            {
                ProcessID = "P6",
                ArrivalTime = 6,
                BurstTime = 2,
                Priority = 2
            }
        };
    }

    public static List<ProcessData> MixedSmall()
    {
        return new List<ProcessData>
        {
            new ProcessData
            {
                ProcessID = "P1",
                ArrivalTime = 0,
                BurstTime = 4,
                Priority = 3
            },

            new ProcessData
            {
                ProcessID = "P2",
                ArrivalTime = 1,
                BurstTime = 19,
                Priority = 1
            },

            new ProcessData
            {
                ProcessID = "P3",
                ArrivalTime = 2,
                BurstTime = 3,
                Priority = 4
            },

            new ProcessData
            {
                ProcessID = "P4",
                ArrivalTime = 4,
                BurstTime = 24,
                Priority = 2
            },

            new ProcessData
            {
                ProcessID = "P5",
                ArrivalTime = 5,
                BurstTime = 6,
                Priority = 5
            },

            new ProcessData
            {
                ProcessID = "P6",
                ArrivalTime = 7,
                BurstTime = 16,
                Priority = 2
            }
        };
    }

    private static List<ProcessData> GenerateCpuBound(
        int processCount,
        int seed)
    {
        Random random = new(seed);
        List<ProcessData> processes = new();

        int arrivalTime = 0;

        for (int i = 1; i <= processCount; i++)
        {
            processes.Add(new ProcessData
            {
                ProcessID = $"P{i}",
                ArrivalTime = arrivalTime,
                BurstTime = random.Next(12, 31),
                Priority = random.Next(1, 6)
            });

            arrivalTime += random.Next(0, 4);
        }

        return processes;
    }

    private static List<ProcessData> GenerateIoBound(
        int processCount,
        int seed)
    {
        Random random = new(seed);
        List<ProcessData> processes = new();

        int arrivalTime = 0;

        for (int i = 1; i <= processCount; i++)
        {
            processes.Add(new ProcessData
            {
                ProcessID = $"P{i}",
                ArrivalTime = arrivalTime,
                BurstTime = random.Next(1, 7),
                Priority = random.Next(1, 6)
            });

            arrivalTime += random.Next(0, 3);
        }

        return processes;
    }

    private static List<ProcessData> GenerateMixed(
        int processCount,
        int seed)
    {
        Random random = new(seed);
        List<ProcessData> processes = new();

        int arrivalTime = 0;

        for (int i = 1; i <= processCount; i++)
        {
            bool useLongBurst = random.Next(0, 2) == 1;

            int burstTime;

            if (useLongBurst)
            {
                burstTime = random.Next(12, 31);
            }
            else
            {
                burstTime = random.Next(1, 7);
            }

            processes.Add(new ProcessData
            {
                ProcessID = $"P{i}",
                ArrivalTime = arrivalTime,
                BurstTime = burstTime,
                Priority = random.Next(1, 6)
            });

            arrivalTime += random.Next(0, 4);
        }

        return processes;
    }

    public static List<ProcessData> CloneProcesses(
        List<ProcessData> processes)
    {
        return processes
            .Select(process => new ProcessData
            {
                ProcessID = process.ProcessID,
                ArrivalTime = process.ArrivalTime,
                BurstTime = process.BurstTime,
                Priority = process.Priority
            })
            .ToList();
    }
}
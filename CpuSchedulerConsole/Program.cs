using CpuSchedulerConsole.Algorithms;
using CpuSchedulerConsole.Metrics;
using CpuSchedulerConsole.Models;
using CpuSchedulerConsole.Workloads;

bool programRunning = true;

while (programRunning)
{
    Console.Clear();

    PrintProgramHeader();

    Console.WriteLine("Main Menu");
    Console.WriteLine("=========");
    Console.WriteLine();

    Console.WriteLine("1. Run One Algorithm in Detail");
    Console.WriteLine("2. Compare All Algorithms on One Workload");
    Console.WriteLine("3. Run Full Workload Benchmark");
    Console.WriteLine("4. Run Edge Case Tests");
    Console.WriteLine("5. Exit");

    Console.WriteLine();
    Console.Write("Select an option: ");

    string? menuInput = Console.ReadLine();

    switch (menuInput)
    {
        case "1":
            RunDetailedSimulation();
            break;

        case "2":
            RunSingleWorkloadComparison();
            break;

        case "3":
            RunFullBenchmark();
            break;

        case "4":
            RunEdgeCaseMenu();
            break;

        case "5":
            programRunning = false;
            break;

        default:
            Console.WriteLine();
            Console.WriteLine("Invalid selection.");
            PauseProgram();
            break;
    }
}

Console.WriteLine();
Console.WriteLine("CPU Scheduling Simulator closed.");


// MAIN PROGRAM OPERATIONS

void RunDetailedSimulation()
{
    Console.Clear();

    PrintProgramHeader();

    Workload? selectedWorkload = SelectAnyWorkload();

    if (selectedWorkload == null)
    {
        return;
    }

    Console.Clear();

    PrintProgramHeader();

    string? selectedAlgorithm = SelectAlgorithm();

    if (selectedAlgorithm == null)
    {
        return;
    }

    Console.Clear();

    PrintProgramHeader();

    Console.WriteLine("Detailed Simulation");
    Console.WriteLine("===================");
    Console.WriteLine();

    Console.WriteLine($"Workload:  {selectedWorkload.Name}");
    Console.WriteLine($"Type:      {selectedWorkload.Type}");
    Console.WriteLine($"Size:      {selectedWorkload.Size}");
    Console.WriteLine($"Algorithm: {selectedAlgorithm}");
    Console.WriteLine($"Processes: {selectedWorkload.Processes.Count}");
    Console.WriteLine();

    PrintLoadedProcesses(selectedWorkload.Processes);

    List<SchedulingResult> results =
        RunSelectedAlgorithm(
            selectedAlgorithm,
            selectedWorkload.Processes
        );

    PrintDetailedResults(results);

    PerformanceMetrics metrics =
        MetricsCalculator.Calculate(results);

    PrintMetrics(metrics);

    PauseProgram();
}


void RunSingleWorkloadComparison()
{
    Console.Clear();

    PrintProgramHeader();

    Workload? selectedWorkload = SelectAnyWorkload();

    if (selectedWorkload == null)
    {
        return;
    }

    Console.Clear();

    PrintProgramHeader();

    Console.WriteLine($"Comparison: {selectedWorkload.Name}");
    Console.WriteLine($"Type: {selectedWorkload.Type}");
    Console.WriteLine($"Size: {selectedWorkload.Size}");
    Console.WriteLine(
        $"Processes: {selectedWorkload.Processes.Count}"
    );
    Console.WriteLine();

    List<AlgorithmComparison> comparisons =
        CompareAllAlgorithms(selectedWorkload.Processes);

    PrintComparisonTable(comparisons);

    PrintBestResults(comparisons);

    PauseProgram();
}


void RunFullBenchmark()
{
    Console.Clear();

    PrintProgramHeader();

    Console.WriteLine("Full Workload Benchmark");
    Console.WriteLine("=======================");
    Console.WriteLine();

    List<Workload> workloads =
        WorkloadData.GetAllWorkloads();

    foreach (Workload workload in workloads)
    {
        PrintWorkloadComparisonSection(workload);
    }

    PauseProgram();
}


void RunEdgeCaseMenu()
{
    bool edgeCaseMenuRunning = true;

    while (edgeCaseMenuRunning)
    {
        Console.Clear();

        PrintProgramHeader();

        Console.WriteLine("Edge Case Tests");
        Console.WriteLine("===============");
        Console.WriteLine();

        Console.WriteLine(
            "1. All Processes Arriving at Time 0"
        );

        Console.WriteLine(
            "2. Processes With Identical Burst Times"
        );

        Console.WriteLine(
            "3. Mix of Very Short and Very Long Burst Times"
        );

        Console.WriteLine(
            "4. Priority Starvation Scenario"
        );

        Console.WriteLine(
            "5. Run All Edge Case Tests"
        );

        Console.WriteLine(
            "0. Return to Main Menu"
        );

        Console.WriteLine();
        Console.Write("Select an edge case: ");

        string? input = Console.ReadLine();

        switch (input)
        {
            case "1":
                RunSingleEdgeCase(
                    EdgeCaseData.AllArriveAtZero()
                );
                break;

            case "2":
                RunSingleEdgeCase(
                    EdgeCaseData.IdenticalBurstTimes()
                );
                break;

            case "3":
                RunSingleEdgeCase(
                    EdgeCaseData.ExtremeBurstTimes()
                );
                break;

            case "4":
                RunSingleEdgeCase(
                    EdgeCaseData.PriorityStarvation()
                );
                break;

            case "5":
                RunAllEdgeCases();
                break;

            case "0":
                edgeCaseMenuRunning = false;
                break;

            default:
                Console.WriteLine();
                Console.WriteLine("Invalid edge case selection.");
                PauseProgram();
                break;
        }
    }
}


void RunSingleEdgeCase(Workload edgeCase)
{
    Console.Clear();

    PrintProgramHeader();

    Console.WriteLine("Edge Case Test");
    Console.WriteLine("==============");
    Console.WriteLine();

    Console.WriteLine($"Test: {edgeCase.Name}");
    Console.WriteLine(
        $"Processes: {edgeCase.Processes.Count}"
    );
    Console.WriteLine();

    PrintLoadedProcesses(edgeCase.Processes);

    List<AlgorithmComparison> comparisons =
        CompareAllAlgorithms(edgeCase.Processes);

    PrintComparisonTable(comparisons);

    PrintBestResults(comparisons);

    PrintEdgeCaseObservation(edgeCase);

    PauseProgram();
}


void RunAllEdgeCases()
{
    Console.Clear();

    PrintProgramHeader();

    Console.WriteLine("All Edge Case Tests");
    Console.WriteLine("===================");
    Console.WriteLine();

    List<Workload> edgeCases =
        EdgeCaseData.GetAllEdgeCases();

    foreach (Workload edgeCase in edgeCases)
    {
        PrintWorkloadComparisonSection(edgeCase);

        PrintEdgeCaseObservation(edgeCase);
    }

    PauseProgram();
}



// WORKLOAD AND ALGORITHM SELECTION

Workload? SelectAnyWorkload()
{
    List<Workload> regularWorkloads =
        WorkloadData.GetAllWorkloads();

    List<Workload> edgeCases =
        EdgeCaseData.GetAllEdgeCases();

    List<Workload> allWorkloads = new();

    allWorkloads.AddRange(regularWorkloads);
    allWorkloads.AddRange(edgeCases);

    Console.WriteLine("Available Workloads");
    Console.WriteLine("===================");
    Console.WriteLine();

    Console.WriteLine("Standard Workloads:");
    Console.WriteLine();

    for (int i = 0; i < regularWorkloads.Count; i++)
    {
        Workload workload = regularWorkloads[i];

        Console.WriteLine(
            $"{i + 1}. " +
            $"{workload.Name,-22} " +
            $"({workload.Processes.Count} processes)"
        );
    }

    Console.WriteLine();
    Console.WriteLine("Edge Case Workloads:");
    Console.WriteLine();

    for (int i = 0; i < edgeCases.Count; i++)
    {
        int menuNumber =
            regularWorkloads.Count + i + 1;

        Workload edgeCase = edgeCases[i];

        Console.WriteLine(
            $"{menuNumber}. " +
            $"{edgeCase.Name,-38} " +
            $"({edgeCase.Processes.Count} processes)"
        );
    }

    Console.WriteLine();
    Console.WriteLine("0. Return to Main Menu");

    Console.WriteLine();
    Console.Write("Select a workload: ");

    string? input = Console.ReadLine();

    if (!int.TryParse(input, out int workloadNumber))
    {
        Console.WriteLine();
        Console.WriteLine("Invalid workload selection.");

        PauseProgram();

        return null;
    }

    if (workloadNumber == 0)
    {
        return null;
    }

    if (workloadNumber < 1 ||
        workloadNumber > allWorkloads.Count)
    {
        Console.WriteLine();
        Console.WriteLine("Invalid workload selection.");

        PauseProgram();

        return null;
    }

    return allWorkloads[workloadNumber - 1];
}


string? SelectAlgorithm()
{
    Console.WriteLine("Available Algorithms");
    Console.WriteLine("====================");
    Console.WriteLine();

    Console.WriteLine("1. First-Come, First-Served");
    Console.WriteLine("2. Shortest Job First");
    Console.WriteLine("3. Priority");
    Console.WriteLine("4. Round Robin");
    Console.WriteLine("5. Shortest Remaining Time First");
    Console.WriteLine("6. Highest Response Ratio Next");
    Console.WriteLine("0. Return to Main Menu");

    Console.WriteLine();
    Console.Write("Select an algorithm: ");

    string? input = Console.ReadLine();

    return input switch
    {
        "1" => "FCFS",
        "2" => "SJF",
        "3" => "Priority",
        "4" => "Round Robin",
        "5" => "SRTF",
        "6" => "HRRN",
        "0" => null,

        _ => InvalidAlgorithmSelection()
    };
}


string? InvalidAlgorithmSelection()
{
    Console.WriteLine();
    Console.WriteLine("Invalid algorithm selection.");

    PauseProgram();

    return null;
}


// RUNNING AND COMPARING ALGORITHMS

List<SchedulingResult> RunSelectedAlgorithm(
    string algorithmName,
    List<ProcessData> processes)
{
    List<ProcessData> processCopy =
        WorkloadData.CloneProcesses(processes);

    return algorithmName switch
    {
        "FCFS" =>
            SchedulingAlgorithms.RunFCFS(processCopy),

        "SJF" =>
            SchedulingAlgorithms.RunSJF(processCopy),

        "Priority" =>
            SchedulingAlgorithms.RunPriority(processCopy),

        "Round Robin" =>
            SchedulingAlgorithms.RunRoundRobin(processCopy),

        "SRTF" =>
            SchedulingAlgorithms.RunSRTF(processCopy),

        "HRRN" =>
            SchedulingAlgorithms.RunHRRN(processCopy),

        _ =>
            throw new ArgumentException(
                $"Unknown scheduling algorithm: " +
                $"{algorithmName}"
            )
    };
}


List<AlgorithmComparison> CompareAllAlgorithms(
    List<ProcessData> processes)
{
    List<AlgorithmComparison> comparisons = new();

    string[] algorithmNames =
    {
        "FCFS",
        "SJF",
        "Priority",
        "Round Robin",
        "SRTF",
        "HRRN"
    };

    foreach (string algorithmName in algorithmNames)
    {
        List<SchedulingResult> results =
            RunSelectedAlgorithm(
                algorithmName,
                processes
            );

        PerformanceMetrics metrics =
            MetricsCalculator.Calculate(results);

        comparisons.Add(
            new AlgorithmComparison
            {
                AlgorithmName = algorithmName,

                AverageWaitingTime =
                    metrics.AverageWaitingTime,

                AverageTurnaroundTime =
                    metrics.AverageTurnaroundTime,

                CpuUtilization =
                    metrics.CpuUtilization,

                Throughput =
                    metrics.Throughput
            }
        );
    }

    return comparisons;
}

// ============================================================
// ============================================================
// GENERAL TERMINAL OUTPUT
// ============================================================
// ============================================================

void PrintProgramHeader()
{
    Console.WriteLine(
        "========================================"
    );

    Console.WriteLine(
        "       CPU Scheduling Simulator"
    );

    Console.WriteLine(
        "          CS 3502 Project 2"
    );

    Console.WriteLine(
        "========================================"
    );

    Console.WriteLine();
}


void PrintLoadedProcesses(
    List<ProcessData> processes)
{
    Console.WriteLine("Loaded Processes");
    Console.WriteLine("================");
    Console.WriteLine();

    Console.WriteLine(
        $"{"Process",-10}" +
        $"{"Arrival",-10}" +
        $"{"Burst",-10}" +
        $"{"Priority",-10}"
    );

    Console.WriteLine(
        new string('-', 40)
    );

    foreach (ProcessData process in processes)
    {
        Console.WriteLine(
            $"{process.ProcessID,-10}" +
            $"{process.ArrivalTime,-10}" +
            $"{process.BurstTime,-10}" +
            $"{process.Priority,-10}"
        );
    }

    Console.WriteLine();
}


void PrintDetailedResults(
    List<SchedulingResult> results)
{
    Console.WriteLine("Scheduling Results");
    Console.WriteLine("==================");
    Console.WriteLine();

    Console.WriteLine(
        $"{"Process",-10}" +
        $"{"Arrival",-10}" +
        $"{"Burst",-10}" +
        $"{"Start",-10}" +
        $"{"Finish",-10}" +
        $"{"Wait",-10}" +
        $"{"Turnaround",-12}"
    );

    Console.WriteLine(
        new string('-', 72)
    );

    foreach (SchedulingResult result in results)
    {
        Console.WriteLine(
            $"{result.ProcessID,-10}" +
            $"{result.ArrivalTime,-10}" +
            $"{result.BurstTime,-10}" +
            $"{result.StartTime,-10}" +
            $"{result.FinishTime,-10}" +
            $"{result.WaitingTime,-10}" +
            $"{result.TurnaroundTime,-12}"
        );
    }

    Console.WriteLine();
}


void PrintMetrics(
    PerformanceMetrics metrics)
{
    Console.WriteLine("Performance Metrics");
    Console.WriteLine("===================");
    Console.WriteLine();

    Console.WriteLine(
        $"Average Waiting Time:     " +
        $"{metrics.AverageWaitingTime:F2}"
    );

    Console.WriteLine(
        $"Average Turnaround Time:  " +
        $"{metrics.AverageTurnaroundTime:F2}"
    );

    Console.WriteLine(
        $"CPU Utilization:          " +
        $"{metrics.CpuUtilization:F2}%"
    );

    Console.WriteLine(
        $"Throughput:               " +
        $"{metrics.Throughput:F4}"
    );

    Console.WriteLine();
}


void PrintComparisonTable(
    List<AlgorithmComparison> comparisons)
{
    Console.WriteLine(
        $"{"Algorithm",-16}" +
        $"{"Avg Wait",12}" +
        $"{"Avg Turnaround",18}" +
        $"{"CPU Util.",14}" +
        $"{"Throughput",14}"
    );

    Console.WriteLine(
        new string('-', 74)
    );

    foreach (
        AlgorithmComparison comparison in comparisons
    )
    {
        Console.WriteLine(
            $"{comparison.AlgorithmName,-16}" +
            $"{comparison.AverageWaitingTime,12:F2}" +
            $"{comparison.AverageTurnaroundTime,18:F2}" +
            $"{comparison.CpuUtilization,13:F2}%" +
            $"{comparison.Throughput,14:F4}"
        );
    }
}


void PrintWorkloadComparisonSection(
    Workload workload)
{
    Console.WriteLine(
        new string('=', 76)
    );

    Console.WriteLine(
        $"WORKLOAD: {workload.Name.ToUpper()}"
    );

    Console.WriteLine(
        $"TYPE: {workload.Type} | " +
        $"SIZE: {workload.Size} | " +
        $"PROCESSES: {workload.Processes.Count}"
    );

    Console.WriteLine(
        new string('=', 76)
    );

    Console.WriteLine();

    List<AlgorithmComparison> comparisons =
        CompareAllAlgorithms(workload.Processes);

    PrintComparisonTable(comparisons);

    Console.WriteLine();
}



// Best-Result comparisons

void PrintBestResults(
    List<AlgorithmComparison> comparisons)
{
    Console.WriteLine();
    Console.WriteLine("Best Results");
    Console.WriteLine("============");
    Console.WriteLine();

    double lowestWaitingTime =
        comparisons.Min(
            comparison =>
                comparison.AverageWaitingTime
        );

    double lowestTurnaroundTime =
        comparisons.Min(
            comparison =>
                comparison.AverageTurnaroundTime
        );

    double highestCpuUtilization =
        comparisons.Max(
            comparison =>
                comparison.CpuUtilization
        );

    double highestThroughput =
        comparisons.Max(
            comparison =>
                comparison.Throughput
        );

    string bestWaitingAlgorithms =
        JoinWinningAlgorithms(
            comparisons.Where(
                comparison =>
                    Math.Abs(
                        comparison.AverageWaitingTime -
                        lowestWaitingTime
                    ) < 0.0001
            )
        );

    string bestTurnaroundAlgorithms =
        JoinWinningAlgorithms(
            comparisons.Where(
                comparison =>
                    Math.Abs(
                        comparison.AverageTurnaroundTime -
                        lowestTurnaroundTime
                    ) < 0.0001
            )
        );

    string bestUtilizationAlgorithms =
        JoinWinningAlgorithms(
            comparisons.Where(
                comparison =>
                    Math.Abs(
                        comparison.CpuUtilization -
                        highestCpuUtilization
                    ) < 0.0001
            )
        );

    string bestThroughputAlgorithms =
        JoinWinningAlgorithms(
            comparisons.Where(
                comparison =>
                    Math.Abs(
                        comparison.Throughput -
                        highestThroughput
                    ) < 0.0001
            )
        );

    Console.WriteLine(
        $"Lowest Average Waiting Time:    " +
        $"{bestWaitingAlgorithms} " +
        $"({lowestWaitingTime:F2})"
    );

    Console.WriteLine(
        $"Lowest Average Turnaround Time: " +
        $"{bestTurnaroundAlgorithms} " +
        $"({lowestTurnaroundTime:F2})"
    );

    Console.WriteLine(
        $"Highest CPU Utilization:        " +
        $"{bestUtilizationAlgorithms} " +
        $"({highestCpuUtilization:F2}%)"
    );

    Console.WriteLine(
        $"Highest Throughput:             " +
        $"{bestThroughputAlgorithms} " +
        $"({highestThroughput:F4})"
    );

    Console.WriteLine();
}


string JoinWinningAlgorithms(
    IEnumerable<AlgorithmComparison> comparisons)
{
    return string.Join(
        ", ",
        comparisons.Select(
            comparison =>
                comparison.AlgorithmName
        )
    );
}



// Edge-case explanations

void PrintEdgeCaseObservation(
    Workload edgeCase)
{
    Console.WriteLine();
    Console.WriteLine("What This Test Demonstrates");
    Console.WriteLine("===========================");
    Console.WriteLine();

    if (edgeCase.Name ==
        "All Processes Arrive at Time 0")
    {
        Console.WriteLine(
            "This test checks how each algorithm behaves " +
            "when every process is immediately available."
        );

    }
    else if (edgeCase.Name ==
        "Processes With Identical Burst Times")
    {
        Console.WriteLine(
            "This test checks whether algorithms remain " +
            "stable when burst times are tied."
        );

    }
    else if (edgeCase.Name ==
        "Very Short and Very Long Burst Times")
    {
        Console.WriteLine(
            "This test compares responsiveness when very " +
            "short jobs compete with extremely long jobs."
        );

    }
    else if (edgeCase.Name ==
        "Priority Starvation Scenario")
    {
        Console.WriteLine(
            "This test checks whether a low-priority " +
            "process experiences a long waiting time."
        );

        Console.WriteLine(
            "The current simulator demonstrates starvation."
        );

        Console.WriteLine(
            "Traditional priority inversion requires shared " +
            "locks or resources, which are not represented " +
            "by the current project model."
        );
    }

    Console.WriteLine();
}



// a PAUSE feature x_x

void PauseProgram()
{
    Console.WriteLine();
    Console.Write(
        "Press Enter to return..."
    );

    Console.ReadLine();
}
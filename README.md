# CPU Scheduling Simulator

A CPU Scheduling Simulator developed for **CS 3502 – Operating Systems**. This project evaluates and compares multiple CPU scheduling algorithms using reproducible workloads, performance metrics, and edge-case testing.

Originally based on the starter project provided by the course instructor, Kennesaw State University's PhD researcher Chris Regan, this version has been significantly expanded to support algorithm comparison, workload benchmarking, and additional scheduling algorithms.

---

## Features

* Six CPU scheduling algorithms
* Detailed simulation mode
* Algorithm comparison mode
* Full workload benchmarking
* Edge-case testing
* Performance metric calculations
* Reproducible workload generation

---

## Implemented Scheduling Algorithms

| Algorithm                            | Type           | Description                                                               |
| ------------------------------------ | -------------- | ------------------------------------------------------------------------- |
| First Come First Served (FCFS)       | Non-preemptive | Executes processes in arrival order.                                      |
| Shortest Job First (SJF)             | Non-preemptive | Selects the process with the shortest CPU burst.                          |
| Shortest Remaining Time First (SRTF) | Preemptive     | Executes the process with the shortest remaining burst time.              |
| Priority Scheduling                  | Non-preemptive | Executes the highest-priority process available.                          |
| Round Robin                          | Preemptive     | Executes each process using a fixed time quantum of **4**.                |
| Highest Response Ratio Next (HRRN)   | Non-preemptive | Selects the process with the highest response ratio to reduce starvation. |

---

## Workload Types

The simulator evaluates every scheduling algorithm using three workload categories.

### CPU-Bound

Processes primarily consist of long CPU bursts.

### I/O-Bound

Processes contain short CPU bursts representing frequent I/O activity.

### Mixed

A combination of CPU-bound and I/O-bound processes.

Each workload is available in three sizes:

* Small (6 processes)
* Medium (30 processes)
* Large (120 processes)

Medium and large workloads are generated using fixed random seeds to ensure every algorithm executes the exact same workload during comparisons.

---

## Edge Case Testing

The simulator includes dedicated edge-case tests required by the project specification.

* All processes arriving at time 0
* Processes with identical burst times
* Mix of very short and very long burst times
* Priority starvation scenario

These tests help verify algorithm correctness under unusual scheduling conditions.

---

## Performance Metrics

The following metrics are calculated for every simulation.

* Average Waiting Time
* Average Turnaround Time
* CPU Utilization
* Throughput

These metrics are displayed for both detailed simulations and algorithm comparisons.

---

## Program Modes

### 1. Detailed Simulation

Runs one scheduling algorithm on a selected workload.

Displays:

* Process information
* Scheduling results
* Performance metrics

---

### 2. Algorithm Comparison

Runs every scheduling algorithm against a selected workload and summarizes the results in a comparison table.

---

### 3. Full Benchmark

Executes every scheduling algorithm on every workload.

This mode is intended for benchmarking and performance comparison.

---

### 4. Edge Case Testing

Runs the required edge-case workloads to demonstrate scheduler behavior under special conditions.

---

## Requirements

* .NET 8 SDK or newer
* Visual Studio 2022 (recommended)

---

## Building the Project

Clone the repository.

```bash
git clone git@github.com:iAmGiG/CS-3502-CPU-Sim-Project-StartingPoint.git
```

Build the project.

```bash
dotnet build
```

Run the project.

```bash
dotnet run
```

---

## Project Structure

```text
Algorithms/
    Scheduling algorithms

Metrics/
    Performance metric calculations

Models/
    Process, scheduling result, workload, and comparison models

Workloads/
    Standard workload generation and edge-case workloads

Program.cs
    Main application and menu system
```

---

## Learning Objectives

This project demonstrates:

* CPU scheduling algorithms
* Performance evaluation
* Operating system scheduling concepts
* Workload benchmarking
* Algorithm comparison
* Edge-case analysis
* C# software design

---

## Acknowledgements

Starter code was provided as part of the CS 3502 Operating Systems course and expanded to implement additional scheduling algorithms (HRRN & SRTF), workload generation, benchmarking tools, edge-case testing, and performance analysis.
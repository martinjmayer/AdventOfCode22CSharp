// See https://aka.ms/new-console-template for more information

using AdventOfCode22CSharpTests;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

var summary = BenchmarkRunner.Run<Day03RucksackReorgBenchmarking>(
    new DebugInProcessConfig().WithOptions(ConfigOptions.DisableOptimizationsValidator));
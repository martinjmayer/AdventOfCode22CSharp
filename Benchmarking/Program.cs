// See https://aka.ms/new-console-template for more information

using AdventOfCode22CSharpTests;
using AdventOfCode22CSharpTests.Day11MonkeyInTheMiddle;
using AdventOfCode22CSharpTests.Day12HillClimbingAlgorithm;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

/*
var summary01 = BenchmarkRunner.Run<Day10CathodeRayTubeBenchmarking>(
    manualConfig);
    
var summary02 = BenchmarkRunner.Run<Day02RockPaperScissorsBenchmarking>(
    manualConfig);
    
var summary03 = BenchmarkRunner.Run<Day03RucksackReorgBenchmarking>(
    new DebugInProcessConfig().WithOptions(ConfigOptions.DisableOptimizationsValidator));
    
var summary06 = BenchmarkRunner.Run<Day06TuningTroubleBenchmarking>(
    new DebugInProcessConfig().WithOptions(ConfigOptions.DisableOptimizationsValidator));

var summary07 = BenchmarkRunner.Run<Day07NoSpaceLeftOnDeviceBenchmarking>(
    manualConfig);
    
var summary08 = BenchmarkRunner.Run<Day08TreeTopTreehouseBenchmarking>(
    manualConfig);
    
var summary09 = BenchmarkRunner.Run<Day09RopeBridgeBenchmarking>(
    manualConfig);
    
var summary10 = BenchmarkRunner.Run<Day10CathodeRayTubeBenchmarking>(
    manualConfig);

var summary11 = BenchmarkRunner.Run<Day11Benchmarking>(
    manualConfig);
    
*/

var manualConfig = new DebugInProcessConfig()
    .WithOptions(ConfigOptions.DisableOptimizationsValidator);

var summary12 = BenchmarkRunner.Run<Day12Benchmarking>(
    manualConfig);
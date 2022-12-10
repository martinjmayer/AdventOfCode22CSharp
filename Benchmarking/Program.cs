// See https://aka.ms/new-console-template for more information

using AdventOfCode22CSharpTests;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

/*
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
*/

var manualConfig = new DebugInProcessConfig()
    .WithOptions(ConfigOptions.DisableOptimizationsValidator);

var summary10 = BenchmarkRunner.Run<Day10CathodeRayTubeBenchmarking>(
    manualConfig);
// See https://aka.ms/new-console-template for more information

using AdventOfCode22CSharpTests;

var day10 = new Day10CathodeRayTubeTests_File();
var output = day10.SignalStrengthCalculator_GetCathodeRayImage("Day10_Input1.txt");

Console.Write(output);
Console.WriteLine('\n');

var ansiOutput = output.Replace(".", "\u001b[40;1m ").Replace("#", "\u001b[46;1m ");
Console.Write(ansiOutput);
Console.WriteLine("\u001b[0m \n");

Console.ReadKey();
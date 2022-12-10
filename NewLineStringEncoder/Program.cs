// See https://aka.ms/new-console-template for more information

var sourceData = File.ReadAllText("Source.txt");
var outputData = sourceData.Replace("\r", string.Empty).TrimEnd('\n').Replace("\n", "\\n") + "\\n";
File.WriteAllText("output.txt", outputData);
Console.Write(sourceData);
//Console.ReadKey(); 
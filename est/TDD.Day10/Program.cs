// See https://aka.ms/new-console-template for more information
using TDD.Day10;
var inputText = @"
..F7.
.FJ|.
SJ.L7
|F--J
LJ...
";
var graphic = new Graphic(inputText);

Console.WriteLine($"The answer of part One :{graphic.GetAnswerOfPart2()}");
graphic.Write1();
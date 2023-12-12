// See https://aka.ms/new-console-template for more information


using TDD.Day10;

var inputText = @"
.....
.S-7.
.|.|.
.L-J.
.....";
var graphic = new Graphic(inputText);
graphic.Write();

Console.ReadLine();
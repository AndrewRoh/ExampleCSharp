// See https://aka.ms/new-console-template for more information

using System.Text;
using Extentions;


Console.WriteLine("Hello, World!");


string text = "LiveKarma";
string snakeCaseText = text.ToSnakeCase2();
Console.WriteLine($"{snakeCaseText}");
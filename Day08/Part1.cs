using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day08;

internal class Part1
{
    public static void Solve(Map map, EndlessEnumerable<char> path)
    {
        var finish = "ZZZ";
        var current = "AAA";
        var next = "";

        while ((next = map.Step(current, path.Next())) != finish)
        {
            Console.WriteLine($"Stepping from {current} to {next} on step {map.Steps} ");
            current = next;
        }

        Console.WriteLine($"Completed from {current} to {finish} on step {map.Steps} ");

    }
}

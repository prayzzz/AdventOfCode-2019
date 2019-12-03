using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;

namespace AdventOfCode2019
{
    public class Day02 : IDay
    {
        public void Part1()
        {
            var input = Helper.ReadEmbeddedFile(GetType().Assembly, $"Input.{GetType().Name}.txt");
            Console.WriteLine($"{GetType().Name} Part 1: {SolvePart1(input)} (Expected: 3790689 )");
        }

        public void Part2()
        {
            var input = Helper.ReadEmbeddedFile(GetType().Assembly, $"Input.{GetType().Name}.txt");
            Console.WriteLine($"{GetType().Name} Part 2: {SolvePart2(input)} (Expected: )");
        }

        private static int SolvePart1(string input)
        {
            var codes = input.Split(',').Select(i => int.Parse(i)).ToList();

            codes[1] = 12;
            codes[2] = 2;

            for (int i = 0; i < codes.Count; i = i + 4)
            {
                var code = codes[i];

                if (code == 1)
                {
                    var pos1 = codes[i + 1];
                    var pos2 = codes[i + 2];

                    var resultPos = codes[i + 3];

                    codes[resultPos] = codes[pos1] + codes[pos2];
                }
                else if (code == 2)
                {
                    var pos1 = codes[i + 1];
                    var pos2 = codes[i + 2];

                    var resultPos = codes[i + 3];

                    codes[resultPos] = codes[pos1] * codes[pos2];
                }
                else if (code == 99)
                {
                    break;
                }
                else
                {
                    throw new Exception("Wrong state");
                }
            }

            return codes[0];
        }

        private static double SolvePart2(string input)
        {

            return 0.0;
        }
    }
}

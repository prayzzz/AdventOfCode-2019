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
            Console.WriteLine($"{GetType().Name} Part 1: {SolvePart1(input)} (Expected: 3790689)");
        }

        public void Part2()
        {
            var input = Helper.ReadEmbeddedFile(GetType().Assembly, $"Input.{GetType().Name}.txt");
            Console.WriteLine($"{GetType().Name} Part 2: {SolvePart2(input)} (Expected: 6533)");
        }

        private static int SolvePart1(string input)
        {
            var codes = input.Split(',').Select(int.Parse).ToList();

            RunProgramm(codes, 12, 2);

            return codes[0];
        }

        private static double SolvePart2(string input)
        {
            // 19690720

            var source = input.Split(',').Select(int.Parse).ToList();

            for (var noun = 0; noun < 99; noun++)
            for (var vern = 0; vern < 99; vern++)
            {
                var codes = source.ToList();
                RunProgramm(codes, noun, vern);

                if (codes[0] == 19690720)
                {
                    return 100 * noun + vern;
                }
            }

            return -1;
        }

        private static void RunProgramm(IList<int> codes, int noun, int vern)
        {
            codes[1] = noun;
            codes[2] = vern;

            for (var i = 0; i < codes.Count; i += 4)
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
        }
    }
}

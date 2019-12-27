using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day05 : IDay
    {
        public void Part1()
        {
            var input = Helper.ReadEmbeddedFile(GetType().Assembly, $"Input.{GetType().Name}.txt");
            Console.WriteLine($"{GetType().Name} Part 1: {SolvePart1(input)} (Expected: 5074395)");
        }

        public void Part2()
        {
            var input = Helper.ReadEmbeddedFile(GetType().Assembly, $"Input.{GetType().Name}.txt");
            Console.WriteLine($"{GetType().Name} Part 2: {SolvePart2(input)} (Expected: 8346937)");
        }

        private static int SolvePart1(string input)
        {
            var source = input.Split(',').Select(int.Parse).ToList();
            return RunProgram(source, 1);
        }

        private static int SolvePart2(string input)
        {
            var source = input.Split(',').Select(int.Parse).ToList();
            return RunProgram(source, 5);
        }

        private static int RunProgram(IList<int> codes, int input)
        {
            var lastOutput = -1;
            
            var i = 0;
            while (true)
            {
                var instruction = codes[i];
                var opCode = instruction % 100;

                if (opCode == 1) // sum
                {
                    var mode1 = (ParameterMode) (instruction % 1000 / 100);
                    var mode2 = (ParameterMode) (instruction % 10000 / 1000);

                    var v1 = Get(codes, i + 1, mode1);
                    var v2 = Get(codes, i + 2, mode2);

                    var resultPos = codes[i + 3];
                    codes[resultPos] = v1 + v2;

                    i += 4;
                }
                else if (opCode == 2) // multiply
                {
                    var mode1 = (ParameterMode) (instruction % 1000 / 100);
                    var mode2 = (ParameterMode) (instruction % 10000 / 1000);

                    var v1 = Get(codes, i + 1, mode1);
                    var v2 = Get(codes, i + 2, mode2);

                    var resultPos = codes[i + 3];
                    codes[resultPos] = v1 * v2;

                    i += 4;
                }
                else if (opCode == 3) // read
                {
                    var resultPos = codes[i + 1];
                    codes[resultPos] = input;

                    i += 2;
                }
                else if (opCode == 4) // write
                {
                    var mode1 = (ParameterMode) (instruction % 1000 / 100);
                    var v1 = Get(codes, i + 1, mode1);

                    // Console.WriteLine(v1);
                    lastOutput = v1;

                    i += 2;
                }
                else if (opCode == 5) // jump if true
                {
                    var mode1 = (ParameterMode) (instruction % 1000 / 100);
                    var mode2 = (ParameterMode) (instruction % 10000 / 1000);

                    var v1 = Get(codes, i + 1, mode1);
                    var v2 = Get(codes, i + 2, mode2);

                    i = v1 != 0 ? v2 : i + 3;
                }
                else if (opCode == 6) // jump if false
                {
                    var mode1 = (ParameterMode) (instruction % 1000 / 100);
                    var mode2 = (ParameterMode) (instruction % 10000 / 1000);

                    var v1 = Get(codes, i + 1, mode1);
                    var v2 = Get(codes, i + 2, mode2);

                    i = v1 == 0 ? v2 : i + 3;
                }
                else if (opCode == 7) // less than
                {
                    var mode1 = (ParameterMode) (instruction % 1000 / 100);
                    var mode2 = (ParameterMode) (instruction % 10000 / 1000);

                    var v1 = Get(codes, i + 1, mode1);
                    var v2 = Get(codes, i + 2, mode2);

                    var resultPos = codes[i + 3];
                    codes[resultPos] = v1 < v2 ? 1 : 0;

                    i += 4;
                }
                else if (opCode == 8) // equals
                {
                    var mode1 = (ParameterMode) (instruction % 1000 / 100);
                    var mode2 = (ParameterMode) (instruction % 10000 / 1000);

                    var v1 = Get(codes, i + 1, mode1);
                    var v2 = Get(codes, i + 2, mode2);

                    var resultPos = codes[i + 3];
                    codes[resultPos] = v1 == v2 ? 1 : 0;

                    i += 4;
                }
                else if (opCode == 99)
                {
                    break;
                }
                else
                {
                    throw new Exception("Wrong state");
                }
            }

            return lastOutput;
        }

        private static int Get(IList<int> codes, int pos, ParameterMode mode)
        {
            return mode switch
            {
                ParameterMode.Position => codes[codes[pos]],
                ParameterMode.Immediate => codes[pos],
                _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
            };
        }

        private enum ParameterMode
        {
            Position = 0,
            Immediate = 1
        }
    }
}
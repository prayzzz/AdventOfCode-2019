using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day03 : IDay
    {
        public void Part1()
        {
            var input = Helper.ReadEmbeddedFile(GetType().Assembly, $"Input.{GetType().Name}.txt");
            Console.WriteLine($"{GetType().Name} Part 1: {SolvePart1(input)} (Expected: 207)");
        }

        public void Part2()
        {
            var input = Helper.ReadEmbeddedFile(GetType().Assembly, $"Input.{GetType().Name}.txt");
            Console.WriteLine($"{GetType().Name} Part 2: {SolvePart2(input)} (Expected: 21196)");
        }

        private static int SolvePart1(string input)
        {
            var intersections = Solve(input);
            return intersections.Select(p => Math.Abs(0 - p.Key.X) + Math.Abs(0 - p.Key.Y)).Min();
        }

        private static int SolvePart2(string input)
        {
            var intersections = Solve(input);
            return intersections.Min(kv => kv.Value);
        }

        private static Dictionary<Point, int> Solve(string input)
        {
            var wires = input.Split('\n').ToList();

            var lines = new Dictionary<string, List<Line>>();
            var intersections = new Dictionary<Point, int>();

            const int startX = 0;
            const int startY = 0;

            foreach (var wire in wires)
            {
                var curX = startX;
                var curY = startY;
                var curSteps = 0;

                var routes = wire.Split(',').ToList();
                foreach (var route in routes)
                {
                    var pointA = new Point(curX, curY);

                    var dir = route.Substring(0, 1);
                    var steps = int.Parse(route.Substring(1));
                    curSteps += steps;

                    switch (dir)
                    {
                        case "R":
                            curX += steps;
                            break;
                        case "L":
                            curX -= steps;
                            break;
                        case "U":
                            curY -= steps;
                            break;
                        case "D":
                            curY += steps;
                            break;
                    }

                    var pointB = new Point(curX, curY);
                    var currentLine = new Line {A = pointA, B = pointB, Steps = curSteps};
                    lines.AddToList(wire, currentLine);

                    foreach (var existingLine in lines.Where(kv => kv.Key != wire).SelectMany(kv => kv.Value))
                    {
                        var intersection = Intersect(existingLine.A, existingLine.B, currentLine.A, currentLine.B);
                        if (intersection != Point.Empty)
                        {
                            // calculate steps until intersection
                            var currentStepsToIntersection = curSteps - Math.Abs((pointB.X - intersection.X) + (pointB.Y - intersection.Y));
                            var existingStepsToIntersection = existingLine.Steps - Math.Abs((existingLine.B.X - intersection.X) + (existingLine.B.Y - intersection.Y));

                            intersections.Add(intersection, currentStepsToIntersection + existingStepsToIntersection);
                        }
                    }
                }
            }

            return intersections;
        }

        private static Point Intersect(Point a1, Point a2, Point b1, Point b2)
        {
            // Check if none of the lines are of length 0
            if (a1 == a2 || b1 == b2)
            {
                return Point.Empty;
            }

            // Check if none of the lines have same start / end
            if (a1 == b1 || a1 == b2 || a2 == b1 || a2 == b2)
            {
                return Point.Empty;
            }

            var denominator = ((b2.Y - b1.Y) * (a2.X - a1.X) - (b2.X - b1.X) * (a2.Y - a1.Y));

            // Lines are parallel
            if (denominator == 0)
            {
                return Point.Empty;
            }

            var ua = ((b2.X - b1.X) * (a1.Y - b1.Y) - (b2.Y - b1.Y) * (a1.X - b1.X)) / (double) denominator;
            var ub = ((a2.X - a1.X) * (a1.Y - b1.Y) - (a2.Y - a1.Y) * (a1.X - b1.X)) / (double) denominator;

            // is the intersection along the segments
            if (ua < 0 || ua > 1 || ub < 0 || ub > 1)
            {
                return Point.Empty;
            }

            // Return a object with the x and y coordinates of the intersection
            var x = a1.X + ua * (a2.X - a1.X);
            var y = a1.Y + ua * (a2.Y - a1.Y);

            return new Point((int) x, (int) y);
        }

        private class Line
        {
            public Point A { get; set; }
            public Point B { get; set; }
            public int Steps { get; set; }
        }
    }
}
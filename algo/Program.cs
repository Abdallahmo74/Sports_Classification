using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string filepath = @"C:\Users\abdal\OneDrive\Desktop\Testcases\Complete\Complete Test\Solvable puzzles\Manhattan Only\15 Puzzle 5.txt";
            string input = File.ReadAllText(filepath);
            List<string> lines = new List<string>();
            lines = File.ReadAllLines(filepath).ToList();
            int n = int.Parse(lines[0]);
            int i = 0, j = 0;
            int[,] puzzle = new int[n, n];
            int N = 0;
            int[] puzz = new int[n * n];
            int[,] Goal = new int[n, n];
            int[] blank = new int[2];
            List<Node> Parents = new List<Node>();
            int HorM;
            int First_h;
            foreach (var row in input.Split('\n'))
            {
                j = 0;
                if (i > 1)
                {
                    foreach (var col in row.Trim().Split(' '))
                    {
                        
                        puzzle[i - 2, j] = int.Parse(col.Trim());
                        puzz[N] = puzzle[i-2, j];
                        N++;
                        Goal[i-2, j] = N;
                        if (puzzle[i - 2, j] == 0)
                        {
                            blank[0] = i - 2;
                            blank[1] = j;
                        }
                        j++;
                    }
                }
                i++;
            }

            Goal[n - 1, n - 1] = 0;

           
            Console.WriteLine("Enter 1 To Test By (Hamming) or Enter 2 To Test By (Manhantten) : ");
            string HorM1 = Console.ReadLine();
            HorM = int.Parse(HorM1);
            if(HorM == 1)
            {
                First_h = NPuzzleGame.Hamming(n, puzz);
            }
            else 
            {
               First_h = NPuzzleGame.Manhatten(n, puzzle);
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();
            bool solvability = NPuzzleGame.Solvability(n, puzz, blank);

            Console.WriteLine("  ");
            Console.WriteLine("The Solvability of this Puzzle : " + solvability);
            Console.WriteLine("  ");
            if (solvability == true)
            {
                Node moves = NPuzzleGame.TotalPuzzleMoves(n, puzzle, blank , First_h , Goal , HorM);
                Node parent = moves.Parent;
                Parents.Add(moves);

                for (int z = 0; z < moves.level ; z++)
                {
                    Parents.Add(parent);
                    parent = parent.Parent;
                }


                for (int g = Parents.Count-1; g >= 0; g-- )
                {
                   for(int x = 0; x<n; x++)
                    {
                        for(int y = 0; y<n; y++)
                        {
                            Console.Write(Parents[g].Puzzle[x, y] + " ");
                        }
                        
                            Console.WriteLine(" ");
                        
                    }

                    Console.WriteLine(" ");
                }
              Console.WriteLine("Total Moves = " + moves.level);
            }


            
            watch.Stop();
            float elapsedMs = watch.ElapsedMilliseconds;
            float Seconds = elapsedMs / 1000;
            Console.WriteLine("Time in MilliSeconds = " + elapsedMs +"ms");
            Console.WriteLine("Time in Seconds = " + Seconds + " sec");

        }
    }
}
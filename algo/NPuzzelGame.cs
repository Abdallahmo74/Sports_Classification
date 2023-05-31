using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp2
{
    class NPuzzleGame
    {


        //Solvable Or Not
        public static bool Solvability(int n, int[] puzzle , int[] blank)
        {
            int N = n * n;
            int inversion = 0;
            bool solv = false;
            for (int i = 0 ; i < N-1 ; i++)
            {
                for (int j = i+1 ; j < N ; j++)
                {
                    if ((puzzle[i] != 0) && (puzzle[j] != 0) && (puzzle[i] > puzzle[j]))
                    {
                        inversion++;
                    }

                }
            }

            //if n is odd
            if ((inversion % 2 == 0) &&(n%2 != 0 ) )
            {

                solv = true;
            }

            // if n is even
            else if(((n - blank[0])%2==0) && (inversion %2 != 0))
            {
                solv = true;
            }

            else if(((n-blank[0])%2!=0) && (inversion %2 ==0))
            {
                solv = true;
            }

            return solv;

        }






        public static int Hamming(int N, int[] puzz)
        {
            int h = 0;
            N = N * N;
            for (int i = 0; i < N ; i++)
            {
                if((puzz[i] != i+1) && puzz[i] != 0)
                {
                    h++;
                }
            }
            return h;
        }

        public static int Hammming_Update(int Old_indexrow, int Old_indexcol, int new_indexrow, int new_indexcol, int[,] Goal , int[,] Puzzle, int[,] old_Puzzle, int Old_h)
        {
            int New_h ;
           

            if((Puzzle[new_indexrow, new_indexcol] == Goal[new_indexrow, new_indexcol]) && (Puzzle[new_indexrow, new_indexcol] !=0))
            {
                New_h = Old_h - 1;
            }

            else if( (old_Puzzle[Old_indexrow, Old_indexcol] == Goal[Old_indexrow, Old_indexcol]) && (old_Puzzle[Old_indexrow, Old_indexcol] != 0) )
            {
                New_h = Old_h + 1;
            }
            else
            {
                New_h = Old_h;
            }

            return New_h;
        }




        public static int Manhatten(int N, int[,] puzzle)
        {
            int sum = 0;
            int[] ind = new int[2];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (puzzle[i, j] != 0)
                    {
                        ind[0] = i;
                        ind[1] = j;
                        sum += Dist(N, puzzle, ind);
                    }
                }
            }

            return sum;
        }
        public static int Dist(int N, int[,] puzzle, int[] index)
        {
            int sum = 0;
            int row = 0;
            if (puzzle[index[0], index[1]] % N == 0)
            {
                row = puzzle[index[0], index[1]] / N - 1;
            }
            else
            {
                row = puzzle[index[0], index[1]] / N;
            }

            int col = 0;
            int goal = puzzle[index[0], index[1]];

            col = (goal) % N;
            if (col == 0)
            {
                col = N;
            }

            col = col - 1;
            if (index[0] > row)
            {
                sum += index[0] - row;
            }
            else
            {
                sum += row - index[0];
            }
            if (index[1] > col)
            {
                sum += index[1] - col;
            }
            else
            {
                sum += col - index[1];
            }
            return sum;
        }


        public static int Manhantten_Update(int Old_indexrow , int Old_indexcol, int new_indexrow , int new_indexcol, int[,] Puzzle , int[,] OldPuzzle , int Old_Manhanttan , int N)
        {

            int Old_sum =0 ;
            int row = 0;
            int Column = 0;

            if (OldPuzzle[Old_indexrow, Old_indexcol] % N == 0)
            {
                row = OldPuzzle[Old_indexrow, Old_indexcol] / N - 1;
            }
            else
            {
                row = OldPuzzle[Old_indexrow, Old_indexcol] / N;
            }

            int Old_goal = OldPuzzle[Old_indexrow, Old_indexcol];

              Column = (Old_goal) % N;


            if (Column == 0)
            {
                Column = N;
            }
            
            Column = Column - 1;


            if (Old_indexrow > row)
            {
                Old_sum += Old_indexrow - row;
            }
            else
            {
                Old_sum += row - Old_indexrow;
            }

            if (Old_indexcol > Column)
            {
                Old_sum += Old_indexcol - Column;
            }
            else
            {
                Old_sum += Column - Old_indexcol;
            }



            row = 0;
            Column = 0;
            int New_sum = 0;
            if (Puzzle[new_indexrow, new_indexcol] % N == 0)
            {
                row = Puzzle[new_indexrow, new_indexcol] / N - 1;
            }
            else
            {
                row = Puzzle[new_indexrow, new_indexcol] / N;
            }
            int New_goal = Puzzle[new_indexrow, new_indexcol];

              Column = (New_goal) % N;
            if (Column == 0)
            {
                Column = N;
            }

              Column = Column - 1;

            if (new_indexrow > row)
            {
                New_sum += new_indexrow - row;
            }
            else
            {
                New_sum += row - new_indexrow;
            }

            if (new_indexcol > Column)
            {
                New_sum += new_indexcol - Column;
            }
            else
            {
                New_sum += Column - new_indexcol;
            }

            return (Old_Manhanttan + (New_sum - Old_sum));

        }


        public static void swap(ref int a,ref int b)
        {

            int tmp = a;
            a = b;
            b = tmp;

        }

         // (0 = up) ... (1 = down) ... (2 = Right) .... (3 = left)
        //#Moves To reach final state
        public static Node TotalPuzzleMoves(int N , int[,] Puzzle  , int[] blankindex , int First_h , int[,] Goal , int HorM)
        {
            Node Childup;
            Node ChildDown;
            Node ChildLeft;
            Node ChildRight;
            Node intialstate = new Node(First_h , 0 , Puzzle, -1 , null , N);
            intialstate.Blankindex[0] = blankindex[0];
            intialstate.Blankindex[1] = blankindex[1];
            bool cup;
            bool cdown;
            bool cright;
            bool cleft;
            PriorityQueue<Node> PriotityQ = new PriorityQueue<Node>();
            int New_indexrow ;
            int New_indexcol; //for hamming update
            int Old_indexrow;
            int Old_indexcol;
            int new_h;
            PriotityQ.Enqueue(intialstate);
            Node DeQ;
            int lev;
            int[,] childup;
            int[,] childleft;
            int[,] childdown;
            int[,] childright;
            while(true)
            {

                cright = false;
                cup = false;
                cdown = false;
                cleft = false;
               
               DeQ = PriotityQ.Dequeue();

               lev = DeQ.level+1;
                if ((DeQ.Blankindex[0] == 0) && (DeQ.Blankindex[1] == 0))
                {
                    
                    cright = true;
                    cdown = true;
               

                }

                else if ((DeQ.Blankindex[0] == 0) && (DeQ.Blankindex[1] == N - 1))
                {
                    cleft = true;
                    cdown = true;
                }


                else if ((DeQ.Blankindex[0] == N - 1) && (DeQ.Blankindex[1] == N - 1))
                {
                    cup = true;
                    cleft = true;
                }

                else if ((DeQ.Blankindex[0] == N - 1) && (DeQ.Blankindex[1] == 0))
                {
                    cup = true;
                    cright = true;
                }

                else if (DeQ.Blankindex[0] == N - 1)
                {
                    cup = true;
                    cright = true;
                    cleft = true;
                }
                else if (DeQ.Blankindex[1] == N - 1)
                {
                    cup = true;
                    cleft = true;
                    cdown = true;
                }
                else if (DeQ.Blankindex[0] == 0)
                {
                    cdown = true;
                    cleft = true;
                    cright = true;

                }
                else if (DeQ.Blankindex[1] == 0)
                {
                    cdown = true;
                    cup = true;
                    cright = true;
                }
                else
                {
                    cdown = true;
                    cup = true;
                    cright = true;
                    cleft = true;
                }




                if (DeQ.h == 0)
                {
                    return DeQ;
                }
                else
                {
                    //Removing invalid Move

                    if (DeQ.invalidmove == 0)
                    {
                        cup = false;
                    }
                    else if (DeQ.invalidmove == 1)
                    {
                        cdown = false;
                    }
                    else if (DeQ.invalidmove == 2)
                    {
                        cright = false;
                    }
                    else if(DeQ.invalidmove == 3)
                    {
                        cleft = false;
                    }



                        //Up
                        if (cup == true)
                        {

                            childup = new int[N, N];
                            Array.Copy(DeQ.Puzzle, childup, N * N);
                            swap(ref childup[DeQ.Blankindex[0], DeQ.Blankindex[1]], ref childup[DeQ.Blankindex[0] - 1, DeQ.Blankindex[1]]);
                            Old_indexrow = DeQ.Blankindex[0] - 1;
                            Old_indexcol = DeQ.Blankindex[1];
                            New_indexrow = DeQ.Blankindex[0];
                            New_indexcol = DeQ.Blankindex[1];
                        if (HorM == 1)
                        {
                            new_h = Hammming_Update(Old_indexrow, Old_indexcol, New_indexrow, New_indexcol, Goal, childup, DeQ.Puzzle, DeQ.h);
                        }
                        else
                        {
                            new_h = Manhantten_Update(Old_indexrow, Old_indexcol, New_indexrow, New_indexcol, childup, DeQ.Puzzle, DeQ.h, N);
                        }
                            Childup = new Node(new_h, lev, childup, 1, DeQ , N);
                            Childup.Blankindex[0] = DeQ.Blankindex[0] - 1;
                            Childup.Blankindex[1] = DeQ.Blankindex[1];
                            PriotityQ.Enqueue(Childup);
                            
                        }

                        //Down
                        if (cdown == true)
                        {
                            childdown = new int[N, N];
                            Array.Copy(DeQ.Puzzle, childdown, N * N);
                            swap(ref childdown[DeQ.Blankindex[0], DeQ.Blankindex[1]], ref childdown[DeQ.Blankindex[0] + 1, DeQ.Blankindex[1]]);
                            Old_indexrow = DeQ.Blankindex[0] + 1;
                            Old_indexcol = DeQ.Blankindex[1];
                            New_indexrow = DeQ.Blankindex[0];
                            New_indexcol = DeQ.Blankindex[1];
                        if (HorM == 1)
                        {
                            new_h = Hammming_Update(Old_indexrow, Old_indexcol, New_indexrow, New_indexcol, Goal, childdown, DeQ.Puzzle, DeQ.h);
                        }
                        else
                        {
                            new_h = Manhantten_Update(Old_indexrow, Old_indexcol, New_indexrow, New_indexcol, childdown, DeQ.Puzzle, DeQ.h, N);
                        }
                        ChildDown = new Node(new_h, lev, childdown, 0, DeQ, N);
                            ChildDown.Blankindex[0] = DeQ.Blankindex[0] + 1;
                            ChildDown.Blankindex[1] = DeQ.Blankindex[1];
                            PriotityQ.Enqueue(ChildDown);  
                        }

                        //Right
                        if (cright == true)
                        {
                            childright = new int[N, N];
                            Array.Copy(DeQ.Puzzle, childright, N * N);
                            swap(ref childright[DeQ.Blankindex[0], DeQ.Blankindex[1]], ref childright[DeQ.Blankindex[0], DeQ.Blankindex[1] + 1]);
                            Old_indexrow = DeQ.Blankindex[0];
                            Old_indexcol = DeQ.Blankindex[1]+1;
                            New_indexrow = DeQ.Blankindex[0];
                            New_indexcol = DeQ.Blankindex[1];
                        if (HorM == 1)
                        {
                            new_h = Hammming_Update(Old_indexrow, Old_indexcol, New_indexrow, New_indexcol, Goal, childright, DeQ.Puzzle, DeQ.h);
                        }
                        else
                        {
                            new_h = Manhantten_Update(Old_indexrow, Old_indexcol, New_indexrow, New_indexcol, childright, DeQ.Puzzle, DeQ.h, N);
                        }
                        ChildRight = new Node(new_h, lev, childright, 3, DeQ, N);
                            ChildRight.Blankindex[0] = DeQ.Blankindex[0];
                            ChildRight.Blankindex[1] = DeQ.Blankindex[1] + 1;
                            PriotityQ.Enqueue(ChildRight);
                        }

                        //Left
                        if (cleft == true)
                        {

                            childleft = new int[N, N];
                            Array.Copy(DeQ.Puzzle, childleft, N * N);
                            swap(ref childleft[DeQ.Blankindex[0], DeQ.Blankindex[1]], ref childleft[DeQ.Blankindex[0], DeQ.Blankindex[1] - 1]);
                            Old_indexrow = DeQ.Blankindex[0];
                            Old_indexcol = DeQ.Blankindex[1]-1;
                            New_indexrow = DeQ.Blankindex[0];
                            New_indexcol = DeQ.Blankindex[1];
                        if (HorM == 1)
                        {
                            new_h = Hammming_Update(Old_indexrow, Old_indexcol, New_indexrow, New_indexcol, Goal, childleft, DeQ.Puzzle, DeQ.h);
                        }
                        else
                        {
                            new_h = Manhantten_Update(Old_indexrow, Old_indexcol, New_indexrow, New_indexcol, childleft, DeQ.Puzzle, DeQ.h, N);
                        }
                        ChildLeft = new Node(new_h, lev, childleft, 2, DeQ, N);
                            ChildLeft.Blankindex[0] = DeQ.Blankindex[0];
                            ChildLeft.Blankindex[1] = DeQ.Blankindex[1] - 1;
                            PriotityQ.Enqueue(ChildLeft);
                           
                        }

                            if(PriotityQ.Count() > 2100000 )
                    {
                        PriotityQ.Remove();
                    }
                }
            }   
        }
    }
}

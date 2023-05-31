using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Node : IComparable<Node>
    {
        public int h;
        public int level;
        public int[,] Puzzle ;
        public int invalidmove;
        public Node Parent = null;
        public int F;
        public int[] Blankindex = new int[2];
        

       

        public Node( int h, int level , int[,] puzzle , int invalidmove , Node Parent , int N )
        {
            this.h = h;
            this.level = level;
            Puzzle = puzzle;
            F = level + h;
            this.invalidmove = invalidmove;
            this.Parent = Parent;

        }

     


        public int CompareTo(Node other)
        {
            if (this.F < other.F) return -1;
            else if (this.F > other.F) return 1;
            else return 0;
        }

      
    }
}

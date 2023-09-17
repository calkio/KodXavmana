using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodXavmana.Infastructure
{
    internal class Node
    {
        public char Character { get; set; }
        public double Frequency { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }
}

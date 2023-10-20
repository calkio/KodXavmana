using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Model
{
    internal class SecondTable
    {
        public string InformativeBits { get; set; }

        public string ParityBits { get; set; }

        public int ParityBit { get; set; }

        public int SDop { get; set; }

        public int[] Sindrom { get; set; }

        public int PositionError { get; set; }

        public int BugFix { get; set; }
    }
}

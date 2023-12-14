using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba4_2
{
    internal class Simbol
    {
        public char Name { get; set; }
        public double Probabilities { get; set; }

        public Simbol(char name, double probabilities)
        {
            Name = name;
            Probabilities = probabilities;
        }
    }
}

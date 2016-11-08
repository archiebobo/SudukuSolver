using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudukuSolver
{
    public class Suduku_EventArgs
    {
        public int ConformIndex { get; set; }
        public Suduku_EventArgs(int ConformIndex)
        {
            this.ConformIndex = ConformIndex;
        }
    }
}

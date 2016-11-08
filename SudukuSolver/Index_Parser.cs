using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudukuSolver
{
    static class Index_Parser
    {
        static public int Row(Suduku_Blank sb)
        {
            return sb.Index / 9;
        }
        static public int Column(Suduku_Blank sb)
        {
            return sb.Index % 9;
        }
        static public int MixGroup(Suduku_Blank sb)
        {
            return Row(sb) / 3 + Column(sb) / 3 * 3;
        }
        static public int Row_index(Suduku_Blank sb)
        {
            return sb.Index % 9;
        }
        static public int Column_index(Suduku_Blank sb)
        {
            return sb.Index / 9;
        }
        static public int Mix_index(Suduku_Blank sb)
        {
            return Column(sb) % 3 + Row(sb) % 3 * 3;
        }
        static public int Row(int index)
        {
            return index / 9;
        }
        static public int Column(int index)
        {
            return index % 9;
        }
        static public int MixGroup(int index)
        {
            return Row(index) / 3 + Column(index) / 3 * 3;
        }
        static public int Row_index(int index)
        {
            return index % 9;
        }
        static public int Column_index(int index)
        {
            return index / 9;
        }
        static public int Mix_index(int index)
        {
            return Column(index) % 3 + Row(index) % 3 * 3;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SudukuSolver
{
    static class Suduku_DataReader
    {
        static public int[] GetDataFromFile(string filepath)
        {
            StreamReader f=new StreamReader(filepath,Encoding.UTF8);
            string text="";
            string tmp;
            List<int> number_list = new List<int>();
            int[] number_array = new int[81];
            while ((tmp = f.ReadLine())!= null)
            {
                text += tmp.ToString();
            }
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] <= '9' && text[i] >= '0')
                {
                    number_list.Add(Int32.Parse(text[i].ToString()));
                }
            }
            if (number_list.Count == 81)
            {
                for (int i = 0; i < 81; i++)
                {
                    number_array[i] = number_list[i];
                }
                return number_array;
            }
            else
            {
                return new int[1] { 0 };
            }
        }
    }
}

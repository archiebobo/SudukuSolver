using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudukuSolver
{
    class Suduku_Data:Suduku_Component
    {
        private Suduku_Blank[] Ori_Blank;
        private Suduku_Group[] Row_Group;
        private Suduku_Group[] Column_Group;
        private Suduku_Group[] Mix_Group;
        private int[] origin_suduku_values;

        protected override void init_comp()
        {
            Ori_Blank = new Suduku_Blank[81];
            Row_Group = new Suduku_Group[9];
            Column_Group = new Suduku_Group[9];
            Mix_Group = new Suduku_Group[9];
            origin_suduku_values=Suduku_DataReader.GetDataFromFile("E:\\Current\\sudukusolver\\SudukuSolver\\Suduku_file");
            this.SetEnabled();
            if (origin_suduku_values.Length != 81)
            {
                this.switch_enable_status();
            }
            if (this.Enabled == true)
            {
                for (int i = 0; i < 81; i++)
                {
                    Ori_Blank[i] = new Suduku_Blank(i, origin_suduku_values[i]);
                }
            }
            for (int i = 0; i < 9; i++)
            {
                Row_Group[i] = new Suduku_Group(GroupType.Row, i, Ori_Blank);
            }
            for (int i = 0; i < 9; i++)
            {
                Column_Group[i] = new Suduku_Group(GroupType.Column, i, Ori_Blank);
            }
            for (int i = 0; i < 9; i++)
            {
                Mix_Group[i] = new Suduku_Group(GroupType.Mix, i, Ori_Blank);
            }
        }
        protected override void init_name()
        {
            this.set_name("NewSudu");
        }
    }
}

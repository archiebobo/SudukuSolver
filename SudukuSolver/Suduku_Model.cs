using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudukuSolver
{
    class Suduku_Model : Suduku_Component
    {
        private Suduku_Blank[] Ori_Blank;
        private Suduku_Group[] Row_Group;
        private Suduku_Group[] Column_Group;
        private Suduku_Group[] Mix_Group;
        private int[] origin_suduku_values;
        public Suduku_Blank[] Blanks
        {
            get
            {
                return Ori_Blank;
            }
        }
        public Suduku_Model(string filepath)
        {
            init_comp(filepath);
            init_name();
        }
        private Suduku_Model()
        {
        }
        protected override void init_comp()
        {
            Ori_Blank = new Suduku_Blank[81];
            Row_Group = new Suduku_Group[9];
            Column_Group = new Suduku_Group[9];
            Mix_Group = new Suduku_Group[9];
            this.SetEnabled();
        }
        protected void init_comp(string filepath)
        {
            origin_suduku_values = Suduku_DataReader.GetDataFromFile(filepath);
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
        public override Suduku_Component Clone()
        {
            Suduku_Model sm = new Suduku_Model();
            for (int i = 0; i < this.Ori_Blank.Length; i++)
            {
                sm.Ori_Blank[i] = this.Ori_Blank[i].Clone() as Suduku_Blank;
                sm.origin_suduku_values = new int[81];
                sm.origin_suduku_values[i] = this.origin_suduku_values[i];
            }
            for (int i = 0; i < 9; i++)
            {
                sm.Row_Group[i] = new Suduku_Group(GroupType.Row, i, sm.Ori_Blank);
            }
            for (int i = 0; i < 9; i++)
            {
                sm.Column_Group[i] = new Suduku_Group(GroupType.Column, i, sm.Ori_Blank);
            }
            for (int i = 0; i < 9; i++)
            {
                sm.Mix_Group[i] = new Suduku_Group(GroupType.Mix, i, sm.Ori_Blank);
            }
            return sm;
        }
        public void solve_suduku()
        {
            this.solve_one();
            this.solve_two();
            this.solve_three();
            this.guess_suduku();
        }
        private void solve_one()
        {
            for (int i = 0; i < 81; i++)
            {
                if (Blanks[i].iValues.Count == 1)
                {
                    Blanks[i].Value = Blanks[i].iValues[0];
                    solve_suduku();
                }
            }
        }
        private void solve_two()
        {
            for (int i = 0; i < 9; i++)
            {
                analize_group_shallow(this.Row_Group[i]);
                analize_group_shallow(this.Column_Group[i]);
                analize_group_shallow(this.Mix_Group[i]);
            }
        }
        private void solve_three()
        {
            for (int i = 0; i < 9; i++)
            {
                analize_group_deep(this.Row_Group[i]);
                analize_group_deep(this.Column_Group[i]);
                analize_group_deep(this.Mix_Group[i]);
            }
        }
        private void analize_group_shallow(Suduku_Group sg)
        {
            for (int i = 0; i < sg.Unsolved_Blank.Count; i++)
            {
                List<int> tmp_list = new List<int>(sg.Unsolved_Blank[i].iValues);
                for (int j = 0; j < sg.Unsolved_Blank.Count; j++)
                {
                    if (i != j)
                    {
                        List<int> test_list = new List<int>(sg.Unsolved_Blank[j].iValues);
                        for (int k = 0; k < sg.Unsolved_Blank[j].iValues.Count; k++)
                        {
                            tmp_list.Remove(test_list[k]);
                        }
                    }
                }
                if (tmp_list.Count == 1)
                {
                    sg.Unsolved_Blank[i].Value = tmp_list[0];
                    solve_suduku();
                    break;
                }
                else if (tmp_list.Count > 1)
                {
                    break;
                }
            }
        }
        private void analize_group_deep(Suduku_Group sg)
        {
            bool success_flag = false;
            for (int i = 2; i <= sg.Unsolved_Blank.Count - 2; i++)
            {
                if (sg.Unsolved_Blank.Count >= i + 2)
                {
                    for (int j = 0; j < sg.Unsolved_Blank.Count; j++)
                    {
                        if (sg.Unsolved_Blank[j].iValues.Count == i)
                        {
                            int counter = 1;
                            List<int> sec_index = new List<int> { j };
                            List<int> test_ivalue = new List<int>(sg.Unsolved_Blank[j].iValues);
                            for (int k = 0; k < sg.Unsolved_Blank.Count; k++)
                            {
                                if (k != j)
                                {
                                    if (sg.Unsolved_Blank[k].iValues.Count <= i)
                                    {
                                        bool check = true;
                                        for (int l = 0; l < sg.Unsolved_Blank[k].iValues.Count; l++)
                                        {
                                            if (!test_ivalue.Contains(sg.Unsolved_Blank[k].iValues[l]))
                                            {
                                                check = false;
                                                break;
                                            }
                                        }
                                        if (check)
                                        {
                                            counter++;
                                            sec_index.Add(k);
                                        }
                                    }
                                }
                                if (counter == i)
                                {
                                    break;
                                }
                            }
                            if (counter == i)
                            {
                                for (int k = 0; k < sg.Unsolved_Blank.Count; k++)
                                {
                                    if (!sec_index.Contains(k))
                                    {
                                        for (int l = 0; l < test_ivalue.Count; l++)
                                        {
                                            if (sg.Unsolved_Blank[k].iValues.Remove(test_ivalue[l]))
                                            {
                                                success_flag = true;
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
                if (success_flag)
                {
                    solve_suduku();
                    break;
                }
            }
        }
        private void guess_suduku()
        {
            Suduku_Model guess_model = this.Clone() as Suduku_Model;
            bool found = false;
            for (int j = 2; j < 9; j++)
            {
                for (int i = 0; i < this.Blanks.Length; i++)
                {
                    if (this.Blanks[i].iValues.Count == j)
                    {
                        found = true;
                        this.Blanks[i].Value = this.Blanks[i].iValues[0];
                        this.solve_suduku();
                        break;
                    }
                }
                if (found)
                {
                    break;
                }
            }
        }
    }
}

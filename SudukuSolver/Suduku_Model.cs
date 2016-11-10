using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudukuSolver
{
    class Suduku_Model : Suduku_Component
    {
        //参数声明
        private Suduku_Blank[] Ori_Blank;
        private Suduku_Group[] Row_Group;
        private Suduku_Group[] Column_Group;
        private Suduku_Group[] Mix_Group;
        private int[] origin_suduku_values;
        //属性声明
        public Suduku_Blank[] Blanks
        {
            get
            {
                return Ori_Blank;
            }
        }
        private bool isfinish
        {
            get
            {
                for (int i = 0; i < this.Blanks.Length; i++)
                {
                    if (this.Blanks[i].iValues.Count != 0)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        private bool isSolved
        {
            get
            {
                if (isfinish)
                {
                    for (int i = 0; i < this.Blanks.Length; i++)
                    {
                        if (this.Blanks[i].Value == 0)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        private bool isNotWrong
        {
            get
            {
                for (int i = 0; i < this.Blanks.Length; i++)
                {
                    if (this.Blanks[i].Value == 0 && this.Blanks[i].iValues.Count == 0)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        //构建函数
        public Suduku_Model(string filepath)
        {
            init_comp(filepath);
            init_name();
        }
        public Suduku_Model(Suduku_Blank[] original_data)
        {
            this.init_comp(original_data);
            this.init_name();
        }
        private Suduku_Model()
        {
        }
        //初始化类属性
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
        protected void init_comp(Suduku_Blank[] original_data)
        {
            if (original_data.Length == 81)
            {
                origin_suduku_values = new int[81];
                for (int i = 0; i < 81; i++)
                {
                    origin_suduku_values[i] = original_data[i].Value;
                }

                for (int i = 0; i < 81; i++)
                {
                    Ori_Blank[i] = new Suduku_Blank(0, 0);
                    Ori_Blank[i].Restore_Image(original_data[i]);
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
        //初始化名称
        protected override void init_name()
        {
            this.set_name("NewSudu");
        }
        //Solve函数
        private void solve_suduku()
        {
            this.solve_one();
            if (!isSolved)
            {
                this.solve_two();
                if (!isSolved)
                {
                    this.solve_three();
                }
            }
        }
        public void Solve_Suduku()
        {
            //this.Blanks[9].Value = 8;
            //this.Blanks[3].Value = 8;
            //this.Blanks[4].Value = 7;
            this.solve_suduku();
            if (!isSolved)
            {
                this.guess_suduku();
            }
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
        }//简单判定
        private void solve_two()
        {
            for (int i = 0; i < 9; i++)
            {
                analize_group_shallow(this.Row_Group[i]);
                analize_group_shallow(this.Column_Group[i]);
                analize_group_shallow(this.Mix_Group[i]);
            }
        }//调用analize_group_shallow方法
        private void solve_three()
        {
            for (int i = 0; i < 9; i++)
            {
                analize_group_deep(this.Row_Group[i]);
                analize_group_deep(this.Column_Group[i]);
                analize_group_deep(this.Mix_Group[i]);
            }
        }//调用analize_group_deep方法
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
        //Guess函数
        private void guess_suduku()
        {
            List<Suduku_Model> g_wh = new List<Suduku_Model>();
            List<int> g_ih = new List<int>();
            List<Suduku_Blank> b_ch = new List<Suduku_Blank>();
            Suduku_Model latest_image = new Suduku_Model();
            Suduku_Blank c_blank = new Suduku_Blank(0, 0);
            int c_ivalue_index = 0;
            do
            {
                if (!isfinish)
                {
                    if (isNotWrong)
                    {
                        g_wh.Add(this.Make_Image());
                        g_ih.Add(0);
                        b_ch.Add(find_cursor());
                        latest_image = g_wh[g_wh.Count - 1];
                        c_blank = b_ch[b_ch.Count - 1];
                        c_ivalue_index = g_ih[g_ih.Count - 1];

                        c_blank.Value = c_blank.iValues[c_ivalue_index];
                        this.solve_suduku();
                    }
                    else
                    {
                        this.Restore_Image(latest_image);
                        g_ih[g_ih.Count - 1]++;
                        while (g_ih[g_ih.Count - 1] > b_ch[b_ch.Count - 1].iValues.Count - 1)
                        {
                            g_wh.RemoveAt(g_wh.Count - 1);
                            this.Restore_Image(g_wh[g_wh.Count - 1]);
                            g_ih.RemoveAt(g_ih.Count - 1);
                            b_ch.RemoveAt(b_ch.Count - 1);
                            g_ih[g_ih.Count - 1]++;
                        }
                        latest_image = g_wh[g_wh.Count - 1];
                        c_blank = b_ch[b_ch.Count - 1];
                        c_ivalue_index = g_ih[g_ih.Count - 1];

                        c_blank.Value = c_blank.iValues[c_ivalue_index];
                        this.solve_suduku();
                    }
                }
                else
                {
                    if (!isNotWrong)
                    {
                        this.Restore_Image(latest_image);
                        g_ih[g_ih.Count - 1]++;
                        while (g_ih[g_ih.Count - 1] > b_ch[b_ch.Count - 1].iValues.Count - 1)
                        {
                            g_wh.RemoveAt(g_wh.Count - 1);
                            this.Restore_Image(g_wh[g_wh.Count - 1]);
                            g_ih.RemoveAt(g_ih.Count - 1);
                            b_ch.RemoveAt(b_ch.Count - 1);
                            g_ih[g_ih.Count - 1]++;
                        }
                        latest_image = g_wh[g_wh.Count - 1];
                        c_blank = b_ch[b_ch.Count - 1];
                        c_ivalue_index = g_ih[g_ih.Count - 1];

                        c_blank.Value = c_blank.iValues[c_ivalue_index];
                        this.solve_suduku();
                    }
                }
            } while (!isSolved);
        }
        private Suduku_Blank find_cursor()
        {
            for (int j = 2; j < 9; j++)
            {
                for (int i = 0; i < this.Blanks.Length; i++)
                {
                    if (this.Blanks[i].iValues.Count == j)
                    {
                        return this.Blanks[i];
                    }
                }
            }
            return new Suduku_Blank(0, 0);
        }
        //IMAGE函数
        public Suduku_Model Make_Image()
        {
            Suduku_Model image = new Suduku_Model(this.Blanks);
            for (int i = 0; i < 9; i++)
            {
                image.Row_Group[i].Resutore_Image(this.Row_Group[i]);
                image.Column_Group[i].Resutore_Image(this.Column_Group[i]);
                image.Mix_Group[i].Resutore_Image(this.Mix_Group[i]);
            }
            return image;
        }
        public void Restore_Image(Suduku_Model image)
        {
            for (int i = 0; i < 81; i++)
            {
                this.Blanks[i].Restore_Image(image.Blanks[i]);
            }
            for (int i = 0; i < 9; i++)
            {
                this.Row_Group[i].Resutore_Image(image.Row_Group[i]);
                this.Column_Group[i].Resutore_Image(image.Column_Group[i]);
                this.Mix_Group[i].Resutore_Image(image.Mix_Group[i]);
            }
        }
        private void debug()
        {
            ConsoleManager.Show();
            Console.WriteLine("----------------------------------");
            for (int i = 0; i < 81; i++)
            {
                if (this.Blanks[i].Value != 0)
                {
                    Console.Write("{0}\t", this.Blanks[i].Value);
                }
                else
                {
                    string tmp = "";
                    for (int j = 0; j < this.Blanks[i].iValues.Count; j++)
                    {
                        tmp += this.Blanks[i].iValues[j];
                    }
                    Console.Write("({0})\t", tmp);
                }
                if (i % 9 == 8)
                {
                    Console.WriteLine();
                }
            }
        }
        public bool Check_Result()
        {
            for (int i = 0; i < 9; i++)
            {
                List<int>[] check_list = new List<int>[3];
                for (int m = 0; m < 3; m++)
                {
                    check_list[m] = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                }
                for (int j = 0; j < 9; j++)
                {

                    check_list[0].Remove(this.Row_Group[i].Blank[j].Value);
                    check_list[1].Remove(this.Column_Group[i].Blank[j].Value);
                    check_list[2].Remove(this.Mix_Group[i].Blank[j].Value);
                }
                if (check_list[0].Count != 0 || check_list[1].Count != 0 || check_list[2].Count != 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudukuSolver
{
    class Suduku_Group : Suduku_Component
    {
        //属性声明
        public GroupType GroupT { get; set; }
        public List<Suduku_Blank> Blank { get; private set; }
        public List<Suduku_Blank> Unsolved_Blank { get; private set; }
        public int GroupIndex { get; set; }
        //构造函数
        public Suduku_Group(GroupType gt, int g_index, Suduku_Blank[] Blk)
        {
            init_comp(gt, g_index, Blk);
            init_name();
        }
        private Suduku_Group()
        {
        }
        //类初数据始化
        protected override void init_comp()
        {
            GroupT = GroupType.Row;
            Blank = new List<Suduku_Blank>();
            GroupIndex = -1;
            this.SetEnabled();
        }
        protected void init_comp(GroupType gt, int g_index, Suduku_Blank[] Blk)
        {
            this.GroupT = gt;
            this.GroupIndex = g_index;
            for (int i = 0; i < Blk.Length; i++)
            {
                switch (this.GroupT)
                {
                    case GroupType.Row:
                        if (Index_Parser.Row(Blk[i]) == this.GroupIndex)
                        {
                            this.Blank.Add(Blk[i]);
                        }
                        break;
                    case GroupType.Column:
                        if (Index_Parser.Column(Blk[i]) == this.GroupIndex)
                        {
                            this.Blank.Add(Blk[i]);
                        }
                        break;
                    case GroupType.Mix:
                        if (Index_Parser.MixGroup(Blk[i]) == this.GroupIndex)
                        {
                            this.Blank.Add(Blk[i]);
                        }
                        break;
                }
            }
            Unsolved_Blank = new List<Suduku_Blank>(this.Blank);
            for (int i = 0; i < this.Blank.Count; i++)
            {
                Blank[i].BlankConform += this.SettleGroup;
                Blank[i].Value = Blank[i].Value;
            }
        }
        //类名称初始化
        protected override void init_name()
        {

            if (this.GroupIndex != -1)
            {
                set_name(this.GroupT.ToString() + " Group - " + this.GroupIndex.ToString());
            }
            else
            {
                set_name("New_Suduku_Group");
            }
        }

        /*整理组函数（绑定BlankConform事件）
        类函数：整理本组成员
        参数：标准事件函数
        返回值：void
         */
        public void SettleGroup(object sender, Suduku_EventArgs e)
        {

            switch (this.GroupT)
            {
                case GroupType.Row:
                    this.Unsolved_Blank.Remove(this.Blank[Index_Parser.Row_index(e.ConformIndex)]);
                    for (int i = 0; i < this.Unsolved_Blank.Count; i++)
                    {
                        this.Unsolved_Blank[i].iValues.Remove(this.Blank[Index_Parser.Row_index(e.ConformIndex)].Value);
                    }
                    break;
                case GroupType.Column:
                    this.Unsolved_Blank.Remove(this.Blank[Index_Parser.Column_index(e.ConformIndex)]);
                    for (int i = 0; i < this.Unsolved_Blank.Count; i++)
                    {
                        this.Unsolved_Blank[i].iValues.Remove(Blank[Index_Parser.Column_index(e.ConformIndex)].Value);
                    }
                    break;
                case GroupType.Mix:
                    this.Unsolved_Blank.Remove(this.Blank[Index_Parser.Mix_index(e.ConformIndex)]);
                    for (int i = 0; i < this.Unsolved_Blank.Count; i++)
                    {
                        this.Unsolved_Blank[i].iValues.Remove(Blank[Index_Parser.Mix_index(e.ConformIndex)].Value);
                    }
                    break;
            }
        }
        public override Suduku_Component Clone()
        {
            Suduku_Group sg = new Suduku_Group();
            sg.GroupT = this.GroupT;
            for (int i = 0; i < this.Blank.Count; i++)
            {
                sg.Blank[i] = this.Blank[i].Clone() as Suduku_Blank;
            }
            sg.Unsolved_Blank = new List<Suduku_Blank>();
            switch (sg.GroupT)
            {
                case GroupType.Row:
                    for (int i = 0; i < this.Unsolved_Blank.Count; i++)
                    {
                        sg.Unsolved_Blank.Add(sg.Blank[Index_Parser.Row_index(this.Unsolved_Blank[i].Index)]);
                    }
                    break;
                case GroupType.Column:
                    for (int i = 0; i < this.Unsolved_Blank.Count; i++)
                    {
                        sg.Unsolved_Blank.Add(sg.Blank[Index_Parser.Column_index(this.Unsolved_Blank[i].Index)]);
                    }
                    break;
                case GroupType.Mix:
                    for (int i = 0; i < this.Unsolved_Blank.Count; i++)
                    {
                        sg.Unsolved_Blank.Add(sg.Blank[Index_Parser.Mix_index(this.Unsolved_Blank[i].Index)]);
                    }
                    break;
            }
            sg.GroupIndex = this.GroupIndex;
            return sg;
        }
    }
}

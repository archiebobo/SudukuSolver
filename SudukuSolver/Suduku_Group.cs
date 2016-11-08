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
        public GroupType GroupT { get; private set; }
        public List<Suduku_Blank> Blank { get; private set; }
        public int GroupIndex { get; private set; }
        //类初数据始化
        protected override void init_comp()
        {
            GroupT = GroupType.Row;
            Blank = new List<Suduku_Blank>();
            GroupIndex = -1;
        }
        protected void init_comp(GroupType gt, int g_index, Suduku_Blank[] Blk)
        {
            this.GroupT = gt;
            this.GroupIndex = g_index;
            Blank = new List<Suduku_Blank>();
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
            for (int i = 0; i < this.Blank.Count; i++)
            {
                Blank[i].BlankConform += this.SettleGroup;
                Blank[i].Value = Blank[i].Value;
            }
        }
        //类名称初始化
        protected override void init_name()
        {
            set_name("New_Suduku_Group");
            if (this.GroupIndex != -1)
            {
                set_name(this.GroupT.ToString() + " Group - " + this.GroupIndex.ToString());
            }
        }
        //类函数：整理本组成员
        //参数：标准事件函数
        //返回值：void
        public void SettleGroup(object sender, Suduku_EventArgs e)
        {
            switch (this.GroupT)
            {
                case GroupType.Row:
                    for (int i = 0; i < this.Blank.Count; i++)
                    {
                        this.Blank[i].iValues.Remove(Blank[Index_Parser.Row_index(e.ConformIndex)].Value);
                    }
                    break;
                case GroupType.Column:
                    for (int i = 0; i < this.Blank.Count; i++)
                    {
                        this.Blank[i].iValues.Remove(Blank[Index_Parser.Column_index(e.ConformIndex)].Value);
                    }
                    break;
                case GroupType.Mix:
                    for (int i = 0; i < this.Blank.Count; i++)
                    {
                        this.Blank[i].iValues.Remove(Blank[Index_Parser.Mix_index(e.ConformIndex)].Value);
                    }
                    break;
            }
        }
    }
}

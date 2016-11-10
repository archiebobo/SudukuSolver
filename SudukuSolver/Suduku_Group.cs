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
        public GroupType GrpType { get; set; }
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
            GrpType = GroupType.Row;
            Blank = new List<Suduku_Blank>();
            GroupIndex = -1;
            this.SetEnabled();
        }
        protected void init_comp(GroupType gt, int g_index, Suduku_Blank[] Blk)
        {
            this.GrpType = gt;
            this.GroupIndex = g_index;
            for (int i = 0; i < Blk.Length; i++)
            {
                switch (this.GrpType)
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
                set_name(this.GrpType.ToString() + " Group - " + this.GroupIndex.ToString());
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

            switch (this.GrpType)
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
        public Suduku_Group Make_Image(Suduku_Blank[] Blk)
        {
            Suduku_Group Image_Copy = new Suduku_Group(this.GrpType, this.GroupIndex, Blk);
            for (int i = 0; i < this.Blank.Count; i++)
            {
                if (Blank[i].IsConformed == true)
                    Image_Copy.Unsolved_Blank.Remove(Blank[i]);
            }
            return Image_Copy;
        }
        public void Resutore_Image(Suduku_Group image)
        {
            this.Unsolved_Blank.Clear();
            switch (this.GrpType)
            {
                case GroupType.Row:
                    for (int i = 0; i < image.Unsolved_Blank.Count; i++)
                    {
                        this.Unsolved_Blank.Add(this.Blank[Index_Parser.Row_index(image.Unsolved_Blank[i].Index)]);
                    }
                    break;
                case GroupType.Column:
                    for (int i = 0; i < image.Unsolved_Blank.Count; i++)
                    {
                        this.Unsolved_Blank.Add(this.Blank[Index_Parser.Column_index(image.Unsolved_Blank[i].Index)]);
                    }
                    break;
                case GroupType.Mix:
                    for (int i = 0; i < image.Unsolved_Blank.Count; i++)
                    {
                        this.Unsolved_Blank.Add(this.Blank[Index_Parser.Mix_index(image.Unsolved_Blank[i].Index)]);
                    }
                    break;
            }
        }
    }
}

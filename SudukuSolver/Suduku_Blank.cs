﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudukuSolver
{
    class Suduku_Blank : Suduku_Component
    {
        //事件声明
        public event Suduku_Control_Handler BlankConform;
        //参数声明
        private int val;
        private List<int> ival;
        //属性声明
        public int Value 
        {
            get
            {
                return val;
            }
            set
            {
                val = value;
                if (val != 0)
                {
                    this.IsConformed = true;
                    this.ival.Clear();
                    OnBlankConform();
                }
            }
        }
        public bool IsConformed { get; set; }
        public List<int> iValues
        {
            get
            {
                return ival;
            }
        }
        public int Index { get; private set; }
        //构建函数
        public Suduku_Blank(int index, int val)
        {
            init_comp(index, val);
            init_name();
        }
        private Suduku_Blank()
        {

        }
        //初始化类属性
        override protected void init_comp()
        {
            this.val = 0;
            this.Index = -1;
            this.ival = new List<int>();
            this.IsConformed = false;
            this.SetEnabled();
        }
        protected void init_comp(int index, int val)
        {
            this.val = val;
            this.Index = index;
            this.ival = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        }
        //初始化名称
        protected override void init_name()
        {
            if (this.Index != -1)
            {
                set_name("Sudu_Blank - "+ this.Index.ToString());
            }
            else
            {
                set_name("New_Suduku_Blank");
            }
        }
        //数字确认事件触发器
        public void OnBlankConform()
        {
            if (BlankConform != null)
            {
                Suduku_EventArgs e = new Suduku_EventArgs(this.Index);
                BlankConform(this, e);
            }
        }
        public void Restore_Image(Suduku_Blank ori)
        {
            this.val = ori.val;
            this.ival.Clear();
            for (int i = 0; i < ori.ival.Count; i++)
            {
                this.ival.Add(ori.ival[i]);
            }
            this.Index = ori.Index;
        }
    }
}

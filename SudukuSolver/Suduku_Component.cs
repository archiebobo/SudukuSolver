using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudukuSolver
{
    public enum GroupType { Row, Column, Mix }
    public delegate void Suduku_Control_Handler(object sender,Suduku_EventArgs e);
    abstract class Suduku_Component
    {
        public string Name { get; private set; }
        public bool Enabled { get; private set; }
        public Suduku_Component()
        {
            set_name("New_Suduku_Component(Name not Init)");
            init_comp();
            init_name();
            this.Enabled = true;
        }
        protected void set_name(string n)
        {
            this.Name=n;
        }
        protected void switch_enable_status()
        {
            if (this.Enabled == true)
            {
                this.Enabled = false;
            }
            else
            {
                this.Enabled = true;
            }
        }
        protected void SetEnabled()
        {
            this.Enabled = true;
        }
        abstract protected void init_comp();
        abstract protected void init_name();
        abstract public Suduku_Component Clone();
    }
}

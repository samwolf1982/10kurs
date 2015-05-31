using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kurs5WF
{
    enum ToDo{ADD,ADC,SUB,SUBB,MUL,DIV,SHR,ROR,RRC,SHL,ROL,RLC,AND,NOT,OR,XOR} ;
    enum Flag { CF, ZF, SF, OF };
        // cf перенос  zf zero sf знак of переполнение
    public partial class Form1 : Form
    {
        bool[] reg = new bool[8];
        bool[] mem1 = new bool[8];
        bool[] mem2 = new bool[8];
        public Form1()
        {
            InitializeComponent();
            comboBox1.DataSource = ToDo.GetValues(typeof(ToDo));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch ((ToDo)comboBox1.SelectedItem)
            {
                case ToDo.ADD:
                    {
                        System.Diagnostics.Debug.WriteLine("ADD size "+(sizeof(bool)*8).ToString());
                    }
                    break;
                case ToDo.ADC:
                    {
                        System.Diagnostics.Debug.WriteLine("ADC");
                    }
                    break;
                case ToDo.SUB:
                    break;
                case ToDo.SUBB:
                    break;
                case ToDo.MUL:
                    break;
                case ToDo.DIV:
                    break;
                case ToDo.SHR:
                    break;
                case ToDo.ROR:
                    break;
                case ToDo.RRC:
                    break;
                case ToDo.SHL:
                    break;
                case ToDo.ROL:
                    break;
                case ToDo.RLC:
                    break;
                case ToDo.AND:
                    break;
                case ToDo.NOT:
                    break;
                case ToDo.OR:
                    break;
                case ToDo.XOR:
                    break;
                default:
                    break;
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
         
        }
    
    }
}

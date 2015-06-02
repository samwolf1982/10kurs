using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace k5x
{
    public partial class Form1 : Form
    {
        byte[] AX = new byte[2];  // 8*2
        byte[] BX = new byte[2];
        byte[] CX = new byte[2];
        byte[] DX = new byte[2];

        


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showBite();
        }
        void showBite()
        {
            System.Diagnostics.Debug.WriteLine(sizeof(byte));
        }


    }
}

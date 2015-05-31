using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kurs5WF
{
    enum ToDo{ADD,ADC,SUB,SUBB,MUL,DIV,SHR,ROR,RRC,SHL,ROL,RLC,AND,NOT,OR,XOR} ;

    
    public partial class Form1 : Form
    {
        bool[] reg; 
        bool[] mem1;
        bool[] mem2; 
   // cf перенос  zf zero sf знак of переполнение
        bool CF = false;    
        bool ZF = false;
        bool SF = false;
        bool OF = false;
        char[] temp1;
        char[] temp2;
        public Form1()
        {
            InitializeComponent();
            comboBox1.DataSource = ToDo.GetValues(typeof(ToDo));
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // очистка всех массивов обязательно
            reg = null;
            mem1 = null;
            mem2 = null;

         reg = new bool[8];
         mem1 = new bool[8];
         mem2 = new bool[8];
            #region чтение входных даных
           temp1 = textBox1.Text.ToArray();
           temp2 = textBox4.Text.ToArray();
           for (int i = 0; i < reg.Length; i++)
           {
               mem1[i] = (temp1[i].ToString()=="0")?false:true;
               mem2[i] = (temp2[i].ToString() == "0") ? false : true;
           } 
            // flags   
          string[] strFlag = textBox2.Text.Split('|');
          CF = strFlag[0] == "0" ? false : true; ZF = strFlag[1] == "0" ? false : true; SF = strFlag[2] == "0" ? false : true; OF = strFlag[3] == "0" ? false : true;

        
 
            #endregion

      

            switch ((ToDo)comboBox1.SelectedItem)
            {
                case ToDo.ADD:
                    {
                        #region прямок -> обрантый -> дополнительный код
                         //    поиск отрицательного числа для перевода в дополнительный код
                        if (mem1[0] == true)
                        {
                            toReturnCode(ref mem1);
                          
                        }
                        if (mem2[0] == true)
                        {
                            toReturnCode(ref mem2);
                        }



                           #endregion

                        DebugBite(mem1, "Mem1 ");
                        DebugBite(mem2, "Mem2 ");
                        
                        add();
                        clear();
                    }
                    break;
                case ToDo.ADC:
                    {
                        #region прямок -> обрантый -> дополнительный код
                        //    поиск отрицательного числа для перевода в дополнительный код
                        if (mem1[0] == true)
                        {
                            toReturnCode(ref mem1);

                        }
                        if (mem2[0] == true)
                        {
                            toReturnCode(ref mem1);
                        }



                        #endregion

                        DebugBite(mem1, "Mem1 ");
                        DebugBite(mem2, "Mem2 ");
                        adc();
                    }
                    break;
                case ToDo.SUB:
                    {
                        //меняем знак второго числа на противополжный    5-2 == 5+(-2)
                        mem2[0] = mem2[0] == true ? false : true;
                        #region прямок -> обрантый -> дополнительный код
                        //    поиск отрицательного числа для перевода в дополнительный код
                      
                        if (mem1[0] == true)
                        {
                            toReturnCode(ref mem1);

                        }
                        if (mem2[0] == true)
                        {
                            toReturnCode(ref mem2);
                        }



                        #endregion

                        DebugBite(mem1, "Mem1 ");
                        DebugBite(mem2, "Mem2 ");

                        add();
                        clear();

                    }

                    break;
                case ToDo.SUBB:
                    {
                        //меняем знак второго числа на противополжный    5-2 == 5+(-2)
                        mem2[0] = mem2[0] == true ? false : true;
                        #region прямок -> обрантый -> дополнительный код
                        //    поиск отрицательного числа для перевода в дополнительный код
                        if (mem1[0] == true)
                        {
                            toReturnCode(ref mem1);

                        }
                        if (mem2[0] == true)
                        {
                            toReturnCode(ref mem1);
                        }
                        #endregion
                        
//                        DebugBite(mem1, "Mem1 ");
                      //  DebugBite(mem2, "Mem2 ");
                        adc();

                    }
                    break;
                case ToDo.MUL:
                    break;
                case ToDo.DIV:
                    break;
                case ToDo.SHR:
                    {

       

                      //  DebugBite(mem1, "SHR ");
                        //DebugBite(mem2, "SHR ");
                        shr();

                    }
                    break;
                case ToDo.ROR:
                    {
                        ror();
                     richTextBox1.Text = "";
                     textBox3.Text = showSingleresult();
                    }
                    break;
                case ToDo.RRC:
                    {
                        rrc();
                        richTextBox1.Text = "";
                        textBox3.Text = showSingleresult();
                    }
                    break;
                case ToDo.SHL:
                    break;
                case ToDo.ROL:
                    {
                        rol();
                        richTextBox1.Text = "";
                        textBox3.Text = showSingleresult();
                    }
                    break;
                case ToDo.RLC:
                    {
                        {
                            rlc();
                            richTextBox1.Text = "";
                            textBox3.Text = showSingleresult();
                        }

                    }
                    break;
                case ToDo.AND:
                    {
                        and();
                        richTextBox1.Text = "";
                        textBox3.Text = showSingleresult();
                    }
                    break;
                case ToDo.NOT:
                    {
                        not();
                           richTextBox1.Text = "";
                        textBox3.Text = showSingleresult();
                    }
                    break;
                case ToDo.OR:
                        {
                        or();
                           richTextBox1.Text = "";
                        textBox3.Text = showSingleresult();
                    }
                    break;
                case ToDo.XOR:
                    {
                        xor();
                        richTextBox1.Text = "";
                        textBox3.Text = showSingleresult();
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// or
        /// </summary>
        void xor()
        {
            for (int i = 0; i < mem1.Length; i++)
            {
                if ((mem1[i] == false && mem2[i] == true) || (mem1[i] == true && mem2[i] == false)) reg[i] = true;
                else reg[i] = false;

            }
            mem1 = reg;

        }
        /// <summary>
        /// or
        /// </summary>
        void or()
        {
            for (int i = 0; i < mem1.Length; i++)
            {
                if (mem1[i] == false && mem2[i] == false) reg[i] = false;
                else reg[i] = true;

            }
            mem1 = reg;

        }

              /// <summary>
        /// not
        /// </summary>
        void not()
        {
            for (int i = 0; i < mem1.Length; i++)
            {
                mem1[i] = mem1[i] == true ? false : true;

            }

        }
        
        /// <summary>
        /// and
        /// </summary>
        void and()
        {
            for (int i = 0; i < mem1.Length; i++)
            {
                if (mem1[i] == true && mem2[i] == true) reg[i] = true;
                else reg[i] = false;

            }
            mem1 = reg;

        }
        /// <summary>
        /// циклический сдвиг R без флага 
        /// </summary>
        void rlc()
        {

     
        
            // количество смещений можно переделать без цикла
            for (int i = 0; i < 1; i++)
            {
                reg[0] = mem1[mem1.Length-1]; //последнее- потом станет первым
                for (int j=0; j <mem1.Length-1; j++)
                {
                    mem1[j+1] = mem1[j];
                    CF = mem1[j +1];
                }
                mem1[0] = reg[0];
            }


        }

        /// <summary>
        /// циклический сдвиг R без флага 
        /// </summary>
        void rrc()
        {
            // количество смещений можно переделать без цикла
            for (int i = 0; i < 1; i++)
            {
                reg[0] = mem1[mem1.Length - 1]; //последнее- потом станет первым
                for (int j = mem1.Length - 1; j > 0; j--)
                {
                    mem1[j] = mem1[j - 1];
                    CF = mem1[j - 1];

                }
                mem1[0] = reg[0];
            }


        }
        /// <summary>
        /// циклический сдвиг R
        /// </summary>
        void ror()
        {
            // количество смещений можно переделать без цикла
            for (int i = 0; i < 1; i++)
            {
                CF=mem1[mem1.Length-1]; //последнее- потом станет первым
                for (int j = mem1.Length-1; j >0; j--)
                {
                    mem1[j] = mem1[j - 1];

                }
                mem1[0] = CF;
            }
            

        }
        /// <summary>
        /// циклический сдвиг L
        /// </summary>
        void rol()
        {
            // количество смещений можно переделать без цикла
            for (int i = 0; i < 1; i++)
            {
                CF = mem1[0]; //последнее- потом станет первым
                for (int j = 0; j <mem1.Length-1 ; j++)
                {
                    mem1[j] = mem1[j+1];

                }
                mem1[mem1.Length-1] = CF;
            }


        }
        /// <summary>
        /// >>
        /// </summary>
        void shr()
        {
           // clear();
         //   DebugBite(mem1,"SHRA ");
            reg[0] = mem1[0];
            reg[1]=reg[0];

            for (int i = 1; i < mem1.Length-1; i++)
			{
                CF=mem1[i]==true?true:false;
                     reg[i+1]=CF;
			}
            mem1 = reg;
           // DebugBite(mem1, "SHRB ");         
            showSingleresult();
            clear();
        }
        void adc()
        {
            for (int i = mem1.Length - 1; i >0; i--)
            {
                if ((mem1[i] == true && mem2[i] == true))
                {
                    reg[i] = false;
                    CF = true;
                }
                else if ((mem1[i] == false && mem2[i] == false))
                {

                    if (CF == true) { reg[i] = true; CF = false; }
                    else reg[i] = false;
                }
                else if (mem1[i] == false && mem2[i] == true || mem1[i] == true && mem2[i] == false)
                {
                    if (CF == true) { reg[i] = false; }
                    else reg[i] = true;
                }
                showresult("");
            }
            if ((mem1[0] == false && mem2[0] == false)){ reg[0] = false; }       
            else reg[0] = true;
            //else if ((mem1[0] == true && mem2[0] == true)) { }
            mem1 = reg;   
            textBox3.Text = showresult();

            DebugBite(mem1, "ADC ");
        }
        void add()
        {
            for (int i = mem1.Length-1; i >=0; i--)
            {
                if ((mem1[i]==true&&  mem2[i]==true)  )
                {
                    reg[i] = false;
                    if (CF == true) { reg[i] = true; CF = true; }
                    else if (CF == false) { CF = true; }

                    //reg[i] = CF==false? false:true;
                    //CF = true;
                }
                    else if((mem1[i]==false &&  mem2[i]==false)){

                        if (CF == true) { reg[i] = true; CF = false; }
                        else reg[i] = false;
                    }
                else if (mem1[i] == false && mem2[i] == true || mem1[i] == true && mem2[i] == false)
                {
                    if (CF == true) { reg[i] = false; }
                    else reg[i] = true;
                }
                showresult("");
                DebugBite(mem1,"A ");
                                DebugBite(mem2,"A ");
                                DebugBite(reg, "A ");
            }


            //if ((mem1[0] == false && mem2[0] == false)) { SF = false; }
            //else if ((mem1[0] == true && mem2[0] == true)) { SF = true; }
            //else reg[0] = true;
            // знак
            if (reg[0] == true) SF = true;
            //     переделать на проверку по знакам для OF
            if (CF == true) OF = true;
            CF = false;
            ZF=true;//is zero??
            for (int i = 0; i < reg.Length; i++)
            {
                if (reg[i] == true) { ZF = false; break; }
            }
            mem1=reg;
          textBox3.Text=  showresult();

        //  DebugBite(mem1,"ADD ");
        }
        string showSingleresult(string s = "\nResult")
        {
            string cobmbo = "";
            string res = s;
            res += "\nmem1:  ";
            for (int i = 0; i < mem1.Length; i++)
            {
                res += (mem1[i] == true) ? "1 " : "0 ";
            }
            //res += "\nmem2:  ";
            //for (int i = 0; i < mem1.Length; i++)
            //{
            //    res += (mem2[i] == true) ? "1 " : "0 ";
            //}
            res += "\nreg:      ";
            for (int i = 0; i < mem1.Length; i++)
            {
                res += (reg[i] == true) ? "1 " : "0 ";
                cobmbo += (reg[i] == true) ? "1 " : "0 ";
            }
            res += "\nflags:  ";

            res += "CF- " + (CF == true ? "1 " : "0 ") + "  ZF- " + (ZF == true ? "1 " : "0 ") + "  SF- " + (SF == true ? "1 " : "0 ") + "  OF- " + (OF == true ? "1 " : "0 ") + "\n------------------\n";

            richTextBox1.Text += res;
            return cobmbo;
        }

   string showresult(string s="\nResult")
        {
          //  richTextBox1.Text = "";
            string cobmbo = "";
            string res = s;
            res += "\nmem1:  ";
            for (int i = 0; i < mem1.Length; i++)
            {
                res += (mem1[i] == true) ? "1 " : "0 ";    
            }
            res += "\nmem2:  ";
            for (int i = 0; i < mem1.Length; i++)
            {
                res += (mem2[i] == true) ? "1 " : "0 ";
            }
            res += "\nreg:      ";
            for (int i = 0; i < mem1.Length; i++)
            {
                res += (reg[i] == true) ? "1 " : "0 ";
                cobmbo += (reg[i] == true) ? "1 " : "0 ";
            }
            res += "\nflags:  ";

            res += "CF- " + (CF == true ? "1 " : "0 ") + "  ZF- " + (ZF == true ? "1 " : "0 ") + "  SF- " + (SF == true ? "1 " : "0 ") + "  OF- " + (OF == true ? "1 " : "0 ")+"\n------------------\n";
           
       richTextBox1.Text += res;
            return cobmbo;
        }
        void DebugBite(bool[] arr,string s=" ")
        {
             StackTrace stackTrace = new StackTrace(true);
        StackFrame sf = stackTrace.GetFrame(1);
            string res="";
            for (int i = 0; i < arr.Length; i++)
            {
               res += (arr[i] == true) ? "1 " : "0 ";    
            }
            System.Diagnostics.Debug.WriteLine("L"+sf.GetFileLineNumber().ToString()+" " + s + res);
        }
     
        /// <summary>
        /// преобразование в обрантый код
        /// </summary>
        /// <param name="arr"></param>
        void toReturnCode(ref bool[] arr){

            DebugBite(arr, "before ");
            // invert
            for (int i = 1; i < arr.Length ; i++)
            {
                arr[i] = arr[i] == true ? false : true;
     
            }           DebugBite(arr, "after inver ");
            // add 1
            bool flag=true;
            for (int i = arr.Length-1; i >0; i--)
            {
                if (arr[i] == false) { arr[i] = true; flag = false; }
                if (flag == false) break;
                if (arr[i] == true) { arr[i] = false;}

            //    if (flag == false) break;
               
            }
            DebugBite(arr, "afteradd1 ");

    }
        void clear()
        {
            for (int i = 0; i < mem1.Length; i++)
            {
                mem1[i] = false;
                mem2[i] = false;
                reg[i] = false;
            }
            CF = false; OF = false; SF = false; ZF = false;
        }
    }
}

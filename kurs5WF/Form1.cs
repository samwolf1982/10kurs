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
        bool[] mem1;    // ячейки памяти 8
        bool[] mem2;    // ячейки памяти 8
  
        //регистры общего назначения
        bool[][] AX = new bool[2][];// имитация 2*8 бит 
        bool[][] BX = new bool[2][];
        bool[][] CX = new bool[2][];
        bool[][] DX = new bool[2][];


        bool[] AH = new bool[8];  // имитиция 8 бит
        bool[] AL = new bool[8];
        bool[] BH = new bool[8];
        bool[] BL = new bool[8];
        bool[] CH = new bool[8];
        bool[] CL = new bool[8];
        bool[] DH = new bool[8];
        bool[] DL = new bool[8];

        // флаги
   // cf перенос  zf zero sf знак of переполнение
        bool CF = false;    
        bool ZF = false;
        bool SF = false;
        bool OF = false;
      

        // для чтения даных из текстовых полей
        char[] temp1;
        char[] temp2;

        // all comands
        List<string[]> allcomands = new List<string[]>();
        int counter = 0;
        public Form1()
        {
            InitializeComponent();
            comboBox1.DataSource = ToDo.GetValues(typeof(ToDo));

            AX[0] = AH;
            AX[1] = AL;
            BX[0] = BH;
            BX[1] = BL;
            CX[0] = CH;
            CX[1] = CL;
            DX[0] = DH;
            DX[1] = DL;

        
        }
        /// <summary>
        /// сложение  2 восьмибитных регистра
        /// </summary>
        void add2(ref bool[] x,ref bool[]y)
        {
            // запоминаем старший бит первого операнда для сравнивания на переполнение    
            bool tmp = x[0];
          
            for (int i = 7; i >= 0; i--)
            {
                if ((x[i] == true && y[i] == true))
                {

                    if (CF == false) { x[i] = false; CF = true; }
                    else
                        if (CF == true) { x[i] = true; CF = true; }

                }
                else if ((x[i] == false && y[i] == false))
                {

                    if (CF == true) { x[i] = true; CF = false; }
                    else x[i] = false;
                }
                else if (x[i] == false && y[i] == true || x[i] == true && y[i] == false)
                {
                    if (CF == false) { x[i] = true; }
                    else if (CF == true) { x[i] = false; CF = true; }
                }


       
            }
            // поиск переполнениея методом сравнивания старшших битов 
            if (x[0] == false)
            {
                if ((tmp == false && y[0] == false) || (tmp == true && y[0] == false) || (tmp == false && y[0] == true)) { OF = false; }
                else OF = true;
            }
            else
                if (x[0] == true)   // поиск переполнениея методом сравнивания старшших битов 
                {
                    if ((tmp == false && y[0] == true) || (tmp == true && y[0] == false) || (tmp == true && y[0] == true)) { OF = false; }
                    else OF = true;
                }


            // проверка значениея на 0
            ZF = true;
            for (int j = 0; j < 8; j++)
            {
                if (x[j] != false) { ZF = false; break; }
            }

            // проверка знака

            if (x[0] == true) { SF = true; }


         

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
                        
                       /// add2()
                      //  add();
                        //clear();
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
        /// циклический сдвиг R без флага  !!!!!!!!!!!!!!!!!!!!!!!!!! 
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

        /// <summary>
        /// отображение значений регистров
        /// </summary>
      void showrReg()
        {

            regText.Text = "";
            string res = "";

            res += "\nAX:  ";
            for (int i = 0; i < 8; i++)
            {
                res += (AX[0][i] == true) ? "1" : "0";
            }
            res += " ";
            for (int i = 0; i < 8; i++)
            {
                res += (AX[1][i] == true) ? "1" : "0";
            }

            ////////////
            res += "\nBX:  ";
            for (int i = 0; i < 8; i++)
            {
                res += (BX[0][i] == true) ? "1" : "0";
            }
            res += " ";
            for (int i = 0; i < 8; i++)
            {
                res += (BX[1][i] == true) ? "1" : "0";
            }

            ////////////
            res += "\nCX:  ";
            for (int i = 0; i < 8; i++)
            {
                res += (CX[0][i] == true) ? "1" : "0";
            }
            res += " ";
            for (int i = 0; i < 8; i++)
            {
                res += (CX[1][i] == true) ? "1" : "0";
            }

            ////////////
            res += "\nDX:  ";
            for (int i = 0; i < 8; i++)
            {
                res += (DX[0][i] == true) ? "1" : "0";
            }
            res += " ";
            for (int i = 0; i < 8; i++)
            {
                res += (DX[1][i] == true) ? "1" : "0";
            }

            ////////////




            string s = "";

            //res += "\nflags:  ";

            s += "CF- " + (CF == true ? "1 " : "0 ") + "  \nZF- " + (ZF == true ? "1 " : "0 ") + "  \nSF- " + (SF == true ? "1 " : "0 ") + "  \nOF- " + (OF == true ? "1 " : "0 ") + "\n------------------\n";
            richTextBox1.Text = s;
            regText.Text += res;
  
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

        
        private void button2_Click(object sender, EventArgs e)
        {
            // 1 команда 2 приемник 3 источник (все разделены пробелом)
           string[] str =     allcomands[counter];
           counter++;

          

            bool[][] xx;
            bool[][] yy;
            bool[] x1 ;
            bool[] y1  ;


            if (str[0] == "mov")
            {
                parsestringRN(out xx,out yy,out x1,out y1,str);

              //  add2();
                mov(ref x1);
                showrReg();
            }
            else
                if (str[0] == "add")
                {
                    parsestringRR(out xx, out yy, out x1, out y1, str);

                      add2(ref x1,ref y1);
                    //mov(ref x1);
                    showrReg();
                }
                else
                    if (str[0] == "neg")      // cмена знака числа
                    {
                        parsestringR(out xx, out yy, out x1, str);

                        neg(ref x1);
                        //mov(ref x1);
            
                        showrReg();
                    }
                    else
                        if (str[0] == "sub")      // cмена знака числа
                        {
                            parsestringRR(out xx, out yy, out x1, out y1, str);

                            sub2(ref x1,ref y1);
                      
                            showrReg();
                        }
                        else
                            if (str[0] == "sbb")      // cмена знака числа
                            {
                                parsestringRR(out xx, out yy, out x1, out y1, str);

                                sbb(ref x1, ref y1);

                                showrReg();
                            }
        }

        /// <summary>
        /// сложение  2 восьмибитных регистра c учетом флага
        /// </summary>
        void sbb(ref bool[] x, ref bool[] y)
        {


            // запоминаем старший бит первого операнда для сравнивания на переполнение    
            bool tmp = x[0];

            // младшему биту приемника добавляем 1 если она есть флаг СF
            bool f = CF;
            for (int i = 7; i >= 0; i--)
            {
                if ((x[i] == true))
                {

                    if (f == false) { x[i] = true; }
                    else
                        if (f == true) { x[i] = false; }

                }
                else if ((x[i] == false))
                {

                    if (f == true) { x[i] = true; f = false; }

                }
            }



            for (int i = 7; i >= 0; i--)
            {
                if ((x[i] == true && y[i] == true))
                {

                    if (CF == false) { x[i] = false; CF = true; }
                    else
                        if (CF == true) { x[i] = true; CF = true; }

                }
                else if ((x[i] == false && y[i] == false))
                {

                    if (CF == true) { x[i] = true; CF = false; }
                    else x[i] = false;
                }
                else if (x[i] == false && y[i] == true || x[i] == true && y[i] == false)
                {
                    if (CF == false) { x[i] = true; }
                    else if (CF == true) { x[i] = false; CF = true; }
                }



            }
            // поиск переполнениея методом сравнивания старшших битов 
            if (x[0] == false)
            {
                if ((tmp == false && y[0] == false) || (tmp == true && y[0] == false) || (tmp == false && y[0] == true)) { OF = false; }
                else OF = true;
            }
            else
                if (x[0] == true)   // поиск переполнениея методом сравнивания старшших битов 
                {
                    if ((tmp == false && y[0] == true) || (tmp == true && y[0] == false) || (tmp == true && y[0] == true)) { OF = false; }
                    else OF = true;
                }


            // проверка значениея на 0
            ZF = true;
            for (int j = 0; j < 8; j++)
            {
                if (x[j] != false) { ZF = false; break; }
            }

            // проверка знака

            if (x[0] == true) { SF = true; }




        }


        /// <summary>
        /// вычитание  2 восьмибитных регистра
        /// </summary>
        void sub2(ref bool[] x, ref bool[] y)
        {


            // cмена знака второго число
            neg(ref y);


            // запоминаем старший бит первого операнда для сравнивания на переполнение    
            bool tmp = x[0];

            for (int i = 7; i >= 0; i--)
            {
                if ((x[i] == true && y[i] == true))
                {

                    if (CF == false) { x[i] = false; CF = true; }
                    else
                        if (CF == true) { x[i] = true; CF = true; }

                }
                else if ((x[i] == false && y[i] == false))
                {

                    if (CF == true) { x[i] = true; CF = false; }
                    else x[i] = false;
                }
                else if (x[i] == false && y[i] == true || x[i] == true && y[i] == false)
                {
                    if (CF == false) { x[i] = true; }
                    else if (CF == true) { x[i] = false; CF = true; }
                }



            }
            // поиск переполнениея методом сравнивания старшших битов 
            if (x[0] == false)
            {
                if ((tmp == false && y[0] == false) || (tmp == true && y[0] == false) || (tmp == false && y[0] == true)) { OF = false; }
                else OF = true;
            }
            else
                if (x[0] == true)   // поиск переполнениея методом сравнивания старшших битов 
                {
                    if ((tmp == false && y[0] == true) || (tmp == true && y[0] == false) || (tmp == true && y[0] == true)) { OF = false; }
                    else OF = true;
                }


            // проверка значениея на 0
            ZF = true;
            for (int j = 0; j < 8; j++)
            {
                if (x[j] != false) { ZF = false; break; }
            }

            // проверка знака

            if (x[0] == true) { SF = true; }




        }


        /// <summary>
        /// cмена знака числа - дополнительный код
        /// </summary>
        /// <param name="xx"></param>
        void neg(ref bool[] xx)
        {

            // инвертируеться
            for (int i = 0; i < 8; i++)
            {
                xx[i] = xx[i]==true?false:true;
            }
            // добавл 1

            bool f = true;
            for (int i = 7; i >= 0; i--)
            {
                if ((xx[i] == true ))
                {

                    if (f == false) { xx[i] = true; }
                    else
                        if (f == true) { xx[i] = false; }

                }
                else if ((xx[i] == false))
                {

                    if (f == true) { xx[i] = true; f = false; }
                   
                }



            }

        

        }
        /// <summary>
        /// задает значение для регистра из ячейки памяти
        /// </summary>
        /// <param name="?"></param>
        void mov(ref bool[] xx)
        {
          

            for (int i = 0; i < 8; i++)
            {
                xx[i] = mem1[i];
            }
            mem1 = null;

        }

              /// <summary>
        /// парсер строки для регистр - число сохраниться в двоичном виде в mem1- 
        /// </summary>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="str"></param>
        void parsestringRN(out bool[][] x2, out bool[][] y2, out bool[] x, out bool[] y, string[] str)
        {


            x2 = null;
            y2 = null;
            x = null;
            y = null;

            if (str[1] == "AX") x2 = AX;
            if (str[1] == "BX") x2 = BX;
            if (str[1] == "CX") x2 = CX;
            if (str[1] == "DX") x2 = DX;
            if (str[1] == "AH") x = AH;
            if (str[1] == "AL") x = AL;
            if (str[1] == "BH") x = BH;
            if (str[1] == "BL") x = BL;
            if (str[1] == "CH") x = CH;
            if (str[1] == "CL") x = CL;
            if (str[1] == "DH") x = DH;
            if (str[1] == "DL") x = DL;


            // поиск десятичного числа
          int num=   int.Parse(str[2]);
            // и конвертация в bool[8]
            bool flag=true;
            int temp=0;
            List<bool> a = new List<bool>();
            do
            {
                temp = num / 2;
                if (temp >=0)
                {
                    a.Insert (0, num%2 != 0 ? true : false);
                    num=temp;
                 
                    if (temp == 0) break;
                }
             


            } while (flag != false);

             
          //  a.Reverse();
            for (int i = a.Count; i < 8; i++)
            {
                a.Insert(0,false);
            }
            //a.Reverse();
            mem1 = a.ToArray();



            //if (str[2] == "AX") y2 = AX;
            //if (str[2] == "BX") y2 = BX;
            //if (str[2] == "CX") y2 = CX;
            //if (str[2] == "DX") y2 = DX;
            //if (str[2] == "AH") y = AH;
            //if (str[2] == "AL") y = AL;
            //if (str[2] == "BH") y = BH;
            //if (str[2] == "BL") y = BL;
            //if (str[2] == "CH") y = CH;
            //if (str[2] == "CL") y = CL;
            //if (str[2] == "DH") y = DH;
            //if (str[2] == "DL") y = DL;
        }

        /// <summary>
        /// парсер строки для регистра (моно операции) сдвиги смена знака инкременты итю
        /// </summary>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="str"></param>
        void parsestringR(out bool[][] x2, out bool[][] y2, out bool[] x, string[] str)
        {
            x2 = null;
            y2 = null;
            x = null;
          

            if (str[1] == "AX") x2 = AX;
            if (str[1] == "BX") x2 = BX;
            if (str[1] == "CX") x2 = CX;
            if (str[1] == "DX") x2 = DX;
            if (str[1] == "AH") x = AH;
            if (str[1] == "AL") x = AL;
            if (str[1] == "BH") x = BH;
            if (str[1] == "BL") x = BL;
            if (str[1] == "CH") x = CH;
            if (str[1] == "CL") x = CL;
            if (str[1] == "DH") x = DH;
            if (str[1] == "DL") x = DL;

    
        }
        /// <summary>
        /// парсер строки для регистр - регистр
        /// </summary>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="str"></param>
        void parsestringRR(out bool[][] x2, out bool[][] y2, out bool[] x, out bool[] y, string[] str)
        {
            x2=null;
            y2 = null;
            x = null;
            y = null;
         
                if (str[1] == "AX") x2 = AX;
                if (str[1] == "BX") x2 = BX;
                if (str[1] == "CX") x2 = CX;
                if (str[1] == "DX") x2 = DX;
                if (str[1] == "AH") x = AH;
                if (str[1] == "AL") x = AL;
                if (str[1] == "BH") x = BH;
                if (str[1] == "BL") x = BL;
                if (str[1] == "CH") x = CH;
                if (str[1] == "CL") x = CL;
                if (str[1] == "DH") x = DH;
                if (str[1] == "DL") x = DL;

                    if (str[2] == "AX") y2 = AX;
                    if (str[2] == "BX") y2 = BX;
                    if (str[2] == "CX") y2 = CX;
                    if (str[2] == "DX") y2 = DX;
                    if (str[2] == "AH") y = AH;
                    if (str[2] == "AL") y = AL;
                    if (str[2] == "BH") y = BH;
                    if (str[2] == "BL") y = BL;
                    if (str[2] == "CH") y = CH;
                    if (str[2] == "CL") y = CL;
                    if (str[2] == "DH") y = DH;
                    if (str[2] == "DL") y = DL;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            allcomands.Clear();
            string[] sre = comandText.Text.Split('\n');
            foreach (var item in sre)
            {
                allcomands.Add(item.Split(' '));
            }


     //   string r=    comandText.Text.Replace('\n',' ');
       //     string[] str = r.Split(' ');
            counter = 0;

           //for (int i = 0; i <= str.Length-3; i+=3)
           // {
           //     string[] t = new string[3];
           //     t[0] = str[i];
           //     t[1] = str[i+1];
           //     t[2] = str[i+2];
           //     allcomands.Add(t);
           //   //  counter++;
           // } 




       

        }
        }




    }


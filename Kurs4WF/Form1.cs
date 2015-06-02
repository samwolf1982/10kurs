using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kurs4WF
{
    public partial class Form1 : Form
    {
        int curind = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

         
            // take float number
               float val=   float.Parse( textBox1.Text);
            // take size type of number
            int size = sizeof(int) *8;
            // массив битов никакие встроеные методы не используються!
            // иначально как float 4*8
           
            int v =(int) val;
          //  v = 8+curind++;
            v = 5;
            int tempval = Math.Abs(v);
            String res = "";
            size = 8;
            BitArray arr = new BitArray(size);

            #region целая часть
            for (int i = size - 1; i >= 0; i--)
            {
                int w = tempval <= 1 ? -1 : tempval % 2;

                if (w == 0)
                {
                    arr[i] = false;
                    res = ("0" + res);
                }
                if (w == 1)
                {
                    arr[i] = true;
                    res = ("1" + res);
                }
                if (w == -1)
                {
                    arr[i] = tempval == 1 ? true : false;
                    i--;
                    for (int j = i; j >= 0; j--)
                    {
                        arr[j] = false;
                        res = ("0" + res);
                    }

                    break;
                }
                tempval = tempval / 2;
            }

            //дополнительный код
            if (v < 0)
            {
                // доделать ручную инверсию
                arr.Not();
                /// add 1
                bool flag = true;
                for (int i = size - 1; i >= 0; i--)
                {
                    if (flag == true)
                    {
                        if (arr[i] == true)
                        {
                            arr[i] = false;
                        }
                        else
                            if (arr[i] == false)
                            {
                                arr[i] = true;
                                flag = false;
                            }

                    }

                }


            }

            richTextBox1.Text += "\n" + arrtotext(arr);
            res = "";
            #endregion


            #region Дробная часть

            for (int i = 0; i < size; i++)
            {
                
            }

            #endregion

            // test show
          



            //  float r=  val - (int)val;
                
            //   int  r=  ( (val- (int) val)) << 4;   //  x * 16 

      }
        string arrtotextExpo(BitArray arr)
        {
            string res = "";
            int indexer = 0;
            foreach (bool item in arr)
            {
                if (indexer ==1 || indexer==9 ) res += " ";
                res += (item == true) ? "1" : "0";
                indexer++;
            }
            return res;
        }
        string arrtotext(BitArray arr)
        {
            string res = "";
            int indexer=0;
            foreach (bool item in arr)
            {
                if (indexer++ == 8) res += " ";
               res+= (item == true) ? "1" : "0";
            }
            return res;
        }
        // 2 buton
        private void button2_Click(object sender, EventArgs e)
        {
            // take float number
            String s=textBox1.Text;
            string[] nuberCol =s.Split(',');
            float val = float.Parse(textBox1.Text);

            int val2 = -1;
            if (comboBox1.SelectedIndex == 3)val2=      nuberCol[1].Length;
      
            // take size type of number
            int size = sizeof(float) * 8;

            int d = int.Parse(comboBox2.Items[comboBox2.SelectedIndex].ToString());

               if(comboBox1.SelectedIndex==0) richTextBox1.Text += "\n" + arrtotext(additionalCode(d,val));

               if (comboBox1.SelectedIndex == 1) richTextBox1.Text += "\n" + arrtotext(directCode(d, val));
               if (comboBox1.SelectedIndex == 2) richTextBox1.Text += "\n" + arrtotext(returnCode(d, val));
               if (comboBox1.SelectedIndex == 3) richTextBox1.Text += "\n" + floatCode3(d,s);
       

        }
        // mantis
        private void button3_Click(object sender, EventArgs e)
        {
            String s = textBox1.Text;
            string[] nuberCol = s.Split(',');
            float val = float.Parse(textBox1.Text);
            float val2 = float.Parse(textBox1.Text);
            int v = (int)val;  // целая
            float vd = val - v; //дробная

          
            if (comboBox1.SelectedIndex == 3) val2 = nuberCol[1].Length;

            // take size type of number
            int size = sizeof(float) * 8;

            bool sign =val>=0?true:false;  //ok

            int a = int.Parse(nuberCol[0]);
            uint b = uint.Parse(nuberCol[1]);

            int countNum = nuberCol[1].Length;
            int counter=0;
            uint res = 0;


            uint c = 30;
            for (int i = 0; i < countNum; i++)// пишем от 30 а потом <<
            {
                vd = vd * 2;
                if (vd >= 1) { res |= 1 << 0; vd = vd - 1; }
                else res |= 0 << 0;
                res = res << 1;
                showBits(res, "BeginB ");
                
            }
            showBits(res, "FinA ");
            for (int i = 0; i < 30-countNum+1; i++)
            {               
            res = res << 1;
            }
            showBits(res, "FinB ");
            //перевод дроби в res




            bool tempBool=false;
            int tempIn;
          bool  flag = false;
          counter = 0;                                           // количество сдвигов - експонента
            res = res >> 1; // двигаем вправо на 1 позицию (место для другой)
            do
            {
              tempBool =  a%2==0 ? false:true; // если число чет значить 0 нечет 1 
              
                tempIn = a >> 1;// >> левая часть
                if (tempIn > 0)
                {
                    a = tempIn;
                    res = res >> 1; // двигаем вправо на 1 позицию (место для другой) // правая часть
                    showBits(res, "Move ");
                    if (tempBool == true) {  res |= 1 << 30; }
                    else { res |= 0 << 30; }
                    showBits(res, "Move ");
                    counter++;
                }
                else { flag = true; }
                
             
            } while (flag!=true);
            res = res << 1;        // cмещение назад

            showBits(res, "FinishM ");// готовая мантисса число b

          //  a =counter+127;
            sbyte expo = 127;

            for (int i = 0; i < counter; i++)
            {
                expo++;
            }

            showsBytes(expo, "Fin ");
            showBits(res, "manF ");

           // showsBytes(expo);
            var exp = expo >> 1;
   

            // ss |= 1 << 2;
         
        }
   String floatCode3(int capacity, string num)
        {
            string resStr = "";
            string[] nuberCol = num.Split(',');
            float val = float.Parse(num);
            float val2 = float.Parse(num);
            int v = (int)val;  // целая
            float vd = val - v; //дробная
       bool sign = val >= 0 ? true : false;  //ok
            vd = Math.Abs(vd);


          //  if (comboBox1.SelectedIndex == 3) val2 = nuberCol[1].Length;

            // take size type of number
            int size = sizeof(float) * 8;

           

            //int a = int.Parse(nuberCol[0]); 
            //uint b = uint.Parse(nuberCol[1]);
            int a =Math.Abs (v);
            uint b = uint.Parse(nuberCol[1]);

            int countNum = nuberCol[1].Length;// количество чисел в дроби для умножения
            int counter = 0; // счетчик
            uint res = 0;// целое число биты числа будут  использоваться как массив 


          
            // пишем от 30 а потом смещаем влевоо до упора
             // для примера   ,1875*2 =0   0,375*2 = 0  0,750*2 =1 0,5*2=1 
         // результат  0000000000000n11; потом смещение до упора и 1100000000n
            for (int i = 0; i < countNum; i++)
            {
                vd = vd * 2;
                if (vd >= 1) { res |= 1 << 0; vd = vd - 1; }
                else res |= 0 << 0;
                res = res << 1;
                showBits(res, "BeginB ");

            }
            showBits(res, "FinA ");
             // <<   до упора
            for (int i = 0; i < 30 - countNum + 1; i++)
            {
                res = res << 1;
            }
            showBits(res, "FinB ");
            //перевод дроби в res




            bool tempBool = false; // определитель четности если число заканчиватеьс на 0 тогда чет иниче нечет
            int tempIn;// временое хранилище для числа
            bool flag = false;
            counter = 0;                                           // количество сдвигов - експонента
            res = res >> 1; // двигаем вправо на 1 позицию (место для другой)
            do
            {
                tempBool = a % 2 == 0 ? false : true; // если число чет значить 0 нечет 1 

                tempIn = a >> 1;// >> левая часть
                
                if (tempIn > 0) // при смещение >> всегда будет число меньше как только дошли до 0 значить сместилися максимально 
                {
                    a = tempIn;//  фиксируем результат
                    res = res >> 1; // двигаем вправо на 1 позицию (место для другой) // правая часть
                    showBits(res, "Move ");
                    // чет нечет
                    if (tempBool == true) { res |= 1 << 30; }   
                    else { res |= 0 << 30; }
                    showBits(res, "Move ");
                    // счетчик сдвигов
                    counter++;
                }
                else { flag = true; }   // флаг для выхода


            } while (flag != true);
            res = res << 1;        // cмещение назад

            showBits(res, "FinishM ");// готовая мантисса число b

            //  a =counter+127;
            sbyte expo = 127;// 8 битная експонента
                            
            for (int i = 0; i < counter; i++)// увеличение на количество сдвигов (только в цикле ) << не работает
            {
                expo++;
            }

            showsBytes(expo, "Fin ");
            showBits(res, "manF ");

            // showsBytes(expo);
            var exp = expo >> 1; 
       //    текстовое представление
          //  resStr = sign == true ? "0 " : "1 ";
          //  resStr += showsBytes(expo);
          //  resStr += showBits(res);

          //  resStr += "     знак= " + ((sign == true) ? "1 " : "0 ");
          //resStr += "     порядок= " + expo.ToString();
          //  resStr += "     мантисса= " + convertbiteToFloat(res);

          //  resStr = sign == true ? "0 " : "1 ";
       resStr += "     знак= " + ((sign == true) ? "1 " : "0 ");
       resStr += "     порядок= " ;
            resStr += showsBytes(expo)+"  ("+ expo.ToString()+")";
       resStr += "     мантисса= " ;
            resStr += showBits(res)+ " ("+ convertbiteToFloat(res)+")";

           
           
           
      
            return resStr;
        }

        /// <summary>
        /// перевод дробной части в десятичную дробь
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
       String convertbiteToFloat(uint a){
           showBits(a, "Conver+ ");
           int size = sizeof(uint) * 8;
           a = a >> 1;
           uint mask = 1 << 30;
           uint temp=0;
           List<bool> rr = new List<bool>();
           List<int> rri = new List<int>();
          double colect = 0;
           for (int i = 1; i <size+1; i++)
           {
               temp=mask&a;
             //  showBits(a, "Conver ");
               if (temp != 0)
               {
                   int v = i * -1;
                   double p = Math.Pow(2,v);
                   colect +=p*1;
                   rr.Add(true);
                   rri.Add(v);
               }
               else
               {
                   colect += 0;
                   rr.Add(false);
               }

               a = a << 1;
      
           }

      
           return " " +colect.ToString();
          


       }
       String showsBytes(int a, string s = "exp: ")
        {
            string res = "";

            int tempIn;

            int counter = 0;
            bool tempBool;
            int size = sizeof(int) * 8;
          //  a = a << size - 8;
            for (int i = 0; i < 8; i++)
            {
                tempBool = a % 2 == 0 ? false : true; // если число чет значить 0 нечет 1 
                counter++;
                tempIn = a >> 1;

                a = tempIn;

                if (tempBool == true)
                {
                    res = "1" + res;
                }
                else { res = "0" + res; }





            }


            return res;


            System.Diagnostics.Debug.WriteLine(s + res.ToString());

        }
        string showBits(uint a,string s="x: "){
            string res="";
         
           uint tempIn;
   
        int    counter = 0;
        bool tempBool;
        int size = sizeof(uint) * 8;
          for (int i = 0; i < size; i++)
			{
			         tempBool =  a%2==0 ? false:true; // если число чет значить 0 нечет 1 
                counter++;
                tempIn = a >> 1;              
                    a = tempIn;
         
                    if (tempBool == true) {
                        res = "1"+res;    
                    }
                    else { res = "0"+res; }            
			}
  System.Diagnostics.Debug.WriteLine(s+res.ToString());
          return " "+res;





        }

        //  формат с плавающей точкой
        //                       разрядность 16 32 64
        BitArray floatCode(int capacity, float val,int val2)
        {
            int size = capacity;
            // массив битов никакие встроеные методы не используються!
            // иначально как float 4*8
            // делжка числа на две части целую и дробную
            BitArray arr = new BitArray(size);
            int expo = 8;
            int mantis = 24;
            BitArray arrMantis = new BitArray(mantis);
            BitArray arrExponent = new BitArray(expo);

            int v = (int)val;  // целая
            float vd = val - v; //дробная
            //  v = 8+curind++;
            int tempval = Math.Abs(v);
            //   float tempval2 = 0.02F;
            //   size = 8;
            float fl = Math.Abs(vd);
            double flint = Math.Floor(val);
            #region целая части
            for (int i = expo-1; i > 0; i--)
            {
                int w = tempval <= 1 ? -1 : tempval % 2;

                if (w == 0)
                {
                    arr[i] = false;
                }
                if (w == 1)
                {
                    arr[i] = true;
                }
                if (w == -1)
                {
                    arr[i] = tempval == 1 ? true : false;
                    i--;
                    for (int j = i; j >= 0; j--)
                    {
                        arr[j] = false;

                    }

                    break;
                }
                tempval = tempval / 2;
            }
            // от 0 до 8 заполнено
            BitArray temp = new BitArray(mantis);
            tempval = val2;
            for (int i = 0; i < mantis; i++)
            {
                int w = tempval <= 1 ? -1 : tempval % 2;
                System.Diagnostics.Debug.WriteLine("w: " + w);
                if (w == 0)
                {
                    temp[i] = false;
                }
                if (w == 1)
                {
                    temp[i] = true;
                }
                if (w == -1)
                {
                    temp[i] = tempval == 1 ? true : false;
                    i++;
                    for (int j = i; j <mantis; j++)
                    {
                        temp[j] = false;

                    }

                    break;
                }
                tempval = tempval / 2;
            }
            // from temp to main
            int index=-1;
            for (int i = temp.Length-1; i>=0 ; i--)
			{
			 if(temp[i]==true) { index=i;  break;}
			}


            for (int i = expo; i < expo+mantis; i++)
            {
               if(index<0)break;
               arr[i] = temp[index--];

            }



            //////-------------
            //bool begin = false;// для начала нантисы
            //float temp = fl;
            ////-----------
            //for (int i = expo; i < size; i++)
            //{
            //    // int w = tempval <= 1 ? -1 : tempval % 2;

            //    fl = fl * 2;
            //    if (fl > 1)
            //    {
            //        if (begin == false) { i = expo; 
            //            //fl = temp; fl = fl * 2;
            //        }
            //            begin = true;
            //            System.Diagnostics.Debug.WriteLine("i1 "+ i +" fl= "+fl); 
            //        arr[i] = true;
            //        fl = fl - 1;
            //    }
            //    else
            //        if (fl < 1)
            //        {
            //            if (begin == false) { System.Diagnostics.Debug.WriteLine("Con " + i + " fl= " + fl); continue; }
            //            if (begin == true) { arr[i] = false;
            //            System.Diagnostics.Debug.WriteLine("i0 " + i + " fl= " + fl); 
            //            }
            //        }

            //}
            
            //   richTextBox1.Text += "\n" + arrtotext(arr);
            // res = "";
            #endregion





            #region дробная часть
            //for (int i = 8; i < size; i++)
            //{
            //    // int w = tempval <= 1 ? -1 : tempval % 2;

            //    fl = fl * 2;
            //    if (fl > 1)
            //    {
            //        arr[i] = true;
            //        fl = fl - 1;
            //    }
            //    else
            //        if (fl < 1)
            //        {
            //            arr[i] = false;
            //        }

            //}

            //дополнительный код
            //если отриц
            //if (val < 0)
            //{
            //    // доделать ручную инверсию
            //    // cразу добавляеться значение для нуля 1бит
            //    // arr.Not();
            //    //arr[0] = true;
            //    for (int i = size - 1; i >= 0; i--)
            //    {
            //        var e = 0;
            //        arr[i] = (arr[i] == true) ? false : true;
            //    }
            //    /// add 1
            //    bool flag = true;
            //    for (int i = size - 1; i > 0; i--)
            //    {
            //        if (flag == true)
            //        {
            //            if (arr[i] == true)
            //            {
            //                arr[i] = false;
            //            }
            //            else
            //                if (arr[i] == false)
            //                {
            //                    arr[i] = true;
            //                    flag = false;
            //                }
            //        }

            //    }
            //}
            #endregion
            return arr;
        }
     

        //  формат с плавающей точкой
        //                       разрядность 16 32 64
        BitArray floatCode2(int capacity, float val, int val2)
        {
            int size = capacity;
            // массив битов никакие встроеные методы не используються!
            // иначально как float 4*8
            // делtжка числа на две части целую и дробную
            BitArray arr = new BitArray(size);
            // знак
            bool sign = val >= 0 ? false : true;

            int v = (int)val;  // целая

            int expo = 8;
            BitArray arrExponent = new BitArray(expo); // для разрядости

            // целую часть в двоичную
            var s = directCode(expo, v);
          //  arrExponent = revers( );

               float vd = val - v; //дробная
            int mantis = 24;
            // мантисса
            BitArray arrMantis = searchMantiss(Math.Abs(vd), val2, mantis);


          // поиск первого индекса
            int findex=0;
            for ( ; findex <s.Length; findex++)
            {
                if (s[findex] == true) break;
            }
            findex = expo - findex - 1;   ///      n*2^finindex
////////////////***********************

            var mant = shufle(s, arrMantis, findex);
            debugArr(mant, "newMan");
            debugArr(s, "oldS ");
            // searchExponent findindex+127
            float r=(float) (findex + 127);
            s = directCodExponent(expo, r);
            debugArr(s, "NewS ");

            debugArr(s, "ss ");
            debugArr(mant, "man ");
            // все вместе
            var f = allbitarr(sign,s,mant,32);

            debugArr(f, "fin ");
            //  v = 8+curind++;
            //int tempval = Math.Abs(v);


            ////   float tempval2 = 0.02F;
            ////   size = 8;
            //float fl = Math.Abs(vd);
            //double flint = Math.Floor(val);
            //#region целая части
            //for (int i = expo - 1; i > 0; i--)
            //{
            //    int w = tempval <= 1 ? -1 : tempval % 2;

            //    if (w == 0)
            //    {
            //        arr[i] = false;
            //    }
            //    if (w == 1)
            //    {
            //        arr[i] = true;
            //    }
            //    if (w == -1)
            //    {
            //        arr[i] = tempval == 1 ? true : false;
            //        i--;
            //        for (int j = i; j >= 0; j--)
            //        {
            //            arr[j] = false;

            //        }

            //        break;
            //    }
            //    tempval = tempval / 2;
            //}
            //// от 0 до 8 заполнено
            //BitArray temp = new BitArray(mantis);
            //tempval = val2;
            //for (int i = 0; i < mantis; i++)
            //{
            //    int w = tempval <= 1 ? -1 : tempval % 2;
            //    System.Diagnostics.Debug.WriteLine("w: " + w);
            //    if (w == 0)
            //    {
            //        temp[i] = false;
            //    }
            //    if (w == 1)
            //    {
            //        temp[i] = true;
            //    }
            //    if (w == -1)
            //    {
            //        temp[i] = tempval == 1 ? true : false;
            //        i++;
            //        for (int j = i; j < mantis; j++)
            //        {
            //            temp[j] = false;

            //        }

            //        break;
            //    }
            //    tempval = tempval / 2;
            //}
            //// from temp to main
            //int index = -1;
            //for (int i = temp.Length - 1; i >= 0; i--)
            //{
            //    if (temp[i] == true) { index = i; break; }
            //}


            //for (int i = expo; i < expo + mantis; i++)
            //{
            //    if (index < 0) break;
            //    arr[i] = temp[index--];

            //}



            //////-------------
            //bool begin = false;// для начала нантисы
            //float temp = fl;
            ////-----------
            //for (int i = expo; i < size; i++)
            //{
            //    // int w = tempval <= 1 ? -1 : tempval % 2;

            //    fl = fl * 2;
            //    if (fl > 1)
            //    {
            //        if (begin == false) { i = expo; 
            //            //fl = temp; fl = fl * 2;
            //        }
            //            begin = true;
            //            System.Diagnostics.Debug.WriteLine("i1 "+ i +" fl= "+fl); 
            //        arr[i] = true;
            //        fl = fl - 1;
            //    }
            //    else
            //        if (fl < 1)
            //        {
            //            if (begin == false) { System.Diagnostics.Debug.WriteLine("Con " + i + " fl= " + fl); continue; }
            //            if (begin == true) { arr[i] = false;
            //            System.Diagnostics.Debug.WriteLine("i0 " + i + " fl= " + fl); 
            //            }
            //        }

            //}

            //   richTextBox1.Text += "\n" + arrtotext(arr);
            // res = "";
          //  #endregion





            #region дробная часть
            //for (int i = 8; i < size; i++)
            //{
            //    // int w = tempval <= 1 ? -1 : tempval % 2;

            //    fl = fl * 2;
            //    if (fl > 1)
            //    {
            //        arr[i] = true;
            //        fl = fl - 1;
            //    }
            //    else
            //        if (fl < 1)
            //        {
            //            arr[i] = false;
            //        }

            //}

            //дополнительный код
            //если отриц
            //if (val < 0)
            //{
            //    // доделать ручную инверсию
            //    // cразу добавляеться значение для нуля 1бит
            //    // arr.Not();
            //    //arr[0] = true;
            //    for (int i = size - 1; i >= 0; i--)
            //    {
            //        var e = 0;
            //        arr[i] = (arr[i] == true) ? false : true;
            //    }
            //    /// add 1
            //    bool flag = true;
            //    for (int i = size - 1; i > 0; i--)
            //    {
            //        if (flag == true)
            //        {
            //            if (arr[i] == true)
            //            {
            //                arr[i] = false;
            //            }
            //            else
            //                if (arr[i] == false)
            //                {
            //                    arr[i] = true;
            //                    flag = false;
            //                }
            //        }

            //    }
            //}
            #endregion
            return f;
        }
        // обраный код
        //                       разрядность 16 32 64
        BitArray returnCode(int capacity, float val)
        {
            int size = capacity;
            // массив битов никакие встроеные методы не используються!
            // иначально как float 4*8
            // делжка числа на две части целую и дробную
            BitArray arr = new BitArray(capacity);
            int v = (int)val;  // целая
            float vd = val - v; //дробная
            //  v = 8+curind++;
            int tempval = Math.Abs(v);
            //   float tempval2 = 0.02F;
            //   size = 8;
            float fl = Math.Abs(vd);
            #region целая часть
            for (int i = 7; i > 0; i--)
            {
                int w = tempval <= 1 ? -1 : tempval % 2;

                if (w == 0)
                {
                    arr[i] = false;
                }
                if (w == 1)
                {
                    arr[i] = true;
                }
                if (w == -1)
                {
                    arr[i] = tempval == 1 ? true : false;
                    i--;
                    for (int j = i; j >= 0; j--)
                    {
                        arr[j] = false;

                    }

                    break;
                }
                tempval = tempval / 2;
            }


            //   richTextBox1.Text += "\n" + arrtotext(arr);
            // res = "";
            #endregion





            #region дробная часть
            for (int i = 8; i < size; i++)
            {
                // int w = tempval <= 1 ? -1 : tempval % 2;

                fl = fl * 2;
                if (fl > 1)
                {
                    arr[i] = true;
                    fl = fl - 1;
                }
                else
                    if (fl < 1)
                    {
                        arr[i] = false;
                    }

            }

            //дополнительный код
            //если отриц
            if (val < 0)
            {
                // доделать ручную инверсию
                // cразу добавляеться значение для нуля 1бит
                // arr.Not();
                //arr[0] = true;
                for (int i = size - 1; i >= 0; i--)
                {
                    var e = 0;
                    arr[i] = (arr[i] == true) ? false : true;
                }
                /// add 1
                //bool flag = true;
                //for (int i = size - 1; i > 0; i--)
                //{
                //    if (flag == true)
                //    {
                //        if (arr[i] == true)
                //        {
                //            arr[i] = false;
                //        }
                //        else
                //            if (arr[i] == false)
                //            {
                //                arr[i] = true;
                //                flag = false;
                //            }
                //    }

                //}
            }
            #endregion
            return arr;
        }

        // прямой код
        //                       разрядность 16 32 64
        BitArray directCode(int capacity, float val)
        {
            int size = capacity;
            // массив битов никакие встроеные методы не используються!
            // иначально как float 4*8
            // делжка числа на две части целую и дробную
            BitArray arr = new BitArray(capacity);
            int v = (int)val;  // целая
            float vd = val - v; //дробная
            //  v = 8+curind++;
            int tempval = Math.Abs(v);
            //   float tempval2 = 0.02F;
            //   size = 8;
            float fl = Math.Abs(vd);
            #region целая часть
            int i =  7;
            for (; i > 0; i--)
            {
                int w = tempval <= 1 ? -1 : tempval % 2;

                if (w == 0)
                {
                    arr[i] = false;
                }
                if (w == 1)
                {
                    arr[i] = true;
                }
                if (w == -1)
                {
                    arr[i] = tempval == 1 ? true : false;
                    i--;
                    for (int j = i; j >= 0; j--)
                    {
                        arr[j] = false;

                    }

                    break;
                }
                tempval = tempval / 2;
            }


            //   richTextBox1.Text += "\n" + arrtotext(arr);
            // res = "";
            #endregion





            #region дробная часть
            for ( i = 8; i < size; i++)
            {
                // int w = tempval <= 1 ? -1 : tempval % 2;

                fl = fl * 2;
                if (fl > 1)
                {
                    arr[i] = true;
                    fl = fl - 1;
                }
                else
                    if (fl < 1)
                    {
                        arr[i] = false;
                    }

            }

            //дополнительный код
            //если отриц
            if (val < 0)
            {
                // доделать ручную инверсию
                // cразу добавляеться значение для нуля 1бит
                // arr.Not();
                arr[0] = true;
                //for (int i = size - 1; i >= 0; i--)
                //{
                //    var e = 0;
                //    arr[i] = (arr[i] == true) ? false : true;
                //}
                /// add 1
                //bool flag = true;
                //for (int i = size - 1; i > 0; i--)
                //{
                //    if (flag == true)
                //    {
                //        if (arr[i] == true)
                //        {
                //            arr[i] = false;
                //        }
                //        else
                //            if (arr[i] == false)
                //            {
                //                arr[i] = true;
                //                flag = false;
                //            }
                //    }

                //}
            }
            #endregion
            return arr;
        }
        BitArray directCodExponent(int capacity, float val)
        {
            int size = capacity;
            // массив битов никакие встроеные методы не используються!
            // иначально как float 4*8
            // делжка числа на две части целую и дробную
            BitArray arr = new BitArray(capacity);
            int v = (int)val;  // целая
            float tempval2 = val;
            float vd = val - v; //дробная
            //  v = 8+curind++;
            int tempval = Math.Abs(v);
            //   float tempval2 = 0.02F;
            //   size = 8;
            float fl = Math.Abs(vd);
            #region целая часть
            int i = 7;
            for (; i >= 0; i--)
            {

                int w = tempval <= 1 ? -1 : tempval % 2;

                if (w == 0)
                {

                    arr[i] = false;
                }
                if (w == 1)
                {
                    arr[i] = true;
                }
                if (w == -1)
                {
                    arr[i] = tempval == 1 ? true : false;
                    i--;
                    for (int j = i; j >= 0; j--)
                    {
                        arr[j] = false;

                    }

                    break;
                }
                tempval = tempval / 2;
                tempval2 = tempval2 / 2;
            }


            //   richTextBox1.Text += "\n" + arrtotext(arr);
            // res = "";
            #endregion





            #region дробная часть
            for (i = 8; i < size; i++)
            {
                // int w = tempval <= 1 ? -1 : tempval % 2;

                fl = fl * 2;
                if (fl > 1)
                {
                    arr[i] = true;
                    fl = fl - 1;
                }
                else
                    if (fl < 1)
                    {
                        arr[i] = false;
                    }

            }

            //дополнительный код
            //если отриц
            if (val < 0)
            {
                // доделать ручную инверсию
                // cразу добавляеться значение для нуля 1бит
                // arr.Not();
                arr[0] = true;
                //for (int i = size - 1; i >= 0; i--)
                //{
                //    var e = 0;
                //    arr[i] = (arr[i] == true) ? false : true;
                //}
                /// add 1
                //bool flag = true;
                //for (int i = size - 1; i > 0; i--)
                //{
                //    if (flag == true)
                //    {
                //        if (arr[i] == true)
                //        {
                //            arr[i] = false;
                //        }
                //        else
                //            if (arr[i] == false)
                //            {
                //                arr[i] = true;
                //                flag = false;
                //            }
                //    }

                //}
            }
            #endregion
            return arr;
        }


        // дополнительный код
        //                       разрядность 16 32 64
        BitArray additionalCode(int capacity,float val)
        {
            int size=capacity;
            // массив битов никакие встроеные методы не используються!
            // иначально как float 4*8
            // делжка числа на две части целую и дробную
            BitArray arr = new BitArray(capacity);
             int v = (int)val;  // целая
            float vd = val - v; //дробная
            //  v = 8+curind++;
            int tempval = Math.Abs(v);
         //   float tempval2 = 0.02F;
         //   size = 8;
            float fl = Math.Abs( vd);
             #region целая часть
             for (int i = 7; i > 0; i--)
             {
                 int w = tempval <= 1 ? -1 : tempval % 2;

                 if (w == 0)
                 {
                     arr[i] = false;
                 }
                 if (w == 1)
                 {
                     arr[i] = true;
                 }
                 if (w == -1)
                 {
                     arr[i] = tempval == 1 ? true : false;
                     i--;
                     for (int j = i; j >= 0; j--)
                     {
                         arr[j] = false;
              
                     }

                     break;
                 }
                 tempval = tempval / 2;
             }

        
          //   richTextBox1.Text += "\n" + arrtotext(arr);
            // res = "";
             #endregion





            #region дробная часть
            for (int i = 8; i <size; i++)
            {
               // int w = tempval <= 1 ? -1 : tempval % 2;

                fl = fl * 2;
                if (fl >1)
                {
                    arr[i] = true;
                    fl = fl - 1;
                }else
                if (fl < 1)
                {
                    arr[i] = false;
                }
         
            }

            //дополнительный код
            //если отриц
            if (val < 0)
            {
                // доделать ручную инверсию
                // cразу добавляеться значение для нуля 1бит
                // arr.Not();
                //arr[0] = true;
                for (int i = size-1; i >=0; i--)
                {
                    var e = 0;
                    arr[i] = (arr[i] == true) ? false : true;
                }
                /// add 1
                bool flag = true;
                for (int i = size-1; i > 0; i--)
                {
                    if (flag == true)
                    {
                        if (arr[i] == true)
                        {
                            arr[i] = false;
                        }
                        else
                            if (arr[i] == false)
                            {
                                arr[i] = true;
                                flag = false;
                            }
                    }

                }
            }
                #endregion
            return arr;
        }

      private  int countNum(float f,int capacity=1000000)
        {
           
            float fl=f;
            int v;
            float vd;


           v = (int)fl;  // целая
           vd = fl - v; //дробная
           vd *=capacity ;

        int r=  (int) vd;

        while (r % 10 == 0)// идем дальше
        {
             r = r / 10;
        }
            System.Diagnostics.Debug.WriteLine(" Count:  " + r);
            return r; ;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 3;
            comboBox2.SelectedIndex = 0;
        }
        private BitArray searchMantiss(float f,int count,int sizemantiss)
        {
            BitArray temp = new BitArray(sizemantiss-1);
            int ind=0;
            do
            {
                f = f * 2;
                if (f >= 1)
                {
                   temp[ind++]=true;
                    f =f- 1;
                }
                else{
                     temp[ind++]=false;
                   
                }
                debugArr(temp,"temp: ");
        
            
            } while (count-- > 0);
            //int ind=-1;
            //// cсмещение вправо >> удаление нулей
            //for (int i = b.Length; i>=0 ; i--)
            //{
            //    if (b[i] == true) { ind = i; break; }
            //}
            //if (ind > -1)
            //{
            //    for (int i = ind,j=0; i <b.Length ; i++)
            //    {

            //    }
            //}

            return temp;


        }

        /// <summary>
        ///  соеденение знака експонетны и мантиссы
        /// </summary>
        /// <param name="sign">знак</param>
        /// <param name="ex">експонентк</param>
        /// <param name="man">мантисса</param>
        /// <param name="capasity">емкость</param>
        /// <returns></returns>
        private BitArray allbitarr(bool sign, BitArray ex, BitArray man,int capasity)
        {
            BitArray result = new BitArray(capasity);

            debugArr(ex, "EX ");
            debugArr(man, "Man ");
            result[0] = sign;
            int p=0;
            if (capasity == 32) p = 8;
            if (capasity == 64) p = 11;
            if (capasity == 79) p = 15;

           
            for (int i = 1,j=0; i <= p; i++,j++)
            {
                result[i]=ex[j];
              
            }
            for (int i = p+1, j = 0; i < capasity; i++, j++)
            {
                result[i] = man[j];
            }

            return result;


        }
        public void debugArr(BitArray b,String s){
            string r=s;
            for (int i = 0; i < b.Length; i++)
			{
                r += b[i] == true ? "1 " : "0 ";
         			}
            System.Diagnostics.Debug.WriteLine(r);

        }


        public BitArray shufle(BitArray a, BitArray b, int index)
        {
            BitArray res = new BitArray(b.Length);
         //   debugArr(b, "ShufleManBef : ");
            for (int i = b.Length - index-1; i>=0;  i--)
            {
                //if (b[i] == true)
                //{

                //}
                res[i + index] = b[i];

            }
            debugArr(res, "ShufleManAft : ");
            debugArr(a, "ShufleManAft2 : ");
            for (int i = index-1,j=a.Length-1; i >= 0; i--,j--)
            {

                res[i] = a[j];
                debugArr(res, "ShufleManRes : ");
            }
            debugArr(res, "ShufleManAft2 : ");
            return res;

        }
    }
}

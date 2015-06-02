// k3.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include <string>
#include <iostream>
#include <iomanip>
#include <cstdio>
#include <iostream>
#include "stdafx.h"
#include <fstream>
#include <conio.h>
#include <math.h>
#include <windows.h>
#define pi 3.14
using namespace std;


using namespace std;
double xxx(double x, double y, double a1, double b1, double c1, double c2, double b2, double a2);
double yyy(double x, double y, double a1, double b1, double c1, double c2, double b2, double a2);
 // привидение функции к удобному виду с одним неизвестным
double fi(double a2, double c1, double b1, double a1, double b2, double c2, double x)
{

	return	(-1 * asin(c2 - a2*cos(atan(c1 - b1*cos(x) / a1) + pi) / a2) + pi);
	//	return  (cosh(0.7*x) - 9) / 3.5;
}

int main() {
	// установка локали
	setlocale(LC_ALL, "Rus");
	int n = 0;
	double x = -2.0, y, b, eps = 0.0000001;
	double a1, a2, b1, b2, c1, c2,p1,p2;
	int who = 0;       // 0 нютона
	c1 = 0.0005; c2 = 0.0005;
	cout << "a1: "; cin >> a1;
	cout << "a2: "; cin >> a2;
	cout << "b1: "; cin >> b1;
	cout << "b2: "; cin >> b2;
	cout << "y: "; cin >> y;
	cout << "0- нютона 1 - итер спуск: "; cin >> who;


	if (who == 0){
		// тест подстановка х от -50 до 50
		// надо потестировать поже
		for (double i = 0; i < 50; i++)
		{
			x = i;
			do {
				y = fi(a2, c1, b1, a1, b2, c2, x);
				b = fabs(x - y);
				x = y;
				;
				n++;
				//	cout << "Iter: " << n << "\n";
			} while (b >= eps && n < 100);

			cout << "Root x =" << x << "\n";
			cout << "Iterations n= " << n << "\n";

			n = 0;
		}
		_getch();

	}
	else
		if (who = 1){


			int i = 1; //счетчик итераций
			string rez = ""; //строка расчета результата
			rez += "";
		//        градиентный поиск по  xy
			p1 = xxx(x, y,a1,b1,c1,c2,b2,a2);
			p2 = yyy(x, y, a1, b1, c1, c2, b2, a2);

			while (abs(p2)>eps)
			{
		
			cout << "количество итер= " << i << "x=" << x << "  y = "<<y<< "\n";

				x = x - eps * p1;
				y = y - eps * p2;
				p1 = xxx(x, y, a1, b1, c1, c2, b2, a2);
				p1 = yyy(x, y, a1, b1, c1, c2, b2, a2);
				i++;
				if (i > 100) break;
			}
	cout << "количество итер= " << i << "x=" << x << "  y = "<< "\n";


		}


}

double xxx(double x, double y, double a1, double b1, double c1, double c2,double b2, double a2) {


	return 2 * a1 * (a1 * tan(x) + b1 * cos(y) - c1) *
		(1 / (cos(x) * cos(x))) +
		2 * a2 * (a2 * cos(x) + b2 * sin(y) - c2) *
		(-1 * sin(x));
}

double yyy(double x, double y, double a1, double b1, double c1, double c2, double b2, double a2)
{


	return 2 * b1 * (a1 * tan(x) + b1 * cos(y) - c1) *
		(-1 * sin(y)) +
		2 * b2 * (a2 * cos(x) + b2 * sin(y) - c2) *
		(cos(y));
}
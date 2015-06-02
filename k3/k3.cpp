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
 // привидение функции к удобному виду с одним неизвестным
double fi(double a2, double c1, double b1, double a1, double b2, double c2, double x)
{

	return	(-1 * asin(c2 - a2*cos(atan(c1 - b1*cos(x) / a1) + pi) / a2) + pi);
	//	return  (cosh(0.7*x) - 9) / 3.5;
}

int main() {
	int n = 0;
	double x = -2.0, y, b, eps = 0.000001;
	double a1, a2, b1, b2, c1, c2;
	c1 = 0.0005; c2 = 0.0005;
	cout << "a1: "; cin >> a1;
	cout << "a2: "; cin >> a2;
	cout << "b1: "; cin >> b1;
	cout << "b2: "; cin >> b2;

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
		} while (b >= eps && n<100);

		cout << "Root x =" << x << "\n";
		cout << "Iterations n= " << n << "\n";

		n = 0;
	}
	_getch();
	return 0;
}


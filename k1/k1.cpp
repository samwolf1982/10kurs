// k1.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include <string>
#include <iostream>
#include <iomanip>
using namespace std;


void binToNum();
void displayBits(int value);

int _tmain(int argc, _TCHAR* argv[])
{
	// установка локали
	setlocale(LC_ALL, "Rus");
	int result = 0; // указатель на выбор

	do{
		result = 0;
		int tempValue = 0; // ввод из клавиатуры

		cout << "Какую операцию вы желаете делать?\n";
		cout << "Десятичное в двоичное [1] или двоичное в десятичное [2] 0 выход \nЗделайте выбор:";
		cin >> tempValue;  //ввод

		if (tempValue != 1 && tempValue != 2) result = 0; // 10 to 2
		else
		{
			if (tempValue == 1){
				int val;
				cout << "Введити число в десятичном формате: ";
				cin >> val;
				cout << "\n\nЧисло " << val << " ->  ";
				displayBits(val);
				cout << "\n\n";
			}
			if (tempValue == 2){  // 2 to 10
				string val;
				cout << "Введити число в двоичном  формате: ";

				binToNum();
				cout << "\n\n";
			}
			result = 1;
		}




	} while (result != 0);

	cout << "Досвидания\n";







	cout << "\n";
	return 0;
}
void displayBits(int value)
{
	int size = sizeof(int) * 8;// размер числа
	unsigned c, displayMask = 1 << size - 1;

	cout << "value = ";

	for (c = 1; c <= size; c++)
	{
		// сравнение : 
	  //  Битовое "&" сравнивает последовательно разряд за разрядом два операнда.Для каждого разряда результат равен 1,
	   //гда и только тогда, когда оба соответствующих разряда операндов равны 1. Так, например,
      //0010011 & 00111101 = 00010001
		cout << (value & displayMask ? '1' : '0');
		// cдвиг и установк значения
		value <<= 1;
		 // если 8 тогда пробел
		if (c % 8 == 0)
			cout << " ";
	}

	cout << "\n";
}
/*
/   битовое знаяение в число
*/
void binToNum(){
	int number = 0;
	char binary[80];
	int mult = 1;

	cin >> binary;
	// по порядку проход по битам
	for (int i = strlen(binary); i; i--, mult *= 2)
	{
		// каждый разряд бита умножаеться на 2 и у нас получается число 
		// так как бит может иметь только 2 значения
		if (binary[i - 1] == '1') number += mult;
	//проверка на ошибку
		else if (binary[i - 1] != '0')
		{
			cout << "error" << endl;
			exit(1);
		}
	}
	cout << "\n\nЧисло " << binary << " ->  " << number;
}

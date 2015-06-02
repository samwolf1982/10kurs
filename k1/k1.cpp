// k1.cpp: ���������� ����� ����� ��� ����������� ����������.
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
	// ��������� ������
	setlocale(LC_ALL, "Rus");
	int result = 0; // ��������� �� �����

	do{
		result = 0;
		int tempValue = 0; // ���� �� ����������

		cout << "����� �������� �� ������� ������?\n";
		cout << "���������� � �������� [1] ��� �������� � ���������� [2] 0 ����� \n�������� �����:";
		cin >> tempValue;  //����

		if (tempValue != 1 && tempValue != 2) result = 0; // 10 to 2
		else
		{
			if (tempValue == 1){
				int val;
				cout << "������� ����� � ���������� �������: ";
				cin >> val;
				cout << "\n\n����� " << val << " ->  ";
				displayBits(val);
				cout << "\n\n";
			}
			if (tempValue == 2){  // 2 to 10
				string val;
				cout << "������� ����� � ��������  �������: ";

				binToNum();
				cout << "\n\n";
			}
			result = 1;
		}




	} while (result != 0);

	cout << "����������\n";







	cout << "\n";
	return 0;
}
void displayBits(int value)
{
	int size = sizeof(int) * 8;// ������ �����
	unsigned c, displayMask = 1 << size - 1;

	cout << "value = ";

	for (c = 1; c <= size; c++)
	{
		// ��������� : 
	  //  ������� "&" ���������� ��������������� ������ �� �������� ��� ��������.��� ������� ������� ��������� ����� 1,
	   //��� � ������ �����, ����� ��� ��������������� ������� ��������� ����� 1. ���, ��������,
      //0010011 & 00111101 = 00010001
		cout << (value & displayMask ? '1' : '0');
		// c���� � �������� ��������
		value <<= 1;
		 // ���� 8 ����� ������
		if (c % 8 == 0)
			cout << " ";
	}

	cout << "\n";
}
/*
/   ������� �������� � �����
*/
void binToNum(){
	int number = 0;
	char binary[80];
	int mult = 1;

	cin >> binary;
	// �� ������� ������ �� �����
	for (int i = strlen(binary); i; i--, mult *= 2)
	{
		// ������ ������ ���� ����������� �� 2 � � ��� ���������� ����� 
		// ��� ��� ��� ����� ����� ������ 2 ��������
		if (binary[i - 1] == '1') number += mult;
	//�������� �� ������
		else if (binary[i - 1] != '0')
		{
			cout << "error" << endl;
			exit(1);
		}
	}
	cout << "\n\n����� " << binary << " ->  " << number;
}

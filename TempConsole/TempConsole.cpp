// TempConsole.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include <bitset>
#include <iostream>
#include <iomanip>
using namespace std;
int _tmain(int argc, _TCHAR* argv[])
{
	bitset<8> f8;
	bitset<7> f7;
	
	cout << sizeof(f8) << "\n" << sizeof(f7) << "\n";
	return 0;
}


// K2.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include <string>
#include <iostream>
#include <iomanip>
#include <cstdio>
#include <iostream>
#include "stdafx.h"
#include <fstream>
#include <windows.h>
using namespace std;
struct BITS7 {
	unsigned b0 : 1;
	unsigned b1 : 1;
	unsigned b2 : 1;
	unsigned b3 : 1;
	unsigned b4 : 1;
	unsigned b5 : 1;
	unsigned b6 : 1;
};
struct BITS {
	unsigned b0 : 1;
	unsigned b1 : 1;
	unsigned b2 : 1;
	unsigned b3 : 1;
	unsigned b4 : 1;
	unsigned b5 : 1;
	unsigned b6 : 1;
	unsigned b7 : 1;
};
void state(char c);
void openfile();
void code();
void decode();
long filesize(char * path);
void createfile(string path);


//Ошибка	1	error C4996 : 'fopen' : This function or variable may be unsafe.Consider using fopen_s instead.To disable deprecation,
// use _CRT_SECURE_NO_WARNINGS.See online help for details....
// Решение  перейти Проект - свойства - свойсва конфигур -  с\с++ - препроцесор - (справа в окоше определение препроцесора) изменить и добавить  _CRT_SECURE_NO_WARNINGS

int _tmain(int argc, _TCHAR* argv[])
{
	
	setlocale(LC_ALL, "Rus");


	cout << "Size:" << filesize("file.txt") << "\n";
	// create file buff
	    createfile("buff.txt");
	code();
	system("pause");

	return 0;
}

void code(){
	char * path = "file.txt";

	long siz = filesize(path);
	long fullsize = siz / 7;
	long lastbite = siz % 7;
	bool readyread = false;

	//char ch;
	BITS arrBit8[7];
	BITS7 arrBit7[8];

	cout << "s=" << siz / 8 << "   last:" << lastbite << "\n";

	int counter = 1;

#pragma region Fuul loops



	/// fill byfer  7*8
	
		FILE * fp = fopen(path, "rb");
		size_t j = 0;
		

		FILE * fpBufer = fopen("buff.txt", "wb");// создание и очистка буфера
		fclose(fpBufer);
		
			while (fread(&arrBit8[j++], 1, 1, fp) != 0)
			{



				if (j>6){
					j = 0;
#pragma region write to buff
					cout << "------------------исходные даные оригинал ------------------------\n";
					//  testr resd 7*8
					/*for (size_t ia = 0; ia < fullsize; ia++)
					{*/
						for (size_t ja = 0; ja < 7; ja++)
						{
							cout << arrBit8[ja].b0 << arrBit8[ja].b1 << arrBit8[ja].b2 << arrBit8[ja].b3 << arrBit8[ja].b4 << arrBit8[ja].b5 << arrBit8[ja].b6 << arrBit8[ja].b7 << "\n";
						}
						//---------------------------------------
						// конвертация
						for (size_t j = 0; j < 7; j++)
						{

							arrBit7[j].b0 = arrBit8[j].b0;
							arrBit7[j].b1 = arrBit8[j].b1;
							arrBit7[j].b2 = arrBit8[j].b2;
							arrBit7[j].b3 = arrBit8[j].b3;
							arrBit7[j].b4 = arrBit8[j].b4;
							arrBit7[j].b5 = arrBit8[j].b5;
							arrBit7[j].b6 = arrBit8[j].b6;
							//	cout << arrBit7[j].b0 << arrBit7[j].b1 << arrBit7[j].b2 << arrBit7[j].b3 << arrBit7[j].b4 << arrBit7[j].b5 << arrBit7[j].b6  << "\n";
						}

						arrBit7[7].b0 = arrBit8[0].b7;
						arrBit7[7].b1 = arrBit8[1].b7;
						arrBit7[7].b2 = arrBit8[2].b7;
						arrBit7[7].b3 = arrBit8[3].b7;
						arrBit7[7].b4 = arrBit8[4].b7;
						arrBit7[7].b5 = arrBit8[5].b7;
						arrBit7[7].b6 = arrBit8[6].b7;
						cout << "------------------после конвертации 8 - 7 ------------------------\n";
						for (size_t j5 = 0; j5 < 8; j5++)
						{
							cout << arrBit7[j5].b0 << arrBit7[j5].b1 << arrBit7[j5].b2 << arrBit7[j5].b3 << arrBit7[j5].b4 << arrBit7[j5].b5 << arrBit7[j5].b6 << "\n";
						}

						cout << "Запись в файл buffer" << "\n";

						FILE * fpBufer = fopen("buff.txt", "ab");
						
						if (fpBufer == NULL){ ofstream outfile("buff.txt"); outfile.close(); fpBufer = fopen("buff.txt", "ab"); }
						//fseek(fpBufer, 0, SEEK_END);
						for (size_t j3 = 0; j3 < 8; j3++)
						{
							fwrite(&arrBit7[j3], 1, 1, fpBufer);
						}
						fclose(fpBufer);
#pragma region Claer
						// clear  arrbit7,8;
						for (size_t i1 = 0; i1 < 8; i1++)
						{
							arrBit7[i1].b0 = 0;
							arrBit7[i1].b1 = 0;
							arrBit7[i1].b2 = 0;
							arrBit7[i1].b3 = 0;
							arrBit7[i1].b4 = 0;
							arrBit7[i1].b5 = 0;
							arrBit7[i1].b6 = 0;
						}
						for (size_t i2 = 0; i2 < 7; i2++)
						{
							arrBit8[i2].b0 = 0;
							arrBit8[i2].b1 = 0;
							arrBit8[i2].b2 = 0;
							arrBit8[i2].b3 = 0;
							arrBit8[i2].b4 = 0;
							arrBit8[i2].b5 = 0;
							arrBit8[i2].b6 = 0;
							arrBit8[i2].b7 = 0;
						}
#pragma endregion
			


#pragma endregion
				
	                  
		   	 }
		
		}

			fclose(fp);


#pragma region LatsBytes

			if (j > 0){
				                    // ok
				for (size_t i = 0; i < j-1; i++)
				{
					// конвертация
					

						arrBit7[i].b0 = arrBit8[i].b0;
						arrBit7[i].b1 = arrBit8[i].b1;
						arrBit7[i].b2 = arrBit8[i].b2;
						arrBit7[i].b3 = arrBit8[i].b3;
						arrBit7[i].b4 = arrBit8[i].b4;
						arrBit7[i].b5 = arrBit8[i].b5;
						arrBit7[i].b6 = arrBit8[i].b6;
						//	cout << arrBit7[j].b0 << arrBit7[j].b1 << arrBit7[j].b2 << arrBit7[j].b3 << arrBit7[j].b4 << arrBit7[j].b5 << arrBit7[j].b6  << "\n";
					
				}

		
				  
				arrBit7[j-1].b0 = arrBit8[0].b7;
				arrBit7[j-1].b1 = arrBit8[1].b7;
				arrBit7[j-1].b2 = arrBit8[2].b7;
				arrBit7[j-1].b3 = arrBit8[3].b7;
				arrBit7[j-1].b4 = arrBit8[4].b7;
				arrBit7[j-1].b5 = arrBit8[5].b7;
				arrBit7[j-1].b6 = arrBit8[6].b7;
                FILE * fpBufer = fopen("buff.txt", "ab");
				//if (fpBufer == NULL){ ofstream outfile("buff.txt"); outfile.close(); fpBufer = fopen("buff.txt", "ab"); }
				// write to buff
				for (size_t i = 0; i < j-1; i++)
				{
					for (size_t j3 = 0; j3 < j; j3++)
					{
						fwrite(&arrBit7[j3], 1, 1, fpBufer);
					}
					fclose(fpBufer);
				}

			}

#pragma region Claer
			// clear  arrbit7,8;
			for (size_t i1 = 0; i1 < 8; i1++)
			{
				arrBit7[i1].b0 = 0;
				arrBit7[i1].b1 = 0;
				arrBit7[i1].b2 = 0;
				arrBit7[i1].b3 = 0;
				arrBit7[i1].b4 = 0;
				arrBit7[i1].b5 = 0;
				arrBit7[i1].b6 = 0;
			}
			for (size_t i2 = 0; i2 < 7; i2++)
			{
				arrBit8[i2].b0 = 0;
				arrBit8[i2].b1 = 0;
				arrBit8[i2].b2 = 0;
				arrBit8[i2].b3 = 0;
				arrBit8[i2].b4 = 0;
				arrBit8[i2].b5 = 0;
				arrBit8[i2].b6 = 0;
				arrBit8[i2].b7 = 0;
			}
#pragma endregion


#pragma endregion
			 //       write is ok
			cout << "Запись успешная прочитано"<< filesize(path) <<"b.  записано "<< filesize("buff.txt") <<" b. \n";


#pragma region Read loop


			while (fread(&arrBit8[j++], 1, 1, fp) != 0)
			{



			}
#pragma endregion



}

void decode(){

	char * path = "buff.txt";

	long siz = filesize(path);
	long fullsize = siz / 7;
	long lastbite = siz % 7;
	bool readyread = false;

	//char ch;
	BITS arrBit8[7];
	BITS7 arrBit7[8];
	cout << "Считывание из буфера\n";
	FILE * fpBufer;
	size_t j = 0;
	fpBufer = fopen("buff.txt", "rb");

	for (size_t j1 = 0; j1 <8; j1++)
	{
		fread(&arrBit7[j1], 1, 1, fpBufer);
	}
	fclose(fpBufer);
	cout << "------------------Обратная конверация ------------------------\n";


	for (size_t jw = 0; jw < 7; jw++)
	{

		arrBit8[jw].b0 = arrBit7[jw].b0;
		arrBit8[jw].b1 = arrBit7[jw].b1;
		arrBit8[jw].b2 = arrBit7[jw].b2;
		arrBit8[jw].b3 = arrBit7[jw].b3;
		arrBit8[jw].b4 = arrBit7[jw].b4;
		arrBit8[jw].b5 = arrBit7[jw].b5;
		arrBit8[jw].b6 = arrBit7[jw].b6;
		//cout << arrBit8[j].b0 << arrBit8[j].b1 << arrBit8[j].b2 << arrBit8[j].b3 << arrBit8[j].b4 << arrBit8[j].b5 << arrBit7[j].b6 << "\n";
	}


	arrBit8[0].b7 = arrBit7[7].b0;
	arrBit8[1].b7 = arrBit7[7].b1;
	arrBit8[2].b7 = arrBit7[7].b2;
	arrBit8[3].b7 = arrBit7[7].b3;
	arrBit8[4].b7 = arrBit7[7].b4;
	arrBit8[5].b7 = arrBit7[7].b5;
	arrBit8[6].b7 = arrBit7[7].b6;



	cout << "------------------после конвертации 7 - 8 ------------------------\n";

	for (size_t j = 0; j < 7; j++)
	{
		cout << arrBit8[j].b0 << arrBit8[j].b1 << arrBit8[j].b2 << arrBit8[j].b3 << arrBit8[j].b4 << arrBit8[j].b5 << arrBit8[j].b6 << arrBit8[j].b7 << "\n";
	}




	cout << "------------------------------------------\n";

#pragma endregion

#pragma region LastLoops
	//FILE * fp = fopen(path, "rb");
	//size_t j = 0;
	//do{

	//	if (fread(&arrBit8[j], 1, 1, fp) == 0){
	//	}
	//	j++;
	//} while (j < lastbite);
	//fclose(fp);



#pragma endregion

}

	long filesize(char * path){
		FILE * ptrFile = fopen(path, "rb");	  
		rand();
	
		long size;
		if (ptrFile == NULL){ cout << "файл не существует: " << path << "  будет создан и заполнен случайними значениями";
	            
		
		ofstream outF(path);
		if (!outF.bad())
		outF << "wedwwert";
		outF.close();
		}

			fseek(ptrFile, 0, SEEK_END);                                      // переместить внутренний указатель в конец файла
			size = ftell(ptrFile);                                       // вернуть текущее положение внутреннего указателя
			fclose(ptrFile);                                                 // закрыть файл


		return size;
	}

	void openfile(){
		FILE * ptrFile = fopen("file.txt", "rb");

		if (ptrFile == NULL) perror("Ошибка открытия файла");
		else
		{
			fseek(ptrFile, 0, SEEK_END);                                      // переместить внутренний указатель в конец файла
			long size = ftell(ptrFile);                                       // вернуть текущее положение внутреннего указателя
			fclose(ptrFile);                                                 // закрыть файл

			cout << "Размер файла file.txt: " << size << " байт\n";
		}
		int s;
		cin >> s;
	}

	void createfile(string path){
		ofstream outF(path);
		if (!outF.bad())
		outF.close();
	
	}

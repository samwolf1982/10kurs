// K2.cpp: ���������� ����� ����� ��� ����������� ����������.
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
// c�������� ����������� 7 ������ ���
struct BITS7 {
	unsigned b0 : 1;
	unsigned b1 : 1;
	unsigned b2 : 1;
	unsigned b3 : 1;
	unsigned b4 : 1;
	unsigned b5 : 1;
	unsigned b6 : 1;
};
// c�������� ����������� 8 ������ ���
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

void openfile();
void code(char * pathtofile);
void decode();
long filesize(char * path);
void createfile(string path);


//������	1	error C4996 : 'fopen' : This function or variable may be unsafe.Consider using fopen_s instead.To disable deprecation,
// use _CRT_SECURE_NO_WARNINGS.See online help for details....
// �������  ������� ������ - �������� - ������� �������� -  �\�++ - ����������� - (������ � ����� ����������� ������������) �������� � ��������  _CRT_SECURE_NO_WARNINGS

int _tmain(int argc, _TCHAR* argv[])
{
	// ������
	setlocale(LC_ALL, "Rus");


	cout << "Size:" << filesize("file.txt") << "\n";
	// create file buff
	    createfile("buff.txt");
	// ����������� �� 8 ������ � 7 ������
		code("file.txt");
		// ������������� �� 8 ������ � 7 ������
	decode();


	return 0;
}

void code(char * pathtofile){
	char * path = pathtofile;
	char * bufer = "buff.txt";
	long siz = filesize(path);
	long fullsize = siz / 7;
	long lastbite = siz % 7;
	char *c = "fddf";
	BITS arrBit8[7];
	BITS7 arrBit7[8];



#pragma region Fuul loops


	
		FILE * fp = fopen(path, "rb");
	

	
		size_t j = 0;
		FILE * fpBufer = fopen(bufer, "wb");// �������� � ������� ������
		fclose(fpBufer);

		// ���������� ������ ����� �� �����
			while (fread(&arrBit8[j++], 1, 1, fp) != 0)
			{
				// ���� ������� 7 ����� ���� � ��������
				if (j>6){
					j = 0;
#pragma region write to buff
					cout << "------------------�������� ����� �������� ------------------------\n";

						for (size_t ja = 0; ja < 7; ja++)
						{
							cout << arrBit8[ja].b0 << arrBit8[ja].b1 << arrBit8[ja].b2 << arrBit8[ja].b3 << arrBit8[ja].b4 << arrBit8[ja].b5 << arrBit8[ja].b6 << arrBit8[ja].b7 << "\n";
						}
						//---------------------------------------
						// �����������
						for (size_t j = 0; j < 7; j++)
						{

							arrBit7[j].b0 = arrBit8[j].b0;
							arrBit7[j].b1 = arrBit8[j].b1;
							arrBit7[j].b2 = arrBit8[j].b2;
							arrBit7[j].b3 = arrBit8[j].b3;
							arrBit7[j].b4 = arrBit8[j].b4;
							arrBit7[j].b5 = arrBit8[j].b5;
							arrBit7[j].b6 = arrBit8[j].b6;
							
						}

						arrBit7[7].b0 = arrBit8[0].b7;
						arrBit7[7].b1 = arrBit8[1].b7;
						arrBit7[7].b2 = arrBit8[2].b7;
						arrBit7[7].b3 = arrBit8[3].b7;
						arrBit7[7].b4 = arrBit8[4].b7;
						arrBit7[7].b5 = arrBit8[5].b7;
						arrBit7[7].b6 = arrBit8[6].b7;

						cout << "------------------����� ����������� 8 - 7 ------------------------\n";
						// show
						for (size_t j5 = 0; j5 < 8; j5++)
						{
							cout << arrBit7[j5].b0 << arrBit7[j5].b1 << arrBit7[j5].b2 << arrBit7[j5].b3 << arrBit7[j5].b4 << arrBit7[j5].b5 << arrBit7[j5].b6 << "\n";
						}

						cout << "������ � ���� buffer" << "\n";

						FILE * fpBufer = fopen(bufer, "ab");
						
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
			         //      �� ����� ��� �������� (�� ������� 7)
			if (j > 0){
				                // ���� ��� � ���� �����������
				for (size_t i = 0; i < j-1; i++)
				{
					// �����������

						arrBit7[i].b0 = arrBit8[i].b0;
						arrBit7[i].b1 = arrBit8[i].b1;
						arrBit7[i].b2 = arrBit8[i].b2;
						arrBit7[i].b3 = arrBit8[i].b3;
						arrBit7[i].b4 = arrBit8[i].b4;
						arrBit7[i].b5 = arrBit8[i].b5;
						arrBit7[i].b6 = arrBit8[i].b6;
						
					
				}

		
				  
				arrBit7[j-1].b0 = arrBit8[0].b7;
				arrBit7[j-1].b1 = arrBit8[1].b7;
				arrBit7[j-1].b2 = arrBit8[2].b7;
				arrBit7[j-1].b3 = arrBit8[3].b7;
				arrBit7[j-1].b4 = arrBit8[4].b7;
				arrBit7[j-1].b5 = arrBit8[5].b7;
				arrBit7[j-1].b6 = arrBit8[6].b7;
				// �������� 
                FILE * fpBufer = fopen(bufer, "ab");
	
				for (size_t j3 = 0; j3 < j; j3++)
					{
						fwrite(&arrBit7[j3], 1, 1, fpBufer);
					}
					fclose(fpBufer);
				

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
			 //       write is ok  ���
			cout << "������ �������� ���������"<< filesize(path) <<"b.  �������� "<< filesize("buff.txt") <<" b. \n";


#pragma region Read loop
#pragma endregion
}



void decode(){
	createfile("result.txt");
	char * path = "buff.txt";
	char * pathResult = "result.txt";

	long siz = filesize(path);
	long fullsize = siz / 7;
	long lastbite = siz % 7;

	//char ch;
	BITS arrBit8[7];
	BITS7 arrBit7[8];
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
	cout << "���������� �� ������\n";
	FILE * fpBufer;
	size_t j = 0;
	fpBufer = fopen(path, "rb");

// read 8*n and convert to 7*n
	while (fread(&arrBit7[j++], 1, 1, fpBufer) != 0)
	{
		if (j > 7){

			for (size_t jw = 0; jw < 7; jw++)
			{

				arrBit8[jw].b0 = arrBit7[jw].b0;
				arrBit8[jw].b1 = arrBit7[jw].b1;
				arrBit8[jw].b2 = arrBit7[jw].b2;
				arrBit8[jw].b3 = arrBit7[jw].b3;
				arrBit8[jw].b4 = arrBit7[jw].b4;
				arrBit8[jw].b5 = arrBit7[jw].b5;
				arrBit8[jw].b6 = arrBit7[jw].b6;
		
			}


			arrBit8[0].b7 = arrBit7[7].b0;
			arrBit8[1].b7 = arrBit7[7].b1;
			arrBit8[2].b7 = arrBit7[7].b2;
			arrBit8[3].b7 = arrBit7[7].b3;
			arrBit8[4].b7 = arrBit7[7].b4;
			arrBit8[5].b7 = arrBit7[7].b5;
			arrBit8[6].b7 = arrBit7[7].b6;
			// read to new file like result
			
			cout << "-DECODE-----------------����� �����������  7 - 8 ------------------------\n";
			// show
			for (size_t j = 0; j < 7; j++)
			{
				cout << arrBit8[j].b0 << arrBit8[j].b1 << arrBit8[j].b2 << arrBit8[j].b3 << arrBit8[j].b4 << arrBit8[j].b5 << arrBit8[j].b6 << arrBit8[j].b7 << "\n";
			}
			cout << "������ � ���� result" << "\n";
			FILE * fp = fopen(pathResult, "ab");
			for (size_t j3 = 0; j3 < 7; j3++)
			{
				fwrite(&arrBit8[j3], 1, 1, fp);
			}
			

			fclose(fp);
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

			j = 0;
		}
	}


	fclose(fpBufer);
#pragma region Last bites
	if (j > 0){
		// ok
		for (size_t i = 0; i < j ; i++)
		{
			// �����������


			arrBit8[i].b0 = arrBit7[i].b0;
			arrBit8[i].b1 = arrBit7[i].b1;
			arrBit8[i].b2 = arrBit7[i].b2;
			arrBit8[i].b3 = arrBit7[i].b3;
			arrBit8[i].b4 = arrBit7[i].b4;
			arrBit8[i].b5 = arrBit7[i].b5;
			arrBit8[i].b6 = arrBit7[i].b6;
			}


		arrBit8[0].b7 = arrBit7[j-1].b0;
		arrBit8[1].b7 = arrBit7[j - 1].b1;
		arrBit8[2].b7 = arrBit7[j - 1].b2;
		arrBit8[3].b7 = arrBit7[j - 1].b3;
		arrBit8[4].b7 = arrBit7[j - 1].b4;
		arrBit8[5].b7 = arrBit7[j - 1].b5;
		arrBit8[6].b7 = arrBit7[j - 1].b6;

		FILE * fpB = fopen("result.txt", "ab");

		for (size_t i = 0; i < j ; i++)
		{
			for (size_t j3 = 0; j3 < j; j3++)
			{
				fwrite(&arrBit8[j3], 1, 1, fpB);
			}
			fclose(fpB);
		}

	}
#pragma endregion





	cout << "------------------------------------------\n";

#pragma endregion


}

	long filesize(char * path){
		FILE * ptrFile = fopen(path, "rb");	  
		rand();
	
		long size;
		if (ptrFile == NULL){ cout << "���� �� ����������: " << path << "  ����� ������ � �������� ���������� ����������";
	            
		
		ofstream outF(path);
		if (!outF.bad())
		outF << "wedwwert";
		outF.close();
		}

			fseek(ptrFile, 0, SEEK_END);                                      // ����������� ���������� ��������� � ����� �����
			size = ftell(ptrFile);                                       // ������� ������� ��������� ����������� ���������
			fclose(ptrFile);                                                 // ������� ����


		return size;
	}

	void openfile(){
		FILE * ptrFile = fopen("file.txt", "rb");

		if (ptrFile == NULL) perror("������ �������� �����");
		else
		{
			fseek(ptrFile, 0, SEEK_END);                                      // ����������� ���������� ��������� � ����� �����
			long size = ftell(ptrFile);                                       // ������� ������� ��������� ����������� ���������
			fclose(ptrFile);                                                 // ������� ����

			cout << "������ ����� file.txt: " << size << " ����\n";
		}
		int s;
		cin >> s;
	}

	void createfile(string path){
		ofstream outF(path);
		if (!outF.bad())
		outF.close();
	
	}

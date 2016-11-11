#include "stdafx.h"
#include "tools.h"

extern char *splitstr;

// ��cppʹ�õ�һЩ��������

// �ַ������
int split(char *instr, vector<string> *v)
{
	vector<string>::iterator vpos;

	v->clear(); 

	char *pNext = NULL;
	vpos = v->end();
	char *temp = strtok_s(instr, splitstr, &pNext);
	while (temp != NULL)
	{
		vpos = v->insert(v->end(), (string)temp);
		temp = strtok_s(NULL, splitstr, &pNext);
	}
	return 0;
}

// long longת�ַ���
string ltos(long long lInput)
{
	char temp[tempstrLen];
	sprintf_s(temp, tempstrLen, "%lld", lInput);
	return (string)temp;
}

// charת�ַ���
string ctos(char c)
{
	char temp[2];
	temp[0] = c;
	temp[1] = 0;
	return (string)temp;
}

// double ת long long
long long dtol(double input)
{
	long long result = 0;
	if (input>0)
		result = (long long)(input*100+0.5);
	else
		result = (long long)(input*100-0.5);
	return result;
}

// doubleת�ַ���
string dtos(double dInput)
{
	if (dInput>DOUBLEMAX || dInput<DOUBLEMIN)
		return "0";
	char temp[tempstrLen];
	sprintf_s(temp, tempstrLen, "%.2f", dInput);
	return (string)temp;
}

// ��ȡʱ��
string gettime()
{
	char temp[20];

	time_t t = time(NULL);
	struct tm tmTemp;
	localtime_s(&tmTemp, &t);
	sprintf_s(temp, sizeof(temp), "%04d-%02d-%02d %2d:%02d:%02d", 
		tmTemp.tm_year + 1900, tmTemp.tm_mon + 1, tmTemp.tm_mday, 
		tmTemp.tm_hour, tmTemp.tm_min, tmTemp.tm_sec
		);
	return (string)temp;
}




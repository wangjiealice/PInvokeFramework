// Win32ProjectDll.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include<iostream>
using namespace std;

enum CZCom_MessageType
{
	CZCom_MessageType_CAN11 = 1,
	CZCom_MessageType_CAN29 = 2,
	CZCom_MessageType_String = 4,
	CZCom_MessageType_Palm = 8,
	CZCom_MessageType_HID = 16,
	CZCom_MessageType_Raw = 32,
	CZCom_MessageType_MAC_LOW = 512,
	CZCom_MessageType_MAC_HIGH = 513,
	CZCom_MessageType_MAC_REQ = 514,
	CZCom_MessageType_MAC_6000 = 1024
};


extern "C" __declspec(dllexport) int Add(int x, int y)
{
	return x + y;
}
extern "C" __declspec(dllexport) int Sub(int x, int y)
{
	return x - y;
}
extern "C" __declspec(dllexport) int Multiply(int x, int y)
{
	return x * y;
}
extern "C" __declspec(dllexport) int Divide(int x, int y)
{
	return x / y;
}

extern "C" __declspec(dllexport) void SetValue(int x, int y)
{
	cout <<"x is"<< x <<"y is" << y << endl;
}

//使用 PInvoke 封送函数指针
typedef int(__stdcall * FunctionPointerType1)(int i);

extern "C" __declspec(dllexport) int SetFunctionPointer(FunctionPointerType1 pointer)
{
	return pointer(1);
}

typedef int(__stdcall * FunctionPointerType2)(char* i);

extern "C" __declspec(dllexport) int SetFunctionPointerString(FunctionPointerType2 pointer)
{
	return pointer("succeed");
}

//使用 PInvoke 封送嵌入式指针
struct ListStruct {
	int count;
	double* item;
};

extern "C"  __declspec(dllexport) void TakesListStruct(ListStruct list)
{
	printf_s("[unmanaged] count = %d\n", list.count);
	for (int i = 0; i < list.count; i++)
	{
		//list.item[i] = 666;
		printf_s("array[%d] = %f\n", i, list.item[i]);
	}
}

//使用 PInvoke 封送数组
extern "C" __declspec(dllexport) void TakesAnArray(int len, int a[])
{
	printf_s("[ChangeAllData to 666 in unmanaged]\n");
	for (int i = 0; i < len; i++)
	{
		a[i] = 666;
		printf("%d = %d\n", i, a[i]);
	}

}

//使用 PInvoke 封送数组，a值过大，比如156
extern "C" __declspec(dllexport) void UnsignedArray(int len,unsigned char a[])
{
	printf_s("UnsignedArray called\n");
	a[0] = 49;
	a[1] = 46;
	a[2] = 49;
	a[3] = 46;
	a[4] = 49;
	a[5] = 46;
	a[6] = 49;


	//for (int i = 0; i < len; i++)
	//{
	//	a[i] = 49;
	//	printf("Change %d to %d\n", i, a[i]);
	//}
}

//使用 PInvoke 封送数组，使用memcpy
extern "C" __declspec(dllexport) void MemcpyArray(int len, unsigned char a[])
{
	unsigned char version[] = "1.1.1.1";
	if (len > 12)
	{
		len = 12;
	}
	memcpy(a, version, len);
}

#pragma region 测试NewCanServer接口和event,包含single event和multiple event,需要手动调一下CallTimer()
//Support string vData
typedef void(__stdcall* _IPortEvents_ReceivedCANMessageEventHandler)(CZCom_MessageType MsgType, int DestAddr, int
	SourceAddr, int CmdClass, int CmdNbr, int SubNr, int sPID, char* vData);

//Support char[] vData
typedef void(__stdcall* _IPortEvents_ReceivedCANMessageEventHandlerArray)(CZCom_MessageType MsgType, int DestAddr, int
	SourceAddr, int CmdClass, int CmdNbr, int SubNr, int sPID, char* vData, int vDataArray);

//Support char[] vData, and multiple event
typedef void(__stdcall* _IPortEvents_ReceivedCANMessageEventHandler_[5])(CZCom_MessageType MsgType, int DestAddr, int
	SourceAddr, int CmdClass, int CmdNbr, int SubNr, int sPID, char* vData, int vDataArray);

_IPortEvents_ReceivedCANMessageEventHandler _fun;
_IPortEvents_ReceivedCANMessageEventHandlerArray _funArray;
_IPortEvents_ReceivedCANMessageEventHandler_ Myfun = { 0 };


int i = 0;


extern "C" __declspec(dllexport) void setFire_ReceivedCANMessage_CB(_IPortEvents_ReceivedCANMessageEventHandler fun)
{
	printf_s("[C]setFire_ReceivedCANMessage_CB called\n");
	_fun = fun;
}

//Multi Event
extern "C" __declspec(dllexport) void setFire_ReceivedCANMessage_MulEvent(_IPortEvents_ReceivedCANMessageEventHandlerArray fun)
{
	printf_s("[C]setFire_ReceivedCANMessage_MulEvent called\n");
	int i = 0;
	for (i = 0; i < 5; i++)
	{
		if (Myfun[i] == 0)
		{
			printf(" %s, %d\n", __FUNCTION__, __LINE__);
			Myfun[i] = fun;
			break;
		}
	}
}

//For char[]vData, single event
extern "C" __declspec(dllexport) void setFire_ReceivedCANMessage_Array(_IPortEvents_ReceivedCANMessageEventHandlerArray fun)
{
	printf_s("[C]setFire_ReceivedCANMessage_Array called\n");
	_funArray = fun;
}

//For string, single event
extern "C" __declspec(dllexport) _IPortEvents_ReceivedCANMessageEventHandler GetReceivedCANMessageEventHandler()
{
	printf_s("[C]GetReceivedCANMessageEventHandler called\n");
	return _fun;
}

//Single Event
VOID   CALLBACK   TimerProc(HWND   hwnd, UINT   uMsg, UINT   idEvent, DWORD   dwTime);
VOID   CALLBACK   TimerProc(HWND   hwnd, UINT   uMsg, UINT   idEvent, DWORD   dwTime)
{
	if (_funArray != nullptr)
	{
		++i;
		printf_s("[C]_funArray called\n");
		char str[3] = { 0 };
		str[0] = 0;
		str[1] = 0;
		str[2] = 6;
		_funArray(CZCom_MessageType_CAN29, i, i, i, i, i, i, str, 3);
	}
	else
	{
		printf_s("[C]_fun is nullptr\n");
	}


	if (_fun != nullptr)
	{
		++i;
		printf_s("[C]_fun called\n");
		char str[3] = { 0 };
		str[0] = 0;
		str[1] = 0;
		str[2] = 6;

		_fun(CZCom_MessageType_CAN29, i, i, i, i, i, i, str);
		_funArray(CZCom_MessageType_CAN29, i, i, i, i, i, i, str, 2);
	}
	else
	{
		printf_s("[C]_fun is nullptr\n");
	}
}


//Multiple Events
VOID   CALLBACK   TimerProcMulEvent(HWND   hwnd, UINT   uMsg, UINT   idEvent, DWORD   dwTime);
VOID   CALLBACK   TimerProcMulEvent(HWND   hwnd, UINT   uMsg, UINT   idEvent, DWORD   dwTime)
{
	++i;
	printf_s("[C]_fun called\n");
	char str[3] = { 0 };
	str[0] = 0;
	str[1] = 0;
	str[2] = 6;

	int j = 0;
	for (j = 0; j < 5; ++j)
	{
		if (Myfun[j] != 0)
		{
			printf("[C] %s, %d\n", __FUNCTION__, __LINE__);
			Myfun[j](CZCom_MessageType_CAN29, i, i, i, i, i, i, str, 3);
		}
	}
}


extern "C" __declspec(dllexport) void CallTimer()
{
	int   timer1 = 1;
	MSG   msg;

	SetTimer(NULL, timer1, 5000, TimerProcMulEvent);
	int   itemp;
	while ((itemp = GetMessage(&msg, NULL, NULL, NULL)) && (itemp != 0) && (-1 != itemp))
	{
		if (msg.message == WM_TIMER)
		{
			//std::cout   <<   "[C]i   got   the   message "   <<   std::endl;
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
	}
}

#pragma endregion





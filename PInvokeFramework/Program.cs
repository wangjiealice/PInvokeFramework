using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;


namespace PInvokeFramework
{
    public struct MListStruct
    {
        public int count;
        public IntPtr item;
    }

    public struct MTB_DATA
    {
        public IntPtr buff;
        public IntPtr count;
    }

    public struct SimpleStruct
    {
        public int intA;
        public double B;
    }

    public struct ShortStruct
    {
        public IntPtr value;
    }

    public enum CZCom_MessageType
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
    }

    public delegate int FunctionPointerType1(int data);

    public delegate int FunctionPointerType2(String data);

    public delegate void _IPortEvents_ReceivedCANMessageEventHandler(CZCom_MessageType MsgType, short DestAddr, 
        short SourceAddr, short CmdClass, short CmdNbr, short SubNr, short sPID, string vData);

    public delegate void _IPortEvents_ReceivedCANMessageEventHandlerArray(CZCom_MessageType MsgType, short DestAddr,
    short SourceAddr, short CmdClass, short CmdNbr, short SubNr, short sPID, IntPtr vData, int vDataArray);

    class Program
    {
        [DllImport("CPlusDLL.dll")]
        public static extern int CalculateSub(int x, int y);

        [DllImport("CPlusDLL.dll")]
        public static extern int CalculateAdd(int a, int b);

        
        [DllImport("Win32ProjectDll.dll")]
        public static extern int Divide(int x, int y);

        [DllImport("Win32ProjectDll.dll")]
        public static extern int Add(int a, int b);

        [DllImport("Win32ProjectDll.dll")]
        public static extern void TakesListStruct(MListStruct list);

        [DllImport("Win32ProjectDll.dll")]
        public static extern void TakesAnArray(int len, int[] a);

        [DllImport("Win32ProjectDll.dll")]
        public static extern void TakesAString(String word);

        [DllImport("Win32ProjectDll.dll")]
        public static extern void UnsignedArray(int len, byte[] a);

        [DllImport("Win32ProjectDll.dll")]
        public static extern void MemcpyArray(int len, byte[] a);

        [DllImport("Win32ProjectDll.dll")]
        public static extern int SetFunctionPointer(FunctionPointerType1 pointer);

        [DllImport("Win32ProjectDll.dll")]
        public static extern int SetFunctionPointerString(FunctionPointerType2 pointer);

        [DllImport("Win32ProjectDll.dll")]
        public static extern void setFire_ReceivedCANMessage_CB(_IPortEvents_ReceivedCANMessageEventHandler pointer);

        [DllImport("Win32ProjectDll.dll")]
        public static extern void setFire_ReceivedCANMessage_MulEvent(_IPortEvents_ReceivedCANMessageEventHandlerArray pointer);

        [DllImport("Win32ProjectDll.dll")]
        public static extern void SetSimpleStruct(SimpleStruct value);

        [DllImport("Win32ProjectDll.dll")]
        public static extern bool GetUSBDeviceProperties(int Number, ShortStruct pPoductId, MTB_DATA pName, MTB_DATA pSerialNumber);

        private static _IPortEvents_ReceivedCANMessageEventHandlerArray ReceivedCANMessage;

        static void Main(string[] args)
        {
            //#region PInvoke simpleStruct  2019.2.18
            //Console.ReadLine();
            //SimpleStruct structValue;
            //structValue.intA = 1;
            //structValue.B = 1234.5678 ;
            //Console.WriteLine("[managed]Before SetSimpleStruct, A is" + structValue.intA +
            //    "; B is " + structValue.B);
            //SetSimpleStruct(structValue);
            //Console.WriteLine("[managed]After SetSimpleStruct, A is" + structValue.intA +
            //    "; B is " + structValue.B);
            //Console.ReadLine();

            //#endregion

            //#region PInvoke string  2019.2.18
            //TakesAString("Framework called!");
            //Console.ReadLine();

            //#endregion

            //#region test FireAndForget 2018.7.17
            //_IPortEvents_ReceivedCANMessageEventHandler canServerFireTest = new _IPortEvents_ReceivedCANMessageEventHandler(OnCanMessageReceived);
            //FireAndForget(canServerFireTest, CZCom_MessageType.CZCom_MessageType_CAN29, (short)0,
            //              (short)0, (short)0, (short)0, (short)0, (short)0, "hello");
            //Console.ReadLine();
            //#endregion

            //#region 测试多个给CanServer设置多个Event 2018.7.16
            //_IPortEvents_ReceivedCANMessageEventHandlerArray canServer = new _IPortEvents_ReceivedCANMessageEventHandlerArray(OnCanServerChanged);
            //setFire_ReceivedCANMessage_MulEvent(canServer);

            //_IPortEvents_ReceivedCANMessageEventHandlerArray canServer1 = new _IPortEvents_ReceivedCANMessageEventHandlerArray(OnCanServerChanged1);
            //setFire_ReceivedCANMessage_MulEvent(canServer1);

            //_IPortEvents_ReceivedCANMessageEventHandlerArray canServer2 = new _IPortEvents_ReceivedCANMessageEventHandlerArray(OnCanServerChanged2);
            //setFire_ReceivedCANMessage_MulEvent(canServer2);

            //Can29.CallTimer();
            //Console.ReadLine();

            //#endregion

            //#region String和byte[]的转换
            //string str = "";
            //byte[] byteArray = System.Text.Encoding.Default.GetBytes(str);

            //#endregion

            //#region PInvokeFunction ptr
            //Can29 can29Object = new Can29();
            ////_IPortEvents_ReceivedCANMessageEventHandler ReceivedCANMessage = new _IPortEvents_ReceivedCANMessageEventHandler(OnCanMessageReceived);
            ////can29Object.SetFire_ReceivedCANMessage_CB(ReceivedCANMessage);
            //ReceivedCANMessage += new _IPortEvents_ReceivedCANMessageEventHandlerArray(OnCanMessageReceived);
            //can29Object.SetFire_ReceivedCANMessage_Array(ReceivedCANMessage);

            //#endregion

            //#region PInvokeFunction GetEventPtr, Fail
            ////_IPortEvents_ReceivedCANMessageEventHandler ReceivedCANMessageBack = can29Object.GetReceivedCANMessageEventHandlerFromC();
            ////ReceivedCANMessageBack += new _IPortEvents_ReceivedCANMessageEventHandler(OnCanMessageReceivedBack);

            //Can29.CallTimer();
            //Console.ReadLine();
            //#endregion

            //#region 测试byte[]对应到C++的unsigned char

            //byte[] testbyteArray = new byte[12];

            //string byteString = "";
            //foreach (var item in testbyteArray)
            //{
            //    byteString += item;
            //    byteString += " ";
            //}
            //Console.WriteLine("Before PINVOKE byteString : {0}", byteString);

            ////it will print value in C codes
            //UnsignedArray(testbyteArray.Length, testbyteArray);

            //byteString = ByteArrayToStringConverter1(testbyteArray);
            //Console.WriteLine("After PINVOKE byteString : {0}", byteString);

            //Console.ReadLine();
            //#endregion

            #region 测试byte[]对应到C++的unsigned char, 拷贝越界的情况,会只传部分string
            //byte[] array1 = new byte[20];

            //string string1 = "";
            //foreach (var item in array1)
            //{
            //    string1 += item;
            //    string1 += " ";
            //}
            //Console.WriteLine("Before PINVOKE byteString : {0}", array1);

            //MemcpyArray(array1.Length, array1);

            //string1 = ByteArrayToStringConverter1(array1);
            //Console.WriteLine("After PINVOKE byteString : {0}", string1);

            //Console.ReadLine();
            #endregion

            #region 测试多个out参数的情况
            short pPoductId = 0;
            string pName = "unknown";
            string pSerialNumber = "unknown";
            GetUSBDevicePropertiesByPInvoke(6, out pPoductId, out pName, out pSerialNumber);
            Console.WriteLine("pPoductId: " + pPoductId + ";   pName:" + pName + ";   pSerialNumber:" + pSerialNumber);
            #endregion

            #region ptr to int
            //MTB_DATA listdata = new MTB_DATA();
            //InitialMTBDATA(ref listdata);
            //Console.WriteLine("after cunction, count is set to: {0} ", (int)Marshal.PtrToStructure(new IntPtr(listdata.count.ToInt32()), typeof(int)));
            //Console.ReadLine();
            #endregion

            #region char to ASCII
            //char[] testchar = new char[6] { '0', '9', (char)'.', '0', '0', '6' };
            //string displayString = "";
            //foreach (var item in testchar)
            //{
            //    displayString += (int)item;
            //    displayString += "\n";

            //}

            //Console.WriteLine(displayString);
            //Console.ReadLine();
            #endregion

            #region char[] to byte[] 
            //char[] testchar1 = new char[6] { '0', '9', '.', '0', '0', '6' };

            //byte[] result = CharArrayToAsciiConverter(testchar1);

            //string displayString = "";
            //foreach (var item in result)
            //{
            //    displayString += item;
            //    displayString += "\n";

            //}

            //Console.WriteLine(displayString);
            //Console.ReadLine();
            #endregion

            #region byte[] to char[]
            byte[] byteBuf = new byte[3] { 20, 16, 21 };
            char[] testchar1 = ByteArrayToCharArrayConverter(byteBuf);
            string displayString = "";
            foreach (var item in testchar1)
            {
                displayString += item.ToString();
                displayString += "\n";

            }

            Console.WriteLine(displayString);
            Console.ReadLine();
            #endregion

            #region PInvokeSimple
            int a = 1;
            int b = 2;
            int c = Divide(b, a);
            int d = Add(a,b);

            Console.WriteLine(c);
            Console.WriteLine(d);
            #endregion

            #region ptr *double,使用 PInvoke 封送嵌入式指针
            double[] parray = new double[10];
            Console.WriteLine("[managed] count = {0}", parray.Length);

            Random r = new Random();
            for (int i = 0; i < parray.Length; i++)
            {
                parray[i] = r.NextDouble() * 100.0;
                Console.WriteLine("array[{0}] = {1}", i, parray[i]);
            }

            MListStruct list;
            int size = Marshal.SizeOf(typeof(double));

            list.count = parray.Length;
            list.item = Marshal.AllocCoTaskMem(size * parray.Length);

            for (int i = 0; i < parray.Length; i++)
            {
                IntPtr t = new IntPtr(list.item.ToInt32() + i * size);
                Marshal.StructureToPtr(parray[i], t, false);
            }

            TakesListStruct(list);

            double[] anotherParray = new double[10];
            for (int i = 0; i < anotherParray.Length; i++)
            {
                anotherParray[i] = (double)Marshal.PtrToStructure(new IntPtr(list.item.ToInt32() +  i * size), typeof(double));
            }


            Marshal.FreeCoTaskMem(list.item);
            #endregion

            #region ptr *double,使用 PInvoke 封送使用 PInvoke 封送数组
            int[] e = new int[3] { 11,33,55};
            Console.WriteLine("[Before P/Invoke function]");
            for (int i = 0; i < 3; i++)
                Console.WriteLine("{0} = {1}", i, e[i]);

            TakesAnArray(3, e);

            Console.WriteLine("[After P/Invoke function]");
            for (int i = 0; i < 3; i++)
                Console.WriteLine("{0} = {1}", i, e[i]);

            #endregion

            #region 使用 PInvoke 封送函数指针
            //This is OK
            FunctionPointerType1 functionB = new FunctionPointerType1(functionA);
            SetFunctionPointer(functionB);

            FunctionPointerType2 functionC = new FunctionPointerType2(functionCC);
            SetFunctionPointerString(functionC);
            #endregion

            Console.ReadKey();
        }

        #region Test C# 
        public static void FireAndForget(Delegate toInvoke, params object[] args)
        {
            toInvoke.DynamicInvoke(args);
        }
        #endregion
        public static void Advise(object toAdvise)
        {
            if (toAdvise == null) throw new ArgumentNullException("toAdvise");
            if (!(toAdvise is ICan29)) throw new ArgumentOutOfRangeException("toAdvise");

            ((ICan29)toAdvise).ReceivedCANMessage += new _IPortEvents_ReceivedCANMessageEventHandler(OnCanMessageReceived);
        }


        public static void OnCanMessageReceivedBack(CZCom_MessageType MsgType, short DestAddr,
           short SourceAddr, short CmdClass, short CmdNbr,
           short SubNr, short sPID, object vData)
        {
            Console.WriteLine("[Main]OnCanMessageReceivedBack called");
        }

        public static void OnCanMessageReceived(CZCom_MessageType MsgType, short DestAddr,
           short SourceAddr, short CmdClass, short CmdNbr,
           short SubNr, short sPID, object vData)
        {
            byte[] byteArray = System.Text.Encoding.Default.GetBytes((string)vData);
            Console.WriteLine("[Main]OnReceivedCANMessage called and vData is {0}", vData);
        }

        public static void OnCanMessageReceived(CZCom_MessageType MsgType, short DestAddr,
    short SourceAddr, short CmdClass, short CmdNbr,
    short SubNr, short sPID, IntPtr vData,int vDataLength)
        {
            int size = Marshal.SizeOf(typeof(char));

            byte[] anotherParray = new byte[vDataLength];
            for (int i = 0; i < vDataLength; i++)
            {
                anotherParray[i] = (byte)Marshal.PtrToStructure(new IntPtr(vData.ToInt32() + i * size), typeof(byte));
            }




            //Console.WriteLine(vData);
            //byte[] byteArray = (byte[])vData;
            //byte[] byteArrayResult = new byte[vDataLength];

            //try
            //{
            //    Array.Copy(byteArray, byteArrayResult, vDataLength);
            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}

            Console.WriteLine("[Main]OnReceivedCANMessage called");
        }

        public static void OnCanServerChanged(CZCom_MessageType MsgType, short DestAddr,
    short SourceAddr, short CmdClass, short CmdNbr, short SubNr, short sPID, IntPtr vData, int vDataArray)
        {
            Console.WriteLine("OnCanServerChanged called");
        }

        public static void OnCanServerChanged1(CZCom_MessageType MsgType, short DestAddr,
    short SourceAddr, short CmdClass, short CmdNbr, short SubNr, short sPID, IntPtr vData, int vDataArray)
        {
            Console.WriteLine("OnCanServerChanged 1 called");
        }

        public static void OnCanServerChanged2(CZCom_MessageType MsgType, short DestAddr,
    short SourceAddr, short CmdClass, short CmdNbr, short SubNr, short sPID, IntPtr vData, int vDataArray)
        {
            Console.WriteLine("OnCanServerChanged 2 called");
        }

        public static int functionA(int data)
        {
            Console.WriteLine("functionA called and data is:" + data);
            return 0;
        }

        public static int functionCC(String data)
        {
            Console.WriteLine("functionCC called and data is:" + data);
            return 0;
        }

        public static void InitialMTBDATA(ref MTB_DATA list)
        {
            list.count = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(int)));
            Marshal.StructureToPtr(-1, list.count, false);
            Console.WriteLine("MTBServer : count is set to: {0} ", (int)Marshal.PtrToStructure(new IntPtr(list.count.ToInt32()), typeof(int)));

            list.buff = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(char)) * 1024);
        }

        //将ASCII转换为对应字符数组：
        public static char[] ByteArrayToCharArrayConverter(byte[] byteBuf)
        {
            int len = byteBuf.Length;

            char[] ascii = new char[len];

            for (int i = 0; i<len; i++)
            {
                //这个10进制转对应ASCII字符才有意义。
                char s1 = (char)byteBuf[i];
                ascii[i] = s1;
            }


            return ascii;
        }

        public static byte[] CharArrayToAsciiConverter(char[] charBuf)
        {
            ASCIIEncoding charToASCII = new ASCIIEncoding();

            byte[] TxdBuf = new byte[charBuf.Length];    // 定义发送缓冲区；
            TxdBuf = charToASCII.GetBytes(charBuf);    // 转换为各字符对应的ASCII

            return TxdBuf;
        }

        public static char[] ByteArrayToCharArrayConverter1(byte[] byteBuf)
        {
            int len = byteBuf.Length;

            char[] ascii = new char[len];

            for (int i = 0; i < len; i++)
            {
                char s1 = (char)byteBuf[i];
                ascii[i] = s1;
            }

            return ascii;
        }

        public static string ByteArrayToStringConverter1(byte[] byteBuf)
        {
            char[] value = ByteArrayToCharArrayConverter1(byteBuf);

            return new string(value);
        }

        public static int IntSize => Marshal.SizeOf(typeof(int));
        public static int CharSize => Marshal.SizeOf(typeof(char));
        public static int ShortSize => Marshal.SizeOf(typeof(short));

        public static void InitializeMtbData(ref MTB_DATA list)
        {
            int charNumber = 100;
            list.count = Marshal.AllocCoTaskMem(IntSize);
            Marshal.StructureToPtr(charNumber, list.count, false);

            list.buff = Marshal.AllocCoTaskMem(CharSize * charNumber);
        }

        public static void InitializeShortData(ref ShortStruct shortStruct)
        {
            //short number = -1;
            shortStruct.value = Marshal.AllocCoTaskMem(ShortSize);
            //Marshal.StructureToPtr(number, shortStruct.value, false);
        }

        public static byte[] MTBDataToArray(MTB_DATA list)
        {
            int size = Marshal.SizeOf(typeof(char));
            int count = (int)Marshal.PtrToStructure(new IntPtr(list.count.ToInt64()), typeof(int));

            if (count == -1)
            {
                return null;
            }

            byte[] byteArray = new byte[count];
            for (int i = 0; i < count; i++)
            {
                byteArray[i] = (byte)Marshal.PtrToStructure(new IntPtr(list.buff.ToInt64() + i * size), typeof(byte));
            }

            Marshal.FreeCoTaskMem(list.buff);

            return byteArray;
        }

        public static short MTBDataToArray(ShortStruct list)
        {
            int size = Marshal.SizeOf(typeof(short));
            short resultValue = (short)Marshal.PtrToStructure(new IntPtr(list.value.ToInt64()), typeof(short));
            Marshal.FreeCoTaskMem(list.value);

            return resultValue;
        }

        public static bool GetUSBDevicePropertiesByPInvoke(int Number, out short pPoductId, out string pName, out string pSerialNumber)
        {
            ShortStruct poductId = new ShortStruct();
            InitializeShortData(ref poductId);

            MTB_DATA name = new MTB_DATA();
            InitializeMtbData(ref name);

            MTB_DATA serialNumber = new MTB_DATA();
            InitializeMtbData(ref serialNumber);

            bool result = GetUSBDeviceProperties(Number, poductId, name, serialNumber);

            pPoductId = MTBDataToArray(poductId);
            byte[] pPoductIdByte = MTBDataToArray(name);
            pName = ByteArrayToStringConverter1(pPoductIdByte);
            byte[] pSerialNumberByte = MTBDataToArray(serialNumber);
            pSerialNumber = ByteArrayToStringConverter1(pSerialNumberByte);

            return result;
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace PInvokeFramework
{
    public class Can29 : ICan29
    {
        [DllImport("Win32ProjectDll.dll")]
        public static extern void setFire_ReceivedCANMessage_CB(_IPortEvents_ReceivedCANMessageEventHandler pointer);

        [DllImport("Win32ProjectDll.dll")]
        public static extern void setFire_ReceivedCANMessage_Array(_IPortEvents_ReceivedCANMessageEventHandlerArray fun);

        [DllImport("Win32ProjectDll.dll")]
        public static extern void CallTimer();


        [DllImport("Win32ProjectDll.dll")]
        public static extern _IPortEvents_ReceivedCANMessageEventHandler GetReceivedCANMessageEventHandler();

        public event _IPortEvents_ReceivedCANMessageEventHandler ReceivedCANMessage;

        public Can29()
        {
            #region 测试NewCanServer接口
            //ReceivedCANMessage += new _IPortEvents_ReceivedCANMessageEventHandler(OnReceivedCANMessage);
            //setFire_ReceivedCANMessage_CB(ReceivedCANMessage);
            #endregion
        }

        public void SetFire_ReceivedCANMessage_CB(_IPortEvents_ReceivedCANMessageEventHandler ReceivedCANMessage)
        {
            setFire_ReceivedCANMessage_CB(ReceivedCANMessage);
        }

        public void SetFire_ReceivedCANMessage_Array(_IPortEvents_ReceivedCANMessageEventHandlerArray ReceivedCANMessage)
        {
            setFire_ReceivedCANMessage_Array(ReceivedCANMessage);
        }

        public _IPortEvents_ReceivedCANMessageEventHandler GetReceivedCANMessageEventHandlerFromC()
        {
            return GetReceivedCANMessageEventHandler();
        }
        public void OnReceivedCANMessage(CZCom_MessageType MsgType, short DestAddr,
            short SourceAddr, short CmdClass, short CmdNbr,
            short SubNr, short sPID, object vData)
        {
            Console.WriteLine("[Can29]OnReceivedCANMessage called");
        }

        

    }
}

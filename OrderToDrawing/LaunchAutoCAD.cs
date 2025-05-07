using System;
using System.Windows;
using System.Runtime.InteropServices;
using System.Threading;
using Autodesk.AutoCAD.Interop;

namespace LaunchAutoCAD
{
    [ComImport,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("00000016-0000-0000-C000-000000000046")]
    public interface IMessageFilter
    {
        [PreserveSig]
        int HandleInComingCall(
          int dwCallType, IntPtr hTaskCaller,
          int dwTickCount, IntPtr lpInterfaceInfo
        );
        [PreserveSig]
        int RetryRejectedCall(
          IntPtr hTaskCallee, int dwTickCount, int dwRejectType
        );
        [PreserveSig]
        int MessagePending(
          IntPtr hTaskCallee, int dwTickCount, int dwPendingType
        );
    }
    public class AcadConnection : IMessageFilter
    {
        [DllImport("ole32.dll")]
        static extern int CoRegisterMessageFilter(
            IMessageFilter lpMessageFilter,
            out IMessageFilter lplpMessageFilter
            );

        private AcadApplication acadApp;
        private bool isRunning;
        public AcadApplication AcadApp 
        {
            get
            {
                //MessageBox.Show(isRunning.ToString());
                if (isRunning == false || acadApp == null)
                    StartAcad();
                return acadApp;
            }
        }

        public AcadConnection()
        {    
        }

        private void StartAcad()
        {
            IMessageFilter oldFilter;
            CoRegisterMessageFilter(this, out oldFilter);
            const string progID = "AutoCAD.Application.25";
            acadApp = null;
            try
            {
                Type acType = Type.GetTypeFromProgID(progID);
                acadApp = (AcadApplication)Activator.CreateInstance(acType);
                //MessageBox.Show("neue Instanz");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot create object of type \"" + progID + "\"" + ex.Message);
            }
            if (acadApp != null)
            {
                try
                {
                    // By the time this is reached AutoCAD is fully
                    // functional and can be interacted with through code
                    acadApp.Visible = true;
                    isRunning = true;
                    // Now let's call our method
                    //acApp.ZoomAll();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void Quit()
        {
            if (isRunning == true || acadApp != null)
                acadApp.Quit();
            acadApp = null;
            isRunning = false;


        }


        #region IMessageFilter Members

        int IMessageFilter.HandleInComingCall(
              int dwCallType, IntPtr hTaskCaller,
              int dwTickCount, IntPtr lpInterfaceInfo
            )
        {
            return 0; // SERVERCALL_ISHANDLED
        }

        int IMessageFilter.RetryRejectedCall(
          IntPtr hTaskCallee, int dwTickCount, int dwRejectType
        )
        {
            return 1000; // Retry in a second
        }

        int IMessageFilter.MessagePending(
          IntPtr hTaskCallee, int dwTickCount, int dwPendingType
        )
        {
            return 1; // PENDINGMSG_WAITNOPROCESS
        }

        #endregion
    }
}

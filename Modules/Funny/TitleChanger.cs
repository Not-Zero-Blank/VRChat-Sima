using System;
using System.Runtime.InteropServices;

namespace Sima.Modules.Funny
{
    internal class TitleChanger : Core.SIMA_Module
    {
        IntPtr VRChat = IntPtr.Zero;
        [DllImport("user32.dll", EntryPoint = "SetWindowText")]
        public static extern bool SetWindowText(System.IntPtr hwnd, System.String lpString);
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern System.IntPtr FindWindow(System.String className, System.String windowName);
        public override void OnApplicationStart()
        {
            VRChat = FindWindow(null, "VRChat");
            SetWindowText(VRChat, "ERP Simulator");
        }
    }
}

using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Sima.Modules.Funny
{
    internal class TitleChanger : Core.SIMA_Module
    {
        IntPtr Garbage_Game = IntPtr.Zero;
        [DllImport("user32.dll", EntryPoint = "SetWindowText")]
        public static extern bool SetWindowText(System.IntPtr hwnd, System.String lpString);
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern System.IntPtr FindWindow(System.String className, System.String windowName);
        public override void OnApplicationStart()
        {
            // Fuck this game. Lets name it how it should be named!
            new Thread(() =>
            {
                Garbage_Game = FindWindow(null, "VRChat");
                while (true)
                {
                    SetWindowText(Garbage_Game, "ERP Simulator");
                    Thread.Sleep(120000); //Ugly Sleep for 2 Min bro.
                    SetWindowText(Garbage_Game, "Mirror Simulator");
                    Thread.Sleep(120000); //Ugly Sleep for 2 Min bro.
                    SetWindowText(Garbage_Game, "Skid Paradise");
                    Thread.Sleep(120000); //Ugly Sleep for 2 Min bro.
                    SetWindowText(Garbage_Game, "I hate my life and im depressed that why i play this shitty game");
                    Thread.Sleep(120000); //Ugly Sleep for 2 Min bro.
                }
               
            }).Start();
         
        }
    }
}

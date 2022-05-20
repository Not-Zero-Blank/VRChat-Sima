using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Sima.Modules.Utils
{
    public class KeyBinds : Core.SIMA_Module
    {
        public override void OnUpdate()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.RightShift))
            {
                File.Copy(@"D:\repo\Github\VRChat-Sima\bin\Debug\SIMA.dll", @"G:\SteamLibrary\steamapps\common\VRChat\Mods\SIMA.dll", true);
                Process.Start(@"G:\SteamLibrary\steamapps\common\VRChat\VRChat.exe");
                Process.GetCurrentProcess().Kill();
            }
        }
    }
}

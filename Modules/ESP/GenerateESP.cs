using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Sima.Modules.ESP
{
    internal class GenerateESP
    {
        public static void Init()
        {
            GenerateESP.Load = new UnityEngine.GameObject();
            GenerateESP.Load.AddComponent<ESPLogic>();
            UnityEngine.Object.DontDestroyOnLoad(GenerateESP.Load);
        }

        public static void Unload()
        {
            _Unload();
        }

        private static void _Unload()
        {
            GameObject.Destroy(Load);
        }

        private static GameObject Load;
    }
}

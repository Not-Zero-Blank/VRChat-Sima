using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC_Object = VRCSDK2.VRC_ObjectSync;

namespace Sima.Modules.Funny
{
    internal class FuckMyGame : Core.SIMA_Module
    {
        public static List<GameObject> Obh = new List<GameObject>();
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if(buildIndex == 0)
            {
                Obh = VRC_Object;
                foreach (UnityEngine.Object item in Resources.FindObjectsOfTypeAll(typeof(UnityEngine.Object)) as UnityEngine.Object)
                {

                }
            } 
            if(buildIndex == 1)
            {

            } 
            if(buildIndex == 2)
            {

            }
        }

    }
}

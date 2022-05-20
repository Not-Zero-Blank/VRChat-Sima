using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sima.Core;
using UnityEngine;

namespace Sima.Modules.LoadingScreen
{
    internal class LoadingScreenUI : Core.SIMA_Module
    {
        AssetBundle ab = AssetBundle.LoadFromFile(@"D:\Unity\SIMA LoadingScreen\Assets\AssetBundles\sima");
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (buildIndex == 1)
            {
                try
                {
                    GameObject.DestroyImmediate(GameObject.Find("/UserInterface/MenuContent/Screens/Title/LogoContainer/Login"));
                    GameObject.DestroyImmediate(GameObject.Find("/UserInterface/MenuContent/Screens/Title/LogoContainer/vrchatlogo2sided"));
                    //GameObject.Instantiate(ab.LoadAsset<GameObject>("Login"), GameObject.Find("/UserInterface/MenuContent/Screens/Title/LogoContainer/").transform);


                    GameObject.DestroyImmediate(GameObject.Find("/UserInterface/LoadingBackground_TealGradient_Music/SkyCube_Baked"));
                    GameObject.DestroyImmediate(GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/SkyCube_Baked"));
                    
                    GameObject.DestroyImmediate(GameObject.Find("/UserInterface/LoadingBackground_TealGradient_Music/_FX_ParticleBubbles"));
                    GameObject.DestroyImmediate(GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/_FX_ParticleBubbles"));
                    
                    GameObject.Destroy(GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/3DElements/LoadingInfoPanel/InfoPanel_Template_ANIM/SCREEN/mainFrame"));
                }
                catch
                {
                }
                    GameObject.DestroyImmediate(GameObject.Find("/UserInterface/MenuContent/Popups/LoadingPopup/3DElements/LoadingInfoPanel/InfoPanel_Template_ANIM/SCREEN/mainFrame"));
                    GameObject.Instantiate(ab.LoadAsset<GameObject>("GameObject"), GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup").transform);
            }
        }
    }
}

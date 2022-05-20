using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using VRC;

namespace Sima.Modules.LoadingScreen
{
    internal class LoadingMusic : Core.SIMA_Module
    {
        public override void OnApplicationStart()
        {
            if (!Directory.Exists(ConfigManager.FilePath + "//LoadingScreenMusic"))
            {
                Directory.CreateDirectory(ConfigManager.FilePath + "//LoadingScreenMusic");
            }
        }
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            Logs.Warning($"build index " + buildIndex);
            if (buildIndex == 1)
            {
                GameObject gameObject = GameObject.Find("LoadingBackground_TealGradient_Music/LoadingSound");
                GameObject gameObject2 = GameObject.Find("MenuContent/Popups/LoadingPopup/LoadingSound");
                if (gameObject != null)
                {
                    gameObject.GetComponent<AudioSource>().Stop();
                }
                if (gameObject2 != null)
                {
                    gameObject2.GetComponent<AudioSource>().Stop();
                }
                UnityWebRequest www = UnityWebRequest.Get("file://" + ConfigManager.FilePath + "//LoadingScreenMusic//clip3.wav");
                www.SendWebRequest();
                while (!www.isDone)
                {
                }
                AudioClip audioClip = WebRequestWWW.InternalCreateAudioClipUsingDH(www.downloadHandler, www.url, false, false, AudioType.UNKNOWN);
                while (!www.isDone || audioClip.loadState == AudioDataLoadState.Loading)
                {
                }
                if (audioClip != null)
                {
                    if (gameObject != null)
                    {
                        gameObject.GetComponent<AudioSource>().clip = audioClip;
                        gameObject.GetComponent<AudioSource>().Play();
                    }
                    if (gameObject2 != null)
                    {
                        gameObject2.GetComponent<AudioSource>().clip = audioClip;
                        gameObject2.GetComponent<AudioSource>().Play();
                    }
                }
            }
        }
    }
}

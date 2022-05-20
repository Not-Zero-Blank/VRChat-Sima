using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Sima.Modules.Utils
{
    internal class FPS_Counter : Core.SIMA_Module
    {
        public override void OnApplicationStart()
        {
            //MelonLoader.MelonCoroutines.Start(FPSCounter1());
            MelonLoader.MelonCoroutines.Start(FPSCounter2());
        }
        public double FPS1 = -1;
        public double FPS2 = -1;
        public IEnumerator FPSCounter1()
        {
            DateTime StartTime;
            DateTime StopTime;
            while (true)
            {
                StartTime = DateTime.Now;
                yield return new WaitForEndOfFrame();
                StopTime = DateTime.Now;
                var runtime = (StartTime - StopTime);
                FPS1 = (1000 / runtime.TotalMilliseconds) * -1;
            }
        }
        public IEnumerator FPSCounter2()
        {
            DateTime StartTime = DateTime.Now;
            DateTime StopTime;
            int frames = 0;
            while (true)
            {
                yield return new WaitForEndOfFrame();
                frames++;
                StopTime = DateTime.Now;
                var runtime = (StartTime - StopTime);
                if (runtime.TotalSeconds <= -1)
                {
                    StartTime = DateTime.Now;
                    FPS2 = frames;
                    frames = 0;
                }
            }
        }
        public override void OnGUI()
        {
            GUI.Label(new Rect(11f, 10f, 1025f, 250f), $"<size=15><color=RED>FPS {FPS2.ToString().Split('.')[0]}\nDeltaTime: {Time.deltaTime}\nFrameTime: </color></size>");
        }
    }
}

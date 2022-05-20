using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using VRC.SDK3.Components;

namespace Sima.Modules.Utils
{
    public class Extented_Items : Core.SIMA_Module
    {
        public override void WorldIntialized()
        {
            var pickups = GameObject.FindObjectsOfType<VRCPickup>();
            foreach (var item in pickups)
            {
                item.gameObject.AddComponent<Blanks_Items>();
            }
        }
    }
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class Blanks_Items : MonoBehaviour
    {
        public Blanks_Items(IntPtr gameobject) : base(gameobject) { }
        public TextMeshPro Text;
        public GameObject textobject;
        void Start()
        {
            Logs.Text($"Registered Item {gameObject.name}");
            textobject = new GameObject("LEXI_GO_Text");
            textobject.transform.SetParent(gameObject.transform);
            Text = textobject.AddComponent<TextMeshPro>();
            Text.text = gameObject.name;
            Text.m_width = 10;
            Text.m_maxHeight = 3;
            Text.fontSize = 2;
            var rect = Text.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(2, 2);
            rect.localScale = new Vector3(1, 1, 1);
            textobject.transform.localPosition = new Vector3(0, 0, 0);
        }
        void Update()
        {
            Vector3 rotate = Camera.main.transform.forward;
            rotate.y -= 180;
            textobject.transform.rotation = Quaternion.LookRotation(-rotate, Camera.main.transform.up);
        }
    }
}

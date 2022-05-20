using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC;
using VRC.Core;

namespace Sima.Core
{
    internal class Callbacks
    {
        public static void StartCallbacks()
        {
            new Blanks_Patches(typeof(NetworkManager).GetMethod("Method_Public_Void_Player_0"), "onJoin", Blanks_Patches.PatchType.postfix, typeof(PatchedCallbacks));
            new Blanks_Patches(typeof(NetworkManager).GetMethod("Method_Public_Void_Player_1"), "onLeft", Blanks_Patches.PatchType.postfix, typeof(PatchedCallbacks));
            new Blanks_Patches(typeof(NetworkManager).GetMethod("OnLeftRoom"), "onWorldLeft", Blanks_Patches.PatchType.postfix, typeof(PatchedCallbacks));
            new Blanks_Patches(typeof(NetworkManager).GetMethod("OnJoinedRoom"), "onWorldJoin", Blanks_Patches.PatchType.postfix, typeof(PatchedCallbacks));
            //MelonLoader.MelonCoroutines.Start(Additional_Callbacks.PlayerVisibleLoop());
            new Blanks_Patches(typeof(VRCPlayer).GetMethod("Awake"), "OnAwake", Blanks_Patches.PatchType.postfix, typeof(PatchedCallbacks));
        }
    }
    public static class PatchedCallbacks
    {
        public static void onJoin(Player param_1)
        {
            if (Additional_Callbacks.WorldLoaded)
            {
                Additional_Callbacks.WorldLoaded = false;
                Additional_Callbacks.OnWorldIntialized();
            }
            foreach (SIMA_Module a in ModuleManager.Modules) a.PlayerJoined(param_1);
        }
        public static void onLeft(Player param_1)
        {
            foreach (SIMA_Module a in ModuleManager.Modules) a.PlayerLeft(param_1);
        }
        public static void onWorldJoin()
        {
            Console.WriteLine("World Join");
            Additional_Callbacks.WorldLoaded = true;
            foreach (SIMA_Module a in ModuleManager.Modules) a.OnWorldJoined(RoomManager.field_Internal_Static_ApiWorld_0, RoomManager.field_Internal_Static_ApiWorldInstance_0);
        }
        public static void onWorldLeft()
        {
            foreach (SIMA_Module a in ModuleManager.Modules) a.OnWorldLeft();
        }
        public static void OnAwake(VRCPlayer __instance)
        {
            return;
            if (__instance == null)
            {
                return;
            }
            Logs.Text($"Added Listener to {__instance.gameObject.GetComponent<APIUser>().displayName}!");
            __instance.Method_Public_add_Void_OnAvatarIsReady_0((System.Action)delegate ()
            {
                if (__instance._player != null && __instance.prop_ApiAvatar_0 != null)
                {
                    var aPIUser = __instance.gameObject.GetComponent<APIUser>();
                    var apiAvatar = __instance.prop_ApiAvatar_0;
                    if (__instance != null)
                    {
                        Logs.Text("[AVATAR] " + aPIUser.displayName + " ----> " + apiAvatar.name + " - By: " + apiAvatar.authorName + "\n AVI ID: " + apiAvatar.id);
                    }
                }
            });
        }
    }
    internal static class Additional_Callbacks
    {
        internal static bool WorldLoaded = false;
        internal static void OnWorldIntialized()
        {
            foreach (SIMA_Module a in ModuleManager.Modules) a.WorldIntialized();
        }
    }
    internal class Avatars : SIMA_Module
    {
        public override void PlayerJoined(Player player)
        {
            player.gameObject.AddComponent<Blanks_Avatar_Manager>();
        }
    }
    [MelonLoader.RegisterTypeInIl2Cpp]
    internal class Blanks_Avatar_Manager : MonoBehaviour
    {
        public Blanks_Avatar_Manager(IntPtr gameobject) : base(gameobject) { }
        ApiAvatar avatar;
        public GameObject Avatar => GameObject.Find($"{gameObject.name}/ForwardDirection/Avatar");
        void Start()
        {
            Logs.Text($"Registered {gameObject.name} {gameObject.GetComponent<VRC.Player>().field_Private_APIUser_0.displayName}");
            var __instance = gameObject.GetComponent<VRCPlayer>();
            __instance.Method_Public_add_Void_OnAvatarIsReady_0((System.Action)delegate ()
            {
                if (__instance._player != null && __instance.prop_ApiAvatar_0 != null)
                {
                    var player = gameObject.GetComponent<VRC.Player>();
                    var apiAvatar = __instance.field_Private_ApiAvatar_0;
                    if (__instance != null)
                    {
                        foreach (SIMA_Module a in ModuleManager.Modules) a.OnAvatarReady(player, avatar, apiAvatar);
                        avatar = apiAvatar;
                    }
                }
            });
        }
    }
}

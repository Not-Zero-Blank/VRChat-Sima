using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC;
using VRC.Core;

namespace Sima.Core
{
    internal class Callbacks
    {
        public static void StartCallbacks()
        {
            new Blanks_Patches(typeof(NetworkManager).GetMethod("Method_Public_Void_Player_0"), "onLeft", Blanks_Patches.PatchType.postfix, typeof(PatchedCallbacks));
            new Blanks_Patches(typeof(NetworkManager).GetMethod("Method_Public_Void_Player_1"), "onJoin", Blanks_Patches.PatchType.postfix, typeof(PatchedCallbacks));
            new Blanks_Patches(typeof(NetworkManager).GetMethod("OnLeftRoom"), "onWorldLeft", Blanks_Patches.PatchType.postfix, typeof(PatchedCallbacks));
            new Blanks_Patches(typeof(NetworkManager).GetMethod("OnJoinedRoom"), "onWorldJoin", Blanks_Patches.PatchType.postfix, typeof(PatchedCallbacks));
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
            foreach (ModuleManager.Module a in ModuleManager.Modules) a.PlayerJoined(param_1);
        }
        public static void onLeft(Player param_1)
        {
            foreach (ModuleManager.Module a in ModuleManager.Modules) a.PlayerLeft(param_1);
        }
        public static void onWorldJoin()
        {
            Additional_Callbacks.WorldLoaded = true;
            foreach (ModuleManager.Module a in ModuleManager.Modules) a.OnWorldJoined(RoomManager.field_Internal_Static_ApiWorld_0, RoomManager.field_Internal_Static_ApiWorldInstance_0);
        }
        public static void onWorldLeft()
        {
            foreach (ModuleManager.Module a in ModuleManager.Modules) a.OnWorldLeft();
        }
    }
    internal static class Additional_Callbacks
    {
        internal static bool WorldLoaded = false;
        internal static void OnWorldIntialized()
        {
            foreach (ModuleManager.Module a in ModuleManager.Modules) a.WorldIntialized();
        }
    }
}

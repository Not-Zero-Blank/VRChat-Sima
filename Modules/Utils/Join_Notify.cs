using Sima.Core;
using VRC;
namespace Sima.Modules.Utils
{
    public class Join_Notify : SIMA_Module
    {
        [Toggle]
        public bool ExcludeMe = false;
        [Toggle]
        public bool DontShowWhenJoining = false;
        [Toggle]
        public bool Active = true;
        public bool Joined = false;
        public override void WorldIntialized() => Joined = true;
        public override void OnWorldLeft() => Joined = false;
        public override void PlayerJoined(Player player)
        {
            if (!Active) return;
            if (player.isMe() && ExcludeMe) return;
            if (DontShowWhenJoining && !Joined) return;
           Logs.Text($"{player.field_Private_APIUser_0.displayName} Joined!");
        }
        public override void PlayerLeft(Player player)
        {
            if (!Active) return;
            if (VRCPlayer.field_Internal_Static_VRCPlayer_0 == null && ExcludeMe) return;
            Logs.Text($"{player.field_Private_APIUser_0.displayName} Left!");
        }
    }
}
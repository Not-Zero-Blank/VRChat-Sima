using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC;
using VRC.Core;
public static class RoomExtensions
{
    public static ApiWorld GetWorld() => RoomManager.field_Internal_Static_ApiWorld_0;
    public static ApiWorldInstance GetWorldInstance() => RoomManager.field_Internal_Static_ApiWorldInstance_0;
    public static string WorldID() => GetWorld().id + ":" + GetWorldInstance().instanceId;
}
public static class PlayerExtensions
{
    public static List<VRC.Player> GetAllPlayers() => PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0.ToArray().ToList();
    public static bool isMe(this VRC.Player player) => player.prop_APIUser_0.id == VRCPlayer.field_Internal_Static_VRCPlayer_0._player.prop_APIUser_0.id;
}


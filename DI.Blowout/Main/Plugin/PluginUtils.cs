using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace DI.Blowout.Main
{
    public partial class BlowoutPlugin
    {
        private void SendUIEffectToAllPlayers(ushort id)
        {
            foreach (SteamPlayer steamPlayer in Provider.clients)
            {
                EffectManager.sendUIEffect(id, 15001, steamPlayer.transportConnection, true);
            }
        }

        private void ClearUIEffectFromAllPlayers(ushort id)
        {
            foreach (SteamPlayer steamPlayer in Provider.clients)
            {
                EffectManager.askEffectClearByID(id, steamPlayer.transportConnection);
            }
        }

        private void DamagePlayers()
        {
            foreach (SteamPlayer steamPlayer in Provider.clients)
            {
                Player player = steamPlayer.player;
                
                if (Physics.Raycast(new Ray(player.look.aim.position, Vector3.up), float.MaxValue, RayMasks.ROOFS_INTERACT) == false)
                {
                    player.life.askDamage(Configuration.DamagePerTick, Vector3.right, EDeathCause.ACID, ELimb.SKULL, CSteamID.Nil, out _);
                }
            }
        }
    }
}

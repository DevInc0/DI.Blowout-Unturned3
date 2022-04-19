using DI.Blowout.Enumerations;
using DI.Library.Extensions;
using Rocket.API;
using Rocket.Core.Commands;
using Rocket.Unturned.Player;

namespace DI.Blowout.Main
{
    public partial class BlowoutPlugin
    {
        [RocketCommand("blowout", "", "", AllowedCaller.Both)]
        [RocketCommandPermission("DI.Blowout")]
        public void ExecuteBlowout(IRocketPlayer caller, string[] parameters)
        {
            UnturnedPlayer sender = caller.ToUnturnedPlayer();
            parameters = parameters.ToLower();

            try
            {
                string mode = parameters[0];

                if (mode == "help")
                {
                    SendPlayerCommandHelp(caller);
                    return;
                }
                
                if (mode == "start")
                {
                    if (_blowoutState != BlowoutState.FINISHED)
                    {
                        SendMessage(sender, "Выброс уже активен!");
                        return;
                    }

                    StartBlowoutPreparing();
                    SendMessage(sender, "Выброс запущен!");
                    return;
                }
                
                if (mode == "end")
                {
                    if (_blowoutState == BlowoutState.FINISHED)
                    {
                        SendMessage(sender, "Выброс уже неактивен!");
                        return;
                    }

                    EndBlowout();
                    SendMessage(sender, "Выброс остановлен!");
                    return;
                }
            }
            catch
            {
                SendMessage(sender, "<color=yellow>Введены неверные параметры!</color>");
            }
        }
    }
}

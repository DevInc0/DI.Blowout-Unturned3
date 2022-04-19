using DI.Blowout.Enumerations;
using DI.Library.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Player;
using System.Collections;
using UnityEngine;

namespace DI.Blowout.Main
{
    public partial class BlowoutPlugin : XMLPlugin<Configuration>
    {        
        private BlowoutState _blowoutState;
        
        protected override void Load()
        {
            U.Events.OnBeforePlayerConnected += OnBeforePlayerConnected;

            _blowoutState = BlowoutState.FINISHED;

            StartCoroutine(BeforeBlowout());
        }

        protected override void Unload()
        {
            U.Events.OnBeforePlayerConnected -= OnBeforePlayerConnected;

            _blowoutState = BlowoutState.FINISHED;

            StopAllCoroutines();
        }        

        private void OnBeforePlayerConnected(UnturnedPlayer player)
        {
            if (_blowoutState != BlowoutState.FINISHED) player.Kick("Ты куда полез?! Там же выброс! Подожди немного...");
        }        

        private void StartBlowoutPreparing()
        {
            _blowoutState = BlowoutState.PREPARING;

            ushort randomPreparingUI_ID = Configuration.PreparingUI_IDs[Random.Range(0, Configuration.PreparingUI_IDs.Count)];
            SendUIEffectToAllPlayers(randomPreparingUI_ID);

            StopAllCoroutines();
            StartCoroutine(BlowoutPreparing());
        }

        private void StartBlowout()
        {
            _blowoutState = BlowoutState.STARTED;

            SendUIEffectToAllPlayers(Configuration.BlowoutUI_ID);
            StopAllCoroutines();
            StartCoroutine(BlowoutProccess());
        }

        private void EndBlowout()
        {
            _blowoutState = BlowoutState.FINISHED;

            ClearUIEffectFromAllPlayers(Configuration.BlowoutUI_ID);

            ushort randomEndingUI_ID = Configuration.EndingUI_IDs[Random.Range(0, Configuration.EndingUI_IDs.Count - 1)];
            SendUIEffectToAllPlayers(randomEndingUI_ID);

            StopAllCoroutines();
            StartCoroutine(BeforeBlowout());
        }

        private IEnumerator BeforeBlowout()
        {
            yield return new WaitForSeconds(Configuration.BlowoutFrequency);

            StartBlowoutPreparing();
        }

        private IEnumerator BlowoutPreparing()
        {
            yield return new WaitForSeconds(Configuration.DelayBeforeBlowout);

            StartBlowout();
        }

        private IEnumerator BlowoutProccess()
        {
            float time = 0f;

            while (time < Configuration.BlowoutDuration)
            {
                yield return new WaitForSeconds(Configuration.DamageFrequency);
                time += Configuration.DamageFrequency;

                DamagePlayers();
            }

            EndBlowout();
        }
    }
}

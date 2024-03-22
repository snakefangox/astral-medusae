

using System;

namespace XRL.World.Parts
{
    [Serializable]
    public class Snakefangox_AstralMedusae_LiquidFilter : IPart
    {
        public string FilterLiquid;
        public string Sound = "SplashStep1";

        public override bool WantTurnTick()
        {
            return true;
        }

        public override bool WantTenTurnTick()
        {
            return true;
        }

        public override bool WantHundredTurnTick()
        {
            return true;
        }

        public override void TurnTick(long TurnNumber)
        {
            Process(1);
        }

        public override void TenTurnTick(long TurnNumber)
        {
            Process(10);
        }

        public override void HundredTurnTick(long TurnNumber)
        {
            Process(100);
        }

        public void Process(int turns)
        {
            LiquidVolume volume = ParentObject.LiquidVolume;
            if (volume == null) { return; }

            if (volume.IsMixed() && volume.ContainsLiquid(FilterLiquid))
            {
                int amount = volume.Amount(FilterLiquid);
                volume.Empty();
                volume.Fill(FilterLiquid, amount);
                SoundManager.PlaySound(Sound);
            }
        }
    }
}
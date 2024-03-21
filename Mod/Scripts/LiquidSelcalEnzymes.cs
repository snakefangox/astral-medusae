using System;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World;
using XRL.World.Capabilities;
using XRL.World.Parts;

namespace XRL.Liquids
{
    [Serializable]
    [IsLiquid]
    public class LiquidSelcalEnzymes : BaseLiquid
    {
        public new const string ID = "selcalenzymes";

        public LiquidSelcalEnzymes() : base(ID)
        {
            Adsorbence = 25;
            Fluidity = 1;
            Staining = 3;
            ConsiderDangerousToContact = true;
            ConsiderDangerousToDrink = true;
            StickyWhenWet = true;
            StickySaveTargetBase = 4;
            StickySaveTargetScale = 0.1;
            StickyDuration = 12;
            StickySaveVs = "Selcal Enzymes Stuck Restraint";
        }

        public override string GetSmearedAdjective(LiquidVolume Liquid)
        {
            return "{{snakefangox_astralmedusae_enzymic|enzymic}}";
        }

        public override string GetSmearedName(LiquidVolume Liquid)
        {
            return "{{snakefangox_astralmedusae_enzymic|enzyme-coated}}";
        }

        public override string GetName(LiquidVolume Liquid)
        {
            return "{{snakefangox_astralmedusae_enzymic|selcal enzymes}}";
        }

        public override string GetWaterRitualName()
        {
            return "selcal enzymes";
        }

        public override string GetStainedName(LiquidVolume Liquid)
        {
            return "{{snakefangox_astralmedusae_enzymic|selcal enzymes}}";
        }

        public override string GetAdjective(LiquidVolume Liquid)
        {
            return "{{snakefangox_astralmedusae_enzymic|selcal enzymic}}";
        }

        public override string GetPreparedCookingIngredient()
        {
            return "dissociation";
        }

        public override float GetValuePerDram()
        {
            return 100f;
        }

        public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
        {
            Message.Compound("Your mind crystallizes and {{snakefangox_astralmedusae_enzymic|shatters}}");
            RendMind(Liquid, Target, By: Target, "4d10");
            ExitInterface = true;
            return true;
        }

        public override void SmearOnTick(LiquidVolume Liquid, GameObject Target, GameObject By, bool FromCell)
        {
            base.SmearOnTick(Liquid, Target, By, FromCell);
            RendMind(Liquid, Target, By);
        }

        private static void RendMind(LiquidVolume Liquid, GameObject Target, GameObject By = null, string dice = "2d4")
        {
            int pv = Liquid.GetLiquidExposureMillidrams(Target, "selcalenzymes") / 500;
            int pens = Stat.RollDamagePenetrations(Stats.GetCombatMA(Target), pv, pv);
            int totalDamage = 0;
            for (int i = 0; i < pens; i++)
            {
                totalDamage += dice.RollCached();
            }

            Target.TakeDamage(totalDamage, "from {{snakefangox_astralmedusae_enzymic|selcalic enzymes}}!", "Mental Psionic", Attacker: By ?? Liquid.ParentObject);
        }

        public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
        {
            if (eRender.ColorsVisible)
            {
                eRender.ColorString = "^g" + eRender.ColorString;
            }
        }

        public override void BaseRenderPrimary(LiquidVolume Liquid)
        {
            Liquid.ParentObject.pRender.ColorString = "&G^m";
            Liquid.ParentObject.pRender.TileColor = "&G";
            Liquid.ParentObject.pRender.DetailColor = "m";
        }

        public override void BaseRenderSecondary(LiquidVolume Liquid)
        {
            Liquid.ParentObject.pRender.ColorString += "&m";
        }

        public override void RenderSmearPrimary(LiquidVolume Liquid, RenderEvent eRender, GameObject obj)
        {
            if (eRender.ColorsVisible)
            {
                int num = XRLCore.CurrentFrame % 60;
                if (num > 5 && num < 15)
                {
                    eRender.ColorString = "&m";
                }
            }
            base.RenderSmearPrimary(Liquid, eRender, obj);
        }

        public override void RenderSecondary(LiquidVolume Liquid, RenderEvent eRender)
        {
            if (eRender.ColorsVisible)
            {
                eRender.ColorString += "&G";
            }
        }
    }
}
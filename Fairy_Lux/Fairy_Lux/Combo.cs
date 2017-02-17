using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using static Fairy_Lux.Menus;

namespace Fairy_Lux
{
    internal static class Combo
    {
        public static AIHeroClient myhero
        {
            get { return ObjectManager.Player; }
        }

        public static void Execute()
        {

            var target = TargetSelector.GetTarget(SpellsManager.E.Range, DamageType.Magical);

            if ((target == null) || target.IsInvulnerable)
                return;

            //Cast E
            if (ComboMenu["E"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.E.Range) && SpellsManager.E.IsReady())
                {
                    var pred = SpellsManager.E.GetPrediction(target);
                    SpellsManager.E.Cast(pred.CastPosition);
                }

            if (myhero.HasBuff("LuxEEnd"))
            {
                SpellsManager.E.Cast();
            }
            
            //Cast Q
            if (ComboMenu["Q"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady())
                {
                    var prediction = SpellsManager.Q.GetPrediction(target);
                    SpellsManager.Q.Cast(prediction.CastPosition);
                }

            var rtarget = TargetSelector.GetTarget(SpellsManager.R.Range, DamageType.Magical);

            if ((rtarget == null) || rtarget.IsInvulnerable)
                return;

            if (ComboMenu["R"].Cast<CheckBox>().CurrentValue && SpellsManager.R.IsReady() && rtarget.IsValidTarget(SpellsManager.R.Range) &&
                        Prediction.Health.GetPrediction(rtarget, SpellsManager.R.CastDelay) <=
                        SpellsManager.GetRealDamage(rtarget, SpellSlot.R))
                    {
                            var prediction = SpellsManager.R.GetPrediction(rtarget);
                            SpellsManager.R.Cast(prediction.CastPosition);
                    }





        }

           public static void ExecuteCombo2()
        {
            var target = TargetSelector.GetTarget(SpellsManager.E.Range, DamageType.Magical);

            if ((target == null) || target.IsInvulnerable)
                return;

            if (ComboMenu["Q"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady())
            {
                var prediction = SpellsManager.Q.GetPrediction(target);
                SpellsManager.Q.Cast(target);
            }
            //Cast E
            if (ComboMenu["E"].Cast<CheckBox>().CurrentValue && target.IsValidTarget(SpellsManager.E.Range) && SpellsManager.E.IsReady())
            {
                var pred = SpellsManager.E.GetPrediction(target);
                SpellsManager.E.Cast(pred.CastPosition);
            }

            if (myhero.HasBuff("LuxEEnd"))
            {
                SpellsManager.E.Cast();
            }
            
            var rtarget = TargetSelector.GetTarget(SpellsManager.R.Range, DamageType.Magical);

            if ((rtarget == null) || rtarget.IsInvulnerable)
                return;

            if (ComboMenu["R"].Cast<CheckBox>().CurrentValue && SpellsManager.R.IsReady() && rtarget.IsValidTarget(SpellsManager.R.Range) &&
                        Prediction.Health.GetPrediction(rtarget, SpellsManager.R.CastDelay) <=
                        SpellsManager.GetRealDamage(rtarget, SpellSlot.R))
            {
                var prediction = SpellsManager.R.GetPrediction(rtarget);
                SpellsManager.R.Cast(prediction.CastPosition);
            }
        }
    }
}




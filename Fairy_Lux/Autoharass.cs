
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace Fairy_Lux
{
    class Autoharass
    {
        public static void Execute7()
        {
            var qtarget = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);

            if ((qtarget == null) || qtarget.IsInvulnerable)
                return;
            //Cast Q
                if (qtarget.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady())
            {
                var prediction = SpellsManager.Q.GetPrediction(qtarget);
                SpellsManager.Q.Cast(prediction.CastPosition);
            }

        }

        public static void Execute8()
        {
            var etarget = TargetSelector.GetTarget(SpellsManager.E.Range, DamageType.Magical);

            if ((etarget == null) || etarget.IsInvulnerable)
                return;
            //Cast E
            if (etarget.IsValidTarget(SpellsManager.E.Range) && SpellsManager.E.IsReady())
            {
                var pred = SpellsManager.E.GetPrediction(etarget);
                SpellsManager.E.Cast(pred.CastPosition);
            }
        }
    }
}

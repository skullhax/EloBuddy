using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using System.Linq;
using static Fairy_Lux.Combo;

namespace Fairy_Lux
{
    internal static class LaneClear
    {
        public static void Execute()
        {
            var minions =
                EntityManager.MinionsAndMonsters.GetLaneMinions()
                    .Where(
                        m => m.IsValidTarget(SpellsManager.E.Range))
                    .ToArray();
            if (minions.Length == 0) return;

            var farmLocation = Prediction.Position.PredictCircularMissileAoe(minions, SpellsManager.E.Range, SpellsManager.E.Width,
                SpellsManager.E.CastDelay, SpellsManager.E.Speed).OrderByDescending(r => r.GetCollisionObjects<Obj_AI_Minion>().Length).FirstOrDefault();

            //Cast Q
            if (Menus.LaneClearMenu["Q"].Cast<CheckBox>().CurrentValue)
                if (SpellsManager.Q.IsReady())
                    SpellsManager.Q.CastOnBestFarmPosition(2);

            if (Menus.LaneClearMenu["E"].Cast<CheckBox>().CurrentValue && SpellsManager.E.IsReady())
            {
                var predictedMinion = farmLocation.GetCollisionObjects<Obj_AI_Minion>();
                if (predictedMinion.Length >= 3)
                {
                   SpellsManager.E.Cast(farmLocation.CastPosition);
                }
            }

            if (myhero.HasBuff("Detonate"))
            {
                SpellsManager.E.Cast();
            }

        }
    }
}
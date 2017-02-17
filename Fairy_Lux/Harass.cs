using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace Fairy_Lux
{
    internal static class Harass
    {
        public static void Execute1()
        {
            var qtarget = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);

            if ((qtarget == null) || qtarget.IsInvulnerable)
                return;
            //Cast Q
            if (Menus.HarassMenu["Q"].Cast<CheckBox>().CurrentValue)
                if (qtarget.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady())
                {
                    var prediction = SpellsManager.Q.GetPrediction(qtarget);
                    SpellsManager.Q.Cast(prediction.CastPosition);
                }
            var etarget = TargetSelector.GetTarget(SpellsManager.E.Range, DamageType.Magical);

            if ((etarget == null) || etarget.IsInvulnerable)
                return;
            //Cast E
            if (Menus.HarassMenu["E"].Cast<CheckBox>().CurrentValue)
                if (etarget.IsValidTarget(SpellsManager.E.Range) && SpellsManager.E.IsReady())
                {
                    var prediction = SpellsManager.E.GetPrediction(qtarget);
                    SpellsManager.E.Cast(prediction.CastPosition);
                }

        }



        /*
        //Summoners Target
        var Summ1 = TargetSelector.GetTarget(Smite.Range, DamageType.Mixed);
        var Summ2 = TargetSelector.GetTarget(Ignite.Range, DamageType.Mixed);
        if ((Summ1 == null) || Summ1.IsInvulnerable)
            return;
        //Cast Smite
        if (ComboMenu["Smite"].Cast<CheckBox>().CurrentValue)
            if (Summ1.IsValidTarget(Smite.Range) && Smite.IsReady())
                Smite.Cast(Smite.GetKillableHero());
        if ((Summ2 == null) || Summ2.IsInvulnerable)
            return;
        //Cast Ignite
        if (ComboMenu["Ignite"].Cast<CheckBox>().CurrentValue)
            if (Summ2.IsValidTarget(Ignite.Range) && Ignite.IsReady())
                Ignite.Cast(Ignite.GetKillableHero());
        */
    }
}
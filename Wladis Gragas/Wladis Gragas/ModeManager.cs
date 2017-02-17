using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using static Wladis_Gragas.Menus;
using static Wladis_Gragas.Combo;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Enumerations;

namespace Wladis_Gragas
{
    internal class ModeManager
    {
        public static void InitializeModes()
        {
            Game.OnTick += Game_OnTick;
            Interrupter.OnInterruptableSpell += KInterrupter;
            Gapcloser.OnGapcloser += KGapCloser;
        }

        private static void Game_OnTick(EventArgs args)
        {
            var orbMode = Orbwalker.ActiveModesFlags;

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Combo) && (ComboMenu["ComboLogic"].Cast<ComboBox>().CurrentValue == 1))
                Execute();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Combo) && (ComboMenu["ComboLogic"].Cast<ComboBox>().CurrentValue == 0))
                Execute7();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Harass))
                Harass.Execute1();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.LaneClear))
                LaneClear.Execute10();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.JungleClear))
                LaneClear.Execute11();
            
            if (KillStealMenu["Q"].Cast<CheckBox>().CurrentValue)
                KillSteal.Execute2();

            if (KillStealMenu["W"].Cast<CheckBox>().CurrentValue)
                KillSteal.Execute3();

            if (KillStealMenu["E"].Cast<CheckBox>().CurrentValue)
                KillSteal.Execute4();

            if (KillStealMenu["R"].Cast<CheckBox>().CurrentValue)
                KillSteal.Execute5();

            if (MiscMenu["Z"].Cast<CheckBox>().CurrentValue)
                Execute6();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Flee))
                Execute17();

            if (HarassMenu["AutoQ"].Cast<CheckBox>().CurrentValue)
            {
                var qtarget = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);

                if ((qtarget == null) || qtarget.IsInvulnerable)
                    return;

                if (qtarget.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady())
                {
                    var prediction = SpellsManager.Q.GetPrediction(qtarget);
                    SpellsManager.Q.Cast(prediction.CastPosition);
                }

                if (myhero.HasBuff("GragasQ") && SpellsManager.Q.IsReady())
                {
                    SpellsManager.Q.Cast(qtarget);
                }
            }
            
            if (HarassMenu["AutoW"].Cast<CheckBox>().CurrentValue)
        {
            var wtarget = TargetSelector.GetTarget(SpellsManager.E.Range, DamageType.Magical);

            if ((wtarget == null) || wtarget.IsInvulnerable)
                return;
            //Cast W
            if (wtarget.IsValidTarget(SpellsManager.E.Range) && SpellsManager.W.IsReady())
                SpellsManager.W.Cast();

        }


    }

        static void KInterrupter(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs args)
        {

            if (args.DangerLevel == DangerLevel.High && sender.IsEnemy && sender is AIHeroClient && sender.Distance(myhero) < SpellsManager.E.Range && SpellsManager.E.IsReady() && MiscMenu["EInterrupt"].Cast<CheckBox>().CurrentValue)
            {
                SpellsManager.E.Cast(sender);
            }
            if (args.DangerLevel == DangerLevel.High && sender.IsEnemy && sender is AIHeroClient && sender.Distance(myhero) < SpellsManager.R.Range && SpellsManager.R.IsReady() && MiscMenu["RInterrupt"].Cast<CheckBox>().CurrentValue)
            {
                SpellsManager.R.Cast(sender);
            }

        }
        static void KGapCloser(Obj_AI_Base sender, Gapcloser.GapcloserEventArgs args)
        {


            if (sender.IsEnemy && sender is AIHeroClient && sender.Distance(myhero) < SpellsManager.E.Range && SpellsManager.E.IsReady() && MiscMenu["EGapCloser"].Cast<CheckBox>().CurrentValue)
            {
                SpellsManager.E.Cast(sender);
            }
            if (sender.IsEnemy && sender is AIHeroClient && sender.Distance(myhero) < SpellsManager.R.Range && SpellsManager.R.IsReady() && MiscMenu["RGapCloser"].Cast<CheckBox>().CurrentValue)
            {
                SpellsManager.R.Cast(sender);

            }
        }


    }
}
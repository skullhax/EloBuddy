using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using System.Linq;
using static Wladis_Gragas.Combo;

namespace Wladis_Gragas
{
    internal static class SpellsManager
    {
        public static Spell.Skillshot Q;
        public static Spell.Active W;
        public static Spell.Skillshot E;
        public static Spell.Skillshot R;
        public static List<Spell.SpellBase> SpellList = new List<Spell.SpellBase>();

        public static void InitializeSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 850, SkillShotType.Circular, 2, 1000, 110);
            Q.AllowedCollisionCount = int.MaxValue;
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Skillshot(SpellSlot.E, 600, SkillShotType.Linear, 0, 1000, 50);
            R = new Spell.Skillshot(SpellSlot.R, 1150, SkillShotType.Circular, 2, 1000, 700);
            R.AllowedCollisionCount = int.MaxValue;

            Obj_AI_Base.OnLevelUp += AutoLevel.Obj_AI_Base_OnLevelUp;
        }

        #region Damages

        public static float GetRealDamage(this Obj_AI_Base target, SpellSlot slot)
        {
            var damageType = DamageType.Magical;
            var ap = Player.Instance.TotalMagicalDamage;
            var sLevel = Player.GetSpell(slot).Level - 1;
            var enemy = EntityManager.Heroes.Enemies.FirstOrDefault(hero => !hero.IsZombie && hero.Distance(myhero) <= SpellsManager.E.Range + 30000);

            var dmg = 0f;

            switch (slot)
            {
                case SpellSlot.Q:
                    if (Q.IsReady())
                        dmg += new float[] { 80, 120, 160, 200, 240 }[sLevel] + 0.60f * ap;
                    break;
                case SpellSlot.W:
                    if (W.IsReady())
                        dmg += new float[] { 20, 50, 80, 110, 140 }[sLevel] + 0.30f * ap + (enemy.MaxHealth * 10 / 100);
                    break;

                case SpellSlot.E:
                    if (E.IsReady())
                        dmg += new float[] { 80, 130, 180, 230, 280 }[sLevel] + 0.60f * ap;
                    break;

                case SpellSlot.R:
                    if (R.IsReady())
                        dmg += new float[] { 200, 300, 400 }[sLevel] + 0.70f * ap;
                    break;
            }

            return Player.Instance.CalculateDamageOnUnit(target, damageType, dmg - 10);
        }


        #endregion damages


    }
}
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace Fairy_Lux
{
    internal static class SpellsManager
    {
        public static Spell.Skillshot Q;
        public static Spell.Skillshot W;
        public static Spell.Skillshot E;
        public static Spell.Skillshot R;
        public static List<Spell.SpellBase> SpellList = new List<Spell.SpellBase>();

        public static void InitializeSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1175, SkillShotType.Linear, 250, 1200, 70)
            {
                AllowedCollisionCount = 1
            };
            W = new Spell.Skillshot(SpellSlot.W, 1075, SkillShotType.Linear, 350, 1500, 130)
            {
                AllowedCollisionCount = int.MaxValue
            };
            E = new Spell.Skillshot(SpellSlot.E, 1100, SkillShotType.Circular, 250, 1300, 335)
            {
                AllowedCollisionCount = int.MaxValue
            };
            R = new Spell.Skillshot(SpellSlot.R, 3000, SkillShotType.Linear, 1000, int.MaxValue, 110)
            {
                AllowedCollisionCount = int.MaxValue
            };

            Obj_AI_Base.OnLevelUp += AutoLevel.Obj_AI_Base_OnLevelUp;
        }

        #region Damages

        public static float GetRealDamage(this Obj_AI_Base target, SpellSlot slot)
        {
            var damageType = DamageType.Magical;
            var ap = Player.Instance.FlatMagicDamageMod;
            var sLevel = Player.GetSpell(slot).Level - 1;

            var dmg = 0f;

            switch (slot)
            {
                case SpellSlot.Q:
                    if (Q.IsReady())
                        dmg += new float[] { 50, 100, 150, 200, 250 }[sLevel] + 0.7f*ap;
                    break;                  // 50, 100, 150, 200, 250
                case SpellSlot.W:
                    if (W.IsReady() && Q.IsReady())
                        dmg += new float[] {50, 150, 160, 170, 180}[sLevel] + 0.20f*ap;
                    break;                  
                case SpellSlot.E:
                    if (E.IsReady())
                        dmg += new float[] { 60, 105, 150, 195, 240 }[sLevel] + 0.6f*ap;
                    break;                  //60, 105, 150, 195, 240
                case SpellSlot.R:
                    if (R.IsReady())
                        dmg += new float[] { 300, 400, 500 }[sLevel] + 0.75f*ap;
                    break;                  //300 400 500
            }
            return Player.Instance.CalculateDamageOnUnit(target, damageType, dmg - 10);
        }


        #endregion damages


        }
    }

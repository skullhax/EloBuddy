using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using static Fairy_Lux.SpellsManager;
using static Fairy_Lux.Menus;

namespace Fairy_Lux
{
    class Active
    {
        public static void Execute6()
        {
            var playerMana = Player.Instance.ManaPercent;
            if (W.IsReady() && WMenu["W"].Cast<CheckBox>().CurrentValue && myhero.HealthPercent < WMenu["dangerSlider"].Cast<Slider>().CurrentValue && playerMana > WMenu["manaSlider"].Cast<Slider>().CurrentValue && myhero.CountEnemiesInRange(500) >= 1)
            {
                W.Cast(myhero.Position);
            }

            var Ally = EntityManager.Heroes.Allies.FirstOrDefault(hero => !hero.IsMe && !hero.IsInShopRange() && !hero.IsZombie && hero.Distance(myhero) <= SpellsManager.W.Range && hero.CountEnemiesInRange(500) >= 1);

            if (WMenu["WAlly"].Cast<CheckBox>().CurrentValue && myhero.ManaPercent > WMenu["manaSliderAlly"].Cast<Slider>().CurrentValue && W.IsReady() && Ally.HealthPercent < WMenu["dangerSliderAlly"].Cast<Slider>().CurrentValue)
            {
                W.Cast(Ally.Position);
            }
        }

        public static AIHeroClient myhero { get { return ObjectManager.Player; } }
    }

}

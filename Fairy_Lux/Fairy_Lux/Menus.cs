using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using static Fairy_Lux.Skins;

namespace Fairy_Lux
{
    internal class Menus
    {
        public const string DrawingsMenuId = "drawingsmenuid";
        public const string MiscMenuId = "miscmenuid";
        public static Menu FirstMenu;
        public static Menu DrawingsMenu;
        public static Menu ComboMenu;
        public static Menu HarassMenu;
        public static Menu LaneClearMenu;
        public static Menu FleeMenu;
        public static Menu MiscMenu;
        public static Menu KillStealMenu;
        public static Menu WMenu;

        public static ColorSlide QColorSlide;
        public static ColorSlide WColorSlide;
        public static ColorSlide EColorSlide;
        public static ColorSlide RColorSlide;
        public static ColorSlide DamageIndicatorColorSlide;

        public static void CreateMenu()
        {

            FirstMenu = MainMenu.AddMenu("Fairy " + Player.Instance.ChampionName,
                Player.Instance.ChampionName.ToLower() + "Lux");
            ComboMenu = FirstMenu.AddSubMenu("• Combo ");
            HarassMenu = FirstMenu.AddSubMenu("• Harass");
            LaneClearMenu = FirstMenu.AddSubMenu("• LaneClear");
            FleeMenu = FirstMenu.AddSubMenu("• Flee");
            WMenu = FirstMenu.AddSubMenu("• W Settings");
            KillStealMenu = FirstMenu.AddSubMenu("• Killsteal");
            DrawingsMenu = FirstMenu.AddSubMenu("• Drawings", DrawingsMenuId);
            MiscMenu = FirstMenu.AddSubMenu("• Misc", MiscMenuId);


            ComboMenu.AddGroupLabel("Combo Settings");
            ComboMenu.Add("Q", new CheckBox("- Use Q"));
            ComboMenu.Add("E", new CheckBox("- Use E"));
            ComboMenu.Add("R", new CheckBox("- Use R"));
            ComboMenu.AddSeparator();
            ComboMenu.Add("ComboLogic", new ComboBox("- Combo Logic", 0, "E > Q > R", "Q > E > R"));

            HarassMenu.AddGroupLabel("Harass Settings");
            HarassMenu.Add("Q", new CheckBox("- Use Q"));
            HarassMenu.Add("E", new CheckBox("- Use E"));
            HarassMenu.Add("ManaSlider", new Slider ("Mana must be higher than [{0}%] to use Harass Spells", 40, 0, 100));

            HarassMenu.AddGroupLabel("Auto Harass");
            HarassMenu.Add("AutoQ", new CheckBox("- Use Q", false));
            HarassMenu.Add("AutoE", new CheckBox("- Use E ", false));
            HarassMenu.AddLabel("Autoharras casts spells from itself, when the enemy is in range");


            LaneClearMenu.AddGroupLabel("Lane Clear Settings");
            LaneClearMenu.Add("Q", new CheckBox("- Use Q"));
            LaneClearMenu.Add("E", new CheckBox("- Use E"));
            LaneClearMenu.Add("ManaSlider", new Slider("Mana must be higher than [{0}%] to use Harass Spells", 40, 0, 100));

            WMenu.AddGroupLabel("W Settings");
            WMenu.Add("W", new CheckBox("- Use W for myself"));
            WMenu.AddLabel("It will only use W if an enemy is in range");
            WMenu.Add("dangerSlider", new Slider ("HP must be lower that[{0}%] to use W", 80, 0 , 100));
            WMenu.Add("ManaSlider", new Slider("Mana must be higher than [{0}%] to use W", 40, 0, 100));
            WMenu.AddSeparator();
            WMenu.Add("WAlly", new CheckBox("- Use W for allies"));
            WMenu.Add("dangerSliderAlly", new Slider("HP from ally must be lower that[{0}%] to use W", 60, 0, 100));
            WMenu.Add("ManaSliderAlly", new Slider("Mana must be higher than [{0}%] to use W for allies", 40, 0, 100));

            KillStealMenu.AddGroupLabel("Killsteal Settings");
            KillStealMenu.Add("Q", new CheckBox("- Use Q"));
            KillStealMenu.Add("E", new CheckBox("- Use E"));
            KillStealMenu.Add("R", new CheckBox("- Use R", false));
            
            FleeMenu.AddGroupLabel("Flee Settings");
            FleeMenu.Add("Q", new CheckBox("- Use Q"));
            FleeMenu.Add("E", new CheckBox("- Use E"));
            FleeMenu.AddLabel("Press T and it will use Q and/or E ");

            MiscMenu.AddGroupLabel("Misc");
            MiscMenu.Add("Interrupt", new CheckBox("- Interrupt"));
            MiscMenu.Add("Gapcloser", new CheckBox("- Gapcloser"));
            MiscMenu.AddGroupLabel("Skin Changer");

            var skinList = SkinsDB.FirstOrDefault(list => list.Champ == Player.Instance.Hero);
            if (skinList != null)
            {
                MiscMenu.Add("SkinComboBox", new ComboBox("Choose the skin", skinList.Skins));
                MiscMenu.Get<ComboBox>("skinComboBox").OnValueChange +=
                    delegate (ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
                    {
                        Player.Instance.SetSkinId(sender.CurrentValue);
                    };
            }

            DrawingsMenu.AddGroupLabel("Setting");
            DrawingsMenu.Add("readyDraw", new CheckBox(" - Draw Spell Range only if Spell is Ready."));
            DrawingsMenu.Add("damageDraw", new CheckBox(" - Draw Damage Indicator."));
            DrawingsMenu.Add("perDraw", new CheckBox(" - Draw Damage Indicator Percent."));
            DrawingsMenu.Add("statDraw", new CheckBox(" - Draw Damage Indicator Statistics.", false));
            DrawingsMenu.AddGroupLabel("Spells");
            DrawingsMenu.Add("readyDraw", new CheckBox(" - Draw Spell Range only if Spell is Ready."));
            DrawingsMenu.Add("qDraw", new CheckBox("- draw Q"));
            DrawingsMenu.Add("wDraw", new CheckBox("- draw W"));
            DrawingsMenu.Add("eDraw", new CheckBox("- draw E"));
            DrawingsMenu.Add("rDraw", new CheckBox("- draw R"));
            DrawingsMenu.AddLabel("It will only draw if ready");
            DrawingsMenu.AddGroupLabel("Drawings Color");
            QColorSlide = new ColorSlide(DrawingsMenu, "qColor", Color.CornflowerBlue, "Q Color:");
            WColorSlide = new ColorSlide(DrawingsMenu, "wColor", Color.White, "W Color:");
            EColorSlide = new ColorSlide(DrawingsMenu, "eColor", Color.Coral, "E Color:");
            RColorSlide = new ColorSlide(DrawingsMenu, "rColor", Color.Red, "R Color:");
            DamageIndicatorColorSlide = new ColorSlide(DrawingsMenu, "healthColor", Color.Gold,
                "DamageIndicator Color:");

            MiscMenu.AddGroupLabel("Auto Level UP");
            MiscMenu.Add("activateAutoLVL", new CheckBox("Activate Auto Leveler", false));
            MiscMenu.AddLabel("The Auto Leveler will always Focus R than the rest of the Spells");
            MiscMenu.Add("firstFocus", new ComboBox("1 Spell to Focus", new List<string> { "Q", "W", "E" }));
            MiscMenu.Add("secondFocus", new ComboBox("2 Spell to Focus", new List<string> { "Q", "W", "E" }, 1));
            MiscMenu.Add("thirdFocus", new ComboBox("3 Spell to Focus", new List<string> { "Q", "W", "E" }, 2));
            MiscMenu.Add("delaySlider", new Slider("Delay Slider", 200, 150, 500));
        }
    }
}
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using static Wladis_Gragas.Skins;

namespace Wladis_Gragas
{
    internal class Menus
    {
        public static SharpDX.Vector3 insecpos, eqpos, movingawaypos, qpos;
        public const string DrawingsMenuId = "drawingsmenuid";
        public const string MiscMenuId = "miscmenuid";
        public static Menu FirstMenu;
        public static Menu DrawingsMenu;
        public static Menu ComboMenu;
        public static Menu HarassMenu;
        public static Menu LaneClearMenu;
        public static Menu JungleClearMenu;
        public static Menu MiscMenu;
        public static Menu KillStealMenu;

        public static ColorSlide QColorSlide;
        public static ColorSlide WColorSlide;
        public static ColorSlide EColorSlide;
        public static ColorSlide RColorSlide;
        public static ColorSlide DamageIndicatorColorSlide;

        public static void CreateMenu()
        {

            FirstMenu = MainMenu.AddMenu("Wladis " + Player.Instance.ChampionName,
                Player.Instance.ChampionName.ToLower() + "Gragas");
            ComboMenu = FirstMenu.AddSubMenu("• Combo ");
            HarassMenu = FirstMenu.AddSubMenu("• Harass");
            LaneClearMenu = FirstMenu.AddSubMenu("• LaneClear");
            JungleClearMenu = FirstMenu.AddSubMenu("• JungleClear");
            KillStealMenu = FirstMenu.AddSubMenu("• Killsteal");
            DrawingsMenu = FirstMenu.AddSubMenu("• Drawings", DrawingsMenuId);
            MiscMenu = FirstMenu.AddSubMenu("• Misc", MiscMenuId);


            ComboMenu.AddGroupLabel("Combo Settings");
            ComboMenu.Add("Q", new CheckBox("- Use Q"));
            ComboMenu.Add("W", new CheckBox("- Use W"));
            ComboMenu.Add("E", new CheckBox("- Use E"));
            ComboMenu.Add("R", new CheckBox("- Use R"));
            ComboMenu.AddLabel("Press T for a insec R ");
            ComboMenu.AddSeparator();
            ComboMenu.Add("ComboLogic", new ComboBox(" Combo Logic ", 0, "E>Q>W", "Q>E>W"));
            ComboMenu.AddSeparator();
            ComboMenu.Add("EDon't", new CheckBox("- Don't use E if enemy is in AA range", false));
            ComboMenu.AddSeparator();
            ComboMenu.Add("RLogic", new ComboBox(" R Logic ", 0, "R insec", "Normal R"));
            ComboMenu.AddSeparator();
            ComboMenu.Add("Ignite", new CheckBox("- Use Ignite", false));
            ComboMenu.AddLabel("It will only use ignite, when the enemy isn't killable with Combo");
            ComboMenu.AddSeparator(15);
            ComboMenu.Add("IgniteHealth", new Slider("- Ignite if enemy Hp % < Slider %", 40, 1, 100));

            HarassMenu.AddGroupLabel("Harass Settings");
            HarassMenu.Add("Q", new CheckBox("- Use Q"));
            HarassMenu.AddSeparator();
            HarassMenu.Add("E", new CheckBox("- Use W"));

            HarassMenu.AddGroupLabel("Auto Harass");
            HarassMenu.Add("AutoQ", new CheckBox("- Use Q", false));
            HarassMenu.Add("AutoW", new CheckBox("- Use W", false));
            HarassMenu.AddLabel("Autoharras casts spells from itself, when the enemy is in range");


            LaneClearMenu.AddGroupLabel("Lane Clear Settings");
            LaneClearMenu.Add("Q", new CheckBox("- Use Q"));
            LaneClearMenu.Add("W", new CheckBox("- Use W"));
            LaneClearMenu.Add("E", new CheckBox("- Use E", false));
            LaneClearMenu.Add("ManaSliderLaneClear", new Slider("- Don't use LaneClear when mana % < X", 40, 1, 100));
            LaneClearMenu.AddSeparator();
            LaneClearMenu.Add("QX", new Slider("- Will hit x minions with Q", 3, 1, 6));
            LaneClearMenu.AddSeparator();
            LaneClearMenu.Add("WX", new Slider("- Will hit x minions with W", 2, 1, 6));
            LaneClearMenu.AddSeparator();
            LaneClearMenu.Add("EX", new Slider("- Will hit x minions with E", 2, 1, 6));

            JungleClearMenu.AddGroupLabel("Jungle clear Settings");
            JungleClearMenu.Add("Q", new CheckBox("- Use Q"));
            JungleClearMenu.Add("W", new CheckBox("- Use W"));
            JungleClearMenu.Add("E", new CheckBox("- Use E"));
            JungleClearMenu.Add("ManaSliderJungleClear", new Slider("- Don't use JungleClear when mana % < X", 30, 1, 100));


            KillStealMenu.AddGroupLabel("Killsteal Settings");
            KillStealMenu.Add("Q", new CheckBox("- Use Q"));
            KillStealMenu.Add("E", new CheckBox("- Use E"));
            KillStealMenu.Add("R", new CheckBox("- Use R"));

            MiscMenu.AddGroupLabel("Misc");
            MiscMenu.Add("Z", new CheckBox("- Use Zhonyas"));
            MiscMenu.AddSeparator(15);
            MiscMenu.Add("Zhealth", new Slider("- Health % for Zhonyas", 20, 0, 100));
            MiscMenu.AddSeparator(15);
            MiscMenu.Add("EInterrupt", new CheckBox("- Interrupt with E"));
            MiscMenu.Add("RInterrupt", new CheckBox("- Interrupt with R"));
            MiscMenu.AddSeparator(15);
            MiscMenu.Add("EGapCloser", new CheckBox("- GapClose with E"));
            MiscMenu.Add("RGapCloser", new CheckBox("- GapClose with R"));
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
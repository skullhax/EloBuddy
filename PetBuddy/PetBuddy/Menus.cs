using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBuddy
{
    internal class Menus
    {
        public static Menu FirstMenu;



        public static void CreateMenu()
        {

            FirstMenu = MainMenu.AddMenu("Score " + "Buddy", Player.Instance.ChampionName.ToLower() + "Buddy");

            FirstMenu.AddGroupLabel("Settings");
            FirstMenu.Add("ScoreVisible", new CheckBox("- Toggle score visible"));
            FirstMenu.Add("NewStart", new CheckBox("- Start from new", false));
            FirstMenu.Add("NewStartt", new Slider("- Are you really sure????", 1, 1, 100));
            FirstMenu.AddLabel("If you are, slide to the value 76");
            FirstMenu.AddSeparator();
            FirstMenu.Add("Safe", new CheckBox("Safe manuelly", false));
            FirstMenu.Add("SafeOnKill", new CheckBox("Safe when killing enemy"));
            FirstMenu.Add("Key", new KeyBind("100 xp", false, KeyBind.BindTypes.HoldActive, 'G'));
        }
    }
}

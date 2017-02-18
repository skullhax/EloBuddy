using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetBuddy
{
    internal class Menus
    {
        public static Menu FirstMenu;
        public static Menu SettingsMenu;
        public static Menu PetMenu;



        public static void CreateMenu()
        {

            FirstMenu = MainMenu.AddMenu("Pet " + "Buddy", Player.Instance.ChampionName.ToLower() + "Buddy");
            SettingsMenu = FirstMenu.AddSubMenu("• Settings");
            PetMenu = FirstMenu.AddSubMenu("• Pet");

            SettingsMenu.AddGroupLabel("Settings");
            SettingsMenu.Add("PetVisible", new CheckBox("- Toggle pet visible"));
            SettingsMenu.Add("NewStart", new CheckBox("- Start from new", false));
            SettingsMenu.Add("NewStartt", new Slider("- Are you really sure????", 1, 1, 100));
            SettingsMenu.AddLabel("If you are, slide to the value 76");
            SettingsMenu.AddSeparator();
            SettingsMenu.Add("Safe", new CheckBox("Safe manuelly", false));
            SettingsMenu.Add("SafeOnKill", new CheckBox("Safe when killing enemy"));
            SettingsMenu.Add("Key", new KeyBind("100 xp", false, KeyBind.BindTypes.HoldActive, 'G'));

            PetMenu.AddGroupLabel("Your Pet");
            PetMenu.AddLabel("Choose your pet's class");
            PetMenu.AddLabel("You can choose it only 1 time");
            var a = PetMenu.Add("Warrior", new CheckBox("Warrior", false));
            var b = PetMenu.Add("Marksman", new CheckBox("Marksman", false));
            var c = PetMenu.Add("Ninja", new CheckBox("Ninja", false));
            var d = PetMenu.Add("Mage", new CheckBox("Mage", false));

            if (a.CurrentValue)
            {
                b.CurrentValue = false;
                c.CurrentValue = false;
                d.CurrentValue = false;
            }
            if (b.CurrentValue)
            {
                c.CurrentValue = false;
                a.CurrentValue = false;
                d.CurrentValue = false;
            }
            if (c.CurrentValue)
            {
                b.CurrentValue = false;
                a.CurrentValue = false;
                d.CurrentValue = false;
            }
            if (d.CurrentValue)
            {
                b.CurrentValue = false;
                a.CurrentValue = false;
                c.CurrentValue = false;
            }

            PetMenu.AddGroupLabel("Your Skills");
            //PetMenu.AddLabel(Pet.PetSkill);

        }
    }
}

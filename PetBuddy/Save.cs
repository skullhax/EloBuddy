using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.IO;
using SharpDX;
using EloBuddy;
using EloBuddy.Sandbox;
using EloBuddy.SDK.Menu.Values;

namespace PetBuddy
{
    public class Save
    {
        //File name setup for saving
        public static string FileName;
        public static readonly string ConfigFolderPath = Path.Combine(SandboxConfig.DataDirectory);

        public static void SaveData()
        {
            //Grab data from text file else create it
            FileName = "PetBuddy.txt";
            if (!Directory.Exists(ConfigFolderPath + @"\Data\PetBuddy"))
            {
                Directory.CreateDirectory(ConfigFolderPath + @"\Data\PetBuddy");
                FirstRun();

            }
            //else read the save
            else
            {
                ReadSave();

            }
        }
        //Used to read data
        public static void ReadSave()
        {
            string LvlStr = null;
            string XpStr = null;
            string neededXPStr = null;
            string DmgStr = null;
            string HpStr = null;

            using (var sr = new StreamReader(ConfigFolderPath + @"\Data\PetBuddy" + FileName, true))
            {
                string line;
                int currentLineNumber = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    switch (++currentLineNumber)
                    {
                        case 1:
                            Pet.PetName = line;
                            break;
                        case 2:
                            LvlStr = line;
                            break;
                        case 3:
                            XpStr = line;
                            break;
                        case 4:
                            neededXPStr = line;
                            break;
                        case 5:
                            DmgStr = line;
                            break;
                        case 6:
                            HpStr = line;
                            break;
                        case 7:
                            Pet.PetSkill = line;
                            break;
                    }
                }
                ConvertString(LvlStr, XpStr, neededXPStr, DmgStr, HpStr);
            }
        }

        //Used to save data
        public static void SaveData(string lvl, string currxp, string neededXP, string Dmg, string Hp)
        {
            File.WriteAllText(ConfigFolderPath + @"\Data\PetBuddy" + FileName, Pet.PetName + "\n");
            using (var file = new StreamWriter(ConfigFolderPath + @"\Data\PetBuddy" + FileName, true))
            {
                file.WriteLine(lvl);
                file.WriteLine(currxp);
                file.WriteLine(neededXP);
                file.WriteLine(Dmg);
                file.WriteLine(Hp);
                file.Close();
            }
        }

        public static void FirstRun()
        {
            RandomName();
            Pet.Lvl = 1;
            Pet.XP = 0;
            Pet.needXP = 100;
            Pet.Dmg = 10;
            Pet.Hp = 50;
            ConvertInt(Pet.Lvl, Pet.XP, Pet.needXP, Pet.Dmg, Pet.Hp);
        }

        //Name Gen Stuff
        public static void RandomName()
        {
            //Random Name Gen
            string[] NameDatabase1 = { "Ba", "Bax", "Dan", "Fi", "Fix", "Fiz", "Gi", "Gix", "Giz", "Gri", "Gree", "Greex", "Grex", "Ja", "Jax", "Jaz", "Jex", "Ji", "Jix", "Ka", "Kax", "Kay", "Kaz", "Ki", "Kix", "Kiz", "Klee", "Kleex", "Kwee", "Kweex", "Kwi", "Kwix", "Kwy", "Ma", "Max", "Ni", "Nix", "No", "Nox", "Qi", "Rez", "Ri", "Ril", "Rix", "Riz", "Ro", "Rox", "So", "Sox", "Vish", "Wi", "Wix", "Wiz", "Za", "Zax", "Ze", "Zee", "Zeex", "Zex", "Zi", "Zix", "Zot" };
            string[] NameDatabase2 = { "b", "ba", "be", "bi", "d", "da", "de", "di", "e", "eb", "ed", "eg", "ek", "em", "en", "eq", "ev", "ez", "g", "ga", "ge", "gi", "ib", "id", "ig", "ik", "im", "in", "iq", "iv", "iz", "k", "ka", "ke", "ki", "m", "ma", "me", "mi", "n", "na", "ni", "q", "qa", "qe", "qi", "v", "va", "ve", "vi", "z", "za", "ze", "zi", "", "", "", "", "", "", "", "", "", "", "", "", "" };
            string[] NameDatabase3 = { "ald", "ard", "art", "az", "azy", "bit", "bles", "eek", "eka", "et", "ex", "ez", "gaz", "geez", "get", "giez", "iek", "igle", "ik", "il", "in", "ink", "inkle", "it", "ix", "ixle", "lax", "le", "lee", "les", "lex", "lyx", "max", "maz", "mex", "mez", "mix", "miz", "mo", "old", "rax", "raz", "reez", "rex", "riez", "tee", "teex", "teez", "to", "uek", "x", "xaz", "xeez", "xik", "xink", "xiz", "xonk", "yx", "zeel", "zil" };

            Random RandName = new Random();
            string Temp = NameDatabase1[RandName.Next(0, NameDatabase1.Length)] + NameDatabase2[RandName.Next(0, NameDatabase2.Length)] + NameDatabase3[RandName.Next(0, NameDatabase3.Length)];
            Pet.PetName = Temp;
        }
        

        public static void NewPet()
        {
            if (Menus.SettingsMenu["NewStart"].Cast<CheckBox>().CurrentValue && Menus.SettingsMenu["NewStartt"].Cast<Slider>().CurrentValue == 76)
            {
                FirstRun();
                Chat.Print("PetBuddy: New Pet Created!", System.Drawing.Color.Violet);
                Menus.SettingsMenu["NewStart"].Cast<CheckBox>().CurrentValue = false;
            }
        }

        public static void ManualSave()
        {
            if (Menus.SettingsMenu["Safe"].Cast<CheckBox>().CurrentValue)
            {
                Chat.Print("PetBuddy: Saving...", System.Drawing.Color.Violet);
                ConvertInt(Pet.Lvl, Pet.XP, Pet.needXP, Pet.Dmg, Pet.Hp);
                Chat.Print("Petbuddy: Progress Saved!", System.Drawing.Color.Violet);
                Menus.SettingsMenu["Safe"].Cast<CheckBox>().CurrentValue = !true ;
            }

        }

        public static void ConvertInt(int lvl, int XP, int needXP, int Dmg, int Hp)
        {
            string level = Pet.Lvl.ToString();
            string currentXP = Pet.XP.ToString();
            string neededXP = Pet.needXP.ToString();
            string Damage = Pet.Dmg.ToString();
            string HealthPoints = Pet.Hp.ToString();
            Save.SaveData(level, currentXP, neededXP, Damage, HealthPoints);
        }

        public static void ConvertString(string lvl, string XP, string needXP, string Dmg, string Hp)
        {

            int level = int.Parse(lvl);
            int currentXP = int.Parse(XP);
            int neededXP = int.Parse(needXP);
            int Damage = int.Parse(Dmg);
            int HealthPoints = int.Parse(Hp);

            Pet.Lvl = level;
            Pet.XP = currentXP;
            Pet.needXP = neededXP;
            Pet.Dmg = Damage;
            Pet.Hp = HealthPoints;
        }

    }
}

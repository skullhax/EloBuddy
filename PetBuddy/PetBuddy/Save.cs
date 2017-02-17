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

namespace ScoreBuddy
{
    public class Save
    {
        //File name setup for saving
        public static string FileName;
        public static readonly string ConfigFolderPath = Path.Combine(SandboxConfig.DataDirectory);

        public static void SaveData()
        {
            //Grab data from text file else create it
            FileName = "QGS.txt";
            if (!Directory.Exists(ConfigFolderPath + @"\Data\QGS"))
            {
                Directory.CreateDirectory(ConfigFolderPath + @"\Data\QGS");
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
            string TotalScore = null;

            using (var sr = new StreamReader(ConfigFolderPath + @"\Data\QGS" + FileName, true))
            {
                string line;
                int currentLineNumber = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    switch (++currentLineNumber)
                    {
                        case 1:
                            LvlStr = line;
                            break;
                        case 2:
                            XpStr = line;
                            break;
                        case 3:
                            neededXPStr = line;
                            break;
                        case 4:
                            TotalScore = line;
                            break;
                    }
                }
                ConvertString(LvlStr, XpStr, neededXPStr, TotalScore);
            }
        }

        //Used to save data
        public static void SaveData(string lvl, string currxp, string neededXP, string TotalScore)
        {
            File.WriteAllText(ConfigFolderPath + @"\Data\QGS" + FileName, Score.TScore + "\n");
            using (var file = new StreamWriter(ConfigFolderPath + @"\Data\QGS" + FileName, true))
            {
                file.WriteLine(lvl);
                file.WriteLine(currxp);
                file.WriteLine(neededXP);
                file.WriteLine(TotalScore);
                file.Close();
            }
        }

        public static void FirstRun()
        {
            Score.Lvl = 1;
            Score.XP = 0;
            Score.needXP = 50;
            Score.TScore = 0;
            ConvertInt(Score.Lvl, Score.XP, Score.needXP, Score.TScore);
        }
        
        public static void NewScore()
        {
            if (Menus.FirstMenu["NewStart"].Cast<CheckBox>().CurrentValue && Menus.FirstMenu["NewStartt"].Cast<Slider>().CurrentValue == 76)
            {
                FirstRun();
                Chat.Print("ScoreBuddy: Started from new!", System.Drawing.Color.Violet);
                Menus.FirstMenu["NewStart"].Cast<CheckBox>().CurrentValue = false;
            }
        }

        public static void ManualSave()
        {
            if (Menus.FirstMenu["Safe"].Cast<CheckBox>().CurrentValue)
            {
                Chat.Print("ScoreBuddy: Saving...", System.Drawing.Color.Violet);
                ConvertInt(Score.Lvl, Score.XP, Score.needXP, Score.TScore);
                Chat.Print("ScoreBuddy: Progress Saved!", System.Drawing.Color.Violet);
                Menus.FirstMenu["Safe"].Cast<CheckBox>().CurrentValue = !true ;
            }

        }

        public static void ConvertInt(int lvl, int XP, int needXP, int TScore)
        {
            string level = Score.Lvl.ToString();
            string currentXP = Score.XP.ToString();
            string neededXP = Score.needXP.ToString();
            string TotalScore = Score.TScore.ToString();
            Save.SaveData(level, currentXP, neededXP, TotalScore);
        }

        public static void ConvertString(string lvl, string XP, string needXP, string TScore)
        {

            int level = int.Parse(lvl);
            int currentXP = int.Parse(XP);
            int neededXP = int.Parse(needXP);
            int TotalScore = int.Parse(TScore);

            Score.Lvl = level;
            Score.XP = currentXP;
            Score.needXP = neededXP;
            Score.TScore = TotalScore;
        }

    }
}

using System;
using System.Linq;
using System.Media;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;
using PetBuddy.Properties;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;


namespace ScoreBuddy
{
    internal class Program
    {

        public static AIHeroClient myhero
        {
            get { return ObjectManager.Player; }
        }

        // ReSharper disable once UnusedParameter.Local
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        
        public static readonly TextureLoader TextureLoader = new TextureLoader();

        
        public static void Loading_OnLoadingComplete(EventArgs args)
        {
            /*var Bot = Bots();
            if (Bot)
            {
                Chat.Print("Scorebuddy disabled for Bots!");
                return;
            }*/

            Save.SaveData();
            Chat.Print("Scorebuddy loaded!", System.Drawing.Color.Red);
            Chat.Say("Just die on cancer @All");

            ScoreBuddy.Menus.CreateMenu();
            //Game.OnNotify += Game_OnNotify;
            //Obj_AI_Base.OnPlayAnimation += Obj_AI_Base_OnPlayAnimation;
            Drawing.OnDraw += Drawing_OnDraw;
            Game.OnTick += Game_OnTick;
            Game.OnUpdate += Score.Game_OnUpdate;
            Game.OnEnd += Game_OnEnd;
            Game.OnNotify += Score.OnGameNotify;


        }
        private static void Game_OnEnd(EventArgs args)
        {
            Score.XP += (50);
            Score.TScore += (50);
            Save.ConvertInt(Score.Lvl, Score.XP, Score.needXP, Score.TScore);
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (Menus.FirstMenu["Key"].Cast<KeyBind>().CurrentValue)
            {
                Score.XP += (1);
                Score.GameScore += (1);
                Score.TScore += (1);
            }
        }

        private static void Drawing_OnDraw(EventArgs args)
        {

                var pos = new Vector2(1700, 130);

            if (Menus.FirstMenu["ScoreVisible"].Cast<CheckBox>().CurrentValue)
            {
                var xpos = 1750;
                var ypos = 320;
                Drawing.DrawText(xpos, ypos + 20, System.Drawing.Color.Gold, "Game score: " + Score.GameScore);
                Drawing.DrawText(xpos, ypos + 40, System.Drawing.Color.Gold, "XP: " + Score.XP + "/" + Score.needXP);
                Drawing.DrawText(xpos, ypos + 60, System.Drawing.Color.Gold, "Level: " + Score.Lvl);
                Drawing.DrawText(xpos, ypos + 80, System.Drawing.Color.Gold, "Total score: " + Score.TScore);
            }

        }

       

        public static bool Bots()
        {
            var CountBots = 0;
            var bot = false;

            if (EntityManager.Heroes.AllHeroes.Count < 3)
            {
                bot = true;
            }
            else
            {
                foreach (var n in EntityManager.Heroes.AllHeroes)
                {
                    if (n.Name.Contains(" Bot"))
                        CountBots++;
                }
                if (CountBots > 1)
                {
                    bot = true;
                }
            }
            return bot;
        }
    }
}
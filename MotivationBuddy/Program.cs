using System;
using System.Linq;
using System.Media;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using static MotivationBuddy.Menus;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;


namespace MotivationBuddy
{
    internal class Program
    {

        public static AIHeroClient myhero
        {
            get { return ObjectManager.Player; }
        }
        
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }
        


        public static void Loading_OnLoadingComplete(EventArgs args)
        {
            Chat.Print("Motivation buddy loaded!", System.Drawing.Color.Violet);
            Chat.Say("/all Good luck and have Fun!");

            Menus.CreateMenu();
            Game.OnTick += Game_OnTick;
            Game.OnNotify += OnGameNotify;
            Game.OnEnd += Game_OnEnd;
        }

        private static void Game_OnEnd(EventArgs args)
        {
            Chat.Say("Good game, I had much fun!");
        }

        private static void Game_OnTick(EventArgs args)
        {
        }

        internal static void OnGameNotify(GameNotifyEventArgs args)
        {
            var Sender = args.NetworkId;

            var Ally = EntityManager.Heroes.Allies.FirstOrDefault(e => e.HealthPercent > 20);
            var AllyD = EntityManager.Heroes.Allies.FirstOrDefault(e => e.HealthPercent < 30);
            var AllyK = EntityManager.Heroes.Allies.LastOrDefault();

            if (FirstMenu["EnableM"].Cast<CheckBox>().CurrentValue)
            {
                switch (args.EventId)
                {
                    case GameEventId.OnChampionKill:
                        if ((Sender == AllyK.NetworkId || Sender == AllyD.NetworkId ) && Sender != myhero.NetworkId)
                        {
                            string[] Motivation1 = { "Good Job!", "Nice man", "really nice", "well done", "well played" };

                            Random RandName = new Random();
                            string Temp1 = Motivation1[RandName.Next(0, Motivation1.Length)];

                            Chat.Say(Temp1);
                        }
                        break;
                    case GameEventId.OnChampionDie:
                        if ((Sender == AllyD.NetworkId || Sender == AllyK.NetworkId) && Sender != myhero.NetworkId)
                        {
                            string[] Motivation2 = { "Next time you get him!", "Nice try, next time maybe", "Don't get greedy", "Be less agressive", "Don't lose motivation", "Don't give up" };

                            Random RandName = new Random();
                            string Temp2 = Motivation2[RandName.Next(0, Motivation2.Length)];

                            Chat.Say(Temp2);
                        }
                        break;
                }
            }
            if (FirstMenu["EnableT"].Cast<CheckBox>().CurrentValue)
            {
                var Enemy = EntityManager.Heroes.Enemies.LastOrDefault(e => e.HealthPercent < 30 && !e.IsDead);
                var EnemyD = EntityManager.Heroes.Enemies.FirstOrDefault(e => !e.IsDead);
                var EnemyDD = EntityManager.Heroes.Enemies.First();
                var EnemyDDD = EntityManager.Heroes.Enemies.Last();





                switch (args.EventId)
                {
                    case GameEventId.OnChampionDie:
                        if (Sender == Enemy.NetworkId || Sender == EnemyD.NetworkId || Sender == EnemyDD.NetworkId || Sender == EnemyDDD.NetworkId || Sender != myhero.NetworkId)
                        {
                            string[] Tilt2 = { "/all You're bad", "/all You suck", "/all Nice try", "/all Go play against bots", "/all noob", "/all ez", "/All so bad", "/all learn 2 play", "/all hahahha", "/all bad", "/All rekt" };

                            Random RandName = new Random();
                            string Temp2 = Tilt2[RandName.Next(0, Tilt2.Length)];

                            Chat.Say(Temp2);
                        }
                        break;
                }
            }
        }
        


    }
}
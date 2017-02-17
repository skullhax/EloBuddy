using EloBuddy;
using System;
using static ScoreBuddy.Program;
using EloBuddy.SDK;
using System.Linq;
using EloBuddy.SDK.Menu.Values;

namespace ScoreBuddy
{
    class Score
    {
        
        private static int AllyD;
        public static int XP;
        public static int needXP;
        public static int Lvl;
        public static int Level;
        public static int TScore;
        public static int GameScore;
        public static int XPMulti = 1;

        public static void Game_OnUpdate(EventArgs args)
        {
            Save.NewScore();
            LevelUp();
            Save.ManualSave();
            //GainSkill();
        }
        

        public static void LevelUp()
        {

            if (XP >= needXP)
            {
                XP = (XP - needXP);
                needXP = (needXP * 120 / 100);
                Lvl++;
                Chat.Print("You have leveled up to level " + Score.Lvl, System.Drawing.Color.Violet);
            }
            
        }
        
        

        internal static void OnGameNotify(GameNotifyEventArgs args)
        {
            var Sender = args.NetworkId;

            switch (args.EventId) //Check for XP events
            {
                case GameEventId.OnChampionDoubleKill:

                    if (Sender == myhero.NetworkId)
                    {
                        Score.XP += (10);
                        Score.TScore += (10);
                        Score.GameScore += (10);
                    }
                    break;
                case GameEventId.OnChampionPentaKill:

                    if (Sender == myhero.NetworkId)
                    {
                        Score.XP += (100);
                        Score.TScore += (100);
                        Score.GameScore += (100);
                    }
                    break;
                case GameEventId.OnChampionQuadraKill:

                    if (Sender == myhero.NetworkId)
                    {
                        Score.XP += (30);
                        Score.TScore += (30);
                        Score.GameScore += (30);
                    }
                    break;
                case GameEventId.OnChampionTripleKill:

                    if (Sender == myhero.NetworkId)
                    {
                        Score.XP += (15);
                        Score.TScore += (15);
                        Score.GameScore += (15);
                    }
                    break;
                case GameEventId.OnAce:
                    
                    var aliveppl = myhero.CountEnemiesInRange(int.MaxValue) < 1;
                        if (aliveppl && !myhero.IsDead)
                        {
                        Score.XP += (20);
                        Score.TScore += (20);
                        Score.GameScore += (20);
                    }
                    break;
                case GameEventId.OnChampionKill:
                    
                    if (Sender == myhero.NetworkId)
                    {
                        Score.XP += (40);
                        Score.TScore += (40);
                        Score.GameScore += (40);
                    }

                    if (Menus.FirstMenu["SafeOnKill"].Cast<CheckBox>().CurrentValue)
                        Save.ConvertInt(Score.Lvl, Score.XP, Score.needXP, Score.TScore);

                    break;
                case GameEventId.OnFirstBlood:
                    if (Sender == myhero.NetworkId)
                    {
                        Score.XP += (20);
                        Score.TScore += (20);
                    }
                    break;
                case GameEventId.OnChampionLevelUp:
                    if (Sender == myhero.NetworkId)
                    {
                        Score.XP += (2);
                        Score.TScore += (2);
                        Score.GameScore += (2);
                    }
                    break;
                case GameEventId.OnKillDragon:
                    {
                        Score.XP += (10);
                        Score.TScore += (10);
                        Score.GameScore += (10);
                    }
                    break;
                case GameEventId.OnMinionKill:
                    if (Sender == myhero.NetworkId)
                    {
                        Score.XP += (1);
                        Score.TScore += (1);
                        Score.GameScore += (1);
                    }
                    break;
                case GameEventId.OnTurretKill:
                    if (Sender == myhero.NetworkId)
                    {
                        Score.XP += (30);
                        Score.TScore += (30);
                        Score.GameScore += (30);
                    }
                    break;
                    /*case GameEventId.OnKillDragon:

                    var Dragon =

                        if (EntityManager.MinionsAndMonsters.Get)
                        {

                        }*/
            }
        }
        

        public static void Obj_AI_Base_OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args)
        {
            if (sender.IsMe && (args.Buff.DisplayName.ToLower().Contains("hand of baron") || args.Buff.Name.ToLower().Contains("baron") || args.Buff.Name.ToLower().Contains("worm")))
            {
                BaronKilled();
            }

            if (sender.IsMe && (args.Buff.DisplayName.ToLower().Contains("DragonBuffEarth") || args.Buff.Name.ToLower().Contains("DragonBuffInfernal") || args.Buff.Name.ToLower().Contains("DragonBuffAir")))
            {
                DragonKilled();
            }

            if (sender.IsMe && (args.Buff.DisplayName.ToLower().Contains("BlessingoftheLizardElder")))
            {
                RedBuff();
            }

            if (sender.IsMe && (args.Buff.DisplayName.ToLower().Contains("CrestoftheAncientGolem")))
            {
                BlueBuff();
            }

        }

        private static void BlueBuff()
        {
            Score.XP += (20);
        }
        private static void RedBuff()
        {
            Score.XP += (20);
        }
        private static void DragonKilled()
        {
            Score.XP += (50);
        }

        private static void BaronKilled()
        {
            Score.XP += (70);
        }




    }
}
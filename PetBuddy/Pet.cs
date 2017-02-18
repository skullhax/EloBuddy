using EloBuddy;
using System;
using static PetBuddy.Program;
using EloBuddy.SDK;
using System.Linq;
using EloBuddy.SDK.Menu.Values;

namespace PetBuddy
{
    class Pet
    {
        
        private static int AllyD;
        public static int XP;
        public static int needXP;
        public static int Lvl;
        public static int Level;
        public static int Dmg;
        public static int Hp;
        public static int BonusXP;
        public static string PetName;
        public static string PetSkill;
        public static int XPMulti = 1;

        public static void Game_OnUpdate(EventArgs args)
        {
            Save.NewPet();
            LevelUp();
            Save.ManualSave();
            //GainSkill();
        }

        public static void Chat_OnClientSideMessage(ChatClientSideMessageEventArgs eventArgs)
        {
            if (eventArgs.Message.ToLower().Contains("Wladis") || eventArgs.Message.ToLower().Contains("Dark") || eventArgs.Message.ToLower().Contains("Fairy"))
                BonusXP = 5;

            else BonusXP = 5;

        }

        public static void LevelUp()
        {

            if (XP >= needXP)
            {
                XP = (XP - needXP);
                needXP = (needXP * 125 / 80);
                Dmg = (Dmg + 2);
                Lvl++;
                Chat.Print("Your pet leveled up to level " + Pet.Lvl, System.Drawing.Color.Violet);
            }

            if (Hp < 1)
            {
                Lvl--;
                needXP = (needXP * 80 / 120);
                XP = (0);
                Hp = 20;
                Dmg = (Dmg - 2);
                Chat.Print("Your pet died, Pet lvl -1 " , System.Drawing.Color.Violet);
            }
        }

        public static void SkillDmg()
        {

        }
        /*public static void GainSkill()
        {
            if (Pet.Lvl == 2 || Pet.Lvl == 3 && XP >= needXP)
                    Skills();
            Chat.Print("Your pet learned" + Pet.PetSkill, System.Drawing.Color.OrangeRed);
        }*/

        public static void Skills()
        {
            //Random Skill
            string[] Skills = { "Pound", "Karate Chop", "Double Slap", "Comet Punch", "Mega Punch", "Pay Day", "Fire Punch", "Ice Punch", "Thunder Punch", "Scratch", "	Vice Grip", "Guillotine", "Razor Wind", "Swords Dance", "Cut", "Gust", "Wing Attack", "Whirlwind", "Fly", "Bind", "Slam", "Vine Whip", "Stomp", "Double Kick", "Mega Kick", "Jump Kick", "Sand Attack", "Headbutt", "Explosion", "Lovely Kiss", "Waterfall", "Acid Armor", "Flame Wheel", "Sweet Kiss", "Sludge Bomb", "Icy Wind", "Bone Rush", "Sandstorm", "False Swipe", "Sleep Talk", "Heal Bell", "Megahorn", "Dragon Breath", "Metal Claw", "Morning Sun", "Buddy attak(OP)", "Crunch", "Whirlpool", "Heat Wave", "Taunt", "Ingrain", "Blaze Kick", "Hyper Voice", "Blast Burn", "Air Cutter", "Cosmic Power", "Bullet Seed", "Mud Shot", "Poison Tail", "Rock Blast", "Power Trick", "Air Slash" };

            Random RandName = new Random();
            string Skillname = Skills[RandName.Next(0, Skills.Length)];
            Pet.PetSkill = Skillname;
        }

        internal static void OnGameNotify(GameNotifyEventArgs args)
        {
            var Sender = args.NetworkId;

            switch (args.EventId) //Check for XP events
            {
                case GameEventId.OnChampionDoubleKill:

                    if (Sender == myhero.NetworkId)
                    {
                            Pet.XP += (10) + BonusXP;

                    }
                    break;
                case GameEventId.OnChampionPentaKill:

                    if (Sender == myhero.NetworkId)
                    {
                            Pet.XP += (100) + BonusXP;

                    }
                    break;
                case GameEventId.OnChampionQuadraKill:

                    if (Sender == myhero.NetworkId)
                    {
                            Pet.XP += (30) + BonusXP;
                    }
                    break;
                case GameEventId.OnChampionTripleKill:

                    if (Sender == myhero.NetworkId)
                    {
                            Pet.XP += (15) + BonusXP;
                    }
                    break;
                case GameEventId.OnAce:
                    
                    var aliveppl = myhero.CountEnemiesInRange(int.MaxValue) < 1;
                        if (aliveppl && !myhero.IsDead)
                        {
                            Pet.XP += (20) + BonusXP;
                        }
                    break;
                case GameEventId.OnChampionKill:
                    
                    if (Sender == myhero.NetworkId)
                        Pet.XP += (40) + BonusXP;
                    Pet.Hp += (1);

                    if (Menus.SettingsMenu["SafeOnKill"].Cast<CheckBox>().CurrentValue)
                        Save.ConvertInt(Pet.Lvl, Pet.XP, Pet.needXP, Pet.Dmg, Pet.Hp);

                    break;
                case GameEventId.OnFirstBlood:
                    if (Sender == myhero.NetworkId)
                        Pet.XP += (20) + BonusXP;

                    break;
                case GameEventId.OnChampionDie:

                    if (Sender == myhero.NetworkId)
                    Pet.Hp += (-1) + BonusXP;
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
            Pet.XP += (20) + BonusXP;
        }
        private static void RedBuff()
        {
            Pet.XP += (20) + BonusXP;
        }
        private static void DragonKilled()
        {
            Pet.XP += (50) + BonusXP;
        }

        private static void BaronKilled()
        {
            Pet.XP += (70) + BonusXP;
        }




    }
}
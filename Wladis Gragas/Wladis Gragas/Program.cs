using System;
using EloBuddy;
using EloBuddy.SDK.Events;
using EloBuddy.SDK;

namespace Wladis_Gragas
{
    internal class Loader
    {
        private static bool _lockedSpellcasts;

        public static bool LockedSpellCasts
        {
            get { return _lockedSpellcasts; }
            set
            {
                _lockedSpellcasts = value;
                if (value)
                {
                    _lockedTime = Core.GameTickCount;
                }
            }
        }

        private static int _lockedTime;

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs bla)
        {
            if (Player.Instance.Hero != Champion.Gragas) return;
            Menus.CreateMenu();
            SpellsManager.InitializeSpells();
            ModeManager.InitializeModes();
            DrawingsManager.InitializeDrawings();

            Obj_AI_Base.OnProcessSpellCast += delegate (Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
            {
                    if (sender.IsMe && (int)args.Slot < 4)
                    {
                        LockedSpellCasts = true;
                    }
            };

            Obj_AI_Base.OnSpellCast += delegate (Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
            {
                    if (sender.IsMe && (int)args.Slot < 4)
                    {
                        LockedSpellCasts = false;
                    }
            };
            Game.OnTick += delegate
            {
                    if (_lockedTime > 0 && LockedSpellCasts && Core.GameTickCount - _lockedTime > 250)
                    {
                        LockedSpellCasts = false;
                    }
            };

            

            Chat.Print("<font color='#FA5858'>Wladis Gragas loaded</font>");
        }
    }
}
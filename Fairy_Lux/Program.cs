using System;
using EloBuddy;
using EloBuddy.SDK.Events;

namespace Fairy_Lux
{
    internal class Loader
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.Hero != Champion.Lux) return;
            SpellsManager.InitializeSpells();
            Menus.CreateMenu();
            ModeManager.InitializeModes();
            DrawingsManager.InitializeDrawings();

            Chat.Print("Fairy Lux Loaded!", System.Drawing.Color.Red);
            Chat.Print("Credits to ExRaZor and Tarakan");
        }
    }
}
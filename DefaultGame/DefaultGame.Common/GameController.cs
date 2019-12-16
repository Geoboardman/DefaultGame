using DefaultGame.Common.Scenes;
using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Locations;

namespace DefaultGame.Common
{
    public static class GameController
    {
        public static CCGameView GameView
        {
            get;
            private set;
        }

        public static EventHandler<LocationChangedEventArgs> OnLocationChanged;

        public static void TriggerLocationChange(Location loc)
        {
            if (OnLocationChanged != null)
                OnLocationChanged(typeof(GameController), new LocationChangedEventArgs(loc));
        }

        public class LocationChangedEventArgs : EventArgs
        {
            public LocationChangedEventArgs(Location iloc)
            { location = iloc; }
            public Location location { get; set; }
        }

        public static void Initialize(CCGameView gameView)
        {
            GameView = gameView;

            var contentSearchPaths = new List<string>() { "Fonts", "Sounds" };

#if __IOS__
            contentSearchPaths.Add("Sounds/iOS/");

#else // android
            contentSearchPaths.Add("Sounds/Android/");


#endif

            contentSearchPaths.Add("Images");
            GameView.ContentManager.SearchPaths = contentSearchPaths;

            // We use a lower-resolution display to get a pixellated appearance
            int width = 384;
            int height = 512;
            GameView.DesignResolution = new CCSizeI(width, height);

            var scene = new TitleScene(GameView);
            GameView.RunWithScene(scene);
        }

        public static void GoToScene(CCScene scene)
        {
            GameView.Director.ReplaceScene(scene);
        }
    }
}

using Android.Locations;
using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultGame.Common
{
    public class GameScene : CCScene
    {
        CCLayer backgroundLayer;
        CCLayer gameplayLayer;
        CCLayer foregroundLayer;
        CCLayer hudLayer;
        bool hasGameEnded;
        CCLabel debugLabel;
        Location location = null;
        string locationString = "GPS Location";

        public GameScene(CCGameView gameView) : base(gameView)
        {
            hasGameEnded = false;
            GameController.OnLocationChanged += HandleLocationChanged;
            CreateLayers();
            debugLabel = new CCLabel(locationString, "Arial", 10, CCLabelFormat.SystemFont);
            debugLabel.PositionX = backgroundLayer.ContentSize.Width / 2.0f;
            debugLabel.PositionY = backgroundLayer.ContentSize.Height / 1.25f;
            backgroundLayer.AddChild(debugLabel);
            Schedule(Activity);
        }

        public void HandleLocationChanged(object sender, GameController.LocationChangedEventArgs e)
        {
            this.location = e.location;
            if(debugLabel != null)
                debugLabel.Text = "Lat: " + location.Latitude + " Long: " + location.Longitude;
        }

        private void CreateLayers()
        {
            backgroundLayer = new CCLayer();
            this.AddLayer(backgroundLayer);

            gameplayLayer = new CCLayer();
            this.AddLayer(gameplayLayer);

            foregroundLayer = new CCLayer();
            this.AddLayer(foregroundLayer);

            hudLayer = new CCLayer();
            this.AddLayer(hudLayer);
        }

        private void Activity(float frameTimeInSeconds)
        {
            if (hasGameEnded == false)
            {

            }
        }
    }
}

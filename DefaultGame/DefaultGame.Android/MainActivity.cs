using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;
using Android.Content;

using CocosSharp;
using DefaultGame.Common;

namespace DefaultGame.Android
{
	[Activity (Label = "DefaultGame.Android", 
		MainLauncher = true, 
		Icon = "@drawable/icon", 
		AlwaysRetainTaskState = true,
		ScreenOrientation = ScreenOrientation.Portrait,
		LaunchMode = LaunchMode.SingleInstance,
		ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden)]
	public class MainActivity : Activity
	{
        Location location = null;
        LocationListener locationListener = null;

        class LocationListener : Java.Lang.Object, ILocationListener
        {
            public LocationListener()
            {
            }

            public void OnLocationChanged(Location newLoc)
            {
                GameController.TriggerLocationChange(newLoc);
            }

            public void OnProviderDisabled(string provider)
            {
            }

            public void OnProviderEnabled(string provider)
            {
            }

            public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
            {
            }
        }

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
            InitLocation();

            // Get our game view from the layout resource,
            // and attach the view created event to it
            CCGameView gameView = (CCGameView)FindViewById (Resource.Id.GameView);
			gameView.ViewCreated += LoadGame;
		}

        private void InitLocation()
        {
            LocationManager locationManager = (LocationManager)GetSystemService(LocationService);

            Criteria criteria = new Criteria();
            string mprovider = locationManager.GetBestProvider(criteria, false);
            location = locationManager.GetLastKnownLocation(mprovider);

            locationListener = new LocationListener();
            locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 5000, 10, locationListener);
        }

        void LoadGame (object sender, EventArgs e)
		{
            CCGameView gameView = sender as CCGameView;

            if (gameView != null)
            {
                GameController.Initialize(gameView);
            }

        }
	}
}



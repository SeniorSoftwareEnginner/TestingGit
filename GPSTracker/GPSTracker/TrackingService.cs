using System;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Widget;

namespace GPSTracker
{
    [Service]
    public class TrackingService : Service, ILocationListener
    {
        LocationManager locManager;
        Criteria locationCriteria;

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            locManager = (LocationManager)GetSystemService(LocationService);
            locationCriteria = new Criteria();

            locationCriteria.Accuracy = Accuracy.Coarse;
            locationCriteria.PowerRequirement = Power.Low;

            string locationProvider = locManager.GetBestProvider(locationCriteria, true);

            // Preferences.MinTime, for example, 60 (seconds)
            // Preferences.MinDist, for example, 100 (meters)
            locManager.RequestLocationUpdates(locationProvider, 60 * 1000, 100, this);

            return StartCommandResult.Sticky;
        }

        public override void OnTaskRemoved(Intent rootIntent)
        {
            Toast.MakeText(this, "OnTaskRemoved:: ", ToastLength.Long).Show();
            base.OnTaskRemoved(rootIntent);
        }

        //#region LocationListener
        public void OnLocationChanged(Location location)
        {
            Toast.MakeText(this, "location:: " + location.ToString(), ToastLength.Long).Show();
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
        //#endregion
    }
}
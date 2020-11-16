using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Xamarin.Essentials;
using System;
using System.Threading.Tasks;

namespace Homework2
{
    [Activity(Label = "Xamarin Essentials", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var coordinates = FindViewById<TextView>(Resource.Id.textView1);
            var getlocation = FindViewById<Button>(Resource.Id.button1);
            var batteryinfo = FindViewById<Button>(Resource.Id.button2);
            var batterystats = FindViewById<TextView>(Resource.Id.textView2);
            Switch s = FindViewById<Switch>(Resource.Id.switch1);

            getlocation.Click += async delegate
            {
                try
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Best);
                    var location = await Geolocation.GetLocationAsync(request);

                    if (location != null)
                    {
                        Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                        //var lat = location.Latitude.ToString;
                        coordinates.Text = location.Latitude + " " + location.Longitude;

                    }
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    // Handle not supported on device exception
                }
                catch (FeatureNotEnabledException fneEx)
                {
                    // Handle not enabled on device exception
                }
                catch (PermissionException pEx)
                {
                    // Handle permission exception
                }
                catch (Exception ex)
                {
                    // Unable to get location
                }
            };

            s.CheckedChange += async delegate (object sender, CompoundButton.CheckedChangeEventArgs e) {
                var toast = Toast.MakeText(this, "Flashlight is " +
                    (e.IsChecked ? "on" : "off"), ToastLength.Short);
                toast.Show();
                if (s.Checked == true)
                {
                    try
                    {
                        // Turn On
                        await Flashlight.TurnOnAsync();
                    }
                    catch (FeatureNotSupportedException fnsEx)
                    {
                        // Handle not supported on device exception
                    }
                    catch (PermissionException pEx)
                    {
                        // Handle permission exception
                    }
                    catch (Exception ex)
                    {
                        // Unable to turn on/off flashlight
                    }
                }
                else
                {
                    try
                    {
                        // Turn Off
                        await Flashlight.TurnOffAsync();
                    }
                    catch (FeatureNotSupportedException fnsEx)
                    {
                        // Handle not supported on device exception
                    }
                    catch (PermissionException pEx)
                    {
                        // Handle permission exception
                    }
                    catch (Exception ex)
                    {
                        // Unable to turn on/off flashlight
                    }
                }
            };

            batteryinfo.Click += delegate
            {
                var level = Battery.ChargeLevel; // returns 0.0 to 1.0 or 1.0 when on AC or no battery.

                var state = Battery.State;

                switch (state)
                {
                    case BatteryState.Charging:
                        // Currently charging
                        break;
                    case BatteryState.Full:
                        // Battery is full
                        break;
                    case BatteryState.Discharging:
                    case BatteryState.NotCharging:
                        // Currently discharging battery or not being charged
                        break;
                    case BatteryState.NotPresent:
                    // Battery doesn't exist in device (desktop computer)
                    case BatteryState.Unknown:
                        // Unable to detect battery state
                        break;
                }

                var source = Battery.PowerSource;

                switch (source)
                {
                    case BatteryPowerSource.Battery:
                        // Being powered by the battery
                        break;
                    case BatteryPowerSource.AC:
                        // Being powered by A/C unit
                        break;
                    case BatteryPowerSource.Usb:
                        // Being powered by USB cable
                        break;
                    case BatteryPowerSource.Wireless:
                        // Powered via wireless charging
                        break;
                    case BatteryPowerSource.Unknown:
                        // Unable to detect power source
                        break;
                }
                batterystats.Text =
                "Level: " + level*100 + "%" + "\n" +
                "State: " + state + "\n" +
                "PowerSource: " + source;
            };

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}
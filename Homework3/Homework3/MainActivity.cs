using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;
using Android.Content;
using System;

namespace Homework3
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]

    public class HomeScreen : ListActivity
    {
        string[] items;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            items = new string[] { "EE", "LV", "LT" };
            ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, items);
        }

        async void GetCountryDetails(string SelectedCountry)
        {
            Country country = new Country();

            string url = "https://covid19-api.org/api/status/" + SelectedCountry;

            var handler = new System.Net.Http.HttpClientHandler();
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient(handler);
            string result = await client.GetStringAsync(url);
            country = Newtonsoft.Json.JsonConvert.DeserializeObject<Country>(result);

            Intent listView = new Intent(this, typeof(viewDetails));
            listView.PutExtra("Country", country.country);
            listView.PutExtra("Cases", country.cases.ToString());
            listView.PutExtra("Deaths", country.deaths.ToString());
            listView.PutExtra("Recovered", country.recovered.ToString());
            StartActivity(listView);
        }

        protected override void OnListItemClick(ListView l, Android.Views.View v, int position, long id)
        {
            var SelectedCountry = items[position];
            GetCountryDetails(SelectedCountry);
        }
    }

    public class Country
    {
        public string country { get; set; }
        public DateTime last_update { get; set; }
        public int cases { get; set; }
        public int deaths { get; set; }
        public int recovered { get; set; }
    }

}
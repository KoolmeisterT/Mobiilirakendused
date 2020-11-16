using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Homework3
{
    [Activity(Label = "Detailid")]
    public class viewDetails : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.details);
            TextView Country = FindViewById<TextView>(Resource.Id.country);
            TextView Cases = FindViewById<TextView>(Resource.Id.cases);
            TextView Deaths = FindViewById<TextView>(Resource.Id.deaths);
            TextView Recovered = FindViewById<TextView>(Resource.Id.recovered);


            string Country_ = Intent.GetStringExtra("Country");
            string Cases_ = Intent.GetStringExtra("Cases");
            string Deaths_ = Intent.GetStringExtra("Deaths");
            string Recovered_ = Intent.GetStringExtra("Recovered");

            Country.Text = Country_;
            Cases.Text = Cases_;
            Deaths.Text = Deaths_;
            Recovered.Text = Recovered_;
        }
    }
}
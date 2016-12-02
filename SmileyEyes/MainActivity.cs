using System;

using Android.App;
using Android.Widget;
using Android.OS;
using Android.Graphics;

using SmileyEyes.Views;
using Android.Content;

[assembly: Application(Theme = "@android:style/Theme.Material.Light.NoActionBar.Fullscreen")]

namespace SmileyEyes
{
    [Activity(Label = "SmileyEyes", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class MainActivity : Activity
    {
        public SmileyView Smiley;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.Smiley = new SmileyView(this);

            EyeSettings Settings = EyeSettings.Instance;

            this.Smiley.BorderColor = Settings.BorderColor;
            this.Smiley.SmileyColor = Settings.SmileyColor;
            this.Smiley.MouthColor = Settings.MouthColor;

            this.Smiley.EyeColor = Settings.EyeColor;
            this.Smiley.SmileyColor = Settings.SmileyColor;
            this.Smiley.MouthColor = Settings.MouthColor;

            this.Smiley.PupilStroke = Settings.PupilStroke;

            this.Smiley.BorderRatio = Settings.BorderRatio;
            this.Smiley.EyeRatio = Settings.EyeRatio;
            this.Smiley.EyeStrokeRatio = Settings.EyeStrokeRatio;
            this.Smiley.PupilRatio = Settings.PupilRatio;
            this.Smiley.PupilStrokeRatio = Settings.PupilStrokeRatio;
            this.Smiley.MouthRadiusRatio = Settings.MouthRadiusRatio;
            this.Smiley.MouthWidthRatio = Settings.MouthWidthRatio;

            this.Smiley.FaceRadius = Settings.FaceRadius;

            this.Smiley.IsHappy = Settings.IsHappy;
            this.Smiley.AutoHappy = Settings.AutoHappy;

            this.Smiley.EyeDistanceRatio = Settings.EyeDistanceRatio;

            this.Smiley.CookieRadius = Settings.CookieRadius;

            // Instancieer de buitenste layout. Hierin kunnen de controls en de ogen komen te staan
            LinearLayout OuterLayout = new LinearLayout(this);
            OuterLayout.Orientation = Orientation.Vertical;

            LinearLayout SettingsAndCookie = new LinearLayout(this);
            SettingsAndCookie.Orientation = Orientation.Horizontal;
            SettingsAndCookie.SetBackgroundColor(Color.Black);

            Button btnSettings = new Button(this);
            btnSettings.Click += this.OpenSettings;
            btnSettings.Text = "Instellingen";

            Button RemoveCookie = new Button(this);
            RemoveCookie.Text = "Haal het koekje weg";
            RemoveCookie.Click += this.btnRemoveCookie;

            SettingsAndCookie.AddView(btnSettings);
            SettingsAndCookie.AddView(RemoveCookie);

            OuterLayout.AddView(SettingsAndCookie);
            OuterLayout.AddView(this.Smiley);

            this.SetContentView(OuterLayout);
        }

        protected void OpenSettings(object sender, EventArgs ea)
        {
            Intent intent = new Intent(this, typeof(SettingsActivity));
            StartActivity(intent);
        }

        protected void btnRemoveCookie(object sender, EventArgs ea)
        {
            this.Smiley.RemoveFingerEvent();
        }
    }
}


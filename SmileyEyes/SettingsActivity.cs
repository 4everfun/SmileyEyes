using System;

using MonoDroid.ColorPickers;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Graphics;

using SmileyEyes.Views;

// Hier zouden nog veel meer instellingen kunnen worden getweaked, maar dit illustreert het idee

namespace SmileyEyes
{
    [Activity(Label = "Instellingen", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class SettingsActivity : Activity
    {
        private ColorPickerPanelView ColorPicker;
        public SmileyView Smiley;

        Random r;

        Button prvwEyeColor;
        Button prvwSmileyColor;
        Button prvwBorderColor;
        SeekBar sldrFaceRadius;

        protected Color BorderColor;
        protected Color SmileyColor;
        protected Color MouthColor;

        protected Color EyeColor;
        protected Color EyeStrokeColor;
        protected Color PupilColor;
        protected Color PupilStrokeColor;

        protected bool PupilStroke;

        protected float? BorderRatio;
        protected float? EyeRatio;
        protected float? EyeStrokeRatio;
        protected float? PupilRatio;
        protected float? PupilStrokeRatio;
        protected float? MouthRadiusRatio;
        protected float? MouthWidthRatio;

        protected float FaceRadius;

        protected bool IsHappy;
        protected bool AutoHappy;

        protected float EyeDistanceRatio;

        protected int CookieRadius;

        protected EyeSettings Settings;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Settings = EyeSettings.Instance;

            this.InitializeSettings();

            r = new Random();

            this.ColorPicker = new ColorPickerPanelView(this);

            LinearLayout OuterLayout = new LinearLayout(this);
            OuterLayout.Orientation = Orientation.Vertical;

            // Boodschap
            TextView Boodschap = new TextView(this);
            Boodschap.Text = "Stel de smiley kleuren in :)";
            Boodschap.TextSize = 25;

            // EyeColor
            LinearLayout EyeColorLayout = new LinearLayout(this);
            EyeColorLayout.Orientation = Orientation.Horizontal;

            Button btnEyeColor = new Button(this);
            btnEyeColor.Click += this.btnEyeColor;
            btnEyeColor.Text = "Kleur van het oog";

            prvwEyeColor = new Button(this);
            prvwEyeColor.SetBackgroundColor(this.EyeColor);

            // SmileyColor
            LinearLayout SmileyColorLayout = new LinearLayout(this);
            SmileyColorLayout.Orientation = Orientation.Horizontal;

            Button btnSmileyColor = new Button(this);
            btnSmileyColor.Click += this.btnSmileyColor;
            btnSmileyColor.Text = "Kleur van de smiley";

            prvwSmileyColor = new Button(this);
            prvwSmileyColor.SetBackgroundColor(this.SmileyColor);

            // BorderColor
            LinearLayout BorderColorLayout = new LinearLayout(this);
            BorderColorLayout.Orientation = Orientation.Horizontal;

            Button btnBorderColor = new Button(this);
            btnBorderColor.Click += this.btnBorderColor;
            btnBorderColor.Text = "Kleur van de rand";

            prvwBorderColor = new Button(this);
            prvwBorderColor.SetBackgroundColor(this.BorderColor);

            // FaceRadius
            LinearLayout FaceRadiusLayout = new LinearLayout(this);
            FaceRadiusLayout.Orientation = Orientation.Horizontal;

            TextView FaceRadiusText = new TextView(this);
            FaceRadiusText.Text = "Verander het formaat van de smiley.";

            sldrFaceRadius = new SeekBar(this);
            sldrFaceRadius.ProgressChanged += this.UpdateRadius;
            sldrFaceRadius.Progress = this.CalcProgress(this.FaceRadius, EyeMinimum.FaceRadius, EyeMaximum.FaceRadius);

            EyeColorLayout.AddView(btnEyeColor);
            EyeColorLayout.AddView(prvwEyeColor);

            SmileyColorLayout.AddView(btnSmileyColor);
            SmileyColorLayout.AddView(prvwSmileyColor);

            BorderColorLayout.AddView(btnBorderColor);
            BorderColorLayout.AddView(prvwBorderColor);

            FaceRadiusLayout.AddView(FaceRadiusText);

            LinearLayout BottomLayout = new LinearLayout(this);
            BottomLayout.Orientation = Orientation.Horizontal;

            // Random
            Button btnRandom = new Button(this);
            btnRandom.Click += this.RandomizeSettings;
            btnRandom.Text = "Randomize!";

            // Default
            Button btnDefault = new Button(this);
            btnDefault.Click += this.DefaultSettings;
            btnDefault.Text = "Reset";

            // Save
            Button btnSave = new Button(this);
            btnSave.Click += this.btnSaveSettings;
            btnSave.Text = "Opslaan";

            BottomLayout.AddView(btnRandom);
            BottomLayout.AddView(btnDefault);
            BottomLayout.AddView(btnSave);

            OuterLayout.AddView(Boodschap);
            OuterLayout.AddView(EyeColorLayout);
            OuterLayout.AddView(SmileyColorLayout);
            OuterLayout.AddView(BorderColorLayout);
            OuterLayout.AddView(FaceRadiusLayout);
            OuterLayout.AddView(BottomLayout);

            // Zorg ervoor dat de SeekBar de rest van het scherm inneemt
            FaceRadiusLayout.AddView(sldrFaceRadius, new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent));
            this.SetContentView(OuterLayout);
        }

        protected void InitializeSettings()
        {
            BorderColor = Settings.BorderColor;
            SmileyColor = Settings.SmileyColor;
            MouthColor = Settings.MouthColor;

            EyeColor = Settings.EyeColor;
            EyeStrokeColor = Settings.EyeStrokeColor;
            PupilColor = Settings.PupilColor;
            PupilStrokeColor = Settings.PupilStrokeColor;

            PupilStroke = Settings.PupilStroke;

            BorderRatio = Settings.BorderRatio;
            EyeRatio = Settings.EyeRatio;
            EyeStrokeRatio = Settings.EyeStrokeRatio;
            PupilRatio = Settings.PupilRatio;
            PupilStrokeRatio = Settings.PupilStrokeRatio;
            MouthRadiusRatio = Settings.MouthRadiusRatio;
            MouthWidthRatio = Settings.MouthWidthRatio;

            FaceRadius = Settings.FaceRadius;

            IsHappy = Settings.IsHappy;
            AutoHappy = Settings.AutoHappy;

            EyeDistanceRatio = Settings.EyeDistanceRatio;

            CookieRadius = Settings.CookieRadius;
        }

        protected void SaveSettings()
        {
            Settings.BorderColor = BorderColor;
            Settings.SmileyColor = SmileyColor;
            Settings.MouthColor = MouthColor;

            Settings.EyeColor = EyeColor;
            Settings.EyeStrokeColor = EyeStrokeColor;
            Settings.PupilColor = PupilColor;
            Settings.PupilStrokeColor = PupilStrokeColor;

            Settings.PupilStroke = PupilStroke;

            Settings.BorderRatio = BorderRatio;
            Settings.EyeRatio = EyeRatio;
            Settings.EyeStrokeRatio = EyeStrokeRatio;
            Settings.PupilRatio = PupilRatio;
            Settings.PupilStrokeRatio = PupilStrokeRatio;
            Settings.MouthRadiusRatio = MouthRadiusRatio;
            Settings.MouthWidthRatio = MouthWidthRatio;

            Settings.FaceRadius = FaceRadius;

            Settings.IsHappy = IsHappy;
            Settings.AutoHappy = AutoHappy;

            Settings.EyeDistanceRatio = EyeDistanceRatio;

            Settings.CookieRadius = CookieRadius;

            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
            Finish();
        }

        protected void btnSaveSettings(object sender, EventArgs ea)
        {
            this.SaveSettings();
        }

        protected void btnEyeColor(object sender, EventArgs ea)
        {
            using (var colorPickerDialog = new ColorPickerDialog(this, this.EyeColor))
            {
                colorPickerDialog.ColorChanged += this.UpdateEyeColor;
                colorPickerDialog.Show();
            }
        }

        protected void btnSmileyColor(object sender, EventArgs ea)
        {
            using (var colorPickerDialog = new ColorPickerDialog(this, this.SmileyColor))
            {
                colorPickerDialog.ColorChanged += this.UpdateSmileyColor;
                colorPickerDialog.Show();
            }
        }

        protected void btnBorderColor(object sender, EventArgs ea)
        {
            using (var colorPickerDialog = new ColorPickerDialog(this, this.BorderColor))
            {
                colorPickerDialog.ColorChanged += this.UpdateBorderColor;
                colorPickerDialog.Show();
            }
        }

        protected void UpdateEyeColor(object sender, ColorChangedEventArgs ea)
        {
            ColorPicker.Color = ea.Color;
            this.prvwEyeColor.SetBackgroundColor(ea.Color);
            this.EyeColor = ea.Color;
        }

        protected void UpdateSmileyColor(object sender, ColorChangedEventArgs ea)
        {
            ColorPicker.Color = ea.Color;
            this.prvwSmileyColor.SetBackgroundColor(ea.Color);
            this.SmileyColor = ea.Color;
        }

        protected void UpdateBorderColor(object sender, ColorChangedEventArgs ea)
        {
            ColorPicker.Color = ea.Color;
            this.prvwBorderColor.SetBackgroundColor(ea.Color);
            this.BorderColor = ea.Color;
        }

        protected void UpdateRadius(object sender, EventArgs ea)
        {
            this.FaceRadius = CalcNewValue(this.sldrFaceRadius.Progress, EyeMinimum.FaceRadius, EyeMaximum.FaceRadius);
        }

        protected void RandomizeSettings(object sender, EventArgs ea)
        {
            this.EyeColor = this.RandomColor();
            this.SmileyColor = this.RandomColor();
            this.BorderColor = this.RandomColor();

            this.FaceRadius = this.CalcRandom((int)EyeMinimum.FaceRadius, (int)EyeMaximum.FaceRadius);

            this.SaveSettings();
        }

        protected void DefaultSettings(object sender, EventArgs ea)
        {
            BorderColor = EyeDefaults.BorderColor;
            SmileyColor = EyeDefaults.SmileyColor;
            MouthColor = EyeDefaults.MouthColor;

            EyeColor = EyeDefaults.EyeColor;
            EyeStrokeColor = EyeDefaults.EyeStrokeColor;
            PupilColor = EyeDefaults.PupilColor;
            PupilStrokeColor = EyeDefaults.PupilStrokeColor;

            PupilStroke = EyeDefaults.PupilStroke;

            BorderRatio = EyeDefaults.BorderRatio;
            EyeRatio = EyeDefaults.EyeRatio;
            EyeStrokeRatio = EyeDefaults.EyeStrokeRatio;
            PupilRatio = EyeDefaults.PupilRatio;
            PupilStrokeRatio = EyeDefaults.PupilStrokeRatio;
            MouthRadiusRatio = EyeDefaults.MouthRadiusRatio;
            MouthWidthRatio = EyeDefaults.MouthWidthRatio;

            FaceRadius = EyeDefaults.FaceRadius;

            IsHappy = EyeDefaults.IsHappy;
            AutoHappy = EyeDefaults.AutoHappy;

            EyeDistanceRatio = EyeDefaults.EyeDistanceRatio;

            CookieRadius = EyeDefaults.CookieRadius;

            this.SaveSettings();
        }

        protected float CalcNewValue(int SliderValue, float minimum, float maximum)
        {
            return (maximum - minimum) * ((float)SliderValue / 100f) + minimum;
        }

        protected int CalcProgress(float current, float minimum, float maximum)
        {
            return (int)((current - minimum) / (maximum - minimum) * 100);
        }

        protected int CalcRandom(int minimum, int maximum)
        {
            return r.Next(minimum, maximum);
        }

        protected Color RandomColor()
        {
            return new Color(r.Next(256), r.Next(256), r.Next(256));
        }
    }
}
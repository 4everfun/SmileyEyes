using Android.Graphics;

namespace SmileyEyes
{
    public class EyeSettings
    {
        private static EyeSettings instance;

        private EyeSettings()
        {

        }

        public static EyeSettings Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new EyeSettings();
                }
                return instance;
            }
        }

        public Color BorderColor = EyeDefaults.BorderColor;
        public Color SmileyColor = EyeDefaults.SmileyColor;
        public Color MouthColor = EyeDefaults.MouthColor;

        public Color EyeColor = EyeDefaults.EyeColor;
        public Color EyeStrokeColor = EyeDefaults.EyeStrokeColor;
        public Color PupilColor = EyeDefaults.PupilColor;
        public Color PupilStrokeColor = EyeDefaults.PupilStrokeColor;

        public bool PupilStroke = EyeDefaults.PupilStroke;

        public float? BorderRatio = EyeDefaults.BorderRatio;
        public float? EyeRatio = EyeDefaults.EyeRatio;
        public float? EyeStrokeRatio = EyeDefaults.EyeStrokeRatio;
        public float? PupilRatio = EyeDefaults.PupilRatio;
        public float? PupilStrokeRatio = EyeDefaults.PupilStrokeRatio;
        public float? MouthRadiusRatio = EyeDefaults.MouthRadiusRatio;
        public float? MouthWidthRatio = EyeDefaults.MouthWidthRatio;

        public float FaceRadius = EyeDefaults.FaceRadius;

        public bool IsHappy = EyeDefaults.IsHappy;
        public bool AutoHappy = EyeDefaults.AutoHappy;

        public float EyeDistanceRatio = EyeDefaults.EyeDistanceRatio;

        public int CookieRadius = EyeDefaults.CookieRadius;
    }
}
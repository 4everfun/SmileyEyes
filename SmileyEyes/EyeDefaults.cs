using Android.Graphics;

namespace SmileyEyes
{
    class EyeDefaults
    {
        public static Color BorderColor = Color.Black;
        public static Color SmileyColor = Color.Yellow;
        public static Color MouthColor = Color.Black;

        public static Color EyeColor = Color.White;
        public static Color EyeStrokeColor = Color.Black;
        public static Color PupilColor = Color.Black;
        public static Color PupilStrokeColor = Color.Blue;

        public static bool PupilStroke = false;

        public static float? BorderRatio = 0.1f;
        public static float? EyeRatio = 0.25f;
        public static float? EyeStrokeRatio = 0.1f;
        public static float? PupilRatio = 0.4f;
        public static float? PupilStrokeRatio = 0.1f;
        public static float? MouthRadiusRatio = 0.8f;
        public static float? MouthWidthRatio = 0.15f;

        public static float FaceRadius = 450;

        public static bool IsHappy = false;
        public static bool AutoHappy = true;

        public static float EyeDistanceRatio = 0.4f;

        public static int CookieRadius = 150;
    }
}
using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;

using Android.Graphics;

namespace SmileyEyes.Views
{
    public class SmileyView : View
    {
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
        public float BorderRadius;
        public float EyeRadius;
        public float EyeStrokeWidth;
        public float PupilRadius;
        public float PupilStrokeWidth;
        public float MouthRadius;
        public float MouthWidth;

        public bool IsHappy;
        public bool AutoHappy = EyeDefaults.AutoHappy;

        public float EyeDistanceRatio = EyeDefaults.EyeDistanceRatio;

        public int CookieRadius = EyeDefaults.CookieRadius;

        protected static Bitmap cookie;

        // Een eventuele offset om de ogen van de rand van het scherm te tekenen
        public float XCenterOffset = 0;
        public float YCenterOffset = 0;

        protected float XCenter;
        protected float YCenter;

        protected List<PointF> Cookies;

        public SmileyView(Context context) :
            base(context)
        {
            Initialize();
        }

        private void Initialize()
        {
            this.Cookies = new List<PointF>();
            
            if (this.AutoHappy)
            {
                this.IsHappy = false;
            }

            this.Touch += this.RegisterTouch;

            if (cookie == null)
            {
                Bitmap OriginalCookie = BitmapFactory.DecodeResource(Resources, Resource.Drawable.koekje);
                cookie = Bitmap.CreateScaledBitmap(OriginalCookie, this.CookieRadius, this.CookieRadius, true);
            }
        }

        private void recalc()
        {
            if (this.BorderRatio != null)
            {
                this.BorderRadius = this.FaceRadius * (float)this.BorderRatio + this.FaceRadius;
            }

            if (this.EyeRatio != null)
            {
                this.EyeRadius = this.FaceRadius * (float)this.EyeRatio;
            }

            if (this.EyeStrokeRatio != null)
            {
                this.EyeStrokeWidth = this.EyeRadius * (float)this.EyeStrokeRatio;
            }

            if (this.PupilRatio != null)
            {
                this.PupilRadius = this.EyeRadius * (float)this.PupilRatio;
            }

            if (this.PupilStrokeRatio != null)
            {
                this.PupilStrokeWidth = this.PupilRadius * (float)this.PupilStrokeRatio;
            }

            if (this.MouthRadiusRatio != null)
            {
                this.MouthRadius = this.FaceRadius * (float)this.MouthRadiusRatio;
            }

            if (this.MouthWidthRatio != null)
            {
                this.MouthWidth = this.FaceRadius * (float)this.MouthWidthRatio;
            }
        }

        public void SetFaceRadius (float FaceRadius)
        {
            this.FaceRadius = FaceRadius;
            this.recalc();
            this.Invalidate();
        }

        public void SetHappy (bool IsHappy)
        {
            this.IsHappy = IsHappy;
            this.Invalidate();
        }

        protected override void OnDraw(Canvas c)
        {
            base.OnDraw(c);

            this.recalc();

            this.XCenter = this.Width / 2 + this.XCenterOffset;
            this.YCenter = this.Height / 2 + this.YCenterOffset;

            this.DrawFace(c);
            this.DrawMouth(c);
            this.DrawEyes(c);
            this.DrawTouchEvent(c);
        }

        protected void DrawFace(Canvas c)
        {
            // Define the different types of paint
            Paint BorderPaint = new Paint();
            BorderPaint.Color = BorderColor;

            Paint SmileyPaint = new Paint();
            SmileyPaint.Color = SmileyColor;

            c.DrawCircle(this.XCenter, this.YCenter, this.BorderRadius, BorderPaint);
            c.DrawCircle(this.XCenter, this.YCenter, this.FaceRadius, SmileyPaint);
        }

        protected void DrawEyes(Canvas c)
        {
            // Bereken de center coördinaten van het linkeroog, gebasseerd op de offset en de oogradius
            float LeftX = this.XCenter - this.FaceRadius * this.EyeDistanceRatio;
            float LeftY = this.YCenter - this.FaceRadius * 0.3f;

            this.DrawEye(c, LeftX, LeftY);

            // Voeg vanaf het centrum van het linkeroog de radius van het linkeroog toe, daarna de afstand tussen de ogen en dan nog de radius van het rechteroog om het centrum van het rechteroog te krijgen. Ook moet twee keer de lijn dikte worden toegevoegd (voor beide ogen één keer).
            float RightX = this.XCenter + this.FaceRadius * this.EyeDistanceRatio;
            float RightY = this.YCenter - this.FaceRadius * 0.3f;

            this.DrawEye(c, RightX, RightY);
        }

        protected void DrawEye(Canvas c, float centerx, float centery)
        {
            Paint EyeStrokePaint = new Paint();
            EyeStrokePaint.Color = this.EyeStrokeColor;

            // Teken eerst de rand van het oog.
            c.DrawCircle(centerx, centery, this.EyeRadius + this.EyeStrokeWidth, EyeStrokePaint);

            Paint EyePaint = new Paint();
            EyePaint.Color = this.EyeColor;

            // En daarna het oog zelf
            c.DrawCircle(centerx, centery, this.EyeRadius, EyePaint);

            // De pupil wordt standaard in het midden van het oog getekend (als er nog geen gegevens van de touchinput bekend zijn)
            float PupilCenterX = centerx;
            float PupilCenterY = centery;

            // Als er wel invoer van de vinger bekend is, bereken dan de waarden opnieuw
            if (this.Cookies.Count > 0)
            {
                PointF LeastDistancePoint = this.Cookies[0];
                float LeastDistance = float.MaxValue;
                int test = 999;

                for(int i = 0; i < this.Cookies.Count; i++)
                {
                    PointF CurrentPoint = this.Cookies[i];

                    float TotalDistance = Math.Abs(centerx - CurrentPoint.X) + Math.Abs(centery - CurrentPoint.Y);

                    if (LeastDistance > TotalDistance)
                    {
                        test = i;
                        LeastDistance = TotalDistance;
                        LeastDistancePoint = CurrentPoint;
                    }
                }

                // De stelling van pythagoras toegepast om de afstand van het midden van het oog tot de vinger te berekenen.
                float dx = LeastDistancePoint.X - centerx;
                float dy = LeastDistancePoint.Y - centery;
                double d = Math.Sqrt(dx * dx + dy * dy);

                // Nu wordt ofwel de waarde die berekend is gepakt (als de vinger binnen het oog zit) of de radius van het oog (minus de pupil) (als de vinger buiten het oog zit). Zo kan de pupil nooit uit het oog komen.
                double e = Math.Min(d, this.EyeRadius - this.PupilRadius);

                // Bereken hoeveel de pupil verplaatst moet worden vanaf het midden. Bereken hiertoe eerst de radius tussen de aftand van de vinger tot het midden van het oog en de hoeveelheid dat de pupil verplaatst kan worden (zodat hij niet buiten het oog komt)
                double ratio = e / d;
                double ex = dx * ratio;
                double ey = dy * ratio;

                // Overschijf de waarden waar de pupil getekend moet worden
                PupilCenterX += (float)ex;
                PupilCenterY += (float)ey;
            }

            // Teken de pupil
            this.DrawPupil(c, PupilCenterX, PupilCenterY);
        }

        protected void DrawPupil(Canvas c, float centerx, float centery)
        {
            // Als er een rand om de pupil getekend moet worden, teken deze dan
            if (this.PupilStroke)
            {
                Paint PupilStrokePaint = new Paint();
                PupilStrokePaint.Color = this.PupilStrokeColor;
                PupilStrokePaint.AntiAlias = true;

                c.DrawCircle(centerx, centery, this.PupilRadius + this.PupilStrokeWidth, PupilStrokePaint);
            }

            // Teken de pupil zelf
            Paint PupilPaint = new Paint();
            PupilPaint.Color = this.PupilColor;
            PupilPaint.SetStyle(Paint.Style.Fill);

            c.DrawCircle(centerx, centery, this.PupilRadius, PupilPaint);
        }

        protected void DrawMouth(Canvas c)
        {
            float MouthStartX = this.XCenter - this.MouthRadius + this.MouthWidth / 2;
            float MouthStartY = this.YCenter - this.MouthRadius + this.MouthWidth / 2;
            float MouthEndX = this.XCenter + this.MouthRadius - this.MouthWidth / 2;
            float MouthEndY = this.YCenter + this.MouthRadius - this.MouthWidth / 2;

            Paint MouthPaint = new Paint();
            MouthPaint.SetStyle(Paint.Style.Stroke);
            MouthPaint.StrokeWidth = this.MouthWidth;
            MouthPaint.StrokeCap = Paint.Cap.Round;
            MouthPaint.Color = this.MouthColor;

            // And finally draw
            if (this.IsHappy)
            {
                c.DrawArc(MouthStartX, MouthStartY, MouthEndX, MouthEndY, 30, 120, false, MouthPaint);
            } else
            {
                MouthStartY += this.FaceRadius;
                MouthEndY += this.FaceRadius;

                c.DrawArc(MouthStartX, MouthStartY, MouthEndX, MouthEndY, 210, 120, false, MouthPaint);
            }
        }

        protected void RegisterTouch(object sender, TouchEventArgs ea)
        {
            this.Cookies.Clear();

            for(int i = 0; i < Math.Min(ea.Event.PointerCount, 2); i++)
            {
                PointF p = new PointF(ea.Event.GetX(i), ea.Event.GetY(i));
                this.Cookies.Add(p);
            }

            if (this.AutoHappy)
            {
                this.IsHappy = true;
            }

            // Teken de view opnieuw
            this.Invalidate();
        }

        public void RemoveFingerEvent()
        {
            this.Cookies.Clear();

            if (this.AutoHappy)
            {
                this.IsHappy = false;
            }

            this.Invalidate();
        }

        protected void DrawTouchEvent(Canvas c)
        {
            if (this.Cookies.Count != 0)
            {
                for (int i = 0; i < this.Cookies.Count; i++)
                {
                    float CookieStartX = this.Cookies[i].X - this.CookieRadius / 2;
                    float CookieStartY = this.Cookies[i].Y - this.CookieRadius / 2;

                    c.DrawBitmap(cookie, CookieStartX, CookieStartY, null);
                }
            }
        }
    }
}
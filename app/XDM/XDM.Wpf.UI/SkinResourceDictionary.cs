using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace XDM.Wpf.UI
{
    public class SkinResourceDictionary : ResourceDictionary
    {
        private Uri _darkSource;
        private Uri _lightSource;

        public Uri DarkSource
        {
            get { return _darkSource; }
            set
            {
                _darkSource = value;
                UpdateSource();
            }
        }
        public Uri LightSource
        {
            get { return _lightSource; }
            set
            {
                _lightSource = value;
                UpdateSource();
            }
        }

        private void UpdateSource()
        {
            var val = App.Skin == Skin.Dark ? DarkSource : LightSource;
            if (val != null && base.Source != val)
                base.Source = val;
        }

        public static void ApplyAccentColor(string hexColor)
        {
            if (string.IsNullOrEmpty(hexColor)) return;
            try
            {
                var color = (Color)ColorConverter.ConvertFromString(hexColor);
                ApplyAccentColor(color);
            }
            catch { }
        }

        public static void ApplyAccentColor(Color color)
        {
            try
            {
                var brush = new SolidColorBrush(color);
                Application.Current.Resources["AccentColor"] = color;
                Application.Current.Resources["AccentBrush"] = brush;
                Application.Current.Resources["ProgressBarForecolor"] = brush;
                Application.Current.Resources["StatusbarIconcolor"] = brush;
                Application.Current.Resources["TabSelectionColor"] = brush;
                Application.Current.Resources["ListViewIconForecolor"] = brush;

                var gradient = new LinearGradientBrush();
                gradient.StartPoint = new Point(0, 0);
                gradient.EndPoint = new Point(1, 1);
                gradient.GradientStops.Add(new GradientStop(color, 0));
                var secondary = Color.FromRgb((byte)Math.Max(0, color.R - 20), (byte)Math.Max(0, color.G - 20), (byte)Math.Min(255, color.B + 30));
                gradient.GradientStops.Add(new GradientStop(secondary, 1));
                Application.Current.Resources["AccentGradientBrush"] = gradient;
            }
            catch { }
        }
    }
}

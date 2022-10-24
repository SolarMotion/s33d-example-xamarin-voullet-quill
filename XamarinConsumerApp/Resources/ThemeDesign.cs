using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinConsumerApp.Resources
{
    public static class ThemeDesign
    {
        #region App Colour

        public static Color TopNavigationBarBackgroundColor
        {
            get { return Color.FromHex("#CE3175"); }
        }

        public static Color TopNavigationBarTextColor
        {
            get { return Color.FromHex("#FFFFFF"); }
        }

        public static Color BackgroundColor
        {
            get { return Color.FromHex("#EAD1DC"); }
        }

        public static Color BottomNavigationBarColor
        {
            get { return Color.FromHex("#CE3175"); }
        }

        #endregion

        #region Button Colour

        public static Color ButtonPrimaryBackgroundColor
        {
            get { return Color.FromHex("#CE3175"); }
        }

        public static Color ButtonPrimaryTextColor
        {
            get { return Color.FromHex("#FFFFFF"); }
        }

        public static Color ButtonSecondaryBackgroundColor
        {
            get { return Color.FromHex("#FFFFFF"); }
        }

        public static Color ButtonSecondaryTextColor
        {
            get { return Color.FromHex("#111111"); }
        }

        #endregion

        public static Thickness StackedLayoutPadding
        {
            get { return new Thickness(5, 5, 5, 5); }
        }

        #region Asterisk

        public static string AsteriskText
        {
            get { return " *"; }
        }

        public static Color AsteriskColour
        {
            get { return Color.FromHex("#FF4136"); }
        }

        #endregion

        public static double FormSpacingHeightRequest
        {
            get { return 5; }
        }

        #region Font Size & Colour

        public static double LabelFontSize
        {
            get { return 18; }
        }

        public static double TitleFontSize
        {
            get { return 20; }
        }

        public static double DescriptionFontSize
        {
            get { return 18; }
        }

        public static Color DescriptionFontColour
        {
            get { return Color.Gray; }
        }

        #endregion
    }
}

using System;
using Xamarin.Forms;

namespace BetterPractice.BetterMvvm.Controls.Labels
{
    public class CaptionLabel : Label
    {
        public CaptionLabel()
        {
            Style = Device.Styles.CaptionStyle;
        }
    }
}

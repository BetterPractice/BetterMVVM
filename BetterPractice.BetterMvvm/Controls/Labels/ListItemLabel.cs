using System;
using Xamarin.Forms;

namespace BetterPractice.BetterMvvm.Controls.Labels
{
    public class ListItemLabel : Label
    {
        public ListItemLabel()
        {
            Style = Device.Styles.ListItemTextStyle;
        }
    }
}

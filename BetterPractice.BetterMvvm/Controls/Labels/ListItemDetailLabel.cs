using System;
using Xamarin.Forms;

namespace BetterPractice.BetterMvvm.Controls.Labels
{
    public class ListItemDetailLabel : Label
    {
        public ListItemDetailLabel()
        {
            Style = Device.Styles.ListItemDetailTextStyle;
        }
    }
}

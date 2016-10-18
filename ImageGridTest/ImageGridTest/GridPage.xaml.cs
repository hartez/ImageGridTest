using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ImageGridTest
{
	public partial class GridPage : ContentPage
	{
		public GridPage ()
		{
			InitializeComponent ();

            foreach (var columnDef in ImageGrid.ColumnDefinitions)
            {
                System.Diagnostics.Debug.WriteLine(App.DisplayScreenWidth);

                // Device Screen Width / 2 ... since we want to display 2 images per row
                // 	take out an extra 6 to accommodate for the padding & spacing
                columnDef.Width = new GridLength(App.DisplayScreenWidth / 2 - 6, GridUnitType.Absolute);

                // TODO: You may need to rerun this code if the user rotates the screen, or just detect the 
                //	orientation and then use DisplayScreenWidth or DisplayScreenHeight depending on the situation
                //	(DisplayScreenWidth when in Portrait)

            }

        }
    }
}

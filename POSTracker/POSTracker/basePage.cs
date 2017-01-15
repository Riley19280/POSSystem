using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace POSTracker
{
	public class basePage : ContentPage
	{
		public basePage()
		{
			Style = (Style)Application.Current.Resources["contentPageStyle"];
		}
	}
}

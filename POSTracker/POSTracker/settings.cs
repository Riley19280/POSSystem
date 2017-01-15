using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace POSTracker
{
	public class settings : basePage
	{
		public settings()
		{
			Title = "Settings";

			new XLabs.Forms.Controls.ExtendedEntry { PlaceholderTextColor = Constants.palette.primary_text };

			Content = new StackLayout
			{
				Padding= 10,
				Spacing = 10,

				Children = {
					new Button {
						Text = "Edit Presets",
						Command = new Command(()=> {
							Navigation.PushAsync(new presetAmounts());
						})
					},
					new Label {
						Text = @"To add a customer click the ""+"" button in the top right on the main menu. You can search for customers as well as ""balance:positive"" or ""balance:negative"" to filter by balance. Preset amounts can be configured by clicking the button above and are visible when you click on a customers name."

					}
				}
			};
		}
	}
}

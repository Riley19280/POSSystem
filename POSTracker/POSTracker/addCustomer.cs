using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace POSTracker
{
	public class addCustomer : basePage
	{
		Entry fname;
		Entry lname;
		Entry startAmt;
		Button add;



		public addCustomer()
		{
			Title = "Add Customer";
			

			fname = new ExtendedEntry { Placeholder = "First Name", FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), HorizontalOptions = LayoutOptions.FillAndExpand, PlaceholderTextColor = Constants.palette.secondary_text };
			lname = new ExtendedEntry { Placeholder = "Last Name", FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), HorizontalOptions = LayoutOptions.FillAndExpand, PlaceholderTextColor = Constants.palette.secondary_text };
			startAmt = new ExtendedNumericEntry { Placeholder = "Starting Balance", Keyboard = Keyboard.Numeric, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), HorizontalOptions = LayoutOptions.FillAndExpand, PlaceholderTextColor = Constants.palette.secondary_text,TextColor = Constants.palette.primary_text,BackgroundColor = Constants.palette.primary_variant };

			add = new Button { Text = "Add" };
			add.Clicked += async (s, e) =>
			{
				if (string.IsNullOrWhiteSpace(fname.Text) || string.IsNullOrWhiteSpace(lname.Text) || string.IsNullOrWhiteSpace(startAmt.Text))
				{
					DisplayAlert("Invalid Fields", "Enter First name, Last name, and a starting balance.", "Dismiss");

				}
				else
				{
					myDataTypes.customer c = App.MANAGER.checkCustomerExists(fname.Text + " " + lname.Text);


					switch (c.name)
					{
						case "$#@)&$()none"://none found
							App.MANAGER.addCustomer(new myDataTypes.customer(fname.Text + " " + lname.Text, Math.Round(double.Parse(startAmt.Text), 2, MidpointRounding.AwayFromZero)));
							Navigation.PopAsync();
							break;

						case "$#@)&$()many"://morethan 1 found
							bool answer = await DisplayAlert("More than two", "Two or more customers with this name already exist. Are you sure you would like to add another?", "Yes", "No");
							if (answer)
							{
								App.MANAGER.addCustomer(new myDataTypes.customer(fname.Text + " " + lname.Text, Math.Round(double.Parse(startAmt.Text), 2, MidpointRounding.AwayFromZero)));
								Navigation.PopAsync();
							}
							else
							{

							}
							break;
						default://one found
							bool answer1 = await DisplayAlert("Identical customer found", "Another customer with this name was found. whould you like to view them instead?", "Yes", "No");
							if (answer1)
							{
								Navigation.PopAsync();
								Navigation.PushAsync(new customerPage(c));

							}
							else
							{
								App.MANAGER.addCustomer(new myDataTypes.customer(fname.Text + " " + lname.Text, Math.Round(double.Parse(startAmt.Text), 2, MidpointRounding.AwayFromZero)));
								Navigation.PopAsync();
							}

							break;
					}

				
				}
			};

			Content = new StackLayout
			{
				Padding = 10,

				Children = {
					fname,
					lname,
					startAmt,
					add

	}

			};

		}

	}
}

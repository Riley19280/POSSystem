using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace POSTracker
{
	public class customerPage : basePage
	{
		myDataTypes.customer customer;
		Label balanceLabel, nameLabel;

		public customerPage(myDataTypes.customer cust)
		{
			customer = cust;

			ToolbarItems.Add(new ToolbarItem("Delete Customer", "@drawable/x", async () =>
			{
				bool answer = await DisplayAlert("Delete Customer", "Are you sure you want to delete this customer?", "Yes", "No");
				if (answer)
				{
					App.MANAGER.deleteCustomer(customer);

					Navigation.PopAsync();
				}


			}));



			Title = customer.name;

			nameLabel = new Label { Text = customer.name, HorizontalOptions = LayoutOptions.CenterAndExpand, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) };
			balanceLabel = new Label { HorizontalOptions = LayoutOptions.CenterAndExpand, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) };

			StackLayout buttonStacks = new StackLayout { Orientation = StackOrientation.Horizontal };
			foreach (double d in App.MANAGER.getDefaultAmounts())
			{
				buttonStacks.Children.Add(addsubstack(d));
			}

			ExtendedEntry customAmount = new ExtendedEntry { Placeholder = "Enter custom amount..", Keyboard = Keyboard.Numeric, HorizontalOptions = LayoutOptions.FillAndExpand, PlaceholderTextColor = Constants.palette.secondary_text };
			customAmount.TextChanged += (s, e) =>
			{
				if (e != null)
					foreach (char c in e.NewTextValue)
					{
						if (!Char.IsDigit(c) && c != '.' && c != ',')
						{
							customAmount.Text = e.OldTextValue;
						}
					}
			};


			StackLayout customStack = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				Children = {
					new Button {
						Text = "+",
						HorizontalOptions = LayoutOptions.FillAndExpand,
						Command = new Command(()=> {
						customer.balance = App.MANAGER.updateCustomerBalance(customer.balance+ Math.Round(double.Parse(customAmount.Text),2, MidpointRounding.AwayFromZero),customer.id);
							setLabels(customer);
						})
					},
					customAmount,
					new Button {
						Text = "-",
						HorizontalOptions = LayoutOptions.FillAndExpand,
						Command = new Command(()=> {
							if(customAmount.Text ==null )
								return;
							customer.balance = App.MANAGER.updateCustomerBalance(customer.balance-Math.Round(double.Parse(customAmount.Text),2, MidpointRounding.AwayFromZero),customer.id);
							setLabels(customer);
						})
					},
				}
			};




			Button finish = new Button
			{
				Text = "Finish",
				Command = new Command(() =>
				{
					Navigation.PopAsync();
				})
			};

			Content = new StackLayout
			{
				Spacing = 10,
				Padding = 10,
				Children = {
					nameLabel,
					balanceLabel,
					buttonStacks,
					customStack,
					finish


				}
			};

			setLabels(customer);
		}

		public void setLabels(myDataTypes.customer cust)
		{

			CultureInfo culture = new CultureInfo("en-us");
			culture.NumberFormat.CurrencyNegativePattern = 1;

			balanceLabel.Text = string.Format(culture, "{0:c2}", cust.balance);
			cust.balance = Math.Round(cust.balance, 2, MidpointRounding.AwayFromZero);
			if (cust.balance == 0)
				balanceLabel.TextColor = Color.Black;
			else if (cust.balance > 0)
				balanceLabel.TextColor = Color.Green;
			else if (cust.balance < 0)
				balanceLabel.TextColor = Color.Red;
		}


		StackLayout addsubstack(double amt)
		{

			return new StackLayout
			{
				Padding = 0,
				Spacing = 0,
				VerticalOptions = LayoutOptions.StartAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children = {
					new Button {
						Text = "+",
						BorderRadius = 0,
						HorizontalOptions = LayoutOptions.FillAndExpand,
						//Scale = .5f,
						Command = new Command(()=> {
							customer.balance =App.MANAGER.updateCustomerBalance(customer.balance+amt,customer.id);
							setLabels(customer);
						})

					},
					new Label {Text = amt.ToString(),HorizontalOptions = LayoutOptions.CenterAndExpand, FontSize = Device.GetNamedSize(NamedSize.Small,typeof(Label)) },
					new Button {
						Text = "-",
						BorderRadius = 0,
						HorizontalOptions = LayoutOptions.FillAndExpand,
						//Scale = .5f,
						Command = new Command(()=> {
						customer.balance =   App.MANAGER.updateCustomerBalance(customer.balance-amt,customer.id);
							setLabels(customer);
						})
					}
				}
			};
		}
	}
}

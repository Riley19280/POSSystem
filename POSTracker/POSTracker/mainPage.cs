using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace POSTracker
{
	public class mainPage : basePage
	{
		ListView customers;
		SearchBar search;


		public mainPage()
		{
			Title = "Customers";

			ToolbarItems.Add(new ToolbarItem("Settings", "@drawable/settings", () =>
			{
				Navigation.PushAsync(new settings());

			}));

			ToolbarItems.Add(new ToolbarItem("New Customer", "@drawable/plus", () =>
			{
				Navigation.PushAsync(new addCustomer());

			}));

		

			customers = CreateListView();

			customers.ItemsSource = App.MANAGER.getAllCustomers();

			search = new SearchBar { Placeholder = "Search", };
			search.SearchCommand = new Command(() =>
			{
				if (search.Text.ToLower() == ("balance:positive"))
				{
					customers.ItemsSource = App.MANAGER.positiveBalances();
				}
				else if (search.Text.ToLower() == ("balance:negative"))
				{
					customers.ItemsSource = App.MANAGER.negativeBalances();
				}
				else if ((string.IsNullOrWhiteSpace(search.Text)))
					customers.ItemsSource = App.MANAGER.getAllCustomers();
				else
					customers.ItemsSource = App.MANAGER.searchForCustomer(search.Text);
			});

			search.TextChanged += (s, e) =>{
				if (search.Text.ToLower() == ("balance:positive"))
				{
					customers.ItemsSource = App.MANAGER.positiveBalances();
				}
				else if (search.Text.ToLower() == ("balance:negative"))
				{
					customers.ItemsSource = App.MANAGER.negativeBalances();
				}
				else if ((string.IsNullOrWhiteSpace(search.Text)))
					customers.ItemsSource = App.MANAGER.getAllCustomers();
				else
					customers.ItemsSource = App.MANAGER.searchForCustomer(search.Text);
			};

			Content = new StackLayout
			{
				Children = {
					search,
					customers
				}
			};
		}

		ListView CreateListView()
		{
			Label balLabel = null;

			ListView listView = new ListView
			{
				// Source of data items.

				
				RowHeight = 60,
				ItemTemplate = new DataTemplate(() =>
				{
					// Create views with bindings for displaying each property.
					Label nameLabel = new Label { FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), HorizontalOptions = LayoutOptions.CenterAndExpand };
					nameLabel.SetBinding(Label.TextProperty, "name");

					balLabel = new Label { FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), HorizontalOptions = LayoutOptions.CenterAndExpand };
					balLabel.SetBinding(Label.TextProperty, "balance");
				

					// Return an assembled ViewCell.
					return new ViewCell
					{
						View = new StackLayout
						{
							Padding = new Thickness(5, 5, 5, 5),

							Children =
							{
								nameLabel,
								balLabel
							}
						}
					};
				})
			};
			listView.IsPullToRefreshEnabled = true;

			listView.Refreshing += ((sender, eventArgs) =>
			{
				search.Text = "";
				listView.ItemsSource = App.MANAGER.getAllCustomers();
				listView.IsRefreshing = false;
			});

			listView.ItemSelected += ((sender, eventArgs) =>
			{
				if (listView.SelectedItem != null)
				{
					var actView = new customerPage(listView.SelectedItem as myDataTypes.customer);

					listView.SelectedItem = null;
					Navigation.PushAsync(actView);
				}
			});

			listView.ItemAppearing += (s, e)=> {
				myDataTypes.customer i = e.Item as myDataTypes.customer;
				double amount = i.balance;
				if (amount == 0)
					balLabel.TextColor = Color.Black;
				else if (amount > 0)
					balLabel.TextColor = Color.Green;
				else if (amount < 0)
					balLabel.TextColor = Color.Red;
				CultureInfo culture = new CultureInfo("en-us");
				culture.NumberFormat.CurrencyNegativePattern = 1;

				balLabel.Text = string.Format(culture, "{0:c2}", amount);
			};

			return listView;
		}

		protected override void OnAppearing()
		{
			customers.IsRefreshing = true;
			customers.ItemsSource = App.MANAGER.getAllCustomers();
			customers.IsRefreshing = false;

		}

	}
}

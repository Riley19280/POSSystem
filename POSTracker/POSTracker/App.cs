using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace POSTracker
{
	public class App : Application
	{
		public static manager MANAGER;
		public App()
		{
			#region Style
			Resources = new ResourceDictionary();
			var contentPageStyle = new Style(typeof(ContentPage))
			{
				Setters = {
				new Setter { Property = ContentPage.BackgroundColorProperty, Value = Constants.palette.primary },
				}
			};
			Resources.Add("contentPageStyle", contentPageStyle);

			var labelStyle = new Style(typeof(Label))
			{
				Setters = {
				new Setter { Property = Label.TextColorProperty, Value = Constants.palette.primary_text },
				}
			};
			Resources.Add(labelStyle);

			var editorStyle = new Style(typeof(Editor))
			{
				Setters = {
				new Setter { Property = Editor.TextColorProperty, Value = Constants.palette.primary_text },
				new Setter { Property = Editor.BackgroundColorProperty, Value = Constants.palette.primary_variant },
				}
			};
			Resources.Add(editorStyle);

			var entryStyle = new Style(typeof(ExtendedEntry))
			{
				Setters = {
				new Setter { Property = ExtendedEntry.TextColorProperty, Value = Constants.palette.primary_text },
				new Setter { Property = ExtendedEntry.BackgroundColorProperty, Value = Constants.palette.primary_variant },
				}
			};
			Resources.Add(entryStyle);

			var searchStyle = new Style(typeof(SearchBar))
			{
				Setters = {
			//	new Setter { Property = SearchBar.TextColorProperty, Value = Constants.palette.primary_text },
				new Setter { Property = SearchBar.BackgroundColorProperty, Value = Color.FromHex("#001528") },
				new Setter { Property = SearchBar.CancelButtonColorProperty, Value = Constants.palette.primary_text },

				}
			};
			Resources.Add(searchStyle);


			var buttonStyle = new Style(typeof(Button))
			{
				Setters = {
				new Setter { Property = Button.TextColorProperty, Value = Color.FromHex("#FFFFFF") },
				new Setter { Property = Button.BackgroundColorProperty, Value = Constants.palette.primary_dark_variant },
				}
			};
			Resources.Add(buttonStyle);

			var activityIndicatorStyle = new Style(typeof(ActivityIndicator))
			{
				Setters = {
				new Setter { Property = ActivityIndicator.ColorProperty, Value = Constants.palette.primary_dark_variant },
				}
			};
			Resources.Add(activityIndicatorStyle);

			var listViewStyle = new Style(typeof(ListView))
			{
				Setters = {
				new Setter { Property = ListView.SeparatorColorProperty, Value = Constants.palette.primary_text },
				}
			};
			Resources.Add(listViewStyle);


			#endregion

			MANAGER = new manager();

			MANAGER.EstablishDatabase();
				
			MainPage = new NavigationPage(new mainPage()) { BarBackgroundColor =Color.FromHex("#001528"), };
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}

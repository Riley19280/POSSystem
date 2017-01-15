using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace POSTracker
{
	public class presetAmounts : basePage
	{
		ExtendedEntry[] entries = new ExtendedEntry[3];//TODO:NUMPRESETS

		public presetAmounts()
		{
			Title = "Preset Amounts";

			StackLayout stack = new StackLayout { Padding = 10, Spacing = 10 };

			for (int i = 0; i < 3; i++)//TODO:NUMPRESETS
			{//presets capped at 5
				entries[i] = ent(i);
				stack.Children.Add(entries[i]);
			}

			stack.Children.Add(
				new Button
				{
					Text = "Save",
					Command = new Command(() =>
					{
						for (int i = 0; i < entries.Length; i++)
						{
							double j = 0;
							double.TryParse(entries[i].Text, out j);
							j = Math.Round(j, 2, MidpointRounding.AwayFromZero);
							App.MANAGER.setPreset(i+1, j);
						}

						Navigation.PopAsync();
					})
				});

			Content = stack;
		}

		ExtendedEntry ent(int num)
		{
			ExtendedEntry ent = new ExtendedEntry
			{
				Placeholder = "Preset " + (num + 1),
				Text = App.MANAGER.getValueOfPreset(num+1).ToString("n2"),
				HorizontalOptions = LayoutOptions.FillAndExpand,
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				Keyboard = Keyboard.Numeric,
				PlaceholderTextColor = Constants.palette.secondary_text
			};
			ent.TextChanged += (s, e) =>
			{
				if (e.NewTextValue != null)
					foreach (char c in e.NewTextValue)
					{
						if (!Char.IsDigit(c) && c != '.' && c != ',')
						{
							ent.Text = e.OldTextValue;
						}
					}
			};
			return ent;
		}

	}
}

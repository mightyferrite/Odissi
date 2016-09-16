using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Xml;
using System.Diagnostics;
using System.Reflection;
using System.IO;

namespace Odissi
{
	public partial class OdissiPage : ContentPage
	{
		public OdissiPage()
		{
			InitializeComponent();
#if __ANDROID__
			string phonetype = "Droid";
#else
			string phonetype = "iOS";
#endif

			ObservableCollection<Word> words = new ObservableCollection<Word>();
			WordView.ItemsSource = words;
			WordView.ItemTapped += (sender, e) =>
			{
				Word item = (Word)e.Item;

				if (item == null)
				{
					Debug.Print("nooooothing selected");
					//await homePage.DisplayAlert("Tapped!", "No item was selected", "OK", null);
				}
				else
				{
					//await homePage.DisplayAlert("Tapped!", item.Name + " was tapped.", "OK", null);
					Debug.Print("selected:" + item.Achumawi);
					DependencyService.Get<IAudio>().PlayAudioFile(item.Sound);

					((ListView)sender).SelectedItem = null;
					//await DisplayAlert("Tapped", item.Achumawi, "OK");
					//FormsXamlPage secondPage = new FormsXamlPage();
					//secondPage.item = item;
					//await homePage.Navigation.PushAsync(secondPage);
				}
			};

			var assembly = typeof(Word).GetTypeInfo().Assembly;
#if __ANDROID__
			Stream stream = assembly.GetManifestResourceStream("Odissi.Droid.worddata.xml");
#else
			Stream stream = assembly.GetManifestResourceStream("Odissi." + phonetype + ".worddata.xml");
			                                                   
#endif
			XmlDocument xmlDoc = new XmlDocument(); // Create an XML document object
			xmlDoc.Load(stream); // Load the XML document from the specified file

			// Get elements
			XmlNodeList rows = xmlDoc.GetElementsByTagName("row");
			foreach (XmlNode row in rows)
			{
				var englishString = row["English"].InnerText;
				var commentaryString = row["Commentary"].InnerText;
				var achumawiString = row["Achumawi"].InnerText;
				var soundString = row["Sound"].InnerText;
				var imageString = row["Image"].InnerText;

				words.Add(new Word { Achumawi = achumawiString, Commentary = commentaryString, English = englishString, Sound = soundString, Image = imageString });
			}
		}

	}
}


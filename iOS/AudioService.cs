using System;
using Xamarin.Forms;
using Odissi;
using Odissi.iOS;
using System.IO;
using Foundation;
using AVFoundation;

[assembly: Dependency(typeof(AudioService))]
namespace Odissi.iOS
{
	public class AudioService : IAudio
	{
		public AudioService()
		{
		}

		public void PlayAudioFile(string fileName)
		{
			var directories = Directory.EnumerateDirectories("./");
			foreach (var directory in directories)
			{
				Console.WriteLine(directory);
			}
			string s = Path.GetFileNameWithoutExtension(fileName);
			string t = Path.GetExtension(fileName);
			//string sFilePath = NSBundle.MainBundle.PathForResource(Path.GetFileNameWithoutExtension(fileName), Path.GetExtension(fileName));
			var url = NSUrl.FromString("./audiofiles/" + fileName);
			var _player = AVAudioPlayer.FromUrl(url);
			//_player.Volume = 255;

			_player.FinishedPlaying += (object sender, AVStatusEventArgs e) =>
			{
				_player = null;
			};
			_player.Play();
		}
	}
}
using System;
using Xamarin.Forms;
using Odissi.Droid;
using Android.Media;
using Android.Content.Res;
using System.Reflection;


[assembly: Dependency(typeof(AudioService))]
namespace Odissi.Droid
{
	public class AudioService : IAudio
	{
		private MediaPlayer player;
		public AudioService()
		{
		}

		public void PlayAudioFile(string fileName)
		{
			var directories = System.IO.Directory.EnumerateDirectories("./");
			foreach (var directory in directories)
			{
				Console.WriteLine(directory);
			}

			var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			directories = System.IO.Directory.EnumerateDirectories(path + "/..");
			foreach (var directory in directories)
			{
				Console.WriteLine(directory);
			}

			var assembly = typeof(AudioService).GetTypeInfo().Assembly;
			foreach (var ress in assembly.GetManifestResourceNames())
				System.Diagnostics.Debug.WriteLine("found resource: " + ress);


			player = new MediaPlayer();
			AssetManager assets = global::Android.App.Application.Context.Assets;
			//String[] filelist = assets.List("");
			AssetFileDescriptor fd = global::Android.App.Application.Context.Assets.OpenFd("audiofiles/" + fileName);


			player.Prepared += (s, e) =>
			{
				player.Start();
			};
			player.SetDataSource(fd.FileDescriptor, fd.StartOffset, fd.Length);
			if (player.IsPlaying)
			{
				player.Pause();
			}
			//player.Prepare();
			player.PrepareAsync();
		}

	}
}
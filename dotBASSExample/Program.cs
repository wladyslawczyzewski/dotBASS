using System;
using dotBASS;

namespace dotBASSExample
{
	class Program
	{
		static void Main(string[] args)
		{
			if (BASS.BASS_Init(-1, 44100, BASSInitFlags.BASS_DEVICE_DEFAULTS, IntPtr.Zero))
			{
				int stream = BASS.BASS_StreamCreateFile(@"D:\Projects\dotBASS\dotBASSExample\bin\Debug\Test.mp3", 0, 0, BASSFlag.BASS_DEFAULT);
				if (stream != 0)
				{
					BASS.BASS_ChannelPlay(stream, false);
					Console.WriteLine("Press any key to exit...");
					Console.ReadKey(false);
					BASS.BASS_StreamFree(stream);
				}
				else
					Console.WriteLine(BASS.BASS_ErrorGetCode());
				BASS.BASS_Free();
			}
		}
	}
}

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
				UInt32 stream = BASS.BASS_StreamCreateFile(@"D:\Projects\dotBASS\dotBASSExample\bin\Debug\Test.mp3", 0, 0, BASSFlag.BASS_DEFAULT);
				if (stream != 0)
				{
					Console.WriteLine("Set playback position (in seconds):");
					double pos = double.Parse(Console.ReadLine());
					Console.WriteLine("Set master volume levevl (0..1):");
					float vlevel = float.Parse(Console.ReadLine());
					BASS.BASS_SetVolume(vlevel);
					BASS.BASS_ChannelSetPosition(stream, BASS.BASS_ChannelSeconds2Bytes(stream, pos), BASSPosMode.BASS_POS_BYTE);
					BASS.BASS_ChannelPlay(stream, false);
					while (true)
					{
						TimeSpan t = TimeSpan.FromSeconds(BASS.BASS_ChannelBytes2Seconds(stream, BASS.BASS_ChannelGetPosition(stream, BASSPosMode.BASS_POS_BYTE)));
						Console.Write("\rPlayback postion: " + string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms", t.Hours, t.Minutes, t.Seconds, t.Milliseconds) + string.Format(" Master volume: {0}", BASS.BASS_GetVolume().ToString()));
					}
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

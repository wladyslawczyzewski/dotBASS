using System;
using dotBASS.BASS;

namespace dotBASSExample
{
	class Program
	{
		static void Main(string[] args)
		{
			BASS.BASS_Init(-1, 44100, BASSInitFlags.BASS_DEVICE_DEFAULTS, IntPtr.Zero);
			Console.WriteLine(BASS.BASS_GetVersion());
			Console.WriteLine(BASS.BASS_ErrorGetCode());
			BASS.BASS_Free();
		}
	}
}

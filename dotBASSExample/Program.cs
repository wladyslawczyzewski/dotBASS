/**
 * dotBASS published under MS-PL license
 * Copyright © Vladyslav Chyzhevskyi, 2012
 * BASS.dll wrapper for .NET example usage
 */

using System;
using dotBASS;
using dotBASS.Tags;

namespace dotBASSExample
{
	class Program
	{
		static void Main(string[] args)
		{
			if (BASS.BASS_Init(-1, 44100, BASSInitFlags.BASS_DEVICE_DEFAULTS, IntPtr.Zero))
			{
				UInt32 stream = BASS.BASS_StreamCreateFile(@"D:\alarm.mp3", 0, 0, BASSFlag.BASS_DEFAULT);
				if (stream != 0)
				{
					Console.WriteLine("Set playback position (in seconds):");
					double pos = double.Parse(Console.ReadLine());
					Console.WriteLine("Set master volume levevl (0..1):");
					float vlevel = float.Parse(Console.ReadLine());
					BASS.BASS_SetVolume(vlevel);
					BASS.BASS_ChannelSetPosition(stream, BASS.BASS_ChannelSeconds2Bytes(stream, pos), BASSPosMode.BASS_POS_BYTE);
					BASS.BASS_ChannelPlay(stream, false);
					BASS_TAG tags = BASS_TAG.BASS_ChannelGetTags(stream, BASSTagFlags.BASS_TAG_ID3V2);
					Console.WriteLine("Artist: {0}\nTitle: {1}\nAlbum: {2}\nTrackNo: {3}\nGenre: {4}\nYear: {5}\nComment: {6}", tags.Artist, tags.Title, tags.Album, tags.TrackNo, tags.Genre, tags.Year, tags.Comment);
					while (true)
					{
						TimeSpan t = TimeSpan.FromSeconds(BASS.BASS_ChannelBytes2Seconds(stream, BASS.BASS_ChannelGetPosition(stream, BASSPosMode.BASS_POS_BYTE)));
						Console.Write(string.Format("\rPlayback postion: {0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms; CPU usage: {4}", t.Hours, t.Minutes, t.Seconds, t.Milliseconds, BASS.BASS_GetCPU()));
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

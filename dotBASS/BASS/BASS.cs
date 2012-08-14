using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace dotBASS.BASS
{
	public class BASS
	{
		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool BASS_Init(int device, int freq, BASSInitFlags flags, IntPtr win, IntPtr clsid);

		public static bool BASS_Init(int device, int freq, BASSInitFlags flags, IntPtr win)
		{
			return BASS_Init(device, freq, flags, win, IntPtr.Zero);
		}

		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BASS_Free();

		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		public static extern BASSError BASS_ErrorGetCode();

		[DllImport(@"bass.dll", EntryPoint = "BASS_GetVersion", CharSet = CharSet.Auto)]
		private static extern int BASS_Version();

		public static string BASS_GetVersion()
		{
			string version = BASS_Version().ToString("x");
			return string.Format("{0}.{1}.{2}", int.Parse(version[0].ToString()),
				int.Parse(version[1].ToString() + version[2].ToString()),
				int.Parse(version[3].ToString() + version[4].ToString()));
		}

		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		private static extern int BASS_StreamCreateFile([MarshalAs(UnmanagedType.Bool)] bool mem, [MarshalAs(UnmanagedType.LPWStr), In] string file, long offset, long length, BASSFlag flags);

		public static int BASS_StreamCreateFile(string file, long offset, long length, BASSFlag flags)
		{
			flags |= BASSFlag.BASS_UNICODE;
			return BASS_StreamCreateFile(false, file, offset, length, flags);
		}

		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BASS_StreamFree(int handle);

		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BASS_ChannelPlay(int handle, [MarshalAs(UnmanagedType.Bool)] bool restart);

		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BASS_ChannelStop(int handle);

		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BASS_ChannelPause(int handle);
	}
}

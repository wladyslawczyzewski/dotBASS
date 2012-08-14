/**
 * dotBASS published under MS-PL license
 * Copyright © Vladyslav Chyzhevskyi, 2012
 * BASS.dll wrapper for .NET
 */

using System;
using System.Runtime.InteropServices;

namespace dotBASS
{
	/// <summary>
	/// Opensource BASS.dll wrapper for .NET published under MS-PL license conditions.
	/// For latest version visit http://github.com/coirius/dotBASS
	/// </summary>
	public class BASS
	{
		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool BASS_Init(int device, int freq, BASSInitFlags flags, IntPtr win, IntPtr clsid);

		/// <summary>
		/// Initializes an output device.
		/// </summary>
		/// <param name="device">The device to use; -1 = default, 0 = no sound, 1 = first real output</param>
		/// <param name="freq">Output sample rate</param>
		/// <param name="flags">Combination of BASSInitFlags</param>
		/// <param name="win">The application's main window; 0 = the current foregound window</param>
		/// <returns>true if the device was successfully initialized, else false</returns>
		public static bool BASS_Init(int device, int freq, BASSInitFlags flags, IntPtr win)
		{
			return BASS_Init(device, freq, flags, win, IntPtr.Zero);
		}

		/// <summary>
		/// Frees all resources used by the output device
		/// </summary>
		/// <returns>true if successful, else false is returned</returns>
		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BASS_Free();

		/// <summary>
		/// Retrieves the error code of the most recent BASS function call
		/// </summary>
		/// <returns>Error code</returns>
		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		public static extern BASSError BASS_ErrorGetCode();

		[DllImport(@"bass.dll", EntryPoint = "BASS_GetVersion", CharSet = CharSet.Auto)]
		private static extern int BASS_Version();

		/// <summary>
		/// Retrieves the version of BASS
		/// </summary>
		/// <returns>The BASS version, for example, 2.4.0</returns>
		public static string BASS_GetVersion()
		{
			string version = BASS_Version().ToString("x");
			return string.Format("{0}.{1}.{2}", int.Parse(version[0].ToString()), int.Parse(version[1].ToString() + version[2].ToString()), int.Parse(version[3].ToString() + version[4].ToString()));
		}

		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		private static extern int BASS_StreamCreateFile([MarshalAs(UnmanagedType.Bool)] bool mem, [MarshalAs(UnmanagedType.LPWStr), In] string file, long offset, long length, BASSFlag flags);

		/// <summary>
		/// Creates a sample stream from file
		/// </summary>
		/// <param name="file">Path to file</param>
		/// <param name="offset">File offset to begin streaming from</param>
		/// <param name="length">Data length; 0 = use all data up to the end of the file</param>
		/// <param name="flags">Combination of BASSFlags</param>
		/// <returns>Stream's handle if successful, else 0 is returned.</returns>
		public static int BASS_StreamCreateFile(string file, long offset, long length, BASSFlag flags)
		{
			flags |= BASSFlag.BASS_UNICODE;
			return BASS_StreamCreateFile(false, file, offset, length, flags);
		}

		/// <summary>
		/// Frees a sample stream's resources
		/// </summary>
		/// <param name="handle">The stream handle</param>
		/// <returns>true if successful, else false is returned</returns>
		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BASS_StreamFree(int handle);

		/// <summary>
		/// Starts or resumes playback of a stream
		/// </summary>
		/// <param name="handle">The stream handle</param>
		/// <param name="restart">Restart playback from the beginnig? [true|false]</param>
		/// <returns>true if successful, else false is returned</returns>
		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BASS_ChannelPlay(int handle, [MarshalAs(UnmanagedType.Bool)] bool restart);

		/// <summary>
		/// Stops a stream playback
		/// </summary>
		/// <param name="handle">The stream handle</param>
		/// <returns>true if successful, else false is returned</returns>
		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BASS_ChannelStop(int handle);

		/// <summary>
		/// Pauses a stream playbach
		/// </summary>
		/// <param name="handle">The stream handle</param>
		/// <returns>true if successful, else false is returned</returns>
		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BASS_ChannelPause(int handle);
	}
}

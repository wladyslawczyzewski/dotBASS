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
	/// For more detailed documentation visit http://un4seen.com/doc
	/// </summary>
	public class BASS
	{
		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool BASS_Init(int device, UInt32 freq, BASSInitFlags flags, IntPtr win, IntPtr clsid);

		/// <summary>
		/// Initializes an output device.
		/// </summary>
		/// <param name="device">The device to use; -1 = default, 0 = no sound, 1 = first real output</param>
		/// <param name="freq">Output sample rate</param>
		/// <param name="flags">Combination of BASSInitFlags</param>
		/// <param name="win">The application's main window; 0 = the current foregound window</param>
		/// <returns>true if the device was successfully initialized, else false</returns>
		public static bool BASS_Init(int device, UInt32 freq, BASSInitFlags flags, IntPtr win)
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
		private static extern UInt32 BASS_Version();

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
		private static extern UInt32 BASS_StreamCreateFile([MarshalAs(UnmanagedType.Bool)] bool mem, [MarshalAs(UnmanagedType.LPWStr), In] string file, UInt64 offset, UInt64 length, BASSFlag flags);

		/// <summary>
		/// Creates a sample stream from file
		/// </summary>
		/// <param name="file">Path to file</param>
		/// <param name="offset">File offset to begin streaming from</param>
		/// <param name="length">Data length; 0 = use all data up to the end of the file</param>
		/// <param name="flags">Combination of BASSFlags</param>
		/// <returns>Stream's handle if successful, else 0 is returned.</returns>
		public static UInt32 BASS_StreamCreateFile(string file, UInt64 offset, UInt64 length, BASSFlag flags)
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
		public static extern bool BASS_StreamFree(UInt32 handle);

		/// <summary>
		/// Starts or resumes playback of a stream
		/// </summary>
		/// <param name="handle">The stream handle</param>
		/// <param name="restart">Restart playback from the beginnig? [true|false]</param>
		/// <returns>true if successful, else false is returned</returns>
		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BASS_ChannelPlay(UInt32 handle, [MarshalAs(UnmanagedType.Bool)] bool restart);

		/// <summary>
		/// Stops a stream playback
		/// </summary>
		/// <param name="handle">The stream handle</param>
		/// <returns>true if successful, else false is returned</returns>
		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BASS_ChannelStop(UInt32 handle);

		/// <summary>
		/// Pauses a stream playbach
		/// </summary>
		/// <param name="handle">The stream handle</param>
		/// <returns>true if successful, else false is returned</returns>
		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BASS_ChannelPause(UInt32 handle);

		/// <summary>
		/// Retrieves the playback position of a stream.
		/// </summary>
		/// <param name="handle">The stream handle</param>
		/// <param name="mode">How to retrieve the posion</param>
		/// <returns>If successful, the channel postion is returned, else -1 is returned</returns>
		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		public static extern UInt64 BASS_ChannelGetPosition(UInt32 handle, BASSPosMode mode);

		/// <summary>
		/// Sets the playback postion of a stream
		/// </summary>
		/// <param name="handle">The stream handle</param>
		/// <param name="pos">The postion, in units determined by the mode</param>
		/// <param name="mode">How to set the postion</param>
		/// <returns>true if successful, else false is returned</returns>
		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BASS_ChannelSetPosition(UInt32 handle, UInt64 pos, BASSPosMode mode);

		/// <summary>
		/// Translates a byte position into time (seconds)
		/// </summary>
		/// <param name="handle">The stream handle</param>
		/// <param name="pos">The position to translate</param>
		/// <returns>If successful, then the translated length is returned, else a negative value is returned</returns>
		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		public static extern double BASS_ChannelBytes2Seconds(UInt32 handle, UInt64 pos);

		/// <summary>
		/// Translate a time (seconds) position into bytes
		/// </summary>
		/// <param name="handle">The stream handle</param>
		/// <param name="pos">The position to translate</param>
		/// <returns>If successful, then the translated length is returned, else -1 is returned</returns>
		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		public static extern UInt64 BASS_ChannelSeconds2Bytes(UInt32 handle, double pos);
	}
}

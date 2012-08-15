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
		private static extern UInt32 BASS_StreamCreateFile([MarshalAs(UnmanagedType.Bool)] bool mem, [MarshalAs(UnmanagedType.LPWStr)] string file, UInt64 offset, UInt64 length, BASSFlag flags);

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

		/// <summary>
		/// Retrieves the current master volume level
		/// </summary>
		/// <returns>If successful, the volume level is returned, else -1 is returned</returns>
		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		public static extern float BASS_GetVolume();

		/// <summary>
		/// Sets the output master volume
		/// </summary>
		/// <param name="volume">The volume level; 0 - silent, 1 - max</param>
		/// <returns>true if successful, else false is returned</returns>
		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BASS_SetVolume(float volume);

		/// <summary>
		/// Retrieves the current CPU usage of BASS
		/// </summary>
		/// <returns>The BASS CPU use as a percentage</returns>
		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		public static extern float BASS_GetCPU();

		/// <summary>
		/// Retrieves onformation on the device being used
		/// </summary>
		/// <param name="info">Reference to a structure to receive the information</param>
		/// <returns>true is successful, else false is returned</returns>
		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BASS_GetInfo(ref BASS_INFO info);

		/// <summary>
		/// Retrieves the device setting of the current thread
		/// </summary>
		/// <returns>If succesful, the device number is returned, else -1 is returned</returns>
		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		public static extern UInt32 BASS_GetDevice();

		/// <summary>
		/// Sets the device to use for subsquent calls in the current thread
		/// </summary>
		/// <param name="device">The device to use: 0 = no sound, 1 = first real output device</param>
		/// <returns>true if succesful, else false is returned</returns>
		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BASS_SetDevice(UInt32 device);

		[DllImport(@"bass.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool BASS_GetDeviceInfo(UInt32 device, ref BASS_DEVICEINFOP info);

		private struct BASS_DEVICEINFOP
		{
			public IntPtr name;
			public IntPtr driver;
			public UInt32 flags;
		}

		/// <summary>
		/// Retrieves information on an output device
		/// </summary>
		/// <param name="device">The device to get the information; 0 = first</param>
		/// <returns>If successful, device information structure is returned, else blank structure is returned</returns>
		public static BASS_DEVICEINFO BASS_GetDeviceInfo(UInt32 device)
		{
			BASS_DEVICEINFOP a = new BASS_DEVICEINFOP();
			if (BASS_GetDeviceInfo(device, ref a))
			{
				BASS_DEVICEINFO info = new BASS_DEVICEINFO();
				info.name = Marshal.PtrToStringAnsi(a.name);
				info.driver = Marshal.PtrToStringAnsi(a.driver);
				info.flags = a.flags;
				return info;
			}
			return new BASS_DEVICEINFO();
		}

		[DllImport(@"bass.dll", EntryPoint = "BASS_ChannelGetTags", CharSet = CharSet.Auto)]
		private static extern IntPtr BASS_ChannelGetTagsP(UInt32 handle, BASSTagFlags flags);

		/// <summary>
		/// Retrieves tags/header from a channel
		/// </summary>
		/// <param name="handle">The channel handle</param>
		/// <param name="flags">The tags/header wanted, on of BASSTagFlags</param>
		/// <returns>If successful, requested tags are returned, else null is returned</returns>
		public static BASS_TAG BASS_ChannelGetTags(UInt32 handle, BASSTagFlags flags)
		{
			BASS_TAG tags = null;
			switch (flags)
			{
				case BASSTagFlags.BASS_TAG_ID3:
					{
						BASS_TAG_ID3 tmp = (BASS_TAG_ID3)Marshal.PtrToStructure(BASS_ChannelGetTagsP(handle, flags), typeof(BASS_TAG_ID3));
						tags = new BASS_TAG();
						if (tmp.comment[28] == (char)0)
						{
							// if 29 byte of comment is null ('\0') then 30 byte of comment string is track nomber
							tags.TrackNo = tmp.comment[29];
							tmp.comment[29] = (char)0;
						}
						tags.Album = new string(tmp.album).Replace("\0", "").Trim();
						tags.Artist = new string(tmp.artist).Replace("\0", "").Trim();
						tags.Comment = new string(tmp.comment).Replace("\0", "").Trim();
						if (tmp.genre > 147)
							tags.Genre = tags.BASS_TAG_GENRE[147];
						else
							tags.Genre = tags.BASS_TAG_GENRE[tmp.genre];
						tags.Title = new string(tmp.title).Replace("\0", "").Trim();
						tags.Year = new string(tmp.year).Replace("\0", "").Trim();
						break;
					}
				case BASSTagFlags.BASS_TAG_ID3V2:
					break;
				case BASSTagFlags.BASS_TAG_OGG:
					break;
				case BASSTagFlags.BASS_TAG_HTTP:
					break;
				case BASSTagFlags.BASS_TAG_ICY:
					break;
				case BASSTagFlags.BASS_TAG_META:
					break;
				case BASSTagFlags.BASS_TAG_APE:
					break;
				case BASSTagFlags.BASS_TAG_MP4:
					break;
				case BASSTagFlags.BASS_TAG_VENDOR:
					break;
				case BASSTagFlags.BASS_TAG_LYRICS3:
					break;
				case BASSTagFlags.BASS_TAG_CA_CODEC:
					break;
				case BASSTagFlags.BASS_TAG_MF:
					break;
				case BASSTagFlags.BASS_TAG_WAVEFORMAT:
					break;
				case BASSTagFlags.BASS_TAG_RIFF_INFO:
					break;
				case BASSTagFlags.BASS_TAG_RIFF_BEXT:
					break;
				case BASSTagFlags.BASS_TAG_RIFF_CART:
					break;
				case BASSTagFlags.BASS_TAG_RIFF_DISP:
					break;
				case BASSTagFlags.BASS_TAG_APE_BINARY:
					break;
				case BASSTagFlags.BASS_TAG_MUSIC_NAME:
					break;
				case BASSTagFlags.BASS_TAG_MUSIC_MESSAGE:
					break;
				case BASSTagFlags.BASS_TAG_MUSIC_ORDERS:
					break;
				case BASSTagFlags.BASS_TAG_MUSIC_INST:
					break;
				case BASSTagFlags.BASS_TAG_MUSIC_SAMPLE:
					break;
				default:
					break;
			}
			return tags;
		}
	}
}

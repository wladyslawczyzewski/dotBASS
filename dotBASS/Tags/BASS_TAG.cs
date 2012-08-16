/**
 * dotBASS published under MS-PL license
 * Copyright © Vladyslav Chyzhevskyi, 2012
 * BASS.dll wrapper for .NET
 */

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using dotBASS.Utils;

namespace dotBASS.Tags
{
	public sealed class BASS_TAG
	{
		internal string[] BASS_TAG_GENRE = new string[148]
		{
			"Blues", "Classic Rock", "Country", "Dance", "Disco", "Funk", "Grunge", "Hip-Hop", "Jazz", "Metal", "New Age", "Oldies", "Other", "Pop", "R&B", "Rap", "Reggae", "Rock", "Techno", "Industrial", "Alternative", "Ska", "Death Metal", "Pranks", "Soundtrack", "Euro-Techno", "Ambient", "Trip-Hop", "Vocal", "Jazz+Funk", "Fusion", "Trance", "Classical", "Instrumental", "Acid", "House", "Game", "Sound Clip", "Gospel", "Noise", "Alternative Rock", "Bass", "Soul", "Punk", "Space", "Meditative", "Instrumental Pop", "Instrumental Rock", "Ethnic", "Gothic", "Darkwave", "Techno-Industrial", "Electronic", "Pop-Folk", "Eurodance", "Dream", "Southern Rock", "Comedy", "Cult", "Gangsta", "Top Christian Rap", "Pop/Funk", "Jungle", "Native US", "Cabaret", "New Wave", "Psychadelic", "Rave", "Showtunes", "Trailer", "Lo-Fi", "Tribal", "Acid Punk", "Acid Jazz", "Polka", "Retro", "Musical", "Rock & Roll", "Hard Rock", "Folk", "Folk-Rock", "National Folk", "Swing", "Fast Fusion", "Bebob", "Latin", "Revival", "Celtic", "Bluegrass", "Avantgarde", "Gothic Rock", "Progressive Rock", "Psychedelic Rock", "Symphonic Rock", "Slow Rock", "Big Band", "Chorus", "Easy Listening", "Acoustic", "Humour", "Speech", "Chanson", "Opera", "Chamber Music", "Sonata", "Symphony", "Booty Bass", "Primus", "Porn Groove", "Satire", "Slow Jam", "Club", "Tango", "Samba", "Folklore", "Ballad", "Power Ballad", "Rhythmic Soul", "Freestyle", "Duet", "Punk Rock", "Drum Solo", "Acapella", "Euro-House", "Dance Hall", "Goa", "Drum & Bass", "Club - House", "Hardcore", "Terror", "Indie", "BritPop", "Negerpunk", "Polsk Punk", "Beat", "Christian Gangsta Rap", "Heavy Metal", "Black Metal", "Crossover", "Contemporary Christian", "Christian Rock", "Merengue", "Salsa", "Thrash Metal", "Anime", "JPop", "Synthpop", "Unknown"
		};
		public string Title { get; set; }
		public string Artist { get; set; }
		public string Album { get; set; }
		public string Year { get; set; }
		public string Comment { get; set; }
		public string Genre { get; set; }
		public int TrackNo { get; set; }

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
						TAG_ID3 tmp = (TAG_ID3)Marshal.PtrToStructure(BASS_ChannelGetTagsP(handle, flags), typeof(TAG_ID3));
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
					{
						IntPtr ptr = BASS_ChannelGetTagsP(handle, flags);
						if (Marshal.PtrToStringAnsi(ptr, 3) == "ID3")
						{
							byte[] version = new byte[2]
							{
								Marshal.ReadByte(ptr, 3),
								Marshal.ReadByte(ptr, 4)
							};
							byte fl = Marshal.ReadByte(ptr, 5);
							int size = (new byte[4]
								{
									Marshal.ReadByte(ptr, 6),
									Marshal.ReadByte(ptr, 7),
									Marshal.ReadByte(ptr, 8),
									Marshal.ReadByte(ptr, 9)
								}).FromSynchsafeInt2Int();
							int i = 10;
							tags = new BASS_TAG();
							while (true)
							{
								string frameId = string.Format("{0}{1}{2}{3}", (char)Marshal.ReadByte(ptr, i), (char)Marshal.ReadByte(ptr, i + 1), (char)Marshal.ReadByte(ptr, i + 2), (char)Marshal.ReadByte(ptr, i + 3));
								int frameSize = (new byte[4] { Marshal.ReadByte(ptr, i + 4), Marshal.ReadByte(ptr, i + 5), Marshal.ReadByte(ptr, i + 6), Marshal.ReadByte(ptr, i + 7) }).FromSynchsafeInt2Int();
								byte[] frameFlags = new byte[2] { Marshal.ReadByte(ptr, i + 8), Marshal.ReadByte(ptr, i + 9) };
								if (i + 10 + frameSize > size)
									break;
								byte frameEncoding = Marshal.ReadByte(ptr, i + 10);
								List<byte> frameContent = new List<byte>();
								i += 11;
								if (frameId.Equals("COMM"))
									i += 7;
								byte[] bomByteOrder = new byte[2] { 0, 0 };
								if (frameEncoding == 1)
									bomByteOrder = new byte[2] { Marshal.ReadByte(ptr, i), Marshal.ReadByte(ptr, i + 1) };
								i += 2;
								int j = i + frameSize - 3;
								if (frameId.Equals("COMM"))
									j -= 7;
								while (i < j)
								{
									frameContent.Add(Marshal.ReadByte(ptr, i));
									i++;
								}
								string tconntent = string.Empty;
								if (frameEncoding == 0)
									tconntent = Encoding.ASCII.GetString(frameContent.ToArray()).Replace('\0', ' ');
								else if (frameEncoding == 1 && bomByteOrder[0] == 0xFF && bomByteOrder[1] == 0xFE)
									tconntent = Encoding.Unicode.GetString(frameContent.ToArray()).Replace('\0', ' ');
								else if (frameEncoding == 1 && bomByteOrder[0] == 0xFE && bomByteOrder[1] == 0xFF)
									tconntent = Encoding.BigEndianUnicode.GetString(frameContent.ToArray()).Replace('\0', ' ');
								else if (frameEncoding == 2)
									tconntent = Encoding.BigEndianUnicode.GetString(frameContent.ToArray()).Replace('\0', ' ');
								else if (frameEncoding == 3)
									tconntent = Encoding.UTF8.GetString(frameContent.ToArray()).Replace('\0', ' ');
								switch (frameId)
								{
									case "TALB":
										{
											tags.Album = tconntent;
											break;
										}
									case "TOPE":
										{
											tags.Artist = tconntent;
											break;
										}
									case "COMM":
										{
											tags.Comment = tconntent;
											break;
										}
									case "TCON":
										{
											Match ma = Regex.Match(tconntent, @"\((?<num>[\d]+)\)");
											if (ma.Groups["num"].Value != string.Empty)
											{
												int g = int.Parse(ma.Groups["num"].Value);
												if (g > 147)
													tags.Genre = tags.BASS_TAG_GENRE[147];
												else
													tags.Genre = tags.BASS_TAG_GENRE[g];
											}
											else
												tags.Genre = tags.BASS_TAG_GENRE[147];
											break;
										}
									case "TIT2":
										{
											tags.Title = tconntent;
											break;
										}
									case "TRCK":
										{
											tags.TrackNo = int.Parse(tconntent);
											break;
										}
									case "TYER":
										{
											tags.Year = tconntent;
											break;
										}
									default:
										break;
								}
							}
						}
						break;
					}
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

/**
 * dotBASS published under MS-PL license
 * Copyright © Vladyslav Chyzhevskyi, 2012
 * BASS.dll wrapper for .NET
 */

using System;
using System.Runtime.InteropServices;

namespace dotBASS.Tags
{
	internal struct TAG_ID3
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public char[] id;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
		public char[] title;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
		public char[] artist;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
		public char[] album;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public char[] year;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
		public char[] comment;
		public Byte genre;
	}
}

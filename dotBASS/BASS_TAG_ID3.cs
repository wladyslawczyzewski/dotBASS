using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace dotBASS
{
	internal struct BASS_TAG_ID3
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

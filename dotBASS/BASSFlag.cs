/**
 * dotBASS published under MS-PL license
 * Copyright © Vladyslav Chyzhevskyi, 2012
 * BASS.dll wrapper for .NET
 */

using System;

namespace dotBASS
{
	[Flags]
	public enum BASSFlag : uint
	{
		/// <summary>
		/// Defaults
		/// </summary>
		BASS_DEFAULT = 0,

		/// <summary>
		/// File is in UTF-16 form. Otherwise it is ANSI on Windows or Windows CE, and UTF-8 on other platforms.
		/// </summary>
		BASS_UNICODE = 2147483648
	}
}

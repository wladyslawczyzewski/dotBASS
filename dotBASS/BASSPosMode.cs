using System;
namespace dotBASS
{
	[Flags]
	public enum BASSPosMode : uint
	{
		/// <summary>
		/// Get the position in bytes
		/// </summary>
		BASS_POS_BYTE = 0,

		/// <summary>
		/// Get the posion in orders and rows.. LOWORD = order, HIWORD = row * scaler
		/// </summary>
		BASS_POS_MUSIC_ORDER = 1,

		/// <summary>
		/// Get the decoding (not playback) position
		/// </summary>
		BASS_POS_DECODE = 268435456,

		/// <summary>
		/// Decode to the position instead of seeking
		/// </summary>
		BASS_POS_DECODETO = 536870912
	}
}

using System;

namespace dotBASS
{
	/// <summary>
	/// Used with BASS_GetDeviceInfo() to retrieve information on a device
	/// </summary>
	public struct BASS_DEVICEINFO
	{
		/// <summary>
		/// Description of the device
		/// </summary>
		public string name;

		/// <summary>
		/// The filename of the driver
		/// </summary>
		public string driver;

		/// <summary>
		/// The device's current status
		/// </summary>
		public UInt32 flags;
	}
}

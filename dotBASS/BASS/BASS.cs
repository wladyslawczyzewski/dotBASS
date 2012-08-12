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
    }
}

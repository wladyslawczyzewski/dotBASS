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
    ///     Used with BASS_GetInfo() to retrieve information on the current device
    /// </summary>
    public struct BASS_INFO
    {
        /// <summary>
        ///     DirectSound version
        /// </summary>
        public UInt32 dsver;

        /// <summary>
        ///     The device supports EAX and has it enabled?
        /// </summary>
        [MarshalAs(UnmanagedType.Bool)] public bool eax;

        /// <summary>
        ///     The device's capabilities
        /// </summary>
        public UInt32 flags;

        /// <summary>
        ///     The number of free 3D sample slots in the hardware
        /// </summary>
        public UInt32 free3d;

        /// <summary>
        ///     The number of free sample slots in the hardware
        /// </summary>
        public UInt32 freesam;

        /// <summary>
        ///     The divice's current output sample rate
        /// </summary>
        public UInt32 freq;

        /// <summary>
        ///     The device's amount of free hardware memory
        /// </summary>
        public UInt32 hwfree;

        /// <summary>
        ///     The device's total amount of hardware memory
        /// </summary>
        public UInt32 hwsize;

        /// <summary>
        ///     The flags parameter of the BASS_init() call
        /// </summary>
        public UInt32 initflags;

        /// <summary>
        ///     The average delay for playback of stream
        /// </summary>
        public UInt32 latency;

        /// <summary>
        ///     The maximum sample rate supported by hardwware
        /// </summary>
        public UInt32 maxrate;

        /// <summary>
        ///     The minimum buffer length
        /// </summary>
        public UInt32 minbuf;

        /// <summary>
        ///     The minimum sample rate supported by hardware
        /// </summary>
        public UInt32 minrate;

        /// <summary>
        ///     The number of available speakers; 2 means that there is no support foe speaker assignment
        /// </summary>
        public UInt32 speakers;
    }
}
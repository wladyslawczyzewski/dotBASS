/**
 * dotBASS published under MS-PL license
 * Copyright © Vladyslav Chyzhevskyi, 2012
 * BASS.dll wrapper for .NET
 */

using System;

namespace dotBASS
{
    [Flags]
    public enum BASSInitFlags : uint
    {
        /// <summary>
        ///     Defaults
        /// </summary>
        BASS_DEVICE_DEFAULTS = 0,

        /// <summary>
        ///     Use 8-bit resolution, else 16-bit
        /// </summary>
        BASS_DEVICE_8BITS = 1,

        /// <summary>
        ///     Use mono, else stereo
        /// </summary>
        BASS_DEVICE_MONO = 2,

        /// <summary>
        ///     Enable 3D functionality
        /// </summary>
        BASS_DEVICE_3D = 4,

        /// <summary>
        ///     Calculates the latency of the device
        /// </summary>
        BASS_DEVICE_LATENCY = 256,

        /// <summary>
        ///     Use the Windows control panel setting to detect the number of speakers
        /// </summary>
        BASS_DEVICE_CPSPEAKERS = 1024,

        /// <summary>
        ///     Force the enabling of speaker assignment
        /// </summary>
        BASS_DEVICE_SPEAKERS = 2048,

        /// <summary>
        ///     Ignore speaker arrangement
        /// </summary>
        BASS_DEVICE_NOSPEAKER = 4096,

        /// <summary>
        ///     Use ALSA "dmix" plugin
        /// </summary>
        BASS_DEVICE_DMIX = 8192,

        /// <summary>
        ///     Set device sample rate
        /// </summary>
        BASS_DEVICE_FREQ = 16384
    }
}
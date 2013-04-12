/**
 * dotBASS published under MS-PL license
 * Copyright © Vladyslav Chyzhevskyi, 2012
 * BASS.dll wrapper for .NET
 */

using System;

namespace dotBASS
{
    [Flags]
    public enum BASSError
    {
        BASS_ERROR_UNKNOWN = -1,
        BASS_OK = 0,
        BASS_ERROR_MEM = 1,
        BASS_ERROR_FILEOPEN = 2,
        BASS_ERROR_DRIVER = 3,
        BASS_ERROR_BUFLOST = 4,
        BASS_ERROR_HANDLE = 5,
        BASS_ERROR_FORMAT = 6,
        BASS_ERROR_POSITION = 7,
        BASS_ERROR_INIT = 8,
        BASS_ERROR_START = 9,
        BASS_ERROR_ALREADY = 14,
        BASS_ERROR_NOCHAN = 18,
        BASS_ERROR_ILLTYPE = 19,
        BASS_ERROR_ILLPARAM = 20,
        BASS_ERROR_NO3D = 21,
        BASS_ERROR_NOEAX = 22,
        BASS_ERROR_DEVICE = 23,
        BASS_ERROR_NOPLAY = 24,
        BASS_ERROR_FREQ = 25,
        BASS_ERROR_NOTFILE = 27,
        BASS_ERROR_NOHW = 29,
        BASS_ERROR_EMPTY = 31,
        BASS_ERROR_NONET = 32,
        BASS_ERROR_CREATE = 33,
        BASS_ERROR_NOFX = 34,
        BASS_ERROR_NOTAVAIL = 37,
        BASS_ERROR_DECODE = 38,
        BASS_ERROR_DX = 39,
        BASS_ERROR_TIMEOUT = 40,
        BASS_ERROR_FILEFORM = 41,
        BASS_ERROR_SPEAKER = 42,
        BASS_ERROR_VERSION = 43,
        BASS_ERROR_CODEC = 44,
        BASS_ERROR_ENDED = 45,
        BASS_ERROR_BUSY = 46
    }
}
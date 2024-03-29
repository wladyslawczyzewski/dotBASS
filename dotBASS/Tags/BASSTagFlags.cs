﻿/**
 * dotBASS published under MS-PL license
 * Copyright © Vladyslav Chyzhevskyi, 2012
 * BASS.dll wrapper for .NET
 */

using System;

namespace dotBASS
{
    [Flags]
    public enum BASSTagFlags : uint
    {
        BASS_TAG_ID3 = 0,
        BASS_TAG_ID3V2 = 1,
        BASS_TAG_OGG = 2,
        BASS_TAG_HTTP = 3,
        BASS_TAG_ICY = 4,
        BASS_TAG_META = 5,
        BASS_TAG_APE = 6,
        BASS_TAG_MP4 = 7,
        BASS_TAG_VENDOR = 9,
        BASS_TAG_LYRICS3 = 10,
        BASS_TAG_CA_CODEC = 11,
        BASS_TAG_MF = 13,
        BASS_TAG_WAVEFORMAT = 14,
        BASS_TAG_RIFF_INFO = 256,
        BASS_TAG_RIFF_BEXT = 257,
        BASS_TAG_RIFF_CART = 258,
        BASS_TAG_RIFF_DISP = 259,
        BASS_TAG_APE_BINARY = 4096,
        BASS_TAG_MUSIC_NAME = 65536,
        BASS_TAG_MUSIC_MESSAGE = 65537,
        BASS_TAG_MUSIC_ORDERS = 65538,
        BASS_TAG_MUSIC_INST = 65792,
        BASS_TAG_MUSIC_SAMPLE = 66304
    }
}
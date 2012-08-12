namespace dotBASS.BASS
{
    public enum BASSInitFlags
    {
        BASS_DEVICE_DEFAULTS = 0,
        BASS_DEVICE_8BITS = 1,
        BASS_DEVICE_MONO = 2,
        BASS_DEVICE_3D = 4,
        BASS_DEVICE_LATENCY = 256,
        BASS_DEVICE_CPSPEAKERS = 1024,
        BASS_DEVICE_SPEAKERS = 2048,
        BASS_DEVICE_NOSPEAKER = 4096,
        BASS_DEVICE_DMIX = 8192,
        BASS_DEVICE_FREQ = 16384
    }
}

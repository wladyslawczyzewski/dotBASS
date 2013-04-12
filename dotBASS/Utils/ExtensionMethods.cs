/**
 * dotBASS published under MS-PL license
 * Copyright © Vladyslav Chyzhevskyi, 2012
 * BASS.dll wrapper for .NET
 */

namespace dotBASS.Utils
{
    internal static class ExtensionMethods
    {
        internal static int FromSynchsafeInt2Int(this byte[] bytes)
        {
            int res = 0;
            for (int i = 0; i < bytes.Length; i++)
                res |= bytes[i] << (bytes.Length - 1 - i)*7;
            return res;
        }
    }
}
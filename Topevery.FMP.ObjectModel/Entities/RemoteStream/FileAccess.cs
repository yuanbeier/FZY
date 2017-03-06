using System;
using System.Collections.Generic;
using System.Text;

namespace Topevery.FMP.ObjectModel
{
    // Summary:
    //     Defines constants for read, write, or read/write access to a file.
    [Serializable]
    [Flags]
    public enum FileAccess
    {
        None = 0,
        // Summary:
        //     Read access to the file. Data can be read from the file. Combine with Write
        //     for read/write access.
        Read = 1,
        //
        // Summary:
        //     Write access to the file. Data can be written to the file. Combine with Read
        //     for read/write access.
        Write = 2,
        //
        // Summary:
        //     Read and write access to the file. Data can be written to and read from the
        //     file.
        ReadWrite = 3,
    }
}

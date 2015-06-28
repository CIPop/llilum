// ==++==
// 
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// 
// ==--==
/*=============================================================================
**
** Class: ThreadPriority
**
**
** Purpose: Enums for the priorities of a Thread
**
**
=============================================================================*/

namespace System.Threading
{
    using System.Threading;

    [Serializable()]
    public enum ThreadPriority
    {
        /*=========================================================================
        ** Constants for thread priorities.
        =========================================================================*/
        Lowest      = 0,
        BelowNormal = 1,
        Normal      = 2,
        AboveNormal = 3,
        Highest     = 4,
    }
}

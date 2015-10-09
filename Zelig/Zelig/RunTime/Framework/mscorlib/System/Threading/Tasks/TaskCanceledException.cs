// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// =+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+
//
//
//
// An exception for task cancellations.
//
// =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading.Tasks
{
    /// <summary>
    /// Represents an exception used to communicate task cancellation.
    /// </summary>
    [Serializable]
    public class TaskCanceledException : OperationCanceledException
    {
        [NonSerialized]
        private Task m_canceledTask; // The task which has been canceled.

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskCanceledException"/> class.
        /// </summary>
#if EXCEPTION_STRINGS
        public TaskCanceledException() : base(Environment.GetResourceString("TaskCanceledException_ctor_DefaultMessage"))
#else // EXCEPTION_STRINGS
        public TaskCanceledException()
#endif // EXCEPTION_STRINGS
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskCanceledException"/>
        /// class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public TaskCanceledException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskCanceledException"/>
        /// class with a specified error message and a reference to the inner exception that is the cause of
        /// this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public TaskCanceledException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskCanceledException"/> class
        /// with a reference to the <see cref="T:System.Threading.Tasks.Task"/> that has been canceled.
        /// </summary>
        /// <param name="task">A task that has been canceled.</param>
        public TaskCanceledException(Task task) :
            base(
#if EXCEPTION_STRINGS
                Environment.GetResourceString("TaskCanceledException_ctor_DefaultMessage"),
#else // EXCEPTION_STRINGS
                null,
#endif // EXCEPTION_STRINGS
                (task != null) ? task.CancellationToken : default(CancellationToken))
        {
            m_canceledTask = task;
        }

#if DISABLED_FOR_LLILUM
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskCanceledException"/>
        /// class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination. </param>
        protected TaskCanceledException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
#endif // DISABLED_FOR_LLILUM

        /// <summary>
        /// Gets the task associated with this exception.
        /// </summary>
        /// <remarks>
        /// It is permissible for no Task to be associated with a 
        /// <see cref="T:System.Threading.Tasks.TaskCanceledException"/>, in which case
        /// this property will return null.
        /// </remarks>
        public Task Task
        {
            get
            {
                return m_canceledTask;
            }
        }
    }
}

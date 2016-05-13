using System;

namespace Gw2Assist.Core.Scheduler.Jobs.Interfaces
{
    public interface IJob
    {
        /// <summary>
        /// Gets the interval to run the job.
        /// </summary>
        TimeSpan Interval { get; }

        /// <summary>
        /// Gets or sets when the job was last ran. NULL if the job has never run before.
        /// </summary>
        DateTime? LastRanAt { get; set; }

        /// <summary>
        /// Gets the unique name of the job.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets or set the action to run when the job is completed.
        /// </summary>
        Action OnCompleted { get; set; }

        /// <summary>
        /// Gets the result of the job.
        /// </summary>
        object Result { get; }

        /// <summary>
        /// Starts the job.
        /// </summary>
        void Start();
    }
}

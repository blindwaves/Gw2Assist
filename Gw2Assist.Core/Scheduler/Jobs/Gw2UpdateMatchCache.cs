using System;

namespace Gw2Assist.Core.Scheduler.Jobs
{
    public class Gw2UpdateMatchCache : Interfaces.IJob
    {
        /// <summary>
        /// Gets the interval to run the job.
        /// </summary>
        public TimeSpan Interval { get { return TimeSpan.FromMinutes(5); } }
        public DateTime? LastRanAt { get; set; }
        public string Name { get { return this.GetType().Name; } }
        public Action OnCompleted { get; set; }
        public object Result { get { return null; } }

        public void Start()
        {
            Cache.Repository.Instance.GetContainer<Cache.Containers.Gw2WvwMatch>().Refresh(Cache.Repository.Instance.StoragePath);
        }
    }
}

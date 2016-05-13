using System;

using Gw2Assist.Anet.GuildWars2.Api.MumbleLink;

namespace Gw2Assist.Core.Scheduler.Jobs
{
    public class Gw2CheckAvatarLocation : Interfaces.IJob
    {
        /// <summary>
        /// Gets the interval to run the job.
        /// </summary>
        public TimeSpan Interval { get { return TimeSpan.FromSeconds(5); } }
        public DateTime? LastRanAt { get; set; }
        public string Name { get { return this.GetType().Name; } }
        public Action OnCompleted { get; set; }

        private Context result;
        public object Result { get { return this.result; } }

        public void Start()
        {
            var mumbleContext = MemorySharedFile.Instance.Read();
            if (mumbleContext != null) this.result = mumbleContext;
        }
    }
}

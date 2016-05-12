using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Gw2Assist.Core.Scheduler.Jobs;

namespace Gw2Assist.Core.Scheduler
{
    public sealed class Dispatcher
    {
        private static volatile Dispatcher instance = null;
        private static object padLock = new object();

        private static readonly TimeSpan Interval = TimeSpan.FromSeconds(1);

        private static CancellationToken CancellationToken;

        private static List<IJob> QueuedJobs = new List<IJob>();

        public static Dispatcher Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padLock)
                    {
                        // https://msdn.microsoft.com/en-us/library/ff650316.aspx
                        // This double-check locking approach solves the thread concurrency problems while
                        // avoiding an exclusive lock in every call to the Instance property method. 
                        if (instance == null) instance = new Dispatcher();
                    }
                }

                return instance;
            }
        }

        private Dispatcher()
        {
            var type = typeof(IJob);
            var types = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => type.IsAssignableFrom(t) && !t.IsInterface);

            foreach (var item in types)
            {
                var job = (IJob)Activator.CreateInstance(item);
                QueuedJobs.Add(job);
            }
        }

        public List<IJob> Jobs { get { return QueuedJobs; } }

        public CancellationToken Start()
        {
            Dispatch().ConfigureAwait(false);

            CancellationToken = CancellationToken.None;
            return new CancellationTokenSource().Token;
        }

        public void Stop(CancellationToken token)
        {
            CancellationToken = token;
        }

        private static async Task Dispatch()
        {
            while (true)
            {
                foreach (var job in QueuedJobs)
                {
                    if (!job.LastRanAt.HasValue ||
                        DateTime.Now.Subtract(job.LastRanAt.Value) >= job.Interval)
                    {
                        job.Start();
                        job.LastRanAt = DateTime.Now;
                        job.OnCompleted?.Invoke();
                    }
                }

                var task = Task.Delay(Interval, CancellationToken);

                try
                {
                    await task;
                }
                catch (TaskCanceledException)
                {
                    return;
                }
            }
        }
    }
}

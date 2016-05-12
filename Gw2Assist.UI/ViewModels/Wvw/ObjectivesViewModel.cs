using System.Collections.Generic;

using Caliburn.Micro;

using Gw2Assist.Anet.GuildWars2.Api.V2.Core.Models.Wvw;
using Gw2Assist.Core.Scheduler.Jobs;

namespace Gw2Assist.UI.ViewModels.Wvw
{
    public class ObjectivesViewModel : BaseViewModel, IHandle<Gw2CheckAvatarLocation>
    {
        private List<Objective> objectives;
        public List<Objective> Objectives
        {
            get { return this.objectives; }
            set
            {
                this.objectives = value;
                NotifyOfPropertyChange(() => this.Objectives);
            }
        }

        private int mapId;
        private readonly IEventAggregator eventAggregator;

        public ObjectivesViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);
        }

        public void Handle(Gw2CheckAvatarLocation job)
        {
            var mapChanged = false;
            var mumbleContext = (Anet.GuildWars2.Api.MumbleLink.Context)job.Result;
            if (mumbleContext != null)
            {
                if (this.mapId != mumbleContext.MapId)
                {
                    this.mapId = mumbleContext.MapId;

                    mapChanged = true;
                }
            }

            if (mapChanged)
            {
                var cacheWvWObjectives = new Core.Cache.Containers.WvwObjectives();

                if (cacheWvWObjectives.Contents.ContainsKey(this.mapId))
                {
                    this.Objectives = cacheWvWObjectives.Contents[this.mapId];
                }
            }
        }
    }
}

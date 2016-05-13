using System.Collections.Generic;

using Caliburn.Micro;

using Gw2Assist.Anet.GuildWars2.Api.V2.Models;
using Gw2Assist.Anet.GuildWars2.Api.V2.Models.Wvw;
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

        private List<World> worlds;
        public List<World> Worlds
        {
            get { return this.worlds; }
            set
            {
                this.worlds = value;
                NotifyOfPropertyChange(() => this.Worlds);
            }
        }

        private int selectedWorld;
        public int SelectedWorld
        {
            get { return this.selectedWorld; }
            set
            {
                this.selectedWorld = value;
                NotifyOfPropertyChange(() => this.SelectedWorld);
            }
        }

        private int mapId;
        private readonly IEventAggregator eventAggregator;

        public ObjectivesViewModel(IEventAggregator eventAggregator)
        {
            // TODO:
            // 1. Grab the world the user is in.
            // 2. Grab the matches for the world.
            // 3. Then compare which map is the user in.
            // 4. Then grab the objectives in that map.
            // 5. last flipped, count down, yada yada.
            //
            // 3 - 5 should be done by scheduler.

            this.Worlds = new List<World>();
            var cacheWorlds = Core.Cache.Repository.Instance.GetContainer<Core.Cache.Containers.Gw2Worlds>();
            foreach (var world in cacheWorlds.Contents)
            {
                this.Worlds.Add(world.Value);
            }

            this.SelectedWorld = 1006;

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

            /** TODO: TEST **/
            var currentId = 94;
            if (this.mapId != currentId)
            {
                this.mapId = currentId;

                mapChanged = true;
            }
            /** **/

            if (mapChanged)
            {
                var cacheWvWObjectives = Core.Cache.Repository.Instance.GetContainer<Core.Cache.Containers.Gw2WvwObjectives>();

                if (cacheWvWObjectives.Contents.ContainsKey(this.mapId))
                {
                    this.Objectives = cacheWvWObjectives.Contents[this.mapId];
                }
                System.Diagnostics.Debug.WriteLine(this.Objectives.Count);
            }
        }
    }
}

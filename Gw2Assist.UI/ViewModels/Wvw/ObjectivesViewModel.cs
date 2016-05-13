using System.Linq;

using Caliburn.Micro;

using Gw2Models = Gw2Assist.Core.Models;
using Gw2Assist.Core.Scheduler.Jobs;

namespace Gw2Assist.UI.ViewModels.Wvw
{
    public class ObjectivesViewModel : BaseViewModel, IHandle<Gw2CheckAvatarLocation>
    {
        private BindableCollection<Gw2Models.GuildWars2.Wvw.Objective> objectives;
        public BindableCollection<Gw2Models.GuildWars2.Wvw.Objective> Objectives
        {
            get { return this.objectives; }
            set
            {
                this.objectives = value;
                NotifyOfPropertyChange(() => this.Objectives);
            }
        }

        private BindableCollection<Gw2Models.GuildWars2.World> worlds;
        public BindableCollection<Gw2Models.GuildWars2.World> Worlds
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
        private Anet.Drawing.Point3D mapPosition;

        private readonly IEventAggregator eventAggregator;
        private readonly Services.GuildWars2.Wvw.Objectives objectivesService;
        private readonly Services.GuildWars2.Worlds worldsService;

        public ObjectivesViewModel(IEventAggregator eventAggregator, Services.GuildWars2.Worlds worldsService, Services.GuildWars2.Wvw.Objectives objectivesService)
        {
            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);

            this.objectivesService = objectivesService;
            this.worldsService = worldsService;

            var tempWorlds = this.worldsService.GetAll().Values.ToList();
            tempWorlds.Sort();
            this.Worlds = new BindableCollection<Gw2Models.GuildWars2.World>(tempWorlds);
        }

        public void Handle(Gw2CheckAvatarLocation job)
        {
            var mapIdChanged = false;
            var mapPositionChanged = false;

            var mumbleContext = (Anet.GuildWars2.Api.MumbleLink.Context)job.Result;
            if (mumbleContext != null)
            {
                if (this.mapId != mumbleContext.MapId)
                {
                    this.mapId = mumbleContext.MapId;
                    mapIdChanged = true;
                }

                if (this.mapPosition != null && (
                    this.mapPosition.X != mumbleContext.Position.X ||
                    this.mapPosition.Y != mumbleContext.Position.Y ||
                    this.mapPosition.Z != mumbleContext.Position.Z
                    ))
                {
                    this.mapPosition = mumbleContext.Position;
                    mapPositionChanged = true;
                }
            }

            if (mapIdChanged || mapPositionChanged)
            {
                this.Objectives = new BindableCollection<Gw2Models.GuildWars2.Wvw.Objective>(this.objectivesService.GetAllByAvatarLocation(this.SelectedWorld, this.mapId, this.mapPosition));
            }
        }
    }
}

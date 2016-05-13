// <copyright company="GW2.NET Coding Team">
//   This product is licensed under the GNU General Public License version 2 (GPLv2). See the License in the project root folder or the following page: http://www.gnu.org/licenses/gpl-2.0.html
// </copyright>
// <summary>
//   Provides an implementation of the Mumble Link protocol.
//   https://github.com/Ruhrpottpatriot/GW2.NET/tree/1b1553ad07aab2ffe8ead285d7bc8ea64836a123/src/GW2NET.MumbleLink
// </summary>
using System.Runtime.InteropServices;

using Gw2Assist.Anet.Drawing;

namespace Gw2Assist.Anet.GuildWars2.Api.MumbleLink
{
    public class Context
    {
        public int MapId { get; set; }
        public int MapType { get; set; }

        /// <summary>
        /// Gets or sets the avatar position.
        /// Mumble : Uses left handed coordinate system, you have to use the x,z coordinates (not x,y). The x,y,z coords are in metres.
        /// For more details:
        /// https://forum-en.guildwars2.com/forum/community/api/Event-Details-API-location-coordinates/3546948
        /// </summary>
        public Point3D Position { get; set; }

        public Context(MumbleLink.Interop.LinkedMem value)
        {
            var contextLength = (int)value.context_len;
            var ptr = Marshal.AllocHGlobal(contextLength);
            Marshal.Copy(value.context, 0, ptr, contextLength);
            var mumbleContext = (MumbleLink.Interop.MumbleContext)Marshal.PtrToStructure(ptr, typeof(MumbleLink.Interop.MumbleContext));

            this.MapId = (int)mumbleContext.mapId;
            this.MapType = (int)mumbleContext.mapType;
            this.Position = new Point3D(value.fAvatarPosition[0], value.fAvatarPosition[1], value.fAvatarPosition[2]);
        }
    }
}

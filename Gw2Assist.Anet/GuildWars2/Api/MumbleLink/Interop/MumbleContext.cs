// <copyright company="GW2.NET Coding Team">
//   This product is licensed under the GNU General Public License version 2 (GPLv2). See the License in the project root folder or the following page: http://www.gnu.org/licenses/gpl-2.0.html
// </copyright>
// <summary>
//   Provides an implementation of the Mumble Link protocol.
//   https://github.com/Ruhrpottpatriot/GW2.NET/tree/1b1553ad07aab2ffe8ead285d7bc8ea64836a123/src/GW2NET.MumbleLink
// </summary>
using System.Runtime.InteropServices;

namespace Gw2Assist.Anet.GuildWars2.Api.MumbleLink.Interop
{
    [StructLayout(LayoutKind.Explicit, Size = 256)]
    public struct MumbleContext
    {
        [FieldOffset(0)]
        public byte serverAddress;

        [FieldOffset(28)]
        public uint mapId;

        [FieldOffset(32)]
        public uint mapType;

        [FieldOffset(36)]
        public uint shardId;

        [FieldOffset(40)]
        public uint instance;

        [FieldOffset(44)]
        public uint buildId;
    }
}

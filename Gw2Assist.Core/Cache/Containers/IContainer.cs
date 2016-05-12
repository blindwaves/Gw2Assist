using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gw2Assist.Core.Cache.Containers
{
    public interface IContainer
    {
        string FileFullPath { get; }
        string Name { get; }

        Task Create(string fullPath);
        void Refresh(string fullPath);
    }
}

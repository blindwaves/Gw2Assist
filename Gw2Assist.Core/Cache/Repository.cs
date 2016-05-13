using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using Gw2Assist.Core.Cache.Containers.Interfaces;

namespace Gw2Assist.Core.Cache
{
    public class Repository
    {
        private static volatile Repository instance = null;
        private static object padLock = new object();

        private static readonly List<string> StoragePossiblePaths = new List<string>();

        private static Dictionary<string, IContainer> Containers;

        public static Repository Instance
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
                        if (instance == null) instance = new Repository();
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Gets the directory path where the files are saved.
        /// </summary>
        public string StoragePath { get; private set; }

        private Repository()
        {
            // Possible places to store the cache files.
            StoragePossiblePaths.Add(Directory.GetCurrentDirectory() + "\\Cache");
            StoragePossiblePaths.Add(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Gw2Assist\\Cache");

            // Files to store the needed cache data.
            Containers = new Dictionary<string, IContainer>();
            var type = typeof(IContainer);
            var types = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => type.IsAssignableFrom(t) && !t.IsInterface);

            foreach (var item in types)
            {
                var container = (IContainer)Activator.CreateInstance(item);
                Containers.Add(container.Name, container);
            }
        }

        public async void Initialize()
        {
            // Check if any cache folder exists.
            var cacheFolderExists = false;
            foreach (var path in StoragePossiblePaths)
            {
                if (Directory.Exists(path))
                {
                    this.StoragePath = path;
                    cacheFolderExists = true;
                    break;
                }
            }

            if (!cacheFolderExists)
            {
                // Create the cache if it doesn't exists by trying to create the cache folder first.
                foreach (var path in StoragePossiblePaths)
                {
                    try
                    {
                        Directory.CreateDirectory(path);
                        this.StoragePath = path;
                        break;
                    }
                    catch (UnauthorizedAccessException) { /* Cannot create, aborb it, and move to next folder. */ }
                }

                if (!string.IsNullOrEmpty(this.StoragePath))
                {
                    // Assume directory can be created, there is write access.
                    foreach (var container in Containers)
                    {
                        await container.Value.Create(this.StoragePath);
                    }
                }
            }

            if (string.IsNullOrEmpty(this.StoragePath)) throw new Exception("Cache storage location cannot be created.");

            this.Refresh();
        }

        public T GetContainer<T>() where T : IContainer
        {
            var container = (T)Activator.CreateInstance(typeof(T));
            return (T)Containers[container.Name];
        }

        public void Refresh()
        {
            if (string.IsNullOrEmpty(this.StoragePath)) this.Initialize();

            foreach (var container in Containers)
            {
                container.Value.Refresh(this.StoragePath);
            }
        }
    }
}

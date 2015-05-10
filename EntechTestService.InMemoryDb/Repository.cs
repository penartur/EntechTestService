using System;
using System.Collections.Generic;
using EntechTestService.Contracts.Internal.Model;

namespace EntechTestService.InMemoryDb
{
    internal class Repository<T>
        where T : class
    {
        private readonly object syncRoot = new object();

        private readonly List<T> storage = new List<T> { null }; //so that indexing will start with 1

        public ICollection<IdentifiedDataEntity<T>> GetAll()
        {
            lock (syncRoot)
            {
                var result = new List<IdentifiedDataEntity<T>>();
                for (var i = 0; i < storage.Count; i++)
                {
                    if (storage[i] != null)
                    {
                        result.Add(new IdentifiedDataEntity<T>(i, storage[i]));
                    }
                }

                return result;
            }
        }

        public T Get(int id)
        {
            lock (syncRoot)
            {
                return storage[id];
            }
        }

        public int Insert(T data)
        {
            lock (syncRoot)
            {
                var id = storage.Count;
                storage.Add(data);
                return id;
            }
        }

        public void Update(int id, T data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            lock (syncRoot)
            {
                storage[id] = data;
            }
        }

        public void Delete(int id)
        {
            lock (syncRoot)
            {
                storage[id] = null;
            }
        }
    }
}

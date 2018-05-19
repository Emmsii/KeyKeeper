using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Helpers
{
    public class Pool<T>
    {
        private readonly List<PoolItem<T>> _items = new List<PoolItem<T>>();
        private readonly Random _random;
        private int _totalWeight;

        public Pool() : this(new Random()) { }

        public Pool(Random random)
        {
            _random = random;
        }

        public T Get()
        {
            int currentWeight = 0;
            int roll = _random.Next(1, _totalWeight);
            foreach(PoolItem<T> item in _items)
            {
                currentWeight += item.Weight;
                if(roll <= currentWeight)
                {
                    Remove(item);
                    return item.Item;
                }
            }

            throw new InvalidOperationException("Could not get an item from the pool.");
        }

        public void Add(T item, int weight)
        {
            _items.Add(new PoolItem<T>(item, weight));
            _totalWeight += weight;
        }

        private void Remove(PoolItem<T> item)
        {
            _items.Remove(item);
            _totalWeight -= item.Weight;
        }
    }

    internal class PoolItem<T>
    {
        public int Weight { get; private set; }
        public T Item { get; private set; }

        public PoolItem(T item, int weight)
        {
            Item = item;
            Weight = weight;
        }
    }
}

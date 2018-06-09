using KeyKeeper.Interfaces;
using KeyKeeper.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Generators.Strategies
{
    internal abstract class BaseGeneratorStrategy : IGeneratorStrategy
    {
        protected readonly Random _random;

        protected IGeneratorStrategy _requiresStrategy;
        private bool _hasCompleted;

        public BaseGeneratorStrategy(Random random)
        {
            _random = random;
        }

        public void RunStrategy(CellMap map)
        {
            Generate(map);
            _hasCompleted = true;
        }

        protected abstract void Generate(CellMap map);
        
        public virtual void Reset()
        {
            _hasCompleted = false;
        }

        public virtual Type RequiredStrategy()
        {
            return null;
        }

        public void SetRequiredStrategy(IGeneratorStrategy strategy)
        {
            _requiresStrategy = strategy;
        }

        public bool HasCompleted()
        {
            return _hasCompleted;
        }
    }
}

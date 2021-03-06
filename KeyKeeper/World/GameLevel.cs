﻿using KeyKeeper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.World
{
    public class GameLevel
    {
        private readonly IMap<Cell> _cellMap;
        private IMap<bool> _exploredMap;

        public int Width { get { return _cellMap.Width; } }
        public int Height { get { return _cellMap.Height; } }
        public int Depth { get; }

        public bool InBounds(int x, int y) => _cellMap.InBounds(x, y);
        public bool IsExplored(int x, int y) =>  _exploredMap.GetTile(x, y);
        public bool IsSolid(int x, int y) => GetCell(x, y).IsSolid;
        public bool IsTransparent(int x, int y) => GetCell(x, y).IsTransparent;

        public Cell GetCell(int x, int y) => _cellMap.GetTile(x, y);

        public GameLevel(IMap<Cell> cellMap, int depth)
        {
            _cellMap = cellMap;
            _exploredMap = new BaseMap<bool>(_cellMap.Width, _cellMap.Height, false);
            Depth = depth;
        }

        public void SetExplored(int x, int y, bool isExplored)
        {
            if (!_exploredMap.InBounds(x, y)) return;
            _exploredMap.SetTile(x, y, isExplored);
        }
    }
}

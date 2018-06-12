using KeyKeeper.Interfaces;
using KeyKeeper.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Generators.Strategies
{
    internal class StandardRoomStrategy : BaseGeneratorStrategy
    {
        private const int TIMEOUT_TRIES = 1000;

        private const int DEFAULT_MIN_ROOM_SIZE = 3;
        private const int DEFAULT_MAX_ROOM_SIZE = 9;
        private const int DEFAULT_MAX_ROOM_COUNT = 15;

        private int _minRoomSize = DEFAULT_MIN_ROOM_SIZE;
        private int _maxRoomSize = DEFAULT_MAX_ROOM_SIZE;
        private int _maxRoomCount = DEFAULT_MAX_ROOM_COUNT;

        private readonly TileType[] _floorTiles;

        public int MinRoomSize {
            get {
                return _minRoomSize;
            }

            set {
                if (value < 1) throw new InvalidOperationException("Min room size must be greater than 1.");
                if (value % 2 == 0 && OddSizesOnly) throw new InvalidOperationException("Min room size must be odd.");
                _minRoomSize = value; ;
            }
        }

        public int MaxRoomSize {
            get {
                return _maxRoomSize;
            }

            set {
                if (value < 1) throw new InvalidOperationException("Max room size must be greater than 1.");
                if (value % 2 == 0 && OddSizesOnly) throw new InvalidOperationException("Max room size must be odd.");
                _maxRoomSize = value;
            }
        }

        public int MaxRoomCount {
            get {
                return _maxRoomCount;
            }

            set{
                if (value < 0) throw new InvalidOperationException("Max room count must be greater than 0.");
                _maxRoomCount = value;
            }
        }

        private int GetRandomSize() => MinRoomSize == MaxRoomSize ? MinRoomSize : _random.Next(MinRoomSize, MaxRoomSize); // TODO: max size + 1?
        private int GetRandomSize(int bound) => _random.Next(Math.Max(MinRoomSize, bound - Tolerance), Math.Min(MaxRoomCount, bound + Tolerance));
        private int NormalizeSize(int size) => size % 2 != 1 ? _random.NextDouble() >= 0.5 ? --size : ++size : size;

        public bool IsPointInRoom(int x, int y) => _rooms.FirstOrDefault(r => r.Contains(x, y)) != null;
        
        public int Tolerance { get; set; }
        public bool OddSizesOnly { get; set; } = true;

        private List<Room> _rooms;
        private int _currentTry;

        public StandardRoomStrategy(Random random, TileType[] floorTiles) : base(random)
        {
            _floorTiles = floorTiles;
        }

        public override void Reset()
        {
            _rooms = new List<Room>(_maxRoomCount);
            _currentTry = 0;
            base.Reset();
        }

        protected override void Generate(CellMap map)
        {
            PlaceAndCarveRooms(map);
        }

        public List<Room> PlaceAndCarveRooms(CellMap map)
        {
            ValidateParametersWithMap(map);

            do
            {
                Room newRoom = CreateRandomRoom(map);
                if (!OverlapsAny(newRoom))
                {
                    _rooms.Add(newRoom);
                }
            } while (_rooms.Count < MaxRoomCount && _currentTry++ < TIMEOUT_TRIES);

            CarveRooms(map);

            return _rooms;
        }

        private void CarveRooms(CellMap map)
        {
            foreach(Room room in _rooms)
            {
                room.Fill(map, _random, _floorTiles);
            }
        }

        private bool OverlapsAny(Room newRoom)
        {
            if (newRoom == null) throw new ArgumentNullException(nameof(newRoom));

            foreach(Room room in _rooms)
            {
                if (room.Overlaps(newRoom)) return true;
            }
            return false;
        }

        private Room CreateRandomRoom(CellMap map)
        {
            int width = NormalizeSize(GetRandomSize());
            int height = NormalizeSize(GetRandomSize());

            Point position = GetRandomRoomPosition(map, width, height);

            return new Room(position.X, position.Y, width, height);
        }

        private Point GetRandomRoomPosition(CellMap map, int width, int height)
        {
            int x = _random.Next(map.Width - width);
            int y = _random.Next(map.Height- height);

            if (x == 0) x = 1;
            if (y == 0) y = 1;
            if (x % 2 == 0) x--;
            if (y % 2 == 0) y--;

            return new Point(x, y);
        }

        private void ValidateParametersWithMap(CellMap map)
        {
            if (MinRoomSize >= map.Width || MinRoomSize >= map.Height) throw new InvalidOperationException("Min room size is to big for the map.");
            if (MaxRoomSize >= map.Width || MaxRoomSize >= map.Height) throw new InvalidOperationException("Max room size is to big for the map.");
        }

        public List<Room> GetRooms()
        {
            if(_rooms == null)
            {
                throw new InvalidOperationException("Rooms have not been initialized.");
            }
            return _rooms;
        }

    }
}

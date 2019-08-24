using System;

namespace SpaceRL.Core.Util.Rng
{
    public class GameRandom : IRandom
    {
        private readonly Random _random;

        public GameRandom(int seed) : this(new Random(seed)) { }

        public GameRandom(Random random = null)
        {
            _random = random ?? new Random();
        }

        public int Next() => _random.Next();
        public int Next(int max) => _random.Next(max);
        public int Next(int min, int max) => _random.Next(min, max);
        public double NextDouble() => _random.NextDouble();
        public double NextDouble(double min, double max) => _random.NextDouble() * (max - min) + min;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace SpaceRL.Core.Util
{
    public class Array2D<TData> : IEnumerable<TData>, IEquatable<Array2D<TData>> where TData : IEquatable<TData>
    {
        private readonly TData[] _data;

        public int Width { get; }
        public int Height { get; }

        public Array2D(int width, int height)
        {
            if (width < 0) throw new ArgumentOutOfRangeException(nameof(width), "Width must be greater than zero.");
            if (height < 0) throw new ArgumentOutOfRangeException(nameof(height), "Height must be greater than zero.");

            _data = new TData[width * height];
            Width = width;
            Height = height;
        }

        public Array2D(Array2D<TData> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            _data = other._data;
            Width = other.Width;
            Height = other.Height;
        }

        public int Area => Width * Height;

        public bool InBounds(Point2D point) => InBounds(point.X, point.Y);
        public bool InBounds(int x, int y) => x >= 0 && y >= 0 && x < Width && y < Height;

        public bool IsEdge(Point2D point) => IsEdge(point.X, point.Y);
        public bool IsEdge(int x, int y) => x == 0 || y == 0 || x == Width - 1 || y == Height - 1;

        public TData this[int x, int y] {
            get => _data[GetIndex(x, y)];
            set => _data[GetIndex(x, y)] = value;
        }

        public TData this[Point2D point] {
            get => this[point.X, point.Y];
            set => this[point.X, point.Y] = value;
        }

        private int GetIndex(int x, int y)
        {
            return x + y * Width;
        }

        public IEnumerator<TData> GetEnumerator() => (IEnumerator<TData>)_data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool Equals(Array2D<TData> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(_data, other._data);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Array2D<TData>)obj);
        }

        public override int GetHashCode()
        {
            return _data != null ? _data.GetHashCode() : 0;
        }
    }
}

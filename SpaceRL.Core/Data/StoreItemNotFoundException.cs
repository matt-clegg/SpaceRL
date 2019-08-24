using System;

namespace SpaceRL.Core.Data
{
    public class StoreItemNotFoundException : Exception
    {
        public StoreItemNotFoundException(string message) : base(message) { }
    }
}

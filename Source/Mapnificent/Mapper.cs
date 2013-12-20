using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace KoC.Mapnificent
{
       public class MappingException : Exception
    {
        public MappingException(string message) : base(message)
        {
        }

        public MappingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class Map
    {
        private readonly Type sourceClassType;
        private readonly Type targetClassType;

        public Map(Type sourceClassType, Type targetClassType)
        {
            this.sourceClassType = sourceClassType;
            this.targetClassType = targetClassType;
        }

        public void Freeze()
        {
            
        }

        public IEnumerable<string> UnMappedTargetMembers()
        {
            throw new NotImplementedException();
        }

        internal class Key
        {
            public Key(Type fromType, Type toType)
            {
                Require.NotNull(fromType, "fromType");
                Require.NotNull(toType, "toType");

                FromType = fromType;
                ToType = toType;
            }

            public Type FromType { get; set; }
            public Type ToType { get; set; }

            protected bool Equals(Key other)
            {
                return FromType == other.FromType && ToType == other.ToType;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != GetType()) return false;
                return Equals((Key) obj);
            }

            public override int GetHashCode()
            {
                return FromType.GetHashCode() ^ ToType.GetHashCode();
            }

            public static bool operator ==(Key left, Key right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(Key left, Key right)
            {
                return !Equals(left, right);
            }
        }


    }

    public interface IMapDefinition
    {
    }

    public static class MapDefinition
    {
        public static MapDefinition<TFrom, TTo> Create<TFrom, TTo>(Mapper mapper)
            where TTo : class
        {
            return new MapDefinition<TFrom, TTo>(mapper);
        }
    }



    public class Mapper
    {
        private readonly Dictionary<Map.Key, IMapDefinition> definitions = new Dictionary<Map.Key, IMapDefinition>();

        public MapDefinition<TFrom, TTo> Define<TFrom, TTo>()
            where TFrom : class
            where TTo : class
        {
            var key = new Map.Key(typeof(TFrom), typeof(TTo));
            IMapDefinition definition;

            if (!definitions.TryGetValue(key, out definition))
            {
                definition = MapDefinition.Create<TFrom, TTo>(this);
                definitions.Add(key, definition);
            }

            return new MapDefinition<TFrom, TTo>(this);
        }

        public TTo Map<TTo>(object from)
            where TTo : class
        {
            return (TTo) Map(from, typeof(TTo));
        }

        public object Map(object from, Type toType)
        {
            return null;
        }

        public void AssertValid()
        {
        }
    }
}
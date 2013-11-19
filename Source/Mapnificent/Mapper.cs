using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

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

    public class MapBuilder<TFrom, TTo>
    {
        MapBuilder<TFrom, TTo> AutoMap()
        {
            return this;
        }

        MapBuilder<TFrom, TTo> Bind(Expression<Func<TFrom>> from, Expression<Func<TTo>> to)
        {
            return this;
        }

        MapBuilder<TFrom, TTo> UnBind(Expression<Func<TTo>>  to)
        {
            return this;
        }
    }

    public class Mapper
    {
        public Mapper Define<TFrom, TTo>(Action<MapBuilder<TFrom, TTo>> builder)
        {
            return this;
        }

        public TTo Map<TTo>(object from)
            where TTo : class
        {
            return (TTo) Map(from, typeof (TTo));
        }

        public object Map(object from, Type toType)
        {
            return null;
        }
    }
}

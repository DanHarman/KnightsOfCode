using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace KoC.Mapnificent
{
    public abstract class MemberDefinition
    {
        public string ToMemberName { get; private set; }

        protected MemberDefinition(string toMemberName)
        {
            ToMemberName = toMemberName;
        }
    }

    public class BindMemberDefinition : MemberDefinition
    {
        public BindMemberDefinition(string toMemberName, Func<object, object> fromMemberDelegate)
            : base(toMemberName)
        {
            
        }
    }

    public class IgnoreMemberDefinition : MemberDefinition
    {
        public IgnoreMemberDefinition(string toMemberName)
            : base(toMemberName)
        {            
        }        
    }

    public class MapDefinition<TFrom, TTo> : IMapDefinition
        where TTo : class
    {
        private Dictionary<string, MemberDefinition> memberDefinitions = new Dictionary<string, MemberDefinition>(); 

        public MapDefinition(Mapper mapper)
        {
            Mapper = mapper;
        }

        public Mapper Mapper { get; set; }

        public MapDefinition<TFrom, TTo> For<TToMember>(Expression<Func<TTo, TToMember>> toMember,
            Action<BindingDefinition<TToMember>> options)
        {
            Require.IsFalse(ExpressionHelpers.IsComplexChain(toMember), "Parameter 'toMember' must be a simple expression.");

            return this;
        }

        public MapDefinition<TFrom, TTo> ConstructUsing(Func<ConstructionContext, TTo> factory)
        {
            return this;
        }

        /// <summary>
        ///     Indicates that this type is a collection type and it's entities should be mapped.
        /// </summary>
        /// <returns></returns>
        public MapDefinition<TFrom, TTo> AsCollection()
        {
            return this;
        }

        public MapDefinition<TFrom, TTo> Inherits<TFromBase, TToBase>(MapDefinition<TFrom, TTo> definition)
        {
            Require.IsTrue(typeof(TFromBase).IsAssignableFrom(typeof(TFrom)));
            Require.IsTrue(typeof(TToBase).IsAssignableFrom(typeof(TTo)));

            return this;
        }

        protected void AddMemberDefinition(MemberDefinition memberDefinition)
        {
            
        }

        internal void Freeze()
        {
            // Get To class properties.

            // Check for corresponding on from class.

            // Match them.

            // Create exception list.
        }


        public class BindingDefinition<TToMember>
        {
            private readonly MapDefinition<TFrom, TTo> mapDefinition;
            private readonly string toMemberName;

            public BindingDefinition(MapDefinition<TFrom, TTo> mapDefinition, string toMemberName)
            {
                this.mapDefinition = mapDefinition;
                this.toMemberName = toMemberName;
            }

            public BindingDefinition<TToMember> From<TFromMember>(
                Expression<Func<TFrom, TFromMember>> fromMember)
            {
                mapDefinition.AddMemberDefinition(null);
                return this;
            }

            public BindingDefinition<TToMember> Ignore()
            {
                mapDefinition.AddMemberDefinition(new IgnoreMemberDefinition(toMemberName));
                return this;
            }
        }

    }

    public class ConstructionContext
    {
        public ConstructionContext(Mapper mapper, object parent)
        {
            Mapper = mapper;
            Parent = parent;
        }

        public Mapper Mapper { get; private set; }

        public object Parent { get; private set; }
    }
}
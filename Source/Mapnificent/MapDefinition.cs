using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace KoC.Mapnificent
{


    public class MapDefinition<TFrom, TTo> : IMapDefinition
        where TTo : class
    {
       // private Dictionary<string, MemberDefinition> memberDefinitions = new Dictionary<string, MemberDefinition>(); 

        public MapDefinition(Mapper mapper)
        {
            Mapper = mapper;
        }

        public Mapper Mapper { get; set; }

        public MapDefinition<TFrom, TTo> For<TToMember>(Expression<Func<TTo, TToMember>> toMember,
            Action<MemberDefinition<TToMember>> options)
        {


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

        internal void Freeze()
        {
            // Get To class properties.

            // Check for corresponding on from class.

            // Match them.

            // Create exception list.
        }


        public class MemberDefinition<TToMember>
        {
            private readonly string toMemberName;
            private readonly Action<object, object> toMemberSetter;
            private string fromMemberName;
            private Func<object, object> fromMemberGetter; 

            public MemberDefinition(Expression<Func<TTo, TToMember>> toMember)
            {
                Require.NotNull(toMember, "toMember");
                Require.IsFalse(ExpressionHelpers.IsComplexChain(toMember), "Parameter 'toMember' must be a simple expression.");

                var memberInfo = ExpressionHelpers.GetMemberInfo(toMember);
                toMemberName = memberInfo.Name;
                toMemberSetter = ReflectionHelpers.CreateWeakMemberSetter(memberInfo);
            }

            public MemberDefinition<TToMember> From<TFromMember>(
                Expression<Func<TFrom, TFromMember>> fromMember)
            {
                Require.NotNull(fromMember, "fromMember");

                var memberInfos = ExpressionHelpers.GetMemberInfos(fromMember);
                fromMemberName = String.Join(".", memberInfos.Select(x => x.Name));
                ReflectionHelpers.CreateSafeWeakMemberChainGetter(memberInfos);

                return this;
            }

            public MemberDefinition<TToMember> Ignore()
            {
                fromMemberName = string.Empty;
                fromMemberGetter = null;

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
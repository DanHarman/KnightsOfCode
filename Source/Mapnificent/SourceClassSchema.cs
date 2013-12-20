using System;
using System.Collections.Generic;
using System.Reflection;

namespace KoC.Mapnificent
{
    public class SourceClassSchema
    {
        private readonly Type classType;
        private readonly Dictionary<string, SourceItem> members = new Dictionary<string, SourceItem>();

        public SourceClassSchema(Type classType)
        {
            this.classType = classType;

            CreatePropertyItems();
            CreateFieldItems();
        }

        public Dictionary<string, SourceItem> Members { get { return members; } }

        private void CreatePropertyItems()
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public;

            var props = classType.GetProperties(flags);

            foreach (var prop in props)
            {
                if (!prop.CanRead)
                    continue;

                var member = new SourceItem(classType, prop.Name, ReflectionHelpers.CreateWeakPropertyGetter(prop));
                members.Add(member.Name, member);
            }
        }

        private void CreateFieldItems()
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public;

            var fields = classType.GetFields(flags);

            foreach (var field in fields)
            {
                var member = new SourceItem(classType, field.Name, ReflectionHelpers.CreateWeakFieldGetter(field));
                members.Add(member.Name, member);
            }
        }

        public class SourceItem
        {
            public SourceItem(Type classType, string name, Func<object, object> getDelegate)
            {
                ClassType = classType;
                Name = name;
                GetDelegate = getDelegate;
            }

            public Type ClassType { get; private set; }
            public string Name { get; private set; }
            public Func<object, object> GetDelegate { get; private set; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Reflection;

namespace KoC.Mapnificent
{
    public class TargetClassSchema
    {
        private readonly Type classType;
        private readonly Dictionary<string, TargetItem> members = new Dictionary<string, TargetItem>();

        public TargetClassSchema(Type classType)
        {
            this.classType = classType;

            CreatePropertyItems();
            CreateFieldItems();
        }

        public Dictionary<string, TargetItem> Members { get { return members; } }

        private void CreatePropertyItems()
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public;

            var props = classType.GetProperties(flags);

            foreach (var prop in props)
            {
                if (!prop.CanWrite)
                    continue;

                var member = new TargetItem(classType, prop.Name, ReflectionHelpers.CreateWeakPropertySetter(prop));
                members.Add(member.Name, member);
            }
        }

        private void CreateFieldItems()
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public;

            var fields = classType.GetFields(flags);

            foreach (var field in fields)
            {
                var member = new TargetItem(classType, field.Name, ReflectionHelpers.CreateWeakFieldSetter(field));
                members.Add(member.Name, member);
            }
        }

        public class TargetItem
        {
            public TargetItem(Type classType, string name, Action<object, object> setDelegate)
            {
                ClassType = classType;
                Name = name;
                SetDelegate = setDelegate;
            }

            public Type ClassType { get; private set; }
            public string Name { get; private set; }
            public Action<object, object> SetDelegate { get; private set; }
        }
    }
}
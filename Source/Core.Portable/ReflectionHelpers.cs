using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace KoC
{
    public static class ReflectionHelpers
    {
        public class SafeWeakMemberGetterResult
        {
            public bool HasResult { get; private set; }
            public object Result { get; private set; }

            public SafeWeakMemberGetterResult(bool hasResult, object result)
            {
                HasResult = hasResult;
                Result = result;
            }

            public static SafeWeakMemberGetterResult WithResult(object result)
            {
                return new SafeWeakMemberGetterResult(true, result);
            }

            public readonly static SafeWeakMemberGetterResult NoResult = new SafeWeakMemberGetterResult(false, null);
        }

        public static Func<object, SafeWeakMemberGetterResult> CreateSafeWeakMemberChainGetter(IEnumerable<MemberInfo> memberInfos)
        {
            Require.NotNull(memberInfos, "memberInfos");
            Require.IsTrue(memberInfos.Any());

            var getters = memberInfos.Select(CreateWeakMemberGetter).ToArray();

            // If there is no chain then avoid unnecessary looping etc.
            if (getters.Length == 1)
                return inst => SafeWeakMemberGetterResult.WithResult(getters[0](inst));

            // With a chain we have to loop.
            return inst =>
            {
                // All but the terminal need to be treated as dereferences.
                for (var i = 0; i < getters.Length - 1; ++ i)
                {
                    var derefGetter = getters[i];

                    // Follow the chain
                    inst = derefGetter(inst);

                    if (inst == null)
                        return SafeWeakMemberGetterResult.NoResult;
                }

                var res = getters[getters.Length - 1](inst);

                return SafeWeakMemberGetterResult.WithResult(res);
            };
        }

        /// <summary>
        ///     Create a weakly typed member getter delegate.
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public static Func<object, object> CreateWeakMemberGetter(MemberInfo memberInfo)
        {
            Require.NotNull(memberInfo, "memberInfo");

            if (memberInfo is PropertyInfo)
                return CreateWeakPropertyGetter((PropertyInfo) memberInfo);

            return CreateWeakFieldGetter((FieldInfo) memberInfo);
        }

        /// <summary>
        ///     Create a weakly typed member setter delegate.
        /// </summary>
        public static Action<object, object> CreateWeakMemberSetter(MemberInfo memberInfo)
        {
            Require.NotNull(memberInfo, "memberInfo");

            if (memberInfo is PropertyInfo)
                return CreateWeakPropertySetter((PropertyInfo) memberInfo);

            return CreateWeakFieldSetter((FieldInfo) memberInfo);
        }

        /// <summary>
        ///     Create a weakly typed property getter delegate.
        /// </summary>
        public static Func<object, object> CreateWeakPropertyGetter(PropertyInfo propertyInfo)
        {
            Require.NotNull(propertyInfo, "propertyInfo");

            var getterMethodInfo = propertyInfo.GetGetMethod(true);

            if (getterMethodInfo == null)
                throw new Exception(string.Format("No get method defined for property '{0}' on class '{1}", propertyInfo.Name, propertyInfo.DeclaringType));

            // Parameters
            var weakInstanceParam = Expression.Parameter(typeof(object));

            // Convert Params
            var typedInstanceParam = Expression.Convert(weakInstanceParam, propertyInfo.DeclaringType);

            var callExpression = Expression.Call(typedInstanceParam, getterMethodInfo);
            var boxedCallExpression = Expression.Convert(callExpression, typeof(object));

            // Build the delegate
            var weakGetter = Expression.Lambda<Func<object, object>>(boxedCallExpression, weakInstanceParam).Compile();

            return weakGetter;
        }

        /// <summary>
        ///     Create a weakly typed property setter delegate.
        /// </summary>
        public static Action<object, object> CreateWeakPropertySetter(PropertyInfo propertyInfo)
        {
            Require.NotNull(propertyInfo, "propertyInfo");

            var setterMethodInfo = propertyInfo.GetSetMethod(true);

            if (setterMethodInfo == null)
                throw new Exception(string.Format("No set method defined for property '{0}' on class '{1}", propertyInfo.Name, propertyInfo.DeclaringType));

            // Parameters
            var weakInstanceParam = Expression.Parameter(typeof(object));
            var weakValueParam = Expression.Parameter(typeof(object));

            // Convert Params
            var typedInstanceParam = Expression.Convert(weakInstanceParam, propertyInfo.DeclaringType);
            var typedValueParam = Expression.Convert(weakValueParam, propertyInfo.PropertyType);

            // Build the delegate
            var callExpression = Expression.Call(typedInstanceParam, setterMethodInfo, typedValueParam);
            var weakSetter = Expression.Lambda<Action<object, object>>(callExpression, weakInstanceParam, weakValueParam).Compile();

            return weakSetter;
        }

        /// <summary>
        ///     Create a weakly typed field getter delegate.
        /// </summary>
        public static Func<object, object> CreateWeakFieldGetter(FieldInfo fieldInfo)
        {
            Require.NotNull(fieldInfo, "fieldInfo");

            // Parameters
            var weakInstanceParam = Expression.Parameter(typeof(object));
            var typedInstanceParam = Expression.Convert(weakInstanceParam, fieldInfo.DeclaringType);

            // Build the weak getter expression
            var fieldExpression = Expression.Field(typedInstanceParam, fieldInfo);
            var weakFieldGetterExpression = Expression.Convert(fieldExpression, typeof(object));

            // Build the delgate
            var weakGetter = Expression.Lambda<Func<object, object>>(weakFieldGetterExpression, weakInstanceParam).Compile();

            return weakGetter;
        }

        /// <summary>
        ///     Create a weakly typed field setter delegate.
        /// </summary>
        public static Action<object, object> CreateWeakFieldSetter(FieldInfo fieldInfo)
        {
            Require.NotNull(fieldInfo, "fieldInfo");

            // Parameters
            var weakInstanceParam = Expression.Parameter(typeof(object));
            var weakValueParam = Expression.Parameter(typeof(object));

            // Convert Params
            var typedInstanceParam = Expression.Convert(weakInstanceParam, fieldInfo.DeclaringType);
            var typedValueParam = Expression.Convert(weakValueParam, fieldInfo.FieldType);

            // Build the weak getter expression
            var fieldExpression = Expression.Field(typedInstanceParam, fieldInfo);
            var fieldSetterExpression = Expression.Assign(fieldExpression, typedValueParam);

            // Build the delegate
            var weakSetter = Expression.Lambda<Action<object, object>>(fieldSetterExpression, weakInstanceParam, weakValueParam).Compile();

            return weakSetter;
        }
    }
}
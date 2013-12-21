using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace KoC
{
    public static class ExpressionHelpers
    {
        /// <summary>
        ///     Get a member name from a simple member expression.
        /// </summary>
        /// <param name="memberExpression">A simple member access expression.</param>
        /// <returns></returns>
        public static string GetMemberName<TClass, TMember>(Expression<Func<TClass, TMember>> memberExpression)
        {
            Require.NotNull(memberExpression, "memberExpression");

            var body = memberExpression.Body as MemberExpression;

            if (body == null)
                throw new ArgumentException("The specified expression is not simple.", "memberExpression");

            return body.Member.Name;
        }

        /// <summary>
        ///     Get a MemberInfo from a simple member access expression. The MemberInfo will be either a FieldInfo or PropertyInfo.
        /// </summary>
        /// <returns>Either a PropertyInfo or FieldInfo</returns>
        public static MemberInfo GetMemberInfo<TClass, TMember>(Expression<Func<TClass, TMember>> memberExpression)
        {
            Require.NotNull(memberExpression, "memberExpression");

            var body = memberExpression.Body as MemberExpression;

            if (body == null)
                throw new ArgumentException("The specified expression is not a simple member expression.", "memberExpression");

            return body.Member;
        }

        public static string[] GetMemberNames<TClass, TValue>(Expression<Func<TClass, TValue>> memberChain)
        {
            Require.NotNull(memberChain, "memberChain");

            var nodes = new List<string>();

            var currentNode = memberChain.Body;

            while (currentNode.NodeType != ExpressionType.Parameter)
            {
                // Ignore boxing operations.
                if (currentNode.NodeType == ExpressionType.Convert || currentNode.NodeType == ExpressionType.ConvertChecked)
                {
                    currentNode = ((UnaryExpression) currentNode).Operand;
                    continue;
                }

                if (currentNode.NodeType == ExpressionType.Call)
                {
                    currentNode = ((MethodCallExpression) currentNode).Object;
                    continue;
                }

                if (currentNode.NodeType != ExpressionType.MemberAccess)
                    throw new ArgumentException(string.Format("memberChain '{0}' has a node that is not a Member '{1}'", memberChain, currentNode));

                var memberExpression = (MemberExpression) currentNode;

                nodes.Insert(0, memberExpression.Member.Name);
                currentNode = memberExpression.Expression;
            }

            return nodes.ToArray();
        }

        public static MemberInfo[] GetMemberInfos<TClass, TValue>(Expression<Func<TClass, TValue>> memberChain)
        {
            Require.NotNull(memberChain, "memberChain");

            var nodes = new List<MemberInfo>();

            var currentNode = memberChain.Body;

            while (currentNode.NodeType != ExpressionType.Parameter)
            {
//                // Ignore boxing operations.
//                if (currentNode.NodeType == ExpressionType.Convert || currentNode.NodeType == ExpressionType.ConvertChecked)
//                {
//                    currentNode = ((UnaryExpression)currentNode).Operand;
//                    continue;
//                }
//
//                if (currentNode.NodeType == ExpressionType.Call)
//                {
//                    currentNode = ((MethodCallExpression)currentNode).Object;
//                    continue;
//                }

                if (currentNode.NodeType != ExpressionType.MemberAccess)
                    throw new ArgumentException(string.Format("memberChain '{0}' has a node that is not a Member '{1}'", memberChain, currentNode));

                var memberExpression = (MemberExpression)currentNode;

                nodes.Insert(0, memberExpression.Member);
                currentNode = memberExpression.Expression;
            }

            return nodes.ToArray();
        }

        /// <summary>
        /// Detects if an expression is a member accessor, with no chaining, convertion etc.
        /// </summary>
        /// <returns>Returns true if it is a memberExpression, otherwise false.</returns>
        public static bool IsMemberExpression<TClass, TMember>(Expression<Func<TClass, TMember>> memberExpression)
        {
            Require.NotNull(memberExpression, "memberExpression");

            return memberExpression.Body is MemberExpression;
        }

        public static bool IsComplexChain<TClass, TValue>(Expression<Func<TClass, TValue>> memberChain)
        {
            Require.NotNull(memberChain, "memberChain");

            var memberNodeFound = false;
            var currentNode = memberChain.Body;

            while (currentNode.NodeType != ExpressionType.Parameter)
            {
                // Functions are always complex.
                if (currentNode.NodeType == ExpressionType.Call)
                {
                    return true;
                }

                if (currentNode.NodeType == ExpressionType.MemberAccess)
                {
                    if (memberNodeFound)
                        return true;

                    memberNodeFound = true;
                    currentNode = ((MemberExpression) currentNode).Expression;
                    continue;
                }

                // Ignore boxing operations.
                if (currentNode.NodeType == ExpressionType.Convert || currentNode.NodeType == ExpressionType.ConvertChecked)
                {
                    currentNode = ((UnaryExpression) currentNode).Operand;
                    continue;
                }
            }

            return false;
        }
    }
}
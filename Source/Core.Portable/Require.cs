using System;

namespace KoC
{
    public static class Require
    {
        /// <summary>
        ///     Ensures that a value is not null.
        /// </summary>
        public static void NotNull<T>(T parameter, string paramaterName = null, string message = null)
            where T : class
        {
            if (parameter != null)
                return;

            message = message ?? String.Format("Argument of type {0} was null", typeof (T));

            throw new ArgumentNullException(paramaterName, message);
        }

        public static void IsTrue(bool predicateResult, string message = null, string parameterName = null)
        {
            if (predicateResult)
                return;

            throw new ArgumentException(message ?? "Require.IsTrue predicate result was false", parameterName);
        }

        public static void IsFalse(bool predicateResult, string message = null, string parameterName = null)
        {
            if (!predicateResult)
                return;

            throw new ArgumentException(message ?? "Require.IsFalse predicate result was true", parameterName);
        }
    }
}
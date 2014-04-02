using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.Linq
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T1, bool>> AndEquals<T1, T2>(this Expression<Func<T1, bool>> expr, Expression<Func<T1, T2>> prop, T2 value)
            where T2 : class
        {
            if (value == null)
            {
                return expr;
            }

            return expr.AndPropertyEquals(prop, value);
        }

        public static Expression<Func<T1, bool>> AndEquals<T1, T2>(this Expression<Func<T1, bool>> expr, Expression<Func<T1, T2>> prop, Nullable<T2> value)
            where T2 : struct
        {
            if (!value.HasValue)
            {
                return expr;
            }

            return expr.AndPropertyEquals(prop, value.Value);
        }

        public static Expression<Func<T, bool>> AndStringMatches<T>(this Expression<Func<T, bool>> expr, Expression<Func<T, string>> prop, string value, bool exactMatch)
        {
            if (string.IsNullOrEmpty(value))
            {
                return expr;
            }

            return exactMatch ?
                expr.AndPropertyEquals(prop, value) :
                expr.AndStringContainsInternal(prop, value);
        }

        public static Expression<Func<T, bool>> AndStringContains<T>(this Expression<Func<T, bool>> expr, Expression<Func<T, string>> prop, string value)
        {
            if (value == null)
            {
                return expr;
            }

            return expr.AndStringContainsInternal(prop, value);
        }

        public static Expression<Func<T, bool>> AndCollectionContains<T>(this Expression<Func<T, bool>> expr, Expression<Func<T, IEnumerable<string>>> prop, string value)
        {
            if (value == null)
            {
                return expr;
            }

            var valueArr = value.Split(',').Select(r => r.Trim().ToLower());

            foreach (var val in valueArr)
            {
                expr = expr.And(
                    Expression.Lambda<Func<T, bool>>(
                        Expression.Call(
                        typeof(Enumerable),
                        "Contains",
                        new[] { typeof(string) },
                        prop.Body,
                        Expression.Constant(val)),
                    prop.Parameters));

            }

            return expr;
        }

        public static Expression<Func<T, bool>> AndDateTimeGreaterThanOrEqual<T>(this Expression<Func<T, bool>> expr, Expression<Func<T, DateTime?>> prop, DateTime? value)
        {
            if (!value.HasValue)
            {
                return expr;
            }

            return expr.AndPropertyGreaterThanOrEqual(prop, value);
        }

        public static Expression<Func<T, bool>> AndDateTimeLessThanOrEqual<T>(this Expression<Func<T, bool>> expr, Expression<Func<T, DateTime?>> prop, DateTime? value)
        {
            if (!value.HasValue)
            {
                return expr;
            }

            return expr.AndPropertyLessThanOrEqual(prop, value);
        }

        private static Expression<Func<T, bool>> AndStringContainsInternal<T>(this Expression<Func<T, bool>> expr, Expression<Func<T, string>> prop, string value)
        {
            return expr.And(
                Expression.Lambda<Func<T, bool>>(
                    Expression.Call(
                        prop.Body,
                        "Contains",
                        null,
                        Expression.Constant(value)),
                    prop.Parameters));
        }

        private static Expression<Func<T1, bool>> AndPropertyEquals<T1, T2>(this Expression<Func<T1, bool>> expr, Expression<Func<T1, T2>> prop, T2 value)
        {
            return expr.And(
                Expression.Lambda<Func<T1, bool>>(
                    Expression.Equal(
                        prop.Body,
                        Expression.Constant(value, typeof(T2))),
                    prop.Parameters));
        }

        private static Expression<Func<T1, bool>> AndPropertyGreaterThanOrEqual<T1, T2>(this Expression<Func<T1, bool>> expr, Expression<Func<T1, T2>> prop, T2 value)
        {
            return expr.And(
                Expression.Lambda<Func<T1, bool>>(
                    Expression.GreaterThanOrEqual(
                        prop.Body,
                        Expression.Constant(value, typeof(T2))),
                    prop.Parameters));
        }

        private static Expression<Func<T1, bool>> AndPropertyLessThanOrEqual<T1, T2>(this Expression<Func<T1, bool>> expr, Expression<Func<T1, T2>> prop, T2 value)
        {
            return expr.And(
                Expression.Lambda<Func<T1, bool>>(
                    Expression.LessThanOrEqual(
                        prop.Body,
                        Expression.Constant(value, typeof(T2))),
                    prop.Parameters));
        }
    }
}

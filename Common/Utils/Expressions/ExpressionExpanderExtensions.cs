using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils.Expressions
{
    public static class ExpressionExpanderExtensions
    {
        public static Expression<F> Expand<F>(this Expression<F> expr)
        {
            return (Expression<F>)new ExpressionExpander().Visit(expr);
        }

        public static Expression Expand(this Expression expr)
        {
            return new ExpressionExpander().Visit(expr);
        }
    }

    internal class ExpressionExpander : ExpressionVisitor
    {
        internal ExpressionExpander()
        {
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            // Expand calls to an expression's Compile() method:
            if (m.Method.Name == "Compile" && m.Object is MemberExpression)
            {
                var me = (MemberExpression)m.Object;
                Expression newExpr = TransformExpr(me);
                if (newExpr != me) return newExpr;
            }

            return base.VisitMethodCall(m);
        }

        Expression TransformExpr(MemberExpression input)
        {
            // Collapse captured outer variables
            if (input == null
                || !(input.Member is FieldInfo)
                || !input.Member.ReflectedType.IsNestedPrivate
                || !input.Member.ReflectedType.Name.StartsWith("<>"))	// captured outer variable
                return input;

            if (input.Expression is ConstantExpression)
            {
                object obj = ((ConstantExpression)input.Expression).Value;
                if (obj == null) return input;
                Type t = obj.GetType();
                if (!t.IsNestedPrivate || !t.Name.StartsWith("<>")) return input;
                FieldInfo fi = (FieldInfo)input.Member;
                object result = fi.GetValue(obj);
                if (result is Expression) return Visit((Expression)result);
            }
            return input;
        }
    }
}

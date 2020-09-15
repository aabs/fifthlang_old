using System.Collections.Generic;

namespace fifth.Parser.AST.Builders
{
    /// <summary>
    /// A fluent API for building the AST node definitions of Functions
    /// </summary>
    public class ExpressionListBuilder
    {
        public ExpressionListBuilder()
        {
            Expressions = new List<Expression>();
        }

        public List<Expression> Expressions { get; private set; }

        public static ExpressionListBuilder Start()
        {
            return new ExpressionListBuilder();
        }

        public List<Expression> Build()
        {
            return Expressions;
        }

        public ExpressionListBuilder WithExpression(Expression expression)
        {
            Expressions.Add(expression);
            return this;
        }
    }
}
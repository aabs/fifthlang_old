using fifth.VirtualMachine;
using System.Collections.Generic;

namespace fifth.Parser.AST
{
    public class BinaryExpression<TExpression, TOperator> : AstNode
    {
        public TExpression Left { get; set; }
        public TOperator Operator { get; set; }
        public TExpression Right { get; set; }
    }

    public class Expr : BinaryExpression<SimExpr, RelationalOperator> { }

    public abstract class Factor : AstNode { }

    public class IntValueExpression : LiteralExpression<int>
    {
        public IntValueExpression(int value) : base(value)
        {
        }
    }

    public class LiteralExpression<T> : Factor
    {
        public LiteralExpression(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
    }

    public class SimExpr : BinaryExpression<Term, AddOperator> { }

    public class StringValueExpression : LiteralExpression<string>
    {
        public StringValueExpression(string value) : base(value)
        {
        }
    }

    public class Term : BinaryExpression<Factor, MulOperator> { }
}
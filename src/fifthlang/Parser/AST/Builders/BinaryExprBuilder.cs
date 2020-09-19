namespace fifth.Parser.AST.Builders
{
    public class BinaryExprBuilder<TExpression, TOperator>
    {
        protected BinaryExprBuilder()
        {
        }

        public TExpression Left { get; private set; }
        public TOperator Operator { get; set; }
        public TExpression Right { get; private set; }

        public static BinaryExprBuilder<TExpression, TOperator> Start()
        {
            return new BinaryExprBuilder<TExpression, TOperator>();
        }

        public BinaryExpression<TExpression, TOperator> Build()
        {
            return new BinaryExpression<TExpression, TOperator> { Left = Left, Operator = Operator, Right = Right };
        }

        public BinaryExprBuilder<TExpression, TOperator> WithLeftExpression(TExpression value)
        {
            this.Left = value;
            return this;
        }

        public BinaryExprBuilder<TExpression, TOperator> WithOperator(TOperator op)
        {
            this.Operator = op;
            return this;
        }

        public BinaryExprBuilder<TExpression, TOperator> WithRightExpression(TExpression value)
        {
            this.Right = value;
            return this;
        }
    }

    public class ExprBuilder : BinaryExprBuilder<SimExpr, RelationalOperator> { }

    public class SimExprBuilder : BinaryExprBuilder<Term, AddOperator> { }

    public class TermBuilder : BinaryExprBuilder<Factor, MulOperator> { }
}
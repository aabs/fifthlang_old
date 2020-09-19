namespace fifth.Parser.AST
{
    public class Argument : AstNode
    {
        public Argument(string name, Expr value)
        {
            ArgumentName = name;
            Value = value;
        }

        public string ArgumentName { get; set; }
        public Expr Value { get; set; }
    }
}
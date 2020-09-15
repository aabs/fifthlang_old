using System.Collections.Generic;

namespace fifth.Parser.AST.Builders
{
    public class Argument : AstNode
    {
        public Argument(string name, Expression value)
        {
            ArgumentName = name;
            Value = value;
        }

        public string ArgumentName { get; set; }
        public Expression Value { get; set; }
    }

    public class ArgumentListBuilder
    {
        public ArgumentListBuilder()
        {
            this.Arguments = new List<Argument>();
        }

        public List<Argument> Arguments { get; private set; }

        public static ArgumentListBuilder Start()
        {
            return new ArgumentListBuilder();
        }

        public List<Argument> Build()
        {
            return this.Arguments;
        }

        public ArgumentListBuilder WithArgument(string name, Expression value)
        {
            this.Arguments.Add(new Argument(name, value));
            return this;
        }
    }
}
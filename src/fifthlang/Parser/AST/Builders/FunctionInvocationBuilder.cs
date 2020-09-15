using fifth.VirtualMachine;
using System.Collections.Generic;

namespace fifth.Parser.AST.Builders
{
    public class FunctionInvocationBuilder
    {
        public FunctionInvocationBuilder()
        {
            Arguments = new List<Argument>();
        }

        public List<Argument> Arguments { get; private set; }
        public string FunctionName { get; private set; }

        public static FunctionInvocationBuilder Start()
        {
            return new FunctionInvocationBuilder();
        }

        public FunctionInvocationExpression Build()
        {
            return new FunctionInvocationExpression
            {
                FunctionName = this.FunctionName,
                Arguments = this.Arguments
            };
        }

        public FunctionInvocationBuilder WithArgument(Argument arg)
        {
            this.Arguments.Add(arg);
            return this;
        }

        public FunctionInvocationBuilder WithName(string functionName)
        {
            FunctionName = functionName;
            return this;
        }
    }
}
using fifth.VirtualMachine;
using System.Collections.Generic;

namespace fifth.Parser.AST.Builders
{
    public class FunctionBuilder
    {
        public FunctionBuilder()
        {
        }

        public List<Expr> Body { get; private set; }
        public string FunctionName { get; private set; }
        public IFifthType ReturnType { get; private set; }
        internal List<ParameterDeclaration> ParameterDeclarations { get; private set; }

        public static FunctionBuilder Start()
        {
            return new FunctionBuilder();
        }

        public FunctionDefinition Build()
        {
            return new FunctionDefinition
            {
                Name = this.FunctionName,
                ReturnType = this.ReturnType,
                Body = this.Body
            };
        }

        public FunctionBuilder WithBody(List<Expr> expressions)
        {
            this.Body = expressions;
            return this;
        }

        public FunctionBuilder WithName(string functionName)
        {
            FunctionName = functionName;
            return this;
        }

        public FunctionBuilder WithParameters(List<ParameterDeclaration> parameterDeclarations)
        {
            this.ParameterDeclarations = parameterDeclarations;
            return this;
        }

        public FunctionBuilder WithReturnType(IFifthType returnType)
        {
            this.ReturnType = returnType;
            return this;
        }
    }
}
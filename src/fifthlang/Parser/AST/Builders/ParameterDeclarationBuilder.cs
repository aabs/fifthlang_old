namespace fifth.Parser.AST.Builders
{
    public class ParameterDeclarationBuilder
    {
        public ParameterDeclarationBuilder()
        {
        }

        public string ParameterName { get; private set; }
        public string TypeName { get; private set; }

        public static ParameterDeclarationBuilder Start()
        {
            return new ParameterDeclarationBuilder();
        }

        public ParameterDeclaration Build()
        {
            return new ParameterDeclaration
            {
                ParameterName = ParameterName,
                TypeName = TypeName
            };
        }

        public ParameterDeclarationBuilder WithName(string parameterName)
        {
            ParameterName = parameterName;
            return this;
        }

        public ParameterDeclarationBuilder WithTypeName(string typeName)
        {
            TypeName = typeName;
            return this;
        }
    }
}
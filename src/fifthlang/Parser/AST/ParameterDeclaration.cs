using fifth.VirtualMachine;

namespace fifth.Parser.AST
{
    public class ParameterDeclaration
    {
        public IFifthType FifthType { get; set; }
        public string Name { get; set; }
    }
}
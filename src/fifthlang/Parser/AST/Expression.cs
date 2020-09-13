using fifth.VirtualMachine;

namespace fifth.Parser.AST
{
    public abstract class Expression : AstNode
    {
        public IFifthType FifthType { get; set; }
    }
}
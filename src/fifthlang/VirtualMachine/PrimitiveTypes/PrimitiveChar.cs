namespace fifth.VirtualMachine.PrimitiveTypes
{
    [TypeTraits(IsPrimitive = true, IsNumeric = true, Keyword = "char")]
    public static class PrimitiveChar
    {
        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "+")]
        public static string Add(char left, char right) => $"{left}{right}";
    }
}
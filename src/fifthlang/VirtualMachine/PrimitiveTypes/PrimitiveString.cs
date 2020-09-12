namespace fifth.VirtualMachine.PrimitiveTypes
{
    [TypeTraits(IsPrimitive = true, IsNumeric = true, Keyword = "string")]
    public static class PrimitiveString
    {
        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "+")]
        public static string Add(string left, string right) => left + right;
    }
}
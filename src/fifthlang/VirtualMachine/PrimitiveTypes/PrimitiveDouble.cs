﻿namespace fifth.VirtualMachine.PrimitiveTypes
{
    [TypeTraits(IsPrimitive = true, IsNumeric = true, Keyword = "double")]
    public static class PrimitiveDouble
    {
        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "+")]
        public static double Add(double left, double right) => left + right;

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "/")]
        public static double Divide(double left, double right) => left / right;

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "*")]
        public static double Multiply(double left, double right) => left * right;

        [OperatorTraits(Position = OperatorPosition.Infix, OperatorRepresentation = "-")]
        public static double Subtract(double left, double right) => left - right;
    }
}
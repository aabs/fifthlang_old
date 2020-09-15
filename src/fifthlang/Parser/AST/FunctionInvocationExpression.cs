﻿using fifth.Parser.AST.Builders;
using System.Collections.Generic;

namespace fifth.Parser.AST
{
    public class FunctionInvocationExpression : Expression
    {
        public List<Argument> Arguments { get; set; }
        public string FunctionName { get; set; }
    }
}
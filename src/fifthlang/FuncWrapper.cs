using System;
using System.Collections.Generic;
using System.Text;

namespace fifth
{
    /// <summary>
    /// A wrapper around any sort of function, to make it easier to extract and perform type
    /// checking on its type parameters and result type.
    /// </summary>
    /// <remarks>
    /// This becomes important when type checking is performed on functions that have been pushed
    /// onto the stack. The interpreter needs to know whether the types of the items below the
    /// function on the stack are compatible with the function. It could make use of reflection, but
    /// this is much simpler, and probably faster.
    /// </remarks>
    public class FuncWrapper
    {
        public List<Type> ArgTypes { get; }
        public Type ResultType { get; }
        public Delegate Function { get; }

        public FuncWrapper(List<Type> argTypes, Type resultType, Delegate f)
        {
            this.ArgTypes = argTypes;
            this.ResultType = resultType;
            Function = f;
        }

    }
}

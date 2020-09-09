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


        public static FuncWrapper Wrap<R>(Func<R> fn) => 
            new FuncWrapper(new List<Type> { }, typeof(R), f: fn);
        public static FuncWrapper Wrap<T1, R>(Func<T1,R> fn) => 
            new FuncWrapper(new List<Type> { typeof(T1) }, typeof(R), f: fn);
        public static FuncWrapper Wrap<T1, T2, R>(Func<T1, T2, R> fn) =>
            new FuncWrapper(new List<Type> { typeof(T1), typeof(T2) }, typeof(R), f: fn);
        public static FuncWrapper Wrap<T1, T2, T3, R>(Func<T1, T2, T3, R> fn) =>
            new FuncWrapper(new List<Type> { typeof(T1), typeof(T2), typeof(T3) }, typeof(R), f: fn);
        public static FuncWrapper Wrap<T1, T2, T3, T4, R>(Func<T1, T2, T3, T4, R> fn) =>
            new FuncWrapper(new List<Type> { typeof(T1), typeof(T2), typeof(T3), typeof(T4) }, typeof(R), f: fn);
        public static FuncWrapper Wrap<T1, T2, T3, T4, T5, R>(Func<T1, T2, T3, T4, T5, R> fn) =>
            new FuncWrapper(new List<Type> { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5) }, typeof(R), f: fn);

    }
}

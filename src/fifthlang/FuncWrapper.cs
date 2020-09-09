using System;
using System.Collections.Generic;
using System.Text;

namespace fifth
{
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

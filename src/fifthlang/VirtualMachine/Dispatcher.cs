using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace fifth.VirtualMachine
{
    public class Dispatcher
    {
        public FuncStack Stack { get; set; }
        public Dispatcher(FuncStack stack)
        {
            Stack = stack;
        }
        object Resolve()
        {
            if (Stack.IsEmpty())
            {
                return null;
            }

            var x = Stack.Pop();
            if (x.IsValue)
            {
                return x.Invoke();
            }
            else
            {
                // we can't resolve this value directly, we need to recurse via dispatch
                Stack.Push(x);
                Dispatch();
                x = Stack.Pop();
                return x.Invoke();
            }

        }

        public void Dispatch()
        {
            // if stack empty do nothing
            if (Stack.IsEmpty())
            {
                return;
            }

            // pop stack into x
            var x = Stack.Pop();
            // if x is a constant then return to stack
            if (x.IsValue)
            {
                Stack.Push(x);
            }
            else
            {
                // if x is a func requiring (m) params:
                //    resolve m values into values
                var args = new List<object>();
                foreach (var t in x.ArgTypes)
                {
                    object o = Resolve();
                    //    check that types of values match type requirements of x
                    if (!t.IsInstanceOfType(o))
                    {
                        throw new Exception("Invalid Parameter Type"); /// TODO need better error message
                    }
                    args.Add(o);
                }
                //    pass values to x as args
                var result = x.Invoke(args.ToArray());
                //    push result onto stack
                Stack.Push(result.AsFun());
            }
        }
    }
}

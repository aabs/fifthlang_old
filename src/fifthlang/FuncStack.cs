using System;
using System.Collections.Generic;
using System.Text;

namespace fifth
{
    public class FuncStack
    {
        public FuncStack()
        {
            Stack = new Stack<FuncWrapper>();
        }
        public Stack<FuncWrapper> Stack { get; private set; }
        public void Push(FuncWrapper funcWrapper)
        {
            Stack.Push(funcWrapper);
        }
    }
}

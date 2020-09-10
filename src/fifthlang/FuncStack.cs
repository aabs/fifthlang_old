using System;
using System.Collections.Generic;
using System.Text;

namespace fifth
{
    public class FuncStack
    {
        public Stack<FuncWrapper> Stack => new Stack<FuncWrapper>();
        public void Push(FuncWrapper funcWrapper)
        {
            Stack.Push(funcWrapper);
        }
    }
}

using fifth;
using fifth.VirtualMachine;
using NUnit.Framework;

namespace fifthlang_tests
{
    class DispatcherTests
    {
        [Test]
        public void TestCanDispatchAnAdd()
        {
            var f5 = 5.AsFun();
            var add = Fun.Wrap((int x, int y) => x + y);
            FuncStack stack = new();
            var sut = new Dispatcher(stack);
            stack.Push(f5);
            stack.Push(f5);
            stack.Push(add);
            Assert.That(stack.Stack, Has.Count.EqualTo(3));
            sut.Dispatch();
            Assert.That(stack.Stack, Has.Count.EqualTo(1));
            var x = stack.Stack.Pop();
            Assert.That(x.IsValue, Is.True);
            Assert.That(x.Invoke(), Is.EqualTo(10));
        }
        [Test]
        public void TestCanDispatchNestedExpressions()
        {
            var add = Fun.Wrap((int x, int y) => x + y);
            var mul = Fun.Wrap((int x, int y) => x * y);
            FuncStack stack = new();
            Dispatcher sut = new(stack);
            stack.Push(3.AsFun());
            stack.Push(5.AsFun());
            stack.Push(add);
            stack.Push(7.AsFun());
            stack.Push(mul);
            Assert.That(stack.Stack, Has.Count.EqualTo(5));
            sut.Dispatch();
            Assert.That(stack.Stack, Has.Count.EqualTo(1));
            var x = stack.Stack.Pop();
            Assert.That(x.IsValue, Is.True);
            Assert.That(x.Invoke(), Is.EqualTo(56));
        }
    }
}

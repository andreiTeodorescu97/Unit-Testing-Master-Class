using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class StackTests
    {
        private Stack<int> _stack;

        [SetUp]
        public void SetUp()
        {
            _stack = new Stack<int>();
            _stack.Push(1);
            _stack.Push(2);
        }

        [Test]
        public void Peek_Success()
        {
            var firstPeekResult = _stack.Peek();
            Assert.That(2, Is.EqualTo(firstPeekResult));
            Assert.That(2, Is.EqualTo(_stack.Count));
        }

        [Test]
        public void Count_EmptyStack()
        {
            var stack = new Stack<int>();
            Assert.That(0, Is.EqualTo(stack.Count));
        }

        [Test]
        public void Peek_ThrowsException()
        {
            var stack = new Stack<int>();
            Assert.That(() => stack.Peek(), Throws.Exception.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Push_Success()
        {
            _stack.Push(3);
            Assert.That(3, Is.EqualTo(_stack.Count));
            _stack.Push(4);
            Assert.That(4, Is.EqualTo(_stack.Count));
        }

        [Test]
        public void Push_ThrowsException()
        {
            var stack = new Stack<object>();
            Assert.That(() => stack.Push(null), Throws.Exception.TypeOf<ArgumentNullException>());

        }

        [Test]
        public void Pop_Success()
        {
            var firstPop = _stack.Pop();
            Assert.That(2, Is.EqualTo(firstPop));
            Assert.That(1, Is.EqualTo(_stack.Count));
            var secondPop = _stack.Pop();
            Assert.That(1, Is.EqualTo(secondPop));
            Assert.That(0, Is.EqualTo(_stack.Count));
        }

        [Test]
        public void Pop_ThrowsException()
        {
            var stack = new Stack<int>();
            Assert.That(() => stack.Pop(), Throws.Exception.TypeOf<InvalidOperationException>());
        }

    }
}

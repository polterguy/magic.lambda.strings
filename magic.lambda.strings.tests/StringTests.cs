/*
 * Magic, Copyright(c) Thomas Hansen 2019, thomas@gaiasoul.com, all rights reserved.
 * See the enclosed LICENSE file for details.
 */

using System.Linq;
using Xunit;
using magic.node.extensions;

namespace magic.lambda.strings.tests
{
    public class StringTests
    {
        [Fact]
        public void Replace()
        {
            var lambda = Common.Evaluate(@"
.foo1:howdy world
strings.replace:x:-
   :world
   :universe");
            Assert.Equal("howdy universe", lambda.Children.Skip(1).First().Value);
        }

        [Fact]
        public void Contains_01()
        {
            var lambda = Common.Evaluate(@"
.foo1:howdy world
strings.contains:x:-
   .:world");
            Assert.Equal(true, lambda.Children.Skip(1).First().Value);
        }

        [Fact]
        public void Contains_02()
        {
            var lambda = Common.Evaluate(@"
.foo1:howdy tjobing
strings.contains:x:-
   .:world");
            Assert.Equal(false, lambda.Children.Skip(1).First().Value);
        }

        [Fact]
        public void Concat()
        {
            var lambda = Common.Evaluate(@"
.foo:foo
strings.concat
   get-value:x:@.foo
   .:' bar'");
            Assert.Equal("foo bar", lambda.Children.Skip(1).First().Value);
        }

        [Fact]
        public void ToLowers()
        {
            var lambda = Common.Evaluate(@"
.foo:FOO
strings.to-lower:x:-");
            Assert.Equal("foo", lambda.Children.Skip(1).First().Value);
        }

        [Fact]
        public void ToUpper()
        {
            var lambda = Common.Evaluate(@"
.foo:foo
strings.to-upper:x:-");
            Assert.Equal("FOO", lambda.Children.Skip(1).First().Value);
        }

        [Fact]
        public void StartsWith()
        {
            var lambda = Common.Evaluate(@"
.foo:foo-xxx
strings.starts-with:x:-
   :foo
strings.starts-with:x:-/-
   :xxx");
            Assert.True(lambda.Children.Skip(1).First().Get<bool>());
            Assert.False(lambda.Children.Skip(2).First().Get<bool>());
        }

        [Fact]
        public void EndsWith()
        {
            var lambda = Common.Evaluate(@"
.foo:foo-xxx
strings.ends-with:x:-
   :foo
strings.ends-with:x:-/-
   :xxx");
            Assert.False(lambda.Children.Skip(1).First().Get<bool>());
            Assert.True(lambda.Children.Skip(2).First().Get<bool>());
        }

        [Fact]
        public void ReplaceRegEx()
        {
            var lambda = Common.Evaluate(@"
.foo:thomas han0123sen
strings.regex-replace:x:-
   :han[0-9]*sen
   :cool hansen");
            Assert.Equal("thomas cool hansen", lambda.Children.Skip(1).First().Get<string>());
        }

        [Fact]
        public void Split()
        {
            var lambda = Common.Evaluate(@"
.foo:a b cde f
strings.split:x:-
   .:' '");
            Assert.Equal(4, lambda.Children.Skip(1).First().Children.Count());
            Assert.Equal("a", lambda.Children.Skip(1).First().Children.First().Value);
            Assert.Equal("b", lambda.Children.Skip(1).First().Children.Skip(1).First().Value);
            Assert.Equal("cde", lambda.Children.Skip(1).First().Children.Skip(2).First().Value);
        }
    }
}

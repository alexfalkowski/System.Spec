NSpec
=====

NSpec is testing tool for the C# programming language.

Usage
-----

NSpec uses the following frameworks to help it achieve the goal of a [BDD](http://en.wikipedia.org/wiki/Behavior-driven_development) framework:

-	[NSubstitute](http://nsubstitute.github.com/) as the mocking framework
- 	[FluentAssertions](http://fluentassertions.codeplex.com/) as the expected outcome framework
-	[NUnit](http://www.nunit.org/) as the testing framework

Below is an example specification in C#:

	namespace YourCompany.Specs
	{
	    using NSubstitute;

	    public class ImportantSpecification : Specification
	    {
	        public override void Validate()
	        {
	            Describe("A very cool specification", describe =>
	                {
	                    describe.It("should do something that is good", () =>
	                        {
	                            var testInterface = Substitute.For<ITestInterface>();

	                            testInterface.Received().TestMethod();
	                        });
	                });
	        }
	    }
	}

To execute your specifications execute the following:
	
	.\nspec.exe -e C:\Projects\OSS\NSpec\NSpec.Specs\bin\Debug

The default output will display the following example:

	..F..F

	1) should be true - NUnit.Framework.AssertionException: Expected True, but found False.
	   at FluentAssertions.Frameworks.LateBoundTestFramework.Throw(String message) in c:\Workspaces\FluentAssertions\Release
	s\1.7.0\FluentAssertions.Net35\Frameworks\LateBoundTestFramework.cs:line 16
	   at FluentAssertions.Verification.FailWith(String failureMessage, Object[] failureArgs) in c:\Workspaces\FluentAsserti
	ons\Releases\1.7.0\FluentAssertions.Net35\Verification.cs:line 158
	   at FluentAssertions.Assertions.BooleanAssertions.BeTrue(String reason, Object[] reasonArgs) in c:\Workspaces\FluentAs
	sertions\Releases\1.7.0\FluentAssertions.Net35\Assertions\BooleanAssertions.cs:line 75
	   at NSpec.Specs.TestSpecificationWithFluentAssertions.<Validate>b__1() in C:\Projects\OSS\NSpec\NSpec.Specs\TestSpecif
	icationWithFluentAssertions.cs:line 16
	   at NSpec.Example.It(String reason, Action action)

	2) call method - NSubstitute.Exceptions.ReceivedCallsException: Expected to receive a call matching:
	        TestMethod()
	Actually received no matching calls.

	   at NSubstitute.Core.ReceivedCallsExceptionThrower.Throw(ICallSpecification callSpecification, IEnumerable`1 matchingC
	alls, IEnumerable`1 nonMatchingCalls, Quantity requiredQuantity)
	   at NSubstitute.Routing.Handlers.CheckReceivedCallsHandler.Handle(ICall call)
	   at System.Linq.Enumerable.WhereSelectArrayIterator`2.MoveNext()
	   at System.Linq.Enumerable.FirstOrDefault[TSource](IEnumerable`1 source, Func`2 predicate)
	   at NSubstitute.Routing.Route.Handle(ICall call)
	   at NSubstitute.Proxies.CastleDynamicProxy.CastleForwardingInterceptor.Intercept(IInvocation invocation)
	   at Castle.DynamicProxy.AbstractInvocation.Proceed()
	   at NSpec.Specs.TestSpecificationWithNSubstitute.<Validate>b__1() in C:\Projects\OSS\NSpec\NSpec.Specs\TestSpecificati
	onWithNSubstitute.cs:line 22
	   at NSpec.Example.It(String reason, Action action)

	Finished in 0.397 seconds
	6 examples, 2 failures
System.Spec
===========

System.Spec is testing tool for the C# programming language.

Usage
-----

System.Spec uses the following frameworks to help it achieve the goal of a [BDD](http://en.wikipedia.org/wiki/Behavior-driven_development) framework:

-	[NSubstitute](http://nsubstitute.github.com/) as the mocking framework
- 	[FluentAssertions](http://fluentassertions.codeplex.com/) as the expected outcome framework
-	[NUnit](http://www.nunit.org/) as the testing framework

Below is an example specification in C#:

	namespace YourCompany.Specs
	{
	    using NSubstitute;
	    using System.Spec;

	    public class ImportantSpecification : Specification
	    {
	        protected override void Define()
	        {
	            Describe("A very cool specification", () => {
                    It("should do something that is good", () => {
                        var testInterface = Substitute.For<ITestInterface>();
                        testInterface.Received().TestMethod();
                    });
                });
	        }
	    }
	}

To execute your specifications execute the following:
	
	./artifacts/spec.sh artifacts/

The default output will display the following example:

	..FF.....

    Failures:

    1) should be true
       Failure/Error: Expected True, but found False.

    2) call method
       Failure/Error: Expected to receive a call matching:
    	TestMethod()
    Actually received no matching calls.

    Finished in 0.273 seconds
    9 examples, 2 failures

Convention
----------

System.Spec expects that you name your DLL's with the suffix Specs.dll. This is so that System.Spec does not load all of the assemblies in your path. You can override this with the -s switch.

Options
-------

The command spec has the following options:

    Usage: spec options

       OPTION          TYPE                   ORDER   DESCRIPTION
       -path (-p)      String                 0       The path to search all the Spec assemblies.
       -example (-e)   String                         The example to execute. This could be a spec, example group or an example.
       -pattern (-P)   String                         Load files matching pattern. The default is Specs.
       -output (-o)    String                         The output path of the test results. The default is test-results.xml.
       -format (-f)    ConsoleFormatterType           Specifies what format to use for output.
       -dryrun (-d)    Switch                         Invokes formatters without executing the examples.
       -version (-v)   Switch                         Display the version.
       -colour (-c)    Switch                         Enable colour in the output.
       -help (-h)      Switch                         You're looking at it.

Migration
---------

From version 2.0 we have changed the way you construct your specifications. Here is a list of changes:

- The new method to override is Define rather Validate.
- The Describe method does not pass an example to the Action.
- BeforeXXX and AfterXXX are now methods that take an action, rather than just an action.
- The command line options have changed to follow the [rspec](https://www.relishapp.com/rspec/rspec-core/v/2-13/docs/command-line) command line.
- The output follows RSpec [format](https://www.relishapp.com/rspec/rspec-core/v/2-13/docs/command-line/format-option).

Future
------

I have always been a fan of the [Jasmine](http://pivotal.github.com/jasmine/) so I will be following that design. If you have any suggestions please raise a request or issue.
NScripto
========

NScripto is a lightweight fast C# script engine, built and harvested from a project that required flexible user configuration. The latter versions of C# have made it more suitable for use as a scripting language. 

In essence, NScripto allows small C# scripts to be written without the end-user worrying about classes and methods, while still providing the developer with tools resembling mixins for script composition that ease the burden of maintainability. It does so by dynamically delegation classes and compiling code on the fly with CodeDom. The resulting compiled scripts are as fast as any other C#.

Note that NScripto does not make use of AppDomains, so without wrapping script usage in your own AppDomains, it will not be possible to unload compiled scripts (thus every script compiled will consume memory until the process is killed). NScripto will cache identical scripts to reduce this problem (see caching section below). 

Also, NScripto does not deal with security out of the box - it allows for arbitrary C# code to be compiled and run - so it is not a good fit for scenarios with potentially malicious users without extra precausions.

# Examples

All examples are expressed as NUnit tests. These can also be found in the test project in the `NScripto.Test.ReadMe` namespace.

# Basic example

The simplest meaningful example of NScripto would be to something like the following:

```csharp
[Test]
public void HelloWorld()
{
    var scriptApi = new ScriptApi();
    var script = scriptApi.CompileScript<HelloWorldEnvironment>("DoIt()");

    var environment = new HelloWorldEnvironment();
    script.Run(environment);

    environment.Result.ShouldEqual("Hello World!");
}

public class HelloWorldEnvironment
{
    public string Result { get; set; }

    public void DoIt()
    {
        Result = "Hello World!";
    }
}
```

The basic unit for integrating with scripts is the concept of an "Environment". Any public method in the contained environment will be available to the script and environment instances can also hold any state / output that the script produces.

The class generated from the above example looks roughly like this:

!!! Example of generated class.

# Composed scripts

When providing multiple different script types in different scenarios, there is often an overlap in the methods that the different script types need access to. Solving it with inheritance might sound promising at first, but it quickly breaks down as combinations arise. NScripto solves this using composition by allowing for multiple script environments to mixed in when compiling a script. 

Extending the first example with an extra "general-purpose" script environment:

```csharp
[Test]
public void MultipleEnvironments()
{
    var scriptApi = new ScriptApi();
    var script = scriptApi.CompileScript<HelloWorldEnvironment, GeneralPurposeEnvironment>("DoIt(GetRandom(42))");

    var helloEnvironment = new HelloWorldEnvironment();
    var generalEnvironment = new GeneralPurposeEnvironment();

    script.Run(helloEnvironment, generalEnvironment);

    helloEnvironment.Result.ShouldEqual("Hello 28!");
}

public class HelloWorldEnvironment
{
    public string Result { get; set; }

    public void DoIt(int num)
    {
        Result = "Hello " + num + "!";
    }
}

public class GeneralPurposeEnvironment
{
    public static Random Random = new Random(42);

    public int GetRandom(int max)
    {
        return Random.Next(max);
    }
}
```

Which on runtime would generate the following delegation class:

!!! Example of generated class with 2 environments.

# Wrapped scripts

NScripto provides a convinient way of wrapping scripts in your own C# classes, hiding the additional complexities of instantiating script environments, performing any relevant post / pre actions and allowing for real return values. 

A wrapper class for our first example script could look like this:

```csharp
public class HappyScript
{
    private readonly IScript<HappyEnvironment> _script;

    public HappyScript(IScript<HappyEnvironment> script)
    {
        _script = script;
    }

    public string Run()
    {
        var happyEnvironment = new HappyEnvironment("Moody");
        _script.Run(happyEnvironment);
        return happyEnvironment.State;
    }
}

public class HappyEnvironment
{
    public string State { get; set; }

    public HappyEnvironment(string initialState)
    {
        State = initialState;
    }

    public void Mood(string mood)
    {
        State = mood;
    }
}
```

Compiling the script inside the wrapper is done using the `ScriptApi` class like so:

```csharp
[Test]
public void Wrapped()
{
    var scriptApi = new ScriptApi();
    var wrappedScript = scriptApi.CompileWrappedScript<HappyScript>("Mood(\"Happy!\")");

    string result = wrappedScript.Run();

    result.ShouldEqual("Happy!");
}
```

NScripto will instantiate the wrapper class and inject the compiled script. 

# Exluding script environment methods

If your script environment contains public methods that should not be available to compiled scripts, you can annotate them with the `[NoScript]` attribute.

# Script documentation

Creating and maintaining an up-to-date documentation of the script methods available in each script types for end users to consume manually will often be errornous and at best labourious. NScripto comes with tools to annotating scripts with documentation, extracting this documentation at runtime and verification tools that are easy to put in a unit test to ensure that your documentation is always up-to-date.

The verification will look for wrapped script constructors (using one of the `IScript` interfaces as only parameter) and script environments annotated with the `[ScriptEnvironment]` attribute.

Starting from the outside in, here is a simple NUnit test using the verification tool to check our script assembly for missing documentation. 

```csharp
[Test]
public void VerifyScripts()
{
    var scriptApi = new ScriptApi();

    scriptApi.VerifyTypes(new [] { typeof(HappyEnvironment) });
}

[ScriptEnvironment("Happy env!", "Happy dappy.")]
public class HappyEnvironment
{
    public string State { get; set; }

    public HappyEnvironment(string initialState)
    {
        State = initialState;
    }

    public void Mood(string mood)
    {
        State = mood;
    }
}
```

Running the test gives the following output, since we have not documented our scripts yet.

```
NScripto.Exceptions.ScriptVerificationException : Script verification failed:

 - Missing script method attribute in environment: HappyEnvironment, method: Mood
 - Missing script parameter (mood) attribute in environment: HappyEnvironment, method: Mood
 ```

Script environments are annotated with a `[ScriptEnvironment("name", "description")]` attribute. Script methods with `[ScriptMethod("description")]` and script method parameters with `[ScriptParameter("name", "description")]` attributes. Methods with the `[NoScript]` attribute will not be documented or accessible to scripts.

Additionally wrapped script classes can be annotated with a `[Script("name", "description")]` attribute, since these provide a natural grouping of available environments in the various scenarios.

Annotating our sample script gives us this:

```csharp
[ScriptEnvironment("Happy env!", "Happy dappy.")]
public class HappyEnvironment
{
    public string State { get; set; }

    public HappyEnvironment(string initialState)
    {
        State = initialState;
    }

    [ScriptMethod("Sets the overall mood.")]
    [ScriptParameter("mood", "How you doing?")]
    public void Mood(string mood)
    {
        State = mood;
    }
}
```

Extracting the documentation for presentation is done in a similar way:

!!! Extracting documentation

The resulting documentation in pseudo-form:

!!! Pseudo documentation structure


# Caching compiled scripts

As NScripto generates and compiles C# code on the fly and since the CLR does not allow for compiled code to be unloaded outside of unloading entire AppDomains, only compiling each individual script once makes a lot of sense to reduce the memory load over time. NScripto provides a simple cache that transparently ensures that unique scripts are only compiled once. 

The cache is static across all instances of `ScriptApi` and as NScripto has no way of unloading scripts, compiled scripts stay in the internal cache indefinitely.


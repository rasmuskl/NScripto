NScripto
========

NScripto is a lightweight fast C# script engine, built and harvested from a project that required flexible user configuration. The latter versions of C# have made it more suitable for use as a scripting language. 

In essence, NScripto allows small C# scripts to be written without the end-user worrying about classes and methods, while still providing the developer with tools resembling mixins for script composition that ease the burden of maintainability. It does so by dynamically delegation classes and compiling code on the fly with CodeDom. The resulting compiled scripts are as fast as any other C#.

Note that NScripto does not make use of AppDomains, so without wrapping script usage in your own AppDomains, it will not be possible to unload compiled scripts (thus every script compiled will consume memory until the process is killed). NScripto will cache identical scripts to reduce this problem (see caching section below). 

Also, NScripto does not deal with security out of the box - it allows for arbitrary C# code to be compiled and run - so it is not a good fit for scenarios with potentially malicious users without extra precausions.

# Basic example

The simplest meaningful example of NScripto would be to something like the following:

!!! Use case 1 - Simple compilation of raw script with a state variable that contains 'Hello world'.

The basic unit for integrating with scripts is the concept of an Environment. Any public method in the contained Environment will be available to the script and Environment instances can also hold any state / output that the script produces.

The class generated from the above example looks roughly like this:

!!! Example of generated class.

# Composed scripts

When providing multiple different script types in different scenarios, there is often an overlap in the methods that the different script types need access to. Solving it with inheritance might sound promising at first, but it quickly breaks down as combinations arise. NScripto solves this using composition by allowing for multiple script environments to mixed in when compiling a script. 

Extending the first example with an extra "general-purpose" script environment:

!!! Example of raw script with 2 environments.

Which on runtime would generate the following delegation class:

!!! Example of generated class with 2 environments.

# Wrapped scripts

NScripto provides a convinient way of wrapping scripts in your own C# classes, hiding the additional complexities of instantiating script environments, performing any relevant post / pre actions and allowing for real return values. 

A wrapper class for our first example script could look like this:

!!! Example wrapper class

Compiling the script inside the wrapper is done using the ScriptApi class like so:

!!! Call to script api wrap method

NScripto will instantiate the wrapper class and inject the compiled script. 

# Exluding script environment methods

If your script environment contains public methods that should not be available to compiled scripts, you can annotate them with the [NoScript] attribute.

# Script documentation

Creating and maintaining an up-to-date documentation of the script methods available in each script types for end users to consume manually will often be errornous and at best labourious. NScripto comes with tools to annotating scripts with documentation, extracting this documentation at runtime and verification tools that are easy to put in a unit test to ensure that your documentation is always up-to-date.

Starting from the outside in, here is a simple NUnit test using the verification tool to check our script assembly for missing documentation.

!!! Example of NUnit verification test

Running the test gives the following output, since we have not documented our scripts yet.

!!! Test output

Script environments are annotated with a [ScriptEnvironment("name", "description")] attribute. Script methods with [ScriptMethod("description")] and script method parameters with [ScriptParameter("name", "description")] attributes. Methods with the [NoScript] attribute will not be documented or accessible to scripts.

Additionally wrapped script classes can be annotated with a [Script("name", "description")] attribute, since these provide a natural grouping of available environments in the various scenarios.

Annotating our sample script gives us this:

!!! Annotated scripts

Extracting the documentation for presentation is done in a similar way:

!!! Extracting documentation

The resulting documentation in pseudo-form:

!!! Pseudo documentation structure


# Caching compiled scripts

As NScripto generates and compiles C# code on the fly and since the CLR does not allow for compiled code to be unloaded outside of unloading entire AppDomains, only compiling each individual script once makes a lot of sense to reduce the memory load over time. NScripto provides a simple cache that transparently ensure that scripts are only compiled once. As NScripto has no way of unloading scripts, compiled scripts stay in the internal cache indefinitely. 

In special scenarios where this caching is not desired, for instance if you are handling unloads using AppDomains, it can be disabled when instantiating the script api.

!!! Example of disabling caching.



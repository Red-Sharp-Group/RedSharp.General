# Red Sharp General Packages

<img src="Assets/Advertisement.png" width="275" align="left"/>

Look, it is a simple, plain solution. I am not trying to hide something under `private` or `internal` without an absolute need, it doesn't contain auto-generators, that make code behind curtains, or any other fancy stuff, it probably has analogs and maybe they even better.

If you like to use the packages or source code, [you are welcome to do it](LICENSE). If my code helps someones to solve their tasks - it was all worth it.

<br/><br/><br/><br/><br/><br/><br/>

## General purpose library

<img src="Source/RedSharp.General/PackageIcon.png" width="100" align="left"/>

**RedSharp.General package**

The RedSharp.General is a general-purpose library that provides a set of utility classes and methods for common programming tasks. It includes features such as basic implementation of the Disposable pattern both with and without events, set of interfaces to indicate state of Disposable objects, static and conditional guards for internal logic security.

<br/>

>The package is available for `.Net Framework`, `.Net Standard` and `.Net Core`.

## Special collections library

<img src="Source/RedSharp.General.Collections/PackageIcon.png" width="100" align="left"/>

**RedSharp.General.Collections package**

The RedSharp.General.Collections is a set of specialized collections and utilities.

<br/>

>The package is available for `.Net Framework`, `.Net Standard` and `.Net Core`.


## Unmanaged memory abstractions library

<img src="Source/RedSharp.General.Unmanaged/PackageIcon.png" width="100" align="left"/>

**RedSharp.General.Unmanaged package**

The RedSharp.General.Unmanaged contains abstractions and wrappers for interaction with unmanaged memory. Also, it contains wrappers that allow usage and interact with **shared memory**.

<br/>

>The package is available for `.Net Framework` and `.Net Core`. The `.Net Standard` is not an option because I do not want to reimplement the `NativeMemory` class on my own.

>The shared memory functionality is OS specific, I have decided that it would be easier to make a simple `if ... else ...` statement than to make OS specific libraries.

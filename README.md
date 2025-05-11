# Red Sharp General Packages

<table style="border: 0px solid black;">
	<tr>
	  <td style="border: 0px solid black; width: 275px; vertical-align: top;">

![Advertisement.png](Assets/Advertisement.png)
	  </td>
	  <td style="border: 0px solid black; vertical-align: top;">

Look, it is a simple, plain solution. I am not trying to hide something under `private` or `internal` without an absolute need, it doesn't contain auto-generators, that make code behind curtains, or any other fancy stuff, it probably has analogs and maybe they even better.

If you like to use the packages or source code, [you are welcome to do it](LICENSE). If my code helps someones to solve their tasks - it was all worth it.
	  </td>
	</tr> 
</table>

## General purpose library

<table style="border: 0px solid black;">
	<tr>
	  <td style="border: 0px solid black; width: 100px; vertical-align: top; padding-top:15px;">

![Logo.png](Source/RedSharp.General/PackageIcon.png)
	  </td>
	  <td style="border: 0px solid black; vertical-align: top;">
	  
**RedSharp.General package**

The RedSharp.General is a general-purpose library that provides a set of utility classes and methods for common programming tasks. It includes features such as basic implementation of the Disposable pattern both with and without events, set of interfaces to indicate state of Disposable objects, static and conditional guards for internal logic security.

>The package is available for `.Net Framework`, `.Net Standard` and `.Net Core`.
	  </td>
	</tr> 
</table>

## Special collections library

<table style="border: 0px solid black;">
	<tr>
	  <td style="border: 0px solid black; width: 100px; vertical-align: top; padding-top:15px;">

![Logo.png](Source/RedSharp.General.Collections/PackageIcon.png)
	  </td>
	  <td style="border: 0px solid black; vertical-align: top;">
	  
**RedSharp.General.Collections package**

The RedSharp.General.Collections is a set of specialized collections and utilities.

>The package is available for `.Net Framework`, `.Net Standard` and `.Net Core`.
	  </td>
	</tr> 
</table>


## Unmanaged memory abstractions library

<table style="border: 0px solid black;">
	<tr>
	  <td style="border: 0px solid black; width: 100px; vertical-align: top; padding-top:15px;">

![Logo.png](Source/RedSharp.General.Unmanaged/PackageIcon.png)
	  </td>
	  <td style="border: 0px solid black; vertical-align: top;">
	  
**RedSharp.General.Unmanaged package**

The RedSharp.General.Unmanaged contains abstractions and wrappers for interaction with unmanaged memory. Also, it contains wrappers that allow usage and interact with **shared memory**.

>The package is available for `.Net Framework` and `.Net Core`. The `.Net Standard` is not an option because I do not want to reimplement the `NativeMemory` class on my own.

>The shared memory functionality is OS specific, I have decided that it would be easier to make a simple `if ... else ...` statement than to make a OS specific libraries.
	  </td>
	</tr> 
</table>

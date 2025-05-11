# Red Sharp General Unmanaged Package

The RedSharp.General.Unmanaged contains abstractions and wrappers for interaction with unmanaged memory. Also, it contains wrappers that allow usage and interact with **shared memory**.

>The package is available for `.Net Framework` and `.Net Core`. The `.Net Standard` is not an option because I do not want to reimplement the `NativeMemory` class on my own.

>The shared memory functionality is OS specific, I have decided that it would be easier to make a simple `if ... else ...` statement than to make a OS specific libraries.
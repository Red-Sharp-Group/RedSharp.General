using System;
using System.Runtime.CompilerServices;
using RedSharp.General.Helpers;

namespace RedSharp.General.Unmanaged.Helpers
{
    public static unsafe class SharedMemory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnmanagedBuffer<TItem> CreateBuffer<TItem>(string name, int length, out SharedMemoryObject @object) where TItem : unmanaged
        {
            NumericGuard.ThrowIfLessOrEqualZero(length, nameof(length));

            var size = (uint)(length * sizeof(TItem));

            @object = new SharedMemoryObject(name, size);

            var mapping = new SharedMemoryMapping(@object);
            var result = new UnmanagedBuffer<TItem>(mapping, length, false);

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnmanagedBuffer<TItem> CreateBuffer<TItem>(int length, out SharedMemoryObject @object) where TItem : unmanaged
        {
            return CreateBuffer<TItem>(Guid.NewGuid().ToString(), length, out @object);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnmanagedStructureWrapper<TItem> CreateStructureWrapper<TItem>(string name, out SharedMemoryObject @object) where TItem : unmanaged
        {
            var size = (uint)sizeof(TItem);

            @object = new SharedMemoryObject(name, size);

            var mapping = new SharedMemoryMapping(@object);
            var result = new UnmanagedStructureWrapper<TItem>(mapping);

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnmanagedStructureWrapper<TItem> CreateStructureWrapper<TItem>(out SharedMemoryObject @object) where TItem : unmanaged
        {
            return CreateStructureWrapper<TItem>(Guid.NewGuid().ToString(), out @object);
        }
    }
}

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DotRecast.Core.Collections
{
    /// <summary>
    /// MemoryMarshal.CreateSpan/CreateReadOnlySpan are not available on netstandard2.0
    /// (the System.Memory package does not expose them). Fall back to an unsafe
    /// pointer-based construction there; other targets use MemoryMarshal directly.
    /// </summary>
    internal static class RcSpan
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> CreateSpan<T>([UnscopedRef] ref T reference, int length) where T : unmanaged
        {
#if NETSTANDARD2_0
            unsafe
            {
                return new Span<T>(Unsafe.AsPointer(ref reference), length);
            }
#else
            return MemoryMarshal.CreateSpan(ref reference, length);
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> CreateReadOnlySpan<T>([UnscopedRef] ref T reference, int length) where T : unmanaged
        {
#if NETSTANDARD2_0
            unsafe
            {
                return new ReadOnlySpan<T>(Unsafe.AsPointer(ref reference), length);
            }
#else
            return MemoryMarshal.CreateReadOnlySpan(ref reference, length);
#endif
        }
    }
}

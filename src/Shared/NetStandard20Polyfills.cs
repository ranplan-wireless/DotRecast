// Minimal compatibility shims for netstandard2.0.
//
// The DotRecast sources target netstandard2.1+ and use a handful of BCL APIs that
// do not exist in netstandard2.0. This file is linked into every project that
// targets netstandard2.0 (see the csproj <Compile Include=...>) and is compiled
// *only* for that target, so it never affects netstandard2.1/net8.0/net9.0/net10.0
// builds.
//
// The types are internal on purpose: each assembly gets its own private copy, so
// nothing leaks into the public API surface and there is no ambiguity for consumers.

#if NETSTANDARD2_0

using System.Collections.Generic;
using System.IO;

namespace System
{
    /// <summary>Subset of System.MathF (netstandard2.1+) implemented on top of Math.</summary>
    internal static class MathF
    {
        public const float PI = (float)Math.PI;

        public static float Abs(float x) => Math.Abs(x);
        public static float Floor(float x) => (float)Math.Floor(x);
        public static float Ceiling(float x) => (float)Math.Ceiling(x);
        public static float Truncate(float x) => (float)Math.Truncate(x);
        public static float Round(float x) => (float)Math.Round(x);
        public static float Sqrt(float x) => (float)Math.Sqrt(x);
        public static float Pow(float x, float y) => (float)Math.Pow(x, y);
        public static float Exp(float x) => (float)Math.Exp(x);
        public static float Log(float x) => (float)Math.Log(x);
        public static float Cos(float x) => (float)Math.Cos(x);
        public static float Sin(float x) => (float)Math.Sin(x);
        public static float Tan(float x) => (float)Math.Tan(x);
        public static float Min(float x, float y) => Math.Min(x, y);
        public static float Max(float x, float y) => Math.Max(x, y);
        public static int Sign(float x) => Math.Sign(x);
        public static bool IsNaN(float x) => float.IsNaN(x);
        public static bool IsInfinity(float x) => float.IsInfinity(x);
    }

    /// <summary>Subset of System.HashCode (netstandard2.1+) used by Vector/struct hash codes.</summary>
    internal struct HashCode
    {
        private static int CombineHash(int hash, int value)
        {
            unchecked
            {
                return (hash * 397) ^ value;
            }
        }

        public static int Combine<T1>(T1 value1)
        {
            unchecked
            {
                return CombineHash(17, EqualityComparer<T1>.Default.GetHashCode(value1));
            }
        }

        public static int Combine<T1, T2>(T1 value1, T2 value2)
        {
            unchecked
            {
                int hash = 17;
                hash = CombineHash(hash, EqualityComparer<T1>.Default.GetHashCode(value1));
                hash = CombineHash(hash, EqualityComparer<T2>.Default.GetHashCode(value2));
                return hash;
            }
        }

        public static int Combine<T1, T2, T3>(T1 value1, T2 value2, T3 value3)
        {
            unchecked
            {
                int hash = 17;
                hash = CombineHash(hash, EqualityComparer<T1>.Default.GetHashCode(value1));
                hash = CombineHash(hash, EqualityComparer<T2>.Default.GetHashCode(value2));
                hash = CombineHash(hash, EqualityComparer<T3>.Default.GetHashCode(value3));
                return hash;
            }
        }

        public static int Combine<T1, T2, T3, T4>(T1 value1, T2 value2, T3 value3, T4 value4)
        {
            unchecked
            {
                int hash = 17;
                hash = CombineHash(hash, EqualityComparer<T1>.Default.GetHashCode(value1));
                hash = CombineHash(hash, EqualityComparer<T2>.Default.GetHashCode(value2));
                hash = CombineHash(hash, EqualityComparer<T3>.Default.GetHashCode(value3));
                hash = CombineHash(hash, EqualityComparer<T4>.Default.GetHashCode(value4));
                return hash;
            }
        }
    }

    /// <summary>string.Split(char, StringSplitOptions) and Split(string, StringSplitOptions) are netstandard2.1+.</summary>
    internal static class StringPolyfills
    {
        public static string[] Split(this string value, char separator, StringSplitOptions options)
        {
            return value.Split(new[] { separator }, options);
        }

        public static string[] Split(this string value, string separator, StringSplitOptions options)
        {
            return value.Split(new[] { separator }, options);
        }

        public static string[] Split(this string value, string separator)
        {
            return value.Split(new[] { separator }, StringSplitOptions.None);
        }
    }
}

namespace System.Collections.Generic
{
    /// <summary>Queue&lt;T&gt;.TryDequeue / Dictionary&lt;TKey,TValue&gt;.TryAdd are netstandard2.1+.</summary>
    internal static class CollectionPolyfills
    {
        public static bool TryDequeue<T>(this Queue<T> queue, out T result)
        {
            if (0 < queue.Count)
            {
                result = queue.Dequeue();
                return true;
            }

            result = default(T);
            return false;
        }

        public static bool TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
                return false;

            dictionary.Add(key, value);
            return true;
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.TryGetValue(key, out var value) ? value : default(TValue);
        }
    }
}

namespace System.IO
{
    /// <summary>Span-based Stream.Read / Stream.Write overloads are netstandard2.1+.</summary>
    internal static class StreamPolyfills
    {
        public static int Read(this Stream stream, Span<byte> buffer)
        {
            var temp = new byte[buffer.Length];
            int read = stream.Read(temp, 0, temp.Length);
            temp.AsSpan(0, read).CopyTo(buffer);
            return read;
        }

        public static int Read(this BinaryReader reader, Span<byte> buffer)
        {
            var temp = new byte[buffer.Length];
            int read = reader.Read(temp, 0, temp.Length);
            temp.AsSpan(0, read).CopyTo(buffer);
            return read;
        }

        public static void Write(this Stream stream, ReadOnlySpan<byte> buffer)
        {
            var temp = buffer.ToArray();
            stream.Write(temp, 0, temp.Length);
        }
    }
}

#endif

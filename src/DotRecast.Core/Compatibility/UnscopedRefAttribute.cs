// System.Diagnostics.CodeAnalysis.UnscopedRefAttribute was introduced in .NET 7.
// netstandard2.0/netstandard2.1 reference assemblies do not define it, but the C#
// compiler recognizes the attribute by full name, so declaring it locally is enough.
// It is needed by RcSpan, which returns a Span built from a ref parameter.

#if NETSTANDARD2_0 || NETSTANDARD2_1

using System;

namespace System.Diagnostics.CodeAnalysis
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter, Inherited = false)]
    internal sealed class UnscopedRefAttribute : Attribute
    {
    }
}

#endif

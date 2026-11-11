/*
Copyright (c) 2009-2010 Mikko Mononen memon@inside.org
recast4j copyright (c) 2015-2019 Piotr Piastucki piotr@jtilia.org
DotRecast Copyright (c) 2023-2024 Choi Ikpil ikpil@naver.com

This software is provided 'as-is', without any express or implied
warranty.  In no event will the authors be held liable for any damages
arising from the use of this software.
Permission is granted to anyone to use this software for any purpose,
including commercial applications, and to alter it and redistribute it
freely, subject to the following restrictions:
1. The origin of this software must not be misrepresented; you must not
 claim that you wrote the original software. If you use this software
 in a product, an acknowledgment in the product documentation would be
 appreciated but is not required.
2. Altered source versions must be plainly marked as such, and must not be
 misrepresented as being the original software.
3. This notice may not be removed or altered from any source distribution.
*/

using System;
using System.Runtime.CompilerServices;

namespace DotRecast.Core
{
    public static class RcMath
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sqr(float f)
        {
            return f * f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Lerp(float value1, float value2, float amount)
        {
            return (value1 * (1.0f - amount)) + (value2 * amount);
        }

        /// <summary>
        /// Same semantics as System.Math.Clamp(float, float, float), which is not available on netstandard2.0.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp(float value, float min, float max)
        {
            if (max < min)
                throw new ArgumentException("max < min");

            if (value < min)
                return min;

            if (value > max)
                return max;

            return value;
        }

        /// <summary>
        /// Same semantics as System.Math.Clamp(int, int, int), which is not available on netstandard2.0.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Clamp(int value, int min, int max)
        {
            if (max < min)
                throw new ArgumentException("max < min");

            if (value < min)
                return min;

            if (value > max)
                return max;

            return value;
        }

        /// <summary>
        /// float.IsFinite is not available on netstandard2.0.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFinite(float value)
        {
#if NETSTANDARD2_0
            return !float.IsNaN(value) && !float.IsInfinity(value);
#else
            return float.IsFinite(value);
#endif
        }
    }
}
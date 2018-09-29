using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chagrins
{
    namespace Utility
    {
        public static class Maths
        {
            public const float ValueDelta = 0.0001f;

            public static bool EqualZero(float _value)
            {
                return _value > -ValueDelta && _value < ValueDelta;
            }

            public static bool EqualZero(float _value, float _delta)
            {
                return _value > -_delta && _value < _delta;
            }
        }
    }
}

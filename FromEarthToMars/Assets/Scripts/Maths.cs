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

            public static Vector2 RandomPointOnRect(Rect _rectangle)
            {
                Vector2 pick = _rectangle.center;
                float width = _rectangle.width;
                float height = _rectangle.height;
                float edgeLength = 2 * width + 2 * height;

                float randomEdgeLength = Random.Range(0.0f, edgeLength);

                //going from bottom left counter-clockwise
                if (randomEdgeLength < height)
                {
                    //left side a1
                    pick = new Vector2(_rectangle.xMin, _rectangle.yMin + randomEdgeLength);
                }
                else if (randomEdgeLength < width + height)
                {
                    //top side b1
                    pick = new Vector2(_rectangle.xMin + randomEdgeLength - width, _rectangle.yMax);
                }
                else if (randomEdgeLength < (width + (height * 2)))
                {
                    //right side a2
                    pick = new Vector2(_rectangle.xMax, _rectangle.yMax - (randomEdgeLength - (width + height)));
                }
                else
                {
                    //bottom side b2
                    pick = new Vector2(_rectangle.xMax - (randomEdgeLength - (width + height * 2)), _rectangle.yMin);
                }
                return pick;
            }
        }
    }
}

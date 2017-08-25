using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DouduckGame.Math {
    public class MathUti {
        public static float FastPow (float num, int exp) {
            float result = 1f;
            while (exp > 0) {
                if (exp % 2 == 1)
                    result *= num;
                exp >>= 1;
                num *= num;
            }
            return result;
        }
    }
}
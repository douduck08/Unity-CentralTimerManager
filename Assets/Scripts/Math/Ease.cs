using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DouduckGame.Math {
    [System.Serializable]
    public class Ease {

        private static float HalfPI = 1.5707963f;

        [SerializeField]
        private EaseType m_easeType;
        public Ease() {

        }


        public static float Linear (float t) {
            return t;
        }
        public static float EaseInQuad (float t) {
            return t * t;
        }
        public static float EaseOutQuad (float t) {
            return (2f - t) * t;
        }
        public static float EaseInOutQuad (float t) {
            if (t < 0.5f) {
                return t * t * 2f;
            }
            return (2f - t) * t * 2f - 1f;
        }
        public static float EaseInCubic (float t) {
            return t * t * t;
        }
        public static float EaseOutCubic (float t) {
            t -= 1f;
            return t * t * t + 1f;
        }
        public static float EaseInOutCubic (float t) {
            if (t < 0.5f) {
                return t * t * t * 4f;
            }
            t -= 1f;
            return t * t * t * 4f + 1f;
        }
        public static float EaseInQuart (float t) {
            t *= t;
            return t * t;
        }
        public static float EaseOutQuart (float t) {
            t -= 1f;
            t *= t;
            return 1f - t * t;
        }
        public static float EaseInOutQuart (float t) {
            if (t < 0.5f) {
                t *= t;
                return t * t * 8f;
            }
            t -= 1f;
            t *= t;
            return 1f - t * t * 8f;
        }

        public static float EaseInQuint (float t) {
            float t2 = t * t;
            return t2 * t2 * t;
        }
        public static float EaseOutQuint (float t) {
            t -= 1f;
            float t2 = t * t;
            return t2 * t2 * t + 1f;
        }
        public static float EaseInOutQuint (float t) {
            float t2;
            if (t < 0.5f) {
                t2 = t * t;
                return t2 * t2 * t * 16f;
            }
            t -= 1f;
            t2 = t * t;
            return t2 * t2 * t * 16f + 1f;
        }

        public static float EaseInSine (float t) {
            return 1f - Mathf.Cos (HalfPI * t);
        }
        public static float EaseOutSine (float t) {
            return Mathf.Sin (HalfPI * t);
        }
        public static float EaseInOutSine (float t) {
            return 0.5f - 0.5f * Mathf.Cos (t * Mathf.PI);
        }

        public static float EaseInExpo (float t) {
            return Mathf.Pow (2f, 10f * t - 10f);
        }
        public static float EaseOutExpo (float t) {
            return 1f - Mathf.Pow (2f, -10f * t);
        }
        public static float EaseInOutExpo (float t) {
            if (t < 0.5f) {
                return Mathf.Pow (2f, 10f * t - 6f);
            }
            return 1 - Mathf.Pow (2f, -20f * t + 9f);
        }

        public static float EaseInCirc (float t) {
            return 1f - Mathf.Sqrt (1f - t * t);
        }
        public static float EaseOutCirc (float t) {
            t -= 1f;
            return Mathf.Sqrt (1f - t * t);
        }
        public static float EaseInOutCirc (float t) {
            if (t < 0.5f) {
                return 0.5f - Mathf.Sqrt (0.25f - t * t);
            }
            return 0.5f + Mathf.Sqrt (0.25f - t * t);
        }

        public static float EaseInBack (float t) {
            return t * t * (2.70158f * t - 1.70158f);
        }

        public static float EaseOutBack (float t) {
            t -= 1f;
            return 1f + t * t * (2.70158f * t + 1.70158f);
        }

        public static float EaseInOutBack (float t) {
            if (t < 0.5) {
                return t * t * (7f * t - 2.5f) * 2f;
            } 
                t -= 1;
                return 1f + t * t * 2f * (7f * t + 2.5f);
        }

        public static float EaseInElastic (float t) {
            float t2 = t * t;
            return t2 * t2 * Mathf.Sin (t * Mathf.PI * 4.5f);
        }

        public static float EaseOutElastic (float t) {
            float t2 = (t - 1) * (t - 1);
            return 1 - t2 * t2 * Mathf.Cos (t * Mathf.PI * 4.5f);
        }

        public static float EaseInOutElastic (float t) {
            float t2;
            if (t < 0.45f) {
                t2 = t * t;
                return 8f * t2 * t2 * Mathf.Sin (t * Mathf.PI * 9f);
            } else if (t < 0.55f) {
                return 0.5f + 0.75f * Mathf.Sin (t * Mathf.PI * 4f);
            } else {
                t2 = (t - 1f) * (t - 1f);
                return 1f - 8f * t2 * t2 * Mathf.Sin (t * Mathf.PI * 9f);
            }
        }

        public static float EaseInBounce (float t) {
            return Mathf.Pow (2f, 6f * (t - 1f)) * Mathf.Abs (Mathf.Sin (t * Mathf.PI * 3.5f));
        }

        public static float EaseOutBounce (float t) {
            return 1 - Mathf.Pow (2f, -6f * t) * Mathf.Abs (Mathf.Cos (t * Mathf.PI * 3.5f));
        }

        public static float EaseInOutBounce (float t) {
            if (t < 0.5f) {
                return 8f * Mathf.Pow (2f, 8f * (t - 1f)) * Mathf.Abs (Mathf.Sin (t * Mathf.PI * 7f));
            } else {
                return 1f - 8f * Mathf.Pow (2f, -8f * t) * Mathf.Abs (Mathf.Sin (t * Mathf.PI * 7f));
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DouduckGame.Math {
    public class EasingCubicBezier {

        private const float ACCURACY = 0.001f;

        private Vector2 m_point1;
        private Vector2 m_point2;

        private Vector2 m_coefficientA;
        private Vector2 m_coefficientB;
        private Vector2 m_coefficientC;

        public EasingCubicBezier () {
            SetCoefficient (new Vector2 (0.5f, 0.5f), new Vector2 (0.5f, 0.5f));
        }

        public EasingCubicBezier (Vector2 point1, Vector2 point2) {
            SetCoefficient (point1, point2);
        }

        public void SetCoefficient (Vector2 point1, Vector2 point2) {
            m_coefficientC = 3f * point1;
            m_coefficientB = 3f * (point2 - point1) - m_coefficientC;
            m_coefficientA = Vector2.one - m_coefficientC - m_coefficientB;
        }

        public Vector2 PointOnCubicBezier (float t) {
            float tSquared = t * t, tCubed = tSquared * t;
            return m_coefficientA * tCubed + m_coefficientB * tSquared + m_coefficientC * t;
        }

        public float XOnCubicBezier (float t) {
            float tSquared = t * t, tCubed = tSquared * t;
            return m_coefficientA.x * tCubed + m_coefficientB.x * tSquared + m_coefficientC.x * t;
        }

        public float YOnCubicBezier (float t) {
            float tSquared = t * t, tCubed = tSquared * t;
            return m_coefficientA.y * tCubed + m_coefficientB.y * tSquared + m_coefficientC.y * t;
        }

        public float Ease (float x) {
            float left = 0f, right = 1f, center = 0.5f;;
            while (right > left + ACCURACY) {
                center = (left + right) / 2f;
                if (x > XOnCubicBezier (center)) {
                    left = center;
                } else {
                    right = center;
                }
            }
            return YOnCubicBezier (center);
        }
    }
}
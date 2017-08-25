using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DouduckGame.Math {
    public class CubicBezier2D {
        // TODO: allow more point

        private Vector2 m_startPoint;
        private Vector2 m_endPoint;

        private Vector2 m_point1;
        private Vector2 m_point2;

        private Vector2 m_coefficientA;
        private Vector2 m_coefficientB;
        private Vector2 m_coefficientC;

        public void ComputeCoefficient () {
            m_coefficientC = 3f * (m_point1 - m_startPoint);
            m_coefficientB = 3f * (m_point2 - m_point1) - m_coefficientC;
            m_coefficientA = m_endPoint - m_startPoint - m_coefficientC - m_coefficientB;
        }

        public Vector2 PointOnCubicBezier (float t) {
            float tSquared = t * t, tCubed = tSquared * t;
            return m_coefficientA * tCubed + m_coefficientB * tSquared + m_coefficientC * t + m_startPoint;
        }
    }
}
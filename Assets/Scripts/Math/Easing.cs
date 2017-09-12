using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace DouduckGame.Math {
	public delegate float EasingEquation(float t);

	[System.Serializable]
	public partial class Easing {

		[SerializeField]
		private EasingType m_settedType = EasingType.Linear;
        private EasingType m_workingType;
        private EasingEquation m_equation = null;

        [SerializeField]
        private Vector2 m_bezierPoint1;
        [SerializeField]
        private Vector2 m_bezierPoint2;
        private EasingCubicBezier m_easingCubicBezier = new EasingCubicBezier ();

        public EasingType easingType {
            get {
                return m_settedType;
            }
            set {
                m_settedType = value;
                SetUpEquation (m_settedType);
            }
        }
        public bool isReady {
            get {
                return m_settedType == m_workingType;
            }
            set {
                if (value == true) {
                    SetUpEquation (m_settedType);
                }
            }
        }

        public Easing () {
            SetUpEquation (m_settedType);
        }

        public Easing (EasingType type) {
            m_settedType = type;
            SetUpEquation (m_settedType);
        }

        public float Ease (float t) {
            if (!isReady) {
                SetUpEquation (m_settedType);
            }
			return m_equation (t);
		}

        public void SetBezierPoint (Vector2 point1, Vector2 point2) {
            m_bezierPoint1 = point1;
            m_bezierPoint2 = point2;
            m_easingCubicBezier.SetCoefficient (m_bezierPoint1, m_bezierPoint2);
        }

        private void SetUpEquation (EasingType type) {
            m_workingType = type;
            if (type == EasingType.CubicBezier) {
                m_easingCubicBezier.SetCoefficient (m_bezierPoint1, m_bezierPoint2);
                m_equation = m_easingCubicBezier.Ease;
            } else {
                m_equation = GetEquation (type);
            }
        }
	}
}

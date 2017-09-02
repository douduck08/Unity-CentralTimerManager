using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace DouduckGame.Math {
	public delegate float EasingEquation(float t);

	[System.Serializable]
	public partial class Easing {

		[SerializeField]
		private EasingType m_easingType = EasingType.Linear;
		public EasingType easingType { 
			get {
				return m_easingType;
			}
			set {
				m_easingType = value;
				SetUpEquation ();
			} 
		}

		private EasingEquation m_equation = null;

		public Easing (EasingType type) {
			easingType = type;
		}

		public float Ease (float t) {
			if (m_equation == null) {
				SetUpEquation ();
			}
			return m_equation (t);
		}

		private void SetUpEquation () {
			m_equation = GetEquation (m_easingType);
		}
	}
}

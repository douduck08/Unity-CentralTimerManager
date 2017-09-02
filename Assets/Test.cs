using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DouduckGame.Math;

public class Test : MonoBehaviour {

	public Easing m_easingEquation = new Easing(EasingType.Linear);

    [SerializeField]
    private int m_pointNumber;
    [SerializeField]
    private int m_scale;

    void OnDrawGizmos () {
        float deltaX_ = 1f / m_pointNumber;
        for (int i = 0; i < m_pointNumber; i++) {
            float x1 = deltaX_ * i;
            float x2 = deltaX_ * (i + 1);
			Gizmos.color = Color.magenta;
			Gizmos.DrawLine (new Vector2 (x1, m_easingEquation.Ease (x1)) * m_scale, new Vector2 (x2, m_easingEquation.Ease (x2)) * m_scale);

            Gizmos.color = Color.red;
            Gizmos.DrawLine (new Vector2 (x1, Easing.EaseInBounce (x1)) * m_scale, new Vector2 (x2, Easing.EaseInBounce (x2)) * m_scale);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine (new Vector2 (x1, Easing.EaseOutBounce (x1)) * m_scale, new Vector2 (x2, Easing.EaseOutBounce (x2)) * m_scale);
            Gizmos.color = Color.green;
            Gizmos.DrawLine (new Vector2 (x1, Easing.EaseInOutBounce (x1)) * m_scale, new Vector2 (x2, Easing.EaseInOutBounce (x2)) * m_scale);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine (new Vector2 (x1, Easing.EaseOutInBounce (x1)) * m_scale, new Vector2 (x2, Easing.EaseOutInBounce (x2)) * m_scale);
        }
    }
}

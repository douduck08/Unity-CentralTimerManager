using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using DouduckGame.Math;

[CustomPropertyDrawer (typeof (Easing))]
public class EasingEquationEditor : PropertyDrawer {

	private readonly float curveFieldHeight = 150f;
	private readonly int pointNumber = 40;
	private EasingEquation m_equation;

	public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
		return base.GetPropertyHeight (property, label);
	}

	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
		label = EditorGUI.BeginProperty (position, label, property);

		EditorGUI.BeginChangeCheck ();
		SerializedProperty typeProperty_ = property.FindPropertyRelative ("m_easingType");
		EditorGUI.PropertyField (position, typeProperty_, new GUIContent ("Easing Type"));
		if (EditorGUI.EndChangeCheck ()) {
			SetUpEquation (typeProperty_.enumValueIndex);
		}

		property.isExpanded = EditorGUI.Foldout (position, property.isExpanded, label);
		if (property.isExpanded) {
			Rect contentPosition = EditorGUI.IndentedRect (position);
			contentPosition.y += 18f;
			contentPosition.height = curveFieldHeight;
			DrawCurveField (contentPosition);
		}

		EditorGUI.EndProperty();
	}

	private void SetUpEquation (int enumIdx) {
		m_equation = Easing.GetEquation ((EasingType)(property.FindPropertyRelative ("m_easingType").enumValueIndex));
	}

	private void DrawCurveField (Rect position) {
		if (m_equation == null) {

		}

		position.x = (position.width - curveFieldHeight) / 2f;
		position.width = curveFieldHeight;
		EditorGUI.DrawRect (position, Color.gray);

		float deltaX_ = 1f / pointNumber;
		for (int i = 0; i < pointNumber; i++) {
			float x1 = deltaX_ * i;
			float x2 = deltaX_ * (i + 1);
			Vector2 point1 = new Vector2 (x1, m_equation (x1));
			Vector2 point2 = new Vector2 (x2, m_equation (x2));

			point1 *= curveFieldHeight;
			point1.x += position.x;
			point1.y = position.y + curveFieldHeight - point1.y;
			point2 *= curveFieldHeight;
			point2.x += position.x;
			point2.y = position.y + curveFieldHeight - point2.y;

			Handles.DrawLine (point1, point2);
		}
	}
}

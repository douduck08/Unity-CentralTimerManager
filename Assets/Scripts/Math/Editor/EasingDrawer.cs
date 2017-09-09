using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;
using DouduckGame.Math;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer (typeof (Easing))]
public class EasingDrawer : PropertyDrawer {

    private readonly float curveFieldHeight = 150f;
    private readonly int pointNumber = 50;

    private Easing m_target = null;
    private Easing GetTarget (SerializedProperty property) {
        if (m_target == null) {
            m_target = property.GetValue<Easing> ();
        }
        Debug.Log("m_target = " + m_target);
        return m_target;
    }

    private T GetValue<T> (SerializedProperty property) where T : class {
        FieldInfo field = property.serializedObject.targetObject.GetType ().GetField (property.propertyPath);
        if (field != null) {
            Debug.Log ("Sucess: " + property.propertyPath);
            var value = field.GetValue (property.serializedObject.targetObject);
            if (value.GetType ().IsArray) {
                var index = Convert.ToInt32 (new string (property.propertyPath.Where (c => char.IsDigit (c)).ToArray ()));
                return ((T[]) value)[index];
            } else {
                return (T) value;
            }
        }
        Debug.Log ("Failed: " + property.propertyPath);
        return null;
    }

    public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
        float baseHeight_ = base.GetPropertyHeight (property, label);
        return property.isExpanded ? curveFieldHeight + baseHeight_ : baseHeight_;
    }

    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
        label = EditorGUI.BeginProperty (position, label, property);

        EditorGUI.BeginChangeCheck ();
        EditorGUI.PropertyField (position, property.FindPropertyRelative ("m_settedType"), label);
        if (EditorGUI.EndChangeCheck ()) {
            GetTarget (property);
            //GetTarget(property).easingType = (EasingType)(property.FindPropertyRelative ("m_settedType").enumValueIndex);
        }

        property.isExpanded = EditorGUI.Foldout (position, property.isExpanded, label);
        if (property.isExpanded) {
            Rect contentPosition = EditorGUI.IndentedRect (position);
            contentPosition.y += 18f;
            contentPosition.height = curveFieldHeight;
            //    DrawCurveField (contentPosition, property);
        }

        EditorGUI.EndProperty ();
    }

    private void DrawCurveField (Rect position, SerializedProperty property) {
        if (m_target == null) {
            GetTarget (property);
        }

        position.x = (position.width - curveFieldHeight) / 2f;
        position.width = curveFieldHeight;
        EditorGUI.DrawRect (position, Color.gray);

        float deltaX_ = 1f / pointNumber;
        for (int i = 0; i < pointNumber; i++) {
            float x1 = deltaX_ * i;
            float x2 = deltaX_ * (i + 1);
            Vector2 point1 = new Vector2 (x1, m_target.Ease (x1));
            Vector2 point2 = new Vector2 (x2, m_target.Ease (x2));

            point1 *= curveFieldHeight;
            point1.x += position.x;
            point1.y = position.y + curveFieldHeight - point1.y;
            point2 *= curveFieldHeight;
            point2.x += position.x;
            point2.y = position.y + curveFieldHeight - point2.y;

            Handles.color = Color.red;
            Handles.DrawLine (point1, point2);
        }
    }
}
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DouduckGame.Math;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer (typeof (Easing))]
public class EasingDrawer : PropertyDrawer {

    private readonly float curveFieldHeight = 150f;
    private readonly int pointNumber = 50;

    public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
        float baseHeight_ = base.GetPropertyHeight (property, label);
        return property.isExpanded ? 36f + curveFieldHeight + baseHeight_ : baseHeight_;
    }

    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
        label = EditorGUI.BeginProperty (position, label, property);
        Rect contentPosition = position;
        contentPosition.height = 18f;

        EditorGUI.BeginChangeCheck ();
        EditorGUI.PropertyField (contentPosition, property.FindPropertyRelative ("m_settedType"), label);
        if (EditorGUI.EndChangeCheck ()) {
            property.GetValue<Easing> ().easingType = (EasingType) (property.FindPropertyRelative ("m_settedType").enumValueIndex);
        }

        property.isExpanded = EditorGUI.Foldout (contentPosition, property.isExpanded, label);
        if (property.isExpanded) {
            GUI.enabled = property.GetValue<Easing> ().easingType == EasingType.CubicBezier;
            EditorGUI.BeginChangeCheck ();
            contentPosition.y += 18f;
            EditorGUI.PropertyField (contentPosition, property.FindPropertyRelative ("m_bezierPoint1"), new GUIContent ("Bezier Point1"));
            contentPosition.y += 18f;
            EditorGUI.PropertyField (contentPosition, property.FindPropertyRelative ("m_bezierPoint2"), new GUIContent ("Bezier Point2"));
            if (EditorGUI.EndChangeCheck ()) {
                property.GetValue<Easing> ().SetBezierPoint(property.FindPropertyRelative ("m_bezierPoint1").vector2Value, property.FindPropertyRelative ("m_bezierPoint2").vector2Value);
            }
            GUI.enabled = true;

            contentPosition = EditorGUI.IndentedRect (contentPosition);
            contentPosition.y += 18f;
            contentPosition.height = curveFieldHeight;
            DrawCurveField (contentPosition, property);
        }

        EditorGUI.EndProperty ();
    }

    private void DrawCurveField (Rect position, SerializedProperty property) {
        Easing target = property.GetValue<Easing> ();

        position.x = (position.width - curveFieldHeight) / 2f;
        position.width = curveFieldHeight;
        EditorGUI.DrawRect (position, Color.gray);

        float deltaX_ = 1f / pointNumber;
        for (int i = 0; i < pointNumber; i++) {
            float x1 = deltaX_ * i;
            float x2 = deltaX_ * (i + 1);
            Vector2 point1 = new Vector2 (x1, target.Ease (x1));
            Vector2 point2 = new Vector2 (x2, target.Ease (x2));

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
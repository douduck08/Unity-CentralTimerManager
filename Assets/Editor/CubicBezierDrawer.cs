using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DouduckGame.Math {

    [CustomPropertyDrawer (typeof (CubicBezierEase))]
    public class CubicBezierDrawer : PropertyDrawer {

        private readonly float bezierFieldHeight = 150f;

        public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
            return property.isExpanded ? bezierFieldHeight + 54f : 18f;
        }

        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
            Rect prefixLabelPosition = position;
            prefixLabelPosition.height = 18f;
            label = EditorGUI.BeginProperty (prefixLabelPosition, label, property);

            property.isExpanded = EditorGUI.Foldout (prefixLabelPosition, property.isExpanded, label);
            if (property.isExpanded) {
                Rect contentPosition = EditorGUI.IndentedRect (prefixLabelPosition);
                contentPosition.y += 18f;
                EditorGUI.PropertyField (contentPosition, property.FindPropertyRelative ("m_point1"), new GUIContent ("Start Tangent"));
                contentPosition.y += 18f;
                EditorGUI.PropertyField (contentPosition, property.FindPropertyRelative ("m_point2"), new GUIContent ("End Tangent"));
                contentPosition.y += 18f;
                contentPosition.height = bezierFieldHeight;
                DrawBezierField (contentPosition, property);
            }

            EditorGUI.EndProperty ();
        }

        private void DrawBezierField (Rect position, SerializedProperty property) {
            position.x = (position.width - bezierFieldHeight) / 2f;
            position.width = bezierFieldHeight;
            EditorGUI.DrawRect (position, Color.gray);

            Vector2[] points = new Vector2[4];
            points[0] = Vector2.zero;
            points[1] = Vector2.one;
            points[2] = property.FindPropertyRelative ("m_point1").vector2Value;
            points[3] = property.FindPropertyRelative ("m_point2").vector2Value;
            for (int i = 0; i < 4; i++) {
                points[i] *= bezierFieldHeight;
                points[i].x += position.x;
                points[i].y = position.y + bezierFieldHeight - points[i].y;
            }

            Handles.DrawBezier (points[0], points[1], points[2], points[3], Color.black, null, 3);
        }
    }
}
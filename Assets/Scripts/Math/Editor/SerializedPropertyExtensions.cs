using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

// Ref: http://answers.unity3d.com/questions/627090/convert-serializedproperty-to-custom-class.html
public static class SerializedPropertyExtensions {

    public static T GetValue<T> (this SerializedProperty property) {
        object obj = property.serializedObject.targetObject;
        string path = property.propertyPath.Replace (".Array.data", "");
        string[] fieldStructure = path.Split ('.');
        Regex rgx = new Regex (@"\[\d+\]");
        for (int i = 0; i < fieldStructure.Length; i++) {
            if (fieldStructure[i].Contains ("[")) {
                int index = System.Convert.ToInt32 (new string (fieldStructure[i].Where (c => char.IsDigit (c)).ToArray ()));
                obj = GetFieldWithIndex (rgx.Replace (fieldStructure[i], ""), obj, index);
            } else {
                obj = GetFieldOrPropertyValue (fieldStructure[i], obj);
            }
        }
        return (T) obj;
    }

    public static bool SetValue<T> (this SerializedProperty property, object value) {
        object obj = property.serializedObject.targetObject;
        string path = property.propertyPath.Replace (".Array.data", "");
        string[] fieldStructure = path.Split ('.');
        Regex rgx = new Regex (@"\[\d+\]");
        for (int i = 0; i < fieldStructure.Length - 1; i++) {
            if (fieldStructure[i].Contains ("[")) {
                int index = System.Convert.ToInt32 (new string (fieldStructure[i].Where (c => char.IsDigit (c)).ToArray ()));
                obj = GetFieldValueWithIndex (rgx.Replace (fieldStructure[i], ""), obj, index);
            } else {
                obj = GetFieldOrPropertyValue (fieldStructure[i], obj);
            }
        }

        string fieldName = fieldStructure.Last ();
        if (fieldStructure[i].Contains ("[")) {
            int index = System.Convert.ToInt32 (new string (fieldStructure[i].Where (c => char.IsDigit (c)).ToArray ()));
            return SetFieldValueWithIndex (fieldName, obj, value);
        } else {
            return SetFieldOrPropertyValue (fieldName, obj, value);
        }
    }

    private static object GetFieldOrPropertyValue (string fieldName, object obj, BindingFlags bindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic) {
        FieldInfo field = obj.GetType ().GetField (fieldName, bindings);
        if (field != null) {
            return field.GetValue (obj);
        }
        PropertyInfo property = obj.GetType ().GetProperty (fieldName, bindings);
        if (property != null) {
            return property.GetValue (obj, null);
        }
        return default (object);
    }

    private static object GetFieldValueWithIndex (string fieldName, object obj, int index, BindingFlags bindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic) {
        FieldInfo field = obj.GetType ().GetField (fieldName, bindings);
        if (field != null) {
            object value = field.GetValue (obj);
            IEnumerable enumerable = value as IEnumerable;
            if (enumerable != null) {
                IEnumerator enumerator = enumerable.GetEnumerator ();
                while (index-- >= 0) {
                    enumerator.MoveNext ();
                }
                return enumerator.Current;
            } else {
                return value;
            }
        }
        return default (object);
    }

    public static bool SetFieldOrPropertyValue (string fieldName, object obj, object value, bool includeAllBases = false, BindingFlags bindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic) {
        FieldInfo field = obj.GetType ().GetField (fieldName, bindings);
        if (field != null) {
            field.SetValue (obj, value);
            return true;
        }

        PropertyInfo property = obj.GetType ().GetProperty (fieldName, bindings);
        if (property != null) {
            property.SetValue (obj, value, null);
            return true;
        }
        return false;
    }

    public static bool SetFieldValueWithIndex (string fieldName, object obj, object value, bool includeAllBases = false, BindingFlags bindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic) {
        FieldInfo field = obj.GetType ().GetField (fieldName, bindings);
        if (field != null) {
            object value = field.GetValue (obj);
            IEnumerable enumerable = value as IEnumerable;
            if (enumerable != null) {
                IEnumerator enumerator = enumerable.GetEnumerator ();
                while (index-- >= 0) {
                    enumerator.MoveNext ();
                }
                return enumerator.Current;
            } else {
                return value;
            }
        }
        return false;
    }
}
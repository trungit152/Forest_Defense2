using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ConditionalFieldAttribute))]
public class ConditionalFieldDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ConditionalFieldAttribute conditional = (ConditionalFieldAttribute)attribute;
        SerializedProperty conditionProperty = property.serializedObject.FindProperty(conditional.ConditionField);

        if (conditionProperty != null && conditionProperty.enumValueIndex == 0) 
        {
            if (property.name == "_turretObject")
                EditorGUI.PropertyField(position, property, label);
            if (property.name == "_attackRange")
                EditorGUI.PropertyField(position, property, label);
        }
        else if (conditionProperty != null && conditionProperty.enumValueIndex == 1) 
        {
            if (property.name == "_trapsObject")
                EditorGUI.PropertyField(position, property, label);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }
}

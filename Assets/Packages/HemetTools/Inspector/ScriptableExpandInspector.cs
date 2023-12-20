using System.Linq;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(MonoBehaviour), true)]
public class MonoBehaviourEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		MonoBehaviour script = (MonoBehaviour)target;

		// Procura todos os campos do tipo ScriptableObject
		var scriptableObjectFields = script.GetType()
			.GetFields()
			.Where(f => f.FieldType.IsSubclassOf(typeof(ScriptableObject)));

		foreach (var field in scriptableObjectFields)
		{
			ScriptableObject scriptableObject = (ScriptableObject)field.GetValue(script);

			if (scriptableObject != null)
			{
				EditorGUILayout.Space();
				EditorGUILayout.LabelField($"{field.Name} Properties", EditorStyles.boldLabel);
				SerializedObject serializedObject = new SerializedObject(scriptableObject);
				EditorGUI.indentLevel++;
				SerializedProperty property = serializedObject.GetIterator();
				bool enterChildren = true;
				while (property.NextVisible(enterChildren))
				{
					EditorGUILayout.PropertyField(property, true);
					enterChildren = false;
				}
				EditorGUI.indentLevel--;
				serializedObject.ApplyModifiedProperties();
			}
		}
	}
}
#endif
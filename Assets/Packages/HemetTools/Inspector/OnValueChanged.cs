using UnityEditor;
using UnityEngine;

public class OnValueChangedAttribute : PropertyAttribute
{
	public string CallbackMethodName { get; }

	public OnValueChangedAttribute(string callbackMethodName)
	{
		CallbackMethodName = callbackMethodName;
	}
}

#if UNITY_EDITOR
[UnityEditor.CustomPropertyDrawer(typeof(OnValueChangedAttribute))]
public class OnValueChangedDrawer : UnityEditor.PropertyDrawer
{
	public override void OnGUI(Rect position, UnityEditor.SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginChangeCheck();
		EditorGUI.PropertyField(position, property, label, true);
		if (EditorGUI.EndChangeCheck())
		{
			OnValueChangedAttribute valueChangedAttribute = (OnValueChangedAttribute)attribute;
			MonoBehaviour target = property.serializedObject.targetObject as MonoBehaviour;

			if (target != null)
			{
				System.Reflection.MethodInfo method = target.GetType().GetMethod(valueChangedAttribute.CallbackMethodName,
					System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);

				if (method != null)
				{
					method.Invoke(target, null);
				}
				else
				{
					Debug.LogWarning($"Method {valueChangedAttribute.CallbackMethodName} not found on {target.GetType()}.");
				}
			}
		}
	}
}
#endif

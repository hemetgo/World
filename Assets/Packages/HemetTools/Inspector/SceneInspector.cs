using UnityEditor;
using UnityEngine;

[System.Serializable]
public class SceneAttribute : PropertyAttribute { }

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(SceneAttribute))]
public class SceneDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		if (property.propertyType == SerializedPropertyType.String)
		{
			EditorGUI.BeginProperty(position, label, property);

			// Obtém a lista de cenas
			string[] sceneNames = GetSceneNames();

			// Verifica se a cena atual está na lista de cenas
			int selectedIndex = Mathf.Max(0, System.Array.IndexOf(sceneNames, property.stringValue));

			// Exibe uma caixa de combinação no Inspector
			selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, sceneNames);

			// Atualiza o valor da propriedade com a cena selecionada
			property.stringValue = sceneNames[selectedIndex];

			EditorGUI.EndProperty();
		}
		else
		{
			EditorGUI.LabelField(position, label.text, "Use [Scene] with string.");
		}
	}

	// Obtém os nomes de todas as cenas no projeto
	private string[] GetSceneNames()
	{
		int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
		string[] sceneNames = new string[sceneCount];

		for (int i = 0; i < sceneCount; i++)
		{
			sceneNames[i] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
		}

		return sceneNames;
	}
}
#endif

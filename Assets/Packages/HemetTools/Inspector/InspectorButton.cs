using UnityEditor;
using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace HemetTools.Inspector
{
#if UNITY_EDITOR
	[CustomEditor(typeof(MonoBehaviour), true)]
	public class ButtonDrawer : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			MonoBehaviour script = target as MonoBehaviour;

			// Obtém todas as funções da classe marcadas com [Button]
			MethodInfo[] methods = script.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

			foreach (var method in methods)
			{
				if (Attribute.IsDefined(method, typeof(ButtonAttribute)))
				{
					// Cria um botão para cada função marcada com [Button]
					if (GUILayout.Button(method.Name))
					{
						method.Invoke(script, null);
					}
				}
			}
		}
	}

	[AttributeUsage(AttributeTargets.Method)]
	public class ButtonAttribute : Attribute
	{
		// Pode adicionar parâmetros adicionais aqui, se necessário
	}
#endif

}

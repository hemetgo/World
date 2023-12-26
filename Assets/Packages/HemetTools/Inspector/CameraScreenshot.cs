using HemetTools.Inspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public class CameraScreenshot : MonoBehaviour
{
	[SerializeField] Camera captureCamera;

	[Button]
	public void CaptureScreenshot()
	{
		if (captureCamera == null)
		{
			Debug.LogError("Null camera");
			return;
		}

		// Configura a câmera para renderizar com canal alfa
		captureCamera.clearFlags = CameraClearFlags.SolidColor;
		captureCamera.backgroundColor = new Color(0, 0, 0, 0); // Cor transparente
		captureCamera.targetTexture = new RenderTexture(256, 256, 24, RenderTextureFormat.ARGB32);
		Texture2D screenshot = new Texture2D(256, 256, TextureFormat.ARGB32, false);

		// Captura a renderização da câmera
		captureCamera.Render();
		RenderTexture.active = captureCamera.targetTexture;
		screenshot.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
		captureCamera.targetTexture = null;
		RenderTexture.active = null;
		Destroy(captureCamera.targetTexture);

		// Converte a textura para um array de bytes PNG
		byte[] bytes = screenshot.EncodeToPNG();
		Destroy(screenshot);

		// Pede ao usuário que escolha o local e o nome do arquivo
		string defaultFilename = "Screenshot.png";
		string directory = Application.dataPath;
		string filename = EditorUtility.SaveFilePanel("Salvar Screenshot", directory, defaultFilename, "png");

		if (filename.Length == 0)
		{
			Debug.Log("Operação de salvar screenshot cancelada pelo usuário.");
			return;
		}

		// Salva o screenshot como um arquivo PNG
		System.IO.File.WriteAllBytes(filename, bytes);

		AssetDatabase.Refresh();

		Debug.Log("Screenshot salvo como: " + filename);
	}
}


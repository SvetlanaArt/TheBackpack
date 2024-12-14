using UnityEditor;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    [SerializeField] Camera cameraShot;
    [SerializeField] string savePath;
    [SerializeField] string name;

    [ContextMenu ("Make screenshot")]
    void MakeScreenshot()
    {
        RenderTexture renderTexture = new RenderTexture(256, 256, 24);
        cameraShot.targetTexture = renderTexture;
        Texture2D screenShot = new Texture2D(256, 256, TextureFormat.RGBA32, false);
        cameraShot.Render();
        RenderTexture.active = renderTexture;
        screenShot.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
        cameraShot.targetTexture = null;
        RenderTexture.active = null;

        if (Application.isEditor)
        {
            DestroyImmediate(renderTexture);
        }
        else
        {
            Destroy(renderTexture);
        }

        byte[] bytes = screenShot.EncodeToPNG();
#if UNITY_EDITOR
        string fullPath = AssetDatabase.GenerateUniqueAssetPath($"{savePath}/{name}.png");
        System.IO.File.WriteAllBytes(fullPath, bytes);
        AssetDatabase.Refresh();
#endif
    }
}


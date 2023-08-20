using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Camera mapCamera; // Assign your orthographic camera in the Inspector
    public RenderTexture renderTexture;
    public string savePath = "Assets/Maps/GeneratedMap.png";

    void Start()
    {
        // Render the scene to the render texture
        mapCamera.targetTexture = renderTexture;
        mapCamera.Render();

        // Capture the render texture into a Texture2D
        RenderTexture.active = renderTexture;
        Texture2D capturedTexture = new Texture2D(renderTexture.width, renderTexture.height);
        capturedTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        capturedTexture.Apply();
        RenderTexture.active = null;

        // Save the captured texture as an image
        byte[] bytes = capturedTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes(savePath, bytes);
    }
}

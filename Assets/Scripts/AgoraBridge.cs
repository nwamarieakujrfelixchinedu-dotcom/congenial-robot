using UnityEngine;

// Placeholder/integration notes for sending Unity RenderTexture frames to Agora.
// Use the Agora Unity SDK's external video source APIs to push frames.
// This script shows the pattern: capture a RenderTexture, read pixels into Texture2D (or use GPU -> CPU path),
// convert to byte[] and call Agora's SDK push-frame API.
//
// IMPORTANT: Refer to Agora Unity docs for exact API calls for your chosen SDK version.
// The code below is illustrative and will need to be adapted to the Agora API you install.

public class AgoraBridge : MonoBehaviour
{
    public Camera renderCamera;
    public int width = 640;
    public int height = 480;
    private RenderTexture rt;
    private Texture2D tex2D;

    void Start()
    {
        rt = new RenderTexture(width, height, 24);
        renderCamera.targetTexture = rt;
        tex2D = new Texture2D(width, height, TextureFormat.RGB24, false);
        // TODO: Initialize Agora SDK, enable external video source
        // Example steps:
        // - Initialize Agora engine with your App ID
        // - Enable external video source
        // - Join channel
    }

    void Update()
    {
        // Capture the camera output into a Texture2D
        RenderTexture.active = rt;
        tex2D.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex2D.Apply();
        RenderTexture.active = null;

        // Get raw bytes (RGB24)
        byte[] bytes = tex2D.GetRawTextureData();

        // TODO: convert bytes into the format Agora expects, then push frame via SDK
        // Example pseudo:
        // AgoraEngine.PushVideoFrame(bytes, width, height, PixelFormat.RGB24, timestamp);
    }

    void OnDestroy()
    {
        if (renderCamera != null) renderCamera.targetTexture = null;
        if (rt != null) rt.Release();
    }
}

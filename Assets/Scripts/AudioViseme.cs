using UnityEngine;

// Simple audio -> mouth blendshape controller.
// Attach to the avatar GameObject that has a SkinnedMeshRenderer with mouth blendshape index.
public class AudioViseme : MonoBehaviour
{
    public string microphoneDevice = null; // null = default mic
    public int sampleRate = 16000;
    public int bufferLengthSec = 1;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public int mouthBlendShapeIndex = 0; // set to the blendshape index for mouth open
    [Range(0f, 10f)] public float sensitivity = 1.0f;
    [Range(0f, 100f)] public float maxWeight = 100f;

    private AudioClip micClip;
    private float[] sampleBuffer = new float[1024];
    private int lastPosition = 0;

    void Start()
    {
        if (Microphone.devices.Length > 0)
        {
            microphoneDevice = Microphone.devices[0];
            micClip = Microphone.Start(microphoneDevice, true, bufferLengthSec, sampleRate);
            while (Microphone.GetPosition(microphoneDevice) <= 0) { } // wait until recording starts
            Debug.Log("Microphone started: " + microphoneDevice);
        }
        else
        {
            Debug.LogError("No microphone found.");
        }
    }

    void Update()
    {
        if (micClip == null || skinnedMeshRenderer == null) return;

        int micPos = Microphone.GetPosition(microphoneDevice);
        int diff = micPos - lastPosition;
        if (diff < 0) diff += micClip.samples;

        // Read a short window from the mic (up to our buffer length)
        int readSamples = Mathf.Min(diff, sampleBuffer.Length);
        if (readSamples <= 0) return;

        micClip.GetData(sampleBuffer, Mathf.Max(0, micPos - readSamples));

        // Compute RMS energy
        float sum = 0f;
        for (int i = 0; i < readSamples; i++)
        {
            float s = sampleBuffer[i];
            sum += s * s;
        }
        float rms = Mathf.Sqrt(sum / Mathf.Max(1, readSamples));

        // Map RMS to blendshape weight
        float weight = Mathf.Clamp(rms * sensitivity * 100f, 0f, maxWeight);

        // Apply smoothing
        float current = skinnedMeshRenderer.GetBlendShapeWeight(mouthBlendShapeIndex);
        float smoothed = Mathf.Lerp(current, weight, Time.deltaTime * 20f);
        skinnedMeshRenderer.SetBlendShapeWeight(mouthBlendShapeIndex, smoothed);

        lastPosition = micPos;
    }

    void OnDisable()
    {
        if (Microphone.IsRecording(microphoneDevice))
        {
            Microphone.End(microphoneDevice);
        }
    }
}

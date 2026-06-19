using UnityEngine;

// Simple controller to expose avatar parameters (eye blink, head tilt) that can be driven by other inputs.
public class AvatarController : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMesh;
    public int leftEyeBlinkBlend = 1;
    public int rightEyeBlinkBlend = 2;

    // Call these from other modules (e.g., camera tracking) to animate eyes/head
    public void SetLeftEyeBlink(float value) => skinnedMesh.SetBlendShapeWeight(leftEyeBlinkBlend, Mathf.Clamp01(value) * 100f);
    public void SetRightEyeBlink(float value) => skinnedMesh.SetBlendShapeWeight(rightEyeBlinkBlend, Mathf.Clamp01(value) * 100f);
}

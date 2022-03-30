using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EffectsManager : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] GameObject GlobalVolume;
    [SerializeField] Rigidbody player;
    [SerializeField] GameObject mesh;

    [Header("Settings")]
    [SerializeField] float speedCap = 50f;

    private Material material;
    private Volume volume;
    private float velRatio = 0f;
    private float glowPower = 0f;

    private void Awake()
    {
        volume = GlobalVolume.GetComponent<Volume>();
        material = mesh.GetComponent<SkinnedMeshRenderer>().sharedMaterial;
    }
    private void Update()
    {
        SpeedEffects();
        GetherData();
        ShaderGlow();
    }

    void ShaderGlow()
    {
        material.SetFloat("_GlowChange", glowPower);
    }

    void GetherData()
    {
        velRatio = player.velocity.z / speedCap;
        glowPower = player.velocity.z * 0.125f;

        velRatio = Mathf.Clamp(velRatio, 0f, 1f);
        glowPower = Mathf.Clamp(glowPower, 0f, 10f);
    }

    void SpeedEffects()
    {
        if (volume.profile.TryGet<MotionBlur>(out var blur))
        {
            blur.intensity.value = velRatio;
        }
        if (volume.profile.TryGet<ChromaticAberration>(out var ca))
        {
            ca.intensity.value = velRatio;
        }
    }

}

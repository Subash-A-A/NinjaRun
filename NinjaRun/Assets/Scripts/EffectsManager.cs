using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EffectsManager : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] GameObject GlobalVolume;
    [SerializeField] Rigidbody player;
    [SerializeField] GameObject mesh;
    [SerializeField] ParticleSystem arc;
    [SerializeField] float arcStart = 0.8f;

    [Header("Settings")]
    [SerializeField] float speedCap = 50f;

    private Material material;
    private Volume volume;

    [HideInInspector]
    public float velRatio = 0f;
    private float glowPower = 0f;
    private bool startedPlaying = false;

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

        if (velRatio >= arcStart && !startedPlaying)
        {
            arc.Play();
            startedPlaying = true;
        }
        else if (velRatio < arcStart)
        {
            arc.Stop();
        }
    }

    void ShaderGlow()
    {
        material.SetFloat("_GlowChange", glowPower);
    }

    void GetherData()
    {
        velRatio = player.velocity.z / speedCap;
        glowPower = player.velocity.z * 0.1f;

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

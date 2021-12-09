using UnityEngine;

using UnityEngine.Rendering;

using UnityEngine.Rendering.HighDefinition;

using System;

[Serializable, VolumeComponentMenu("Post-processing/Custom/Circle")]

public sealed class Circle : CustomPostProcessVolumeComponent, IPostProcessComponent

{

    [Tooltip("Controls the intensity of the effect.")]

    public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f);

    Material m_Material;

    public bool IsActive() => m_Material != null && intensity.value > 0f;

    public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

    public override void Setup()

    {

        if (Shader.Find("Hidden/Shader/Circle") != null)

            m_Material = new Material(Shader.Find("Hidden/Shader/Circle"));

    }

    public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)

    {
        if (m_Material == null)
            return;

        m_Material.SetFloat("_Intensity", intensity.value);

        m_Material.SetTexture("_InputTexture", source);

        HDUtils.DrawFullScreen(cmd, m_Material, destination);

    }


    public override void Cleanup() => CoreUtils.Destroy(m_Material);

    public void setIntensity(float value) {
        ClampedFloatParameter tmp = new ClampedFloatParameter(value, 0f, 1f);
        intensity.SetValue(tmp);
    }
}
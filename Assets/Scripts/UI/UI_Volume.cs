using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class UI_Volume : MonoBehaviour
{
    private Vignette vignette;

    private void Awake()
    {
        var volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);
        GameManager.Instance.OnHPChanged += VignetteIntensity;
        
    }

    public void VignetteIntensity(int currentHP)
    {
        float endIntensity = 0.3f;

        DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, endIntensity, 0.5f)
            .OnComplete(() =>
            {
                DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0f, 0.5f);
            });
    }
}

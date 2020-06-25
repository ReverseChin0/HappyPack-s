using UnityEngine;
using UnityEngine.Rendering;

public class SCR_MenuAndSettings : MonoBehaviour
{
    [SerializeField] RenderPipelineAsset low = default, mid = default, high = default;
    //[SerializeField] AudioSource audioSource = default;

    public void ChangeQuality(int level) {
        switch (level) {
            case 0:
                QualitySettings.SetQualityLevel(0);
                GraphicsSettings.renderPipelineAsset = low;
                break;
            case 1:
                QualitySettings.SetQualityLevel(2);
                GraphicsSettings.renderPipelineAsset = mid;
                break;
            case 2:
                QualitySettings.SetQualityLevel(3);
                GraphicsSettings.renderPipelineAsset = high;
                break;
        }
    }

    public void SetVolume(float _v) {
       //audioSource.volume = _v;
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}

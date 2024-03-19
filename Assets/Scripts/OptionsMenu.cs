using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    private Slider mouseSensitivitySlider;
    private CinemachineVirtualCamera virtualCamera;
    private CinemachinePOV pov;
    private TMP_Dropdown qualityDropdown;
    private TMP_Dropdown resolutionDropdown;
    [SerializeField] GameObject fpsCounter;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private int currentQualityLevel;
    private int currentResolutionIndex;

    private void Awake()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level 1"))
        {
            virtualCamera = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
            mouseSensitivitySlider = GameObject.Find("Mouse sensitivity Slider").GetComponent<Slider>();
            pov = virtualCamera.GetCinemachineComponent<CinemachinePOV>();

            mouseSensitivitySlider.value = pov.m_HorizontalAxis.m_MaxSpeed;
        }

        qualityDropdown = GameObject.Find("Graphics Preset Dropdown").GetComponent<TMP_Dropdown>();
        currentQualityLevel = QualitySettings.GetQualityLevel();
        qualityDropdown.value = currentQualityLevel;

        resolutions = Screen.resolutions;
        resolutionDropdown = GameObject.Find("Resolution dropdown").GetComponent<TMP_Dropdown>();
        resolutionDropdown.ClearOptions();

        // Now filtering based on resolution size only, without refresh rate
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string resolutionOption = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(resolutionOption);
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }

    public void SetQualityLevelDropdown(int index)
    {
        QualitySettings.SetQualityLevel(index, false);
    }

    public void MouseSensitivitySlider()
    {
        pov.m_HorizontalAxis.m_MaxSpeed = mouseSensitivitySlider.value;
        pov.m_VerticalAxis.m_MaxSpeed = mouseSensitivitySlider.value;
    }

    public void FPSCounterToggle(bool toggle)
    {
        fpsCounter.SetActive(toggle);
    }
}

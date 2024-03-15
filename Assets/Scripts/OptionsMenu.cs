using Cinemachine;
using System.Collections;
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
    private float currentRefreshRate;
    private int currentResolutionIndex;



    private void Awake()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level 1"))
        {
            virtualCamera = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
            mouseSensitivitySlider = GameObject.Find("Mouse sensitivity Slider").GetComponent<Slider>();
            pov = virtualCamera.GetCinemachineComponent<CinemachinePOV>();

            mouseSensitivitySlider.value = pov.m_HorizontalAxis.m_MaxSpeed; //Assigne le slider à la sensibilité actuel de la souris
        }

        qualityDropdown = GameObject.Find("Graphics Preset Dropdown").GetComponent<TMP_Dropdown>();
        currentQualityLevel = QualitySettings.GetQualityLevel(); //Assigne l'index de la qualité du jeu (ex: Ultra = 5)
        qualityDropdown.value = currentQualityLevel; //Donne la valeur actuel du preset graphics

        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();
        resolutionDropdown = GameObject.Find("Resolution dropdown").GetComponent<TMP_Dropdown>();
        resolutionDropdown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRate == currentRefreshRate)
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " " + filteredResolutions[i].refreshRate + " Hz";
            options.Add(resolutionOption);
            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
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
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }

    public void SetQualityLevelDropdown(int index)
    {
        QualitySettings.SetQualityLevel(index, false);
    }

    public void MouseSensitivitySlider() //Change la sensibilité de la souris selon le slider dans le menu options
    {
        pov.m_HorizontalAxis.m_MaxSpeed = mouseSensitivitySlider.value; //Change la sensibilité horizontal
        pov.m_VerticalAxis.m_MaxSpeed = mouseSensitivitySlider.value; //Change la sensibilité vertical
    }

    public void FPSCounterToggle(bool toggle)
    {
        if (toggle)
        {
            fpsCounter.SetActive(true);
        }
        if (!toggle)
        {
            fpsCounter.SetActive(false);
        }
    }
}

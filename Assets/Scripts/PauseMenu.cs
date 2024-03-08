using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = false;
    [SerializeField] GameObject PauseMenuUI;
    [SerializeField] GameObject GameUI;
    [SerializeField] GameObject OptionsUI;
    [SerializeField] GameObject QuitUI;

    public void PauseGame() //Met le jeu en pause
    {
        PauseMenuUI.SetActive(true);
        GameUI.SetActive(false);
        Time.timeScale = 0f; //Stop le temps de d�rouler
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void ResumeGame()
    {
        PauseMenuUI.SetActive(false);
        OptionsUI.SetActive(false);
        QuitUI.SetActive(false);
        GameUI.SetActive(true);
        Time.timeScale = 1f; //Remet le temps � �a valeur par d�faut.
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Options() //Active l'interface utilisateur du menu option dans le menu pause ou la d�sactive si elle l'est d�ja
    {
        if (PauseMenuUI.activeInHierarchy)
        {
            PauseMenuUI.SetActive(false);
            OptionsUI.SetActive(true);
        }
        else
        {
            PauseMenuUI.SetActive(true);
            OptionsUI.SetActive(false);
        }
    }

    public void QuitGame() // Active l'interface utilisateur de l'option quit du menu pause ou la d�sactive si elle l'est d�ja
    {
        if (PauseMenuUI.activeInHierarchy)
        {
            PauseMenuUI.SetActive(false);
            QuitUI.SetActive(true);
        }
        else
        {
            PauseMenuUI.SetActive(true);
            QuitUI.SetActive(false);
        }
    }

    //Retourne au menu principal
    public void MainMenu()
    {
        //Utilise la fonction ResumeGame() pour que le jeu ne soit pas coinc� � Time.TimeScale = 0f;
        ResumeGame();
        //Permet de bouger le curseau � l'�cran
        Cursor.lockState = CursorLockMode.Confined;
        //Charge le menu principal
        SceneManager.LoadSceneAsync(0);
    }

    public void Quit() // Sert a quitter l'application
    {
        Application.Quit();
    }

}

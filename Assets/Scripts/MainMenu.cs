using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] GameObject MainMenuUI;
    [SerializeField] GameObject OptionsUI;
    private bool inOptions = false;
    public void buttonStart()
    {
        SceneManager.LoadSceneAsync(1); //Charge la scène ?l'index 1
    }

    public void Options() //Permet d'ouvrir le menu options et sert aussi a quitter lee menu options avec un bouton ?l'écran
    {
        if (inOptions == false)
        {
            MainMenuUI.SetActive(false);
            OptionsUI.SetActive(true);
            inOptions = true;
        }
        else if (inOptions == true)
        {
            MainMenuUI.SetActive(true);
            OptionsUI.SetActive(false);
            inOptions = false;
        }
    }

    public void QuitButton()
    {
        // Quit Game
        Application.Quit();
    }
}

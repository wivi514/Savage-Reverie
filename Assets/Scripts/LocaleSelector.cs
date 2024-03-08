using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour
{
    //Variables
    private bool active = false; // Indique si le changement de langue est en cours

    // Méthode pour changer la langue en fonction de l'ID de la langue
    public void ChangeLocale(int localeID)
    {
        // Vérifie si le changement de langue est déjà en cours, si oui, ne fait rien
        if (active == true)
            return;

        // Démarre la coroutine pour définir la langue
        StartCoroutine(SetLocale(localeID));
    }

    // Coroutine pour définir la langue
    IEnumerator SetLocale(int _localeID)
    {
        // Indique que le changement de langue est en cours
        active = true;

        // Attend que l'initialisation des paramètres de localisation soit terminée
        yield return LocalizationSettings.InitializationOperation;

        // Définit la langue sélectionnée en fonction de l'ID de la langue
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];

        // Indique que le changement de langue est terminé
        active = false;
    }
}

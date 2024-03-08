using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour
{
    //Variables
    private bool active = false; // Indique si le changement de langue est en cours

    // M�thode pour changer la langue en fonction de l'ID de la langue
    public void ChangeLocale(int localeID)
    {
        // V�rifie si le changement de langue est d�j� en cours, si oui, ne fait rien
        if (active == true)
            return;

        // D�marre la coroutine pour d�finir la langue
        StartCoroutine(SetLocale(localeID));
    }

    // Coroutine pour d�finir la langue
    IEnumerator SetLocale(int _localeID)
    {
        // Indique que le changement de langue est en cours
        active = true;

        // Attend que l'initialisation des param�tres de localisation soit termin�e
        yield return LocalizationSettings.InitializationOperation;

        // D�finit la langue s�lectionn�e en fonction de l'ID de la langue
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];

        // Indique que le changement de langue est termin�
        active = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    //Variables
    public float interactionRange = 3f; // La distance ?laquel le joueur peut int�ragir avec un objet
    private TMP_Text interactionText; // Le texte ?l'�cran permettant d'afficher si 

    void Awake()
    {
        // attribue ?la variable interactionText le Text de l'UI pour l'interaction avec des objets.
        interactionText = GameObject.Find("Interaction Text").GetComponent<TMP_Text>();
        //Cache le texte d'interaction lorsque la partie commence 
        interactionText.enabled = false;
    }

    private void Update()
    {
        //Utilise la fonction UpdateUI() pour permettre d'afficher ?l'�cran le UI d'interaction si il y ?un objets avec lequel ont peut int�ragir
        UpdateUI();
    }

    //Fonction qui permet d'int�ragir avec les objets qui ont le script InteractableObject attach?sur eux
    public void TryInteract()
    {
        // Envoie un raycast au centre de l'�cran pour voir si il y ?un objet
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // Regarde si il y a un objet avec lequel interagir ?la distance appropri?
        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            // Assigne le script InteractableObject de l'objet touch??la variable interactable object
            Interactable interactableObject = hit.collider.GetComponent<Interactable>();

            if (interactableObject != null) // Si l'objet touch??un Component InteractableObject
            {
                // Appel la m�thode Interact du script InteractableObject de l'objet
                interactableObject.Interact();
            }
        }
    }

    private void UpdateUI()
    {
        // Envoie un raycast au centre de l'�cran pour voir si il y ?un objet
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // Regarde si il y a un objet avec lequel interagir ?la distance appropri?
        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            // Assigne le script InteractableObject de l'objet toucher � la variable interactable object
            Interactable interactableObject = hit.collider.GetComponent<Interactable>();

            // Affiche le texte UI si il y � un objet d�tect?par le raycast qui ?un composant InteractableObject
            if (interactableObject != null)
            {
                interactionText.text = "Press 'E' to interact with " + interactableObject.gameObject.name; //Affiche le texte d'int�raction avec le nom de l'objet
                interactionText.enabled = true;
            }
            else
            {
                // Si le raycast touche un objet qui ne peut pas �tre interragit avec �a enleve le texte d'interaction
                interactionText.enabled = false;
            }
        }
        else
        {
            // Si le raycast ne touche rien n'affiche pas le texte
            interactionText.enabled = false;
        }
    }
    void OnDrawGizmos()
    {
        // Affiche le raycast envoy� par la cam�ra en tant que rayon de couleur cyan dans la fen�tre scene sur Unity si on a enable les Gizmos
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * interactionRange);
    }
}

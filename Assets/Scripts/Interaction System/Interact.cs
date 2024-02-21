using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    //Variables
    public float interactionRange = 3f; // La distance ?laquel le joueur peut intéragir avec un objet
    private TMP_Text interactionText; // Le texte ?l'écran permettant d'afficher si 

    void Awake()
    {
        // attribue ?la variable interactionText le Text de l'UI pour l'interaction avec des objets.
        interactionText = GameObject.Find("Interaction Text").GetComponent<TMP_Text>();
        //Cache le texte d'interaction lorsque la partie commence 
        interactionText.enabled = false;
    }

    private void Update()
    {
        //Utilise la fonction UpdateUI() pour permettre d'afficher ?l'écran le UI d'interaction si il y ?un objets avec lequel ont peut intéragir
        UpdateUI();
    }

    //Fonction qui permet d'intéragir avec les objets qui ont le script InteractableObject attach?sur eux
    public void TryInteract()
    {
        // Envoie un raycast au centre de l'écran pour voir si il y ?un objet
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // Regarde si il y a un objet avec lequel interagir ?la distance appropri?
        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            // Assigne le script InteractableObject de l'objet touch??la variable interactable object
            Interactable interactableObject = hit.collider.GetComponent<Interactable>();

            if (interactableObject != null) // Si l'objet touch??un Component InteractableObject
            {
                // Appel la méthode Interact du script InteractableObject de l'objet
                interactableObject.Interact();
            }
        }
    }

    private void UpdateUI()
    {
        // Envoie un raycast au centre de l'écran pour voir si il y ?un objet
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // Regarde si il y a un objet avec lequel interagir ?la distance appropri?
        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            // Assigne le script InteractableObject de l'objet toucher à la variable interactable object
            Interactable interactableObject = hit.collider.GetComponent<Interactable>();

            // Affiche le texte UI si il y à un objet détect?par le raycast qui ?un composant InteractableObject
            if (interactableObject != null)
            {
                interactionText.text = "Press 'E' to interact with " + interactableObject.gameObject.name; //Affiche le texte d'intéraction avec le nom de l'objet
                interactionText.enabled = true;
            }
            else
            {
                // Si le raycast touche un objet qui ne peut pas être interragit avec ça enleve le texte d'interaction
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
        // Affiche le raycast envoyé par la caméra en tant que rayon de couleur cyan dans la fenêtre scene sur Unity si on a enable les Gizmos
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * interactionRange);
    }
}

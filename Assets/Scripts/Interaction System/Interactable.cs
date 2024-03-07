using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public void Interact()
    {
        // Affiche l'objet avec lequel on int�ragit dans la console Unity
        Debug.Log("Interacting with " + gameObject.name);

        #region Door
        //Regarde si l'objet � un tag "Button"
        if (gameObject.CompareTag("Door"))
        {
            //Regarde si l'objet � un Animator
            Animator doorAnimator = gameObject.GetComponent<Animator>();

            //Si l'objet � un animator
            if (doorAnimator != null)
            {
                // Regarde si la porte est ouverte
                bool isDoorOpen = doorAnimator.GetBool("doorOpen");

                AudioSource doorAudio = gameObject.GetComponent<AudioSource>();
                // Ferme ou ouvre la porte d�pendamment de si elle est d�ja ouverte en inversant le boolean
                doorAnimator.SetBool("doorOpen", !isDoorOpen);
                doorAudio.Play();
            }
        }
        #endregion

        #region Object
        // Check if the object has a Object tag
        if (gameObject.CompareTag("Object"))
        {
            Inventory playerInventory = GameObject.Find("Player Inventory").GetComponent<Inventory>();
            SceneObjectInformation sceneObjectInformation = gameObject.GetComponent<SceneObjectInformation>();
            playerInventory.AddItem(sceneObjectInformation.scriptableObject);
            Destroy(gameObject); // Destroy the interactable object from the scene once it's picked up
        }
        #endregion
    }
}

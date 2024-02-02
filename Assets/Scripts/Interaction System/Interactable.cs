using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //Variables
    [SerializeField] GameObject interactable; // � utiliser pour les portes c'est surtout au cas ou l'on ouvre les portes avec un bouton

    public void Interact()
    {
        if (interactable != null)
        {
            // Affiche l'objet avec lequel on int�ragit dans la console Unity
            Debug.Log("Interacting with " + gameObject.name);

            #region Door
            //Regarde si l'objet � un tag "Button"
            if (this.gameObject.CompareTag("Button"))
            {
                //Regarde si l'objet � un Animator
                Animator doorAnimator = interactable.GetComponent<Animator>();

                //Si l'objet � un animator
                if (doorAnimator != null)
                {
                    // Regarde si la porte est ouverte
                    bool isDoorOpen = doorAnimator.GetBool("doorOpen");

                    AudioSource doorAudio = interactable.GetComponent<AudioSource>();
                    // Ferme ou ouvre la porte d�pendamment de si elle est d�ja ouverte en inversant le boolean
                    doorAnimator.SetBool("doorOpen", !isDoorOpen);
                    doorAudio.Play();
                }
            }
            #endregion

            #region Object
            //Regarde si l'objet � un tag Object
            if (this.gameObject.CompareTag("Object"))
            {
                Destroy(this.gameObject);
            }
            #endregion
        }
    }
}
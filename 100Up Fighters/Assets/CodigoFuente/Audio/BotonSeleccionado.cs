using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BotonSeleccionado : MonoBehaviour {

    public AudioSource fuenteSonido;
    public AudioClip clipHover;

    public void HoverSound() {
        fuenteSonido.PlayOneShot(clipHover);
    }
}

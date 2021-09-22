using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent (typeof(AudioSource))]
public class SonidoBotonesMenuPrincipal : MonoBehaviour {

    public Button[] botones;
    public Button botonAtras;

    public AudioClip Navegando;
    public AudioClip ClicEnBoton;
    public AudioClip Atras;
    public AudioClip Error;

    public AudioSource origenDeSonido;

    private void Awake() {
        foreach (Button boton in botones){
            boton.onClick.AddListener(clicEnBoton);
        }
    }

    public void navegandoABoton(BaseEventData eventData) {
        origenDeSonido.PlayOneShot(Navegando);
    }


    private void clicEnBoton() {
        origenDeSonido.PlayOneShot(ClicEnBoton);
    }

    public void entradaDePuntero(PointerEventData eventData) {
        
    }

    public void punteroPresionado(PointerEventData eventData) {
        
    }
}

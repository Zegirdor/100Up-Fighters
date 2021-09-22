using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BotonSeleccionadoGM : MonoBehaviour {
    public GameObject[] botones;
    public Transform flechaDeSeleccion;


    public void navegandoABotonJugar(BaseEventData eventData) {
        foreach (GameObject boton in botones){
            if (boton.name.Equals("Jugar")) {
                flechaDeSeleccion.position = new Vector3(boton.transform.position.x + 100, boton.transform.position.y, boton.transform.position.z);
            }
        }
    }

    public void navegandoABotonElegirNivel(BaseEventData eventData) {
        foreach (GameObject boton in botones) {
            if (boton.name.Equals("Elegir nivel")) {
                flechaDeSeleccion.position = new Vector3(boton.transform.position.x + 100, boton.transform.position.y, boton.transform.position.z);
            }
        }
    }

    public void navegandoABotonOpciones(BaseEventData eventData) {
        foreach (GameObject boton in botones) {
            if (boton.name.Equals("Opciones")) {
                flechaDeSeleccion.position = new Vector3(boton.transform.position.x + 100, boton.transform.position.y, boton.transform.position.z);
            }
        }
    }


    public void reiniciarPosicion(BaseEventData eventData) {
        flechaDeSeleccion.position = new Vector3(-151.11f, 1463.8f, 0f);
    }
}

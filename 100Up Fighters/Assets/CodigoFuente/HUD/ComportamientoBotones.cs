using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComportamientoBotones : MonoBehaviour {

    public Button botonInicio;
    public Button botonSalir;

    //Awake se ejecuta antes de inicializar el objeto, es ideal para las referencias externas.
    private void Awake() {
        botonInicio.onClick.AddListener(clickEnInicio);
        botonSalir.onClick.AddListener(clicEnSalir);
    }

    private void clicEnSalir() {
        Debug.Log("Salir");
        Application.Quit();
    }

    private void clickEnInicio() {
        Debug.Log("Inicio");
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}

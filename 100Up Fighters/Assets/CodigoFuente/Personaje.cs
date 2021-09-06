using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Personaje : MonoBehaviour {

    [Header("Atributos")]
    public float puntosDeVida = 10f;

    [Header("Movimiento")]
    public float velocidad = 5f;
    public float fuerzaDeSalto = 6f;
    public float margenDeSalto = 0.1f;

    private Rigidbody2D cuerpo = null;
    private Animator animador = null;
    private float puntosDeVidaActuales;

    public Rigidbody2D Cuerpo {
        get { return cuerpo; }
        protected set { cuerpo = value; }
    }

    public float PuntosDeVidaActuales {
        get { return puntosDeVidaActuales; }
        set { puntosDeVidaActuales = value; }
    }

    public Animator Animador {
        get { return animador; }
        protected set { animador = value; }
    }
    void Awake() {
        this.puntosDeVidaActuales = this.puntosDeVida;
        if (GetComponent<Rigidbody2D>()) {
            cuerpo = GetComponent<Rigidbody2D>();
        }
        if (GetComponent<Animator>()) {
            animador = GetComponent<Animator>();
        }
    }

    void Update() {

    }

    /// <summary>
    /// Esta función regresa un valor booleano indicando si el personaje está a una distancia del suelo
    /// suficiente para poder saltar.
    /// </summary>
    /// <returns></returns>
    protected bool enElSuelo() {
        return Physics2D.Raycast(transform.position, -Vector2.up, margenDeSalto);
    }

    protected virtual void morir() {
        gameObject.SetActive(false);
        Destroy(gameObject, 1f);
    }

    protected virtual void muerteDelJugador() {
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}

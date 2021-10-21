using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemigo : MonoBehaviour
{

    public float velocidad = 5f;
    public float margenDeSalto = 0.1f;

    private Rigidbody2D cuerpo = null;
    private Animator animador = null;

    // Awake is called when the object is instantiated
    private void Awake() {
        if (GetComponent<Rigidbody2D>()) {
            cuerpo = GetComponent<Rigidbody2D>();
        }
        if (GetComponent<Animator>()) {
            animador = GetComponent<Animator>();
        }
    }

    public Rigidbody2D Cuerpo {
        get { return cuerpo; }
        protected set { cuerpo = value; }
    }

    public Animator Animador {
        get { return animador; }
        protected set { animador = value; }
    }

    protected bool enElSuelo() {
        return Physics2D.Raycast(transform.position, -Vector2.up, margenDeSalto);
    }
}

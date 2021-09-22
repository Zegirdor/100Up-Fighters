using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mejora : MonoBehaviour {

    private Jugador jugador;

    void Awake() {
        jugador = GetComponent<Jugador>();
    }

    public Jugador Jugador {
        get { return jugador; }
    }

    private void OnTriggerEnter2D(Collider2D colision) {
        if (colision.CompareTag("Jugador") && (jugador = colision.GetComponent<Jugador>())) {

            activarMejora1();
        }
    }

    public virtual void activarMejora1() {
        Destroy(gameObject);
    }
}

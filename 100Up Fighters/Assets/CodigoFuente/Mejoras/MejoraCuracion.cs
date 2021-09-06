using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MejoraCuracion : Mejora {

    [Header("Configuraciones")]
    public float cantidad = 5f;

    public override void activarMejora1() {
        Jugador.PuntosDeVidaActuales += cantidad;
        Debug.Log("Recuperados: " + Jugador.PuntosDeVidaActuales + "puntos de vida.");
        base.activarMejora1();
    }
}

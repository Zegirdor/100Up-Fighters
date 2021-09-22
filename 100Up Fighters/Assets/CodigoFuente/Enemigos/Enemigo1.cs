using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo1 : Personaje, IDañable {

    public virtual void dañar(float cantidadDeDaño) {
        PuntosDeVidaActuales -= cantidadDeDaño;
        if (PuntosDeVidaActuales <= 0) {
            morir();
        }
    }
}

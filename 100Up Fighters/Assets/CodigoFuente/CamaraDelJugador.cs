using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraDelJugador : MonoBehaviour {

    public Transform posicionDelJugador;
    public Vector3 margen;

    void Update() {
        // Camera follows the player with specified offset position
        transform.position = new Vector2(posicionDelJugador.position.x + margen.x, posicionDelJugador.position.y + margen.y);

    }
}

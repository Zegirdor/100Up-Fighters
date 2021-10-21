using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IADePrueba : Enemigo {

    public Transform origenDelAtaqueCAC = null;

    private float movimientoEjeX = 0;
    public float velocidadParaCorrer = 5f;
    public string tipoMovimientoEjeX = "Horizontal";
    public Transform posicionDelJugador;

    private bool intentoDeAtaqueCAC = false;
    private bool intentoDeAtaqueADistancia = false;
    private bool intentoDeSalto = false;

    public float enfriamientoDeAtaqueCAC = 1.1f;
    public float enfriamientoDeAtaqueADistancia = 1.1f;
    public float retrasoEnRecuperarControl = 1.0f;
    public float esperaAtaqueCAC = 1f;
    public float esperaAtaqueCACListo = 0f;
    public float alcanceDeAtaqueCAC;
    public float dañoAtaqueCAC = 2f;

    public LayerMask capaDeObjetosGolpeables = 9;

    private bool estaAtacandoCuerpoACuerpo = false;

    private bool noControlable = false;

    // Update is called once per frame
    void Update() {
        seguirJugador();
        controlDeAnimaciones();
        intentarAtacar();
    }

    private void FixedUpdate() {
        correr();
    }

    private void controlDeAnimaciones() {

        Animador.SetBool("EnElSuelo", enElSuelo());

        if (intentoDeAtaqueCAC) {
            if (!estaAtacandoCuerpoACuerpo) {
                StartCoroutine(retrasoEnAnimacionDeAtaque());
                StartCoroutine(accionRecuperarElControl());
            }
        }

        if (intentoDeAtaqueADistancia) {
            Animador.SetTrigger("Disparar");
        }

        if (intentoDeSalto && enElSuelo() || Cuerpo.velocity.y > 1f) {
            if (!estaAtacandoCuerpoACuerpo) {
                Animador.SetTrigger("Saltar");
            }
        }

        if (Mathf.Abs(movimientoEjeX) > 0.1f && enElSuelo()) {
            Animador.SetInteger("EstadoAnimacion", 2);
        }
        else {
            Animador.SetInteger("EstadoAnimacion", 0);
        }
    }

    private void seguirJugador() {
        if (enElSuelo() && !noControlable) {
            if (posicionDelJugador.position.x > gameObject.transform.position.x) {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + velocidadParaCorrer * Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);
                movimientoEjeX = 1f;
            }
            else if (posicionDelJugador.position.x < gameObject.transform.position.x) {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x - velocidadParaCorrer * Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);
                movimientoEjeX = -1f;
            }
        }
    }

    private void intentarAtacar() {
        //Debug.Log("Distancia: " + Math.Abs(posicionDelJugador.position.x - gameObject.transform.position.x));
        //Debug.Log("Tiempo de espera: " + esperaAtaqueCACListo);
        if (esperaAtaqueCACListo <= 0) {
            if (Math.Abs(posicionDelJugador.position.x - gameObject.transform.position.x) <= 1f) {
                Debug.Log("Ataque cuerpo a cuerpo del enemigo.");
                Collider2D[] colisionesEmpalmadas = Physics2D.OverlapCircleAll(origenDelAtaqueCAC.position, alcanceDeAtaqueCAC, capaDeObjetosGolpeables);
                for (int c = 0; c < colisionesEmpalmadas.Length; c++) {
                    IDañable enemigoGolpeado = colisionesEmpalmadas[c].GetComponent<IDañable>();
                    if (enemigoGolpeado != null) {
                        enemigoGolpeado.dañar(dañoAtaqueCAC);
                        Debug.Log("Daño de: " + dañoAtaqueCAC + " al enemigo " + colisionesEmpalmadas[c].gameObject.name);
                    }
                }
                esperaAtaqueCACListo = enfriamientoDeAtaqueCAC;
            }

        }
        else {
            esperaAtaqueCACListo -= Time.deltaTime;
        }
    }

    private void correr() {
        if (movimientoEjeX > 0 && transform.rotation.y == 0) {
            //Si nos movemos a la derecha, y volteamos a la izquierda, entonces voltear a la derecha.
            transform.rotation = Quaternion.Euler(0, 180f, 0);
            //Vice versa.
        }
        else if (movimientoEjeX < 0 && transform.rotation.y != 0 && !estaAtacandoCuerpoACuerpo) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (!noControlable || !enElSuelo()) {
            Cuerpo.velocity = new Vector2(movimientoEjeX * velocidad, Cuerpo.velocity.y);
        }
        else if (enElSuelo()) {
            Cuerpo.velocity = new Vector2(0f, Cuerpo.velocity.y);
        }
    }

    private IEnumerator retrasoEnAnimacionDeAtaque() {
        Animador.SetTrigger("Atacar");
        estaAtacandoCuerpoACuerpo = true;
        yield return new WaitForSeconds(enfriamientoDeAtaqueCAC);
        estaAtacandoCuerpoACuerpo = false;
    }

    private IEnumerator accionRecuperarElControl() {
        noControlable = true;
        yield return new WaitForSeconds(retrasoEnRecuperarControl);
        noControlable = false;
    }
}

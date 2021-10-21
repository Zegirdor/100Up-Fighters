using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : Personaje, IDañable {

    //Variables de teclas
    [Header("Controles")]
    public KeyCode teclaAtaqueCAC = KeyCode.Z;
    public KeyCode teclaSalto = KeyCode.X;
    public KeyCode teclaAtaqueADistancia = KeyCode.C;
    public string tipoMovimientoEjeX = "Horizontal";

    //Variables de Unity
    [Header("Propiedades")]
    private float movimientoEjeX = 0;
    private bool intentoDeSalto = false;
    private bool intentoDeAtaqueCAC = false;
    private bool intentoDeAtaqueADistancia = false;
    private float esperaAtaqueCACListo = 0;
    private float esperaAtaqueADistanciaListo = 0;

    [Header("Combate Cuerpo a Cuerpo")]
    public Transform origenDelAtaqueCAC = null;
    public Transform origenDelAtaqueADistancia = null;
    public GameObject proyectil = null;
    public float alcanceDeAtaqueCAC;
    public float dañoAtaqueCAC = 2f;
    public float enfriamientoDeAtaqueCAC = 1.1f;
    public float enfriamientoDeAtaqueADistancia = 1.1f;
    public float retrasoEnRecuperarControl = 1.0f;
    public LayerMask capaDeEnemigos = 9;
    private bool estaAtacandoCuerpoACuerpo = false;
    private bool noControlable = false;

    //Métodos
    void Update() {
        registrarTecla();
        saltar();
        ataqueCuerpoACuerpo();
        ataqueADistancia();
        controlDeAnimaciones();
    }

    private void FixedUpdate() {
        correr();
    }

    private void OnDrawGizmosSelected() {
        Debug.DrawRay(transform.position, -Vector2.up * margenDeSalto, Color.white);
        if (origenDelAtaqueCAC != null) {
            Gizmos.DrawWireSphere(origenDelAtaqueCAC.position, alcanceDeAtaqueCAC);
        }
    }

    private void registrarTecla() {
        movimientoEjeX = Input.GetAxis(tipoMovimientoEjeX);
        intentoDeAtaqueCAC = Input.GetKeyDown(teclaAtaqueCAC);
        intentoDeAtaqueADistancia = Input.GetKeyDown(teclaAtaqueADistancia);
        intentoDeSalto = Input.GetKeyDown(teclaSalto);
    }

    private void correr() {
        if (movimientoEjeX > 0 && transform.rotation.y == 0) {
            //Si nos movemos a la derecha, y volteamos a la izquierda, entonces voltear a la derecha.
            transform.rotation = Quaternion.Euler(0, 180f, 0);
            //Vice versa.
        } else if (movimientoEjeX < 0 && transform.rotation.y != 0 && !estaAtacandoCuerpoACuerpo) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if(!noControlable || !enElSuelo()) {
            Cuerpo.velocity = new Vector2(movimientoEjeX * velocidad, Cuerpo.velocity.y);
        } else if (enElSuelo()) {
            Cuerpo.velocity = new Vector2(0f, Cuerpo.velocity.y);
        }
    }

    private void saltar() {
        if (intentoDeSalto && enElSuelo()) {
            Cuerpo.velocity = new Vector2(Cuerpo.velocity.x, fuerzaDeSalto);
        }
    }

    private void ataqueCuerpoACuerpo() {
        if (intentoDeAtaqueCAC && esperaAtaqueCACListo <= 0) {
            Debug.Log("Ataque cuerpo a cuerpo del jugador.");
            Collider2D[] colisionesEmpalmadas = Physics2D.OverlapCircleAll(origenDelAtaqueCAC.position, alcanceDeAtaqueCAC, capaDeEnemigos);
            for (int c = 0; c < colisionesEmpalmadas.Length; c++) {
                IDañable enemigoGolpeado = colisionesEmpalmadas[c].GetComponent<IDañable>();
                if (enemigoGolpeado != null) {
                    enemigoGolpeado.dañar(dañoAtaqueCAC);
                    Debug.Log("Daño de: " + dañoAtaqueCAC + " al enemigo " + colisionesEmpalmadas[c].gameObject.name);
                }
            }

            esperaAtaqueCACListo = enfriamientoDeAtaqueCAC;
        } else {
            esperaAtaqueCACListo -= Time.deltaTime;
        }
    }

    private void ataqueADistancia() {
        if (intentoDeAtaqueADistancia && esperaAtaqueADistanciaListo <= 0) {
            Debug.Log("Ataque a distancia del jugador.");

            Instantiate(proyectil, origenDelAtaqueADistancia.position, origenDelAtaqueADistancia.rotation);


            esperaAtaqueADistanciaListo = enfriamientoDeAtaqueADistancia;
        } else {
            esperaAtaqueADistanciaListo -= Time.deltaTime;
        }
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

        if(intentoDeSalto && enElSuelo() || Cuerpo.velocity.y > 1f) {
            if (!estaAtacandoCuerpoACuerpo) {
                Animador.SetTrigger("Saltar");
            }
        }

        if (Mathf.Abs(movimientoEjeX) > 0.1f && enElSuelo()) {
            Animador.SetInteger("EstadoAnimacion", 2);
        } else {
            Animador.SetInteger("EstadoAnimacion", 0);
        }
    }

    public virtual void dañar(float cantidadDeDaño) {
        PuntosDeVidaActuales -= cantidadDeDaño;
        if (PuntosDeVidaActuales <= 0) {
            muerteDelJugador();
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
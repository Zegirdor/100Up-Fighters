using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Proyectil : MonoBehaviour {

    public Rigidbody2D cuerpo;
    public float velocidad = 15f;
    public float daño = 1f;
    public float segundosDeRetraso = 2f;
    public LayerMask capaDeEnemigos;

    private WaitForSeconds retrasoParaDesaparecer = null;

    // Start is called before the first frame update
    void Start() {
        this.cuerpo = GetComponent<Rigidbody2D>();
        this.cuerpo.velocity = transform.right * velocidad;

        this.retrasoParaDesaparecer = new WaitForSeconds(segundosDeRetraso);
        StartCoroutine(retrasoDeDesaparicion());
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D colision) {
        //if(colision.gameObject.layer == 9) {
        if ((capaDeEnemigos.value & 1 << colision.gameObject.layer) == 1 << colision.gameObject.layer) {
            IDañable enemigoGolpeado = colision.GetComponent<IDañable>();
            if (enemigoGolpeado != null) {
                enemigoGolpeado.dañar(this.daño);
                Debug.Log("Daño de: " + this.daño + " al enemigo " + colision.gameObject.name);
            }

            gameObject.SetActive(false);
            Destroy(gameObject, 1f);
        }
    }

    private IEnumerator retrasoDeDesaparicion() {
        yield return retrasoParaDesaparecer;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}

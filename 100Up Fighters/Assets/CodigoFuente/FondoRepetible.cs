using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoRepetible : MonoBehaviour {

    private Vector3 coordenadasIniciales;
    public GameObject[] fondos;
    public float velocidad = 500f;
    private float horzExtent;

    private void Start() {
        horzExtent = Screen.width;
        Debug.Log("Tamaño camara: " + horzExtent);
        coordenadasIniciales = fondos[0].transform.position;
    }

    private void Update() {
        foreach(GameObject fondo in fondos) {
            if (fondo.transform.position.x <= coordenadasIniciales.x - (horzExtent - 20) && velocidad >= 0f) {
                fondo.transform.position = new Vector3(coordenadasIniciales.x + (horzExtent - 25), coordenadasIniciales.y, coordenadasIniciales.z);
            }
            else if (fondo.transform.position.x >= coordenadasIniciales.x + (horzExtent - 25) && velocidad < 0f) {
                fondo.transform.position = new Vector3(coordenadasIniciales.x - (horzExtent - 25), coordenadasIniciales.y, coordenadasIniciales.z);
            }

            var direccion = new Vector3(-1, 0, 0);
            fondo.transform.position += direccion * velocidad * Time.deltaTime;
        }
    }

}

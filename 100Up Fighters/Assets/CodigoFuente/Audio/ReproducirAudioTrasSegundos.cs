using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ReproducirAudioTrasSegundos : MonoBehaviour {

    public float segundosDeRetraso = 2f;

    // Start is called before the first frame update
    void Start() {
        Invoke("playAudio", segundosDeRetraso);
    }

    void playAudio() {
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update() {

    }
}

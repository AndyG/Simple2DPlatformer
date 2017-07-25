using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour, Useable {

    public float force = 10;

    public GameObject exhaust;
    public GameObject exhaustOrigin;

    public AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void use(Player player)
    {
        playSound();
        player.pushUpwards(force);
        Instantiate(exhaust, exhaustOrigin.transform.position, Quaternion.identity);
    }

    private void playSound() {
        if (!audioSource.isPlaying) {
            audioSource.Play();
        }
    }
}

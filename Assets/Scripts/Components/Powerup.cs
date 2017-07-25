using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    public PowerupType powerupType;

    public void onCollected() {
        Destroy(gameObject);
    }

    public enum PowerupType
    {
        JETPACK
    }
}

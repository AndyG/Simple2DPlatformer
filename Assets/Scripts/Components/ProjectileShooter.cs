using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour {

    public GameObject projectile;
    public float velocityX;
    public float velocityY;
    public float velocityAngular;

    public void shootProjectile() {
        GameObject instantiatedProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        Rigidbody2D rigidBody = instantiatedProjectile.GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(velocityX, velocityY);
        rigidBody.angularVelocity = velocityAngular;
    }
}

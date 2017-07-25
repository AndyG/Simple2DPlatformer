using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{

    public GameObject projectile;
    public float velocityX;
    public float velocityY;
    public float velocityAngular;

    public float rateOfFirePerSec = 1;

    private float timeSinceLastProjectile;
    private float minTimeBetweenProjectiles;

    private AudioSource audioSource;

    private bool isFlipped = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timeSinceLastProjectile = rateOfFirePerSec;
        minTimeBetweenProjectiles = 1f / rateOfFirePerSec;
    }

    void Update()
    {
        timeSinceLastProjectile += Time.deltaTime;
    }

    public void shootProjectile()
    {
        shootProjectile(0, 0);
    }

    public void shootProjectile(Vector2 shooterVelocity)
    {
        shootProjectile(shooterVelocity.x, shooterVelocity.y);
    }

    public void shootProjectile(float additionalVelocityX, float additionalVelocityY)
    {
        if (timeSinceLastProjectile < minTimeBetweenProjectiles)
        {
            return;
        }

        timeSinceLastProjectile = 0f;

        playSound();
        GameObject instantiatedProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        Rigidbody2D rigidBody = instantiatedProjectile.GetComponent<Rigidbody2D>();

        Vector2 projectileVelocity = new Vector2(velocityX, velocityY);

        Debug.Log("projectile velocity1: " + projectileVelocity);
        Debug.Log("transform right x: " + transform.right.x);

        if (isFlipped)
        {
            projectileVelocity.x *= -1;
        }

        projectileVelocity += new Vector2(additionalVelocityX, additionalVelocityY);

        Debug.Log("projectile velocity2: " + projectileVelocity);
        rigidBody.velocity = projectileVelocity;
        rigidBody.angularVelocity = velocityAngular;
    }

    public void setIsFlipped(bool isFlipped)
    {
        this.isFlipped = isFlipped;
    }

    private void playSound()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}

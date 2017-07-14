using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    private SpriteTracker spriteTracker;

    // Use this for initialization
    void Start()
    {
        SpriteTracker[] spriteTrackers = FindObjectsOfType(typeof(SpriteTracker)) as SpriteTracker[];
        spriteTracker = spriteTrackers[0];
    }


    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            spriteTracker.handleSpriteCollected();
        }
    }
}

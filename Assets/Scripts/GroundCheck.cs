using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    private Player player;

	// Use this for initialization
	void Start () {
        player = gameObject.GetComponentInParent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        setPlayerGrounded(true);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        setPlayerGrounded(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        setPlayerGrounded(false);
    }

    private void setPlayerGrounded(bool isGrounded)
    {
        if (player != null)
        {
			player.setGrounded (isGrounded);
        }
    }
}

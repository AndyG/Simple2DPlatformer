using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckerRays : GroundChecker {

    private Collider2D boundsProvider;
    private Transform target;
    private int layerMask;

    private float groundDistance = 0.1f;

    public GroundCheckerRays(Collider2D boundsProvider, Transform target, int layerMask)
    {
        this.boundsProvider = boundsProvider;
        this.target = target;
        this.layerMask = layerMask;
    }

    public bool isGrounded() {
        return RayUtils.doAnyRaysCollide(getGroundRays(), groundDistance, layerMask);
    }

    private Ray2D[] getGroundRays()
    {
        float colliderWidth = boundsProvider.bounds.size.x * .9f;
        float colliderHeight = boundsProvider.bounds.size.y;

        Vector2 colliderBottomLeft = target.position + new Vector3(-colliderWidth / 2, -colliderHeight / 2);
        Vector2 colliderBottomRight = target.position + new Vector3(colliderWidth / 2, -colliderHeight / 2);

        Ray2D[] rays = new Ray2D[2];
        rays[0] = new Ray2D(colliderBottomLeft, Vector3.down);
        rays[1] = new Ray2D(colliderBottomRight, Vector3.down);
        return rays;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPushCheckerRays : WallPushChecker {

    private Collider2D boundsProvider;
    private Transform target;
    private int layerMask;

    private float groundDistance = 0.1f;

    public WallPushCheckerRays(Collider2D boundsProvider, Transform target, int layerMask)
    {
        this.boundsProvider = boundsProvider;
        this.target = target;
        this.layerMask = layerMask;
    }

    public WallPushState getWallPushState() {
        float horizInput = Input.GetAxis("Horizontal");

        if (horizInput == 0) {
            return WallPushState.NONE;
        }

        bool goingRight = horizInput > 0;
        if (goingRight && RayUtils.doAnyRaysCollide(getWallRaysRight(), groundDistance, layerMask))
        {
            return WallPushState.PUSHING_RIGHT;
        } else if (!goingRight && RayUtils.doAnyRaysCollide(getWallRaysLeft(), groundDistance, layerMask))
        {
            return WallPushState.PUSHING_LEFT;
        } else
        {
            return WallPushState.NONE;
        }
    }

    private Ray2D[] getWallRaysLeft()
    {
        float colliderWidth = boundsProvider.bounds.size.x;
        Vector2 colliderMiddleLeft = target.position + (Vector3.left * colliderWidth / 2);

        Ray2D[] rays = new Ray2D[1];
        rays[0] = new Ray2D(colliderMiddleLeft, Vector3.left);
        return rays;
    }

    private Ray2D[] getWallRaysRight()
    {
        float colliderWidth = boundsProvider.bounds.size.x;
        Vector2 colliderMiddleRight = target.position + (Vector3.right * colliderWidth / 2);

        Ray2D[] rays = new Ray2D[1];
        rays[0] = new Ray2D(colliderMiddleRight, Vector3.right);
        return rays;
    }
}

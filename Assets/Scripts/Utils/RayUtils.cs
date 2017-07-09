using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayUtils
{

    public static bool doAnyRaysCollide(Ray2D[] rays, float distance, int layerMask)
    {
        for (int i = 0; i < rays.Length; i++)
        {
            if (checkRayCollision(rays[i], distance, layerMask) == true)
            {
                return true;
            }
        }

        return false;
    }

    public static bool checkRayCollision(Ray2D ray, float distance, int layerMask)
    {
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, distance, layerMask);
        return hit.collider != null;
    }

    public static void drawDebugRays(Ray2D[] rays, float distance, Color color) {
        for(int i = 0; i < rays.Length; i++) {
            Debug.DrawRay(rays[i].origin, rays[i].direction * distance, Color.red);
        }
    }

}
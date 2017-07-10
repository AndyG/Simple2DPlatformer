using UnityEngine;

public class SpriteFlipper {

    private Transform transform;

    public SpriteFlipper(Transform transform)
    {
        this.transform = transform;
    }
    
    public void flipSprite(bool flip) {
        int scaleX = flip ? -1 : 1;
        transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
    }
}

using UnityEngine;

public class SpriteFlipper {

    public delegate bool ShouldFlip();

    private ShouldFlip shouldFlip;


    public SpriteFlipper(ShouldFlip shouldFlip) {
        this.shouldFlip = shouldFlip;
    }
    
    public void tryFlipSprite(Transform transform) {
        int scaleX = shouldFlip() ? -1 : 1;
        transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
    }
}

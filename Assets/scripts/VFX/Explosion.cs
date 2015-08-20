using UnityEngine;
using System.Collections;

/// <summary>
/// This class shows an explosion-animation, by scaling a sprite, to a specified size and
/// than shrink it again.
/// </summary>
public class Explosion : MonoBehaviour {

    // How long the explosion shall take
    [SerializeField]
    private float explosionTime;

    // How big the explosion shall be
    [SerializeField]
    private float explosionScale;

    // The time-point, when the explosion has its maximal size
    private float halfExplosionTime;

    // The number of Update-calls, till the explosion has finished
    private int numberOfTicks;

    private int currentTick;

    // A vector which specifies the size how much the sprite will grow every update-tick.
    private Vector3 sizeChangePerTickVector;

	void Start () {

        halfExplosionTime = explosionTime / 2;

        numberOfTicks =  (int) (halfExplosionTime / Time.fixedDeltaTime);

        // The explosion has to reach its maximum size in the half number of ticks.
        float sizeChangePerTick = explosionScale / numberOfTicks * 2;
        sizeChangePerTickVector = new Vector3(sizeChangePerTick, sizeChangePerTick, 1);

        // The explosion shall start without size
        this.transform.localScale = new Vector3(0, 0, 1);
	}
	

    void Update() {
        // If the animation finished, the explosionobject will destroy itsely
        if(currentTick < numberOfTicks) {
            // Growing till the half number óf ticks is not reached yet. 
            // Otherwise shrink.
            if (currentTick < numberOfTicks / 2) {
                transform.localScale += sizeChangePerTickVector;
            } else {
                transform.localScale -= sizeChangePerTickVector;
            }
            currentTick++;
        } else {
            // Animation finished -> Destroy
            Destroy(this.gameObject);
        }
    }
}

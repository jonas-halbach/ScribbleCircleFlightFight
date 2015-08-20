using UnityEngine;
using System.Collections;

/// <summary>
/// This script is used to visualie the players-livepoints.
/// </summary>
public class LivePointVisualizer : MonoBehaviour {

    public GameObject LivePointVisualisationObject {
        get;
        set;
    }

    /// <summary>
    /// Setting the livepoints of the player
    /// </summary>
    /// <param name="livePoints"></param>
    public void SetLivePoints(int livePoints) {

        //Iterating and deleting the existing objects, which visualize the players livepoints
        foreach (Transform vizualizer in GetComponentInChildren<Transform>()) {
            if (vizualizer != null) {
                GameObject.Destroy(vizualizer.gameObject);
            }
        }

        //Creating new livepoint-visualisation-objects
        for (int i = 0; i < livePoints; i++) {
            GameObject currentVisualizer = GameObject.Instantiate<GameObject>(LivePointVisualisationObject);
            Renderer renderer = currentVisualizer.GetComponent<Renderer>();
            Vector3 size = renderer.bounds.size;


            currentVisualizer.transform.parent = this.transform;
            currentVisualizer.transform.position = new Vector3(transform.position.x + i * size.x, transform.position.y, transform.position.z); 
        }
    }
}

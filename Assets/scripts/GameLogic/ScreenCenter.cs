using UnityEngine;
using System.Collections;

using UnityGameBase;

public class ScreenCenter : GameComponent<GameLogic> {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider) {
        Projectile projectile = collider.GetComponent<Projectile>();
        if (projectile != null) {
            GameObject.Destroy(projectile.gameObject);
        }
    }
}

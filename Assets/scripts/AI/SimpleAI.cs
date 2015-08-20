/// <summary>
/// This class is a simple AI for one enemy-type; 
/// </summary>

using UnityEngine;
using System.Collections;

public class SimpleAI : MonoBehaviour {

    [SerializeField]
    private float shootingRange;

    GameObject player;

    Vehicle toControl;

	
	void Start () {

        toControl = this.GetComponent<Vehicle>();

	}
	

	void Update () {

        // Every tick the enemy shall fly downward and into the direction of 
        // the player
        if (player != null) {
            float xDistance = player.transform.position.x - this.transform.position.x;

            Direction direction = Direction.left;

            ICommand moveCommand = new MoveCommand(direction);
            moveCommand.Execute(toControl);

            // if distance on the x-axis of this enemies and the players 
            // position is smaller than a given range the enemy shall shoot
            if (Mathf.Abs(xDistance) < shootingRange) {
                ICommand shootCommand = new ShootCommand();
                shootCommand.Execute(toControl);
            }
        }
	}


    public void SetPlayer(GameObject player) {
        this.player = player;
    }
}

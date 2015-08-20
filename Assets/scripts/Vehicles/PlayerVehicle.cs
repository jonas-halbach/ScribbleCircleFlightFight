using UnityEngine;
using System.Collections;

public class PlayerVehicle : Vehicle {

	// Use this for initialization
	void Start () {
        gameManager = Game;

        rigidbody = GetComponent<Rigidbody2D>();

        PlayerType = PlayerType.Player;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// This method sets the current velocity of this vehicle
    /// </summary>
    /// <param name="direction"></param>
    public override void Move(Direction direction) {

        if (direction == Direction.right) {
            this.transform.parent.Rotate(new Vector3(0, 0, 1), velocity);
        }
        
        if (direction == Direction.left) {
            this.transform.parent.Rotate(new Vector3(0, 0, 1), -velocity);
        }
    }

    public override void Move(Vector2 movedirection) {

    }
}

using UnityEngine;
using System.Collections;

using UnityGameBase;

public class Projectile : GameComponent<GameLogic> {

    // Value how fast the projectile can fly
    [SerializeField]
    private float velocity;

    // Value how much damage this projectile causes
    [SerializeField]
    private int damage;

    // The direction in with the projectile will fly
    [SerializeField]
    private Vector2 direction;

    // Storing the direction combined with the velocity in which the projectile will fly
    private Vector3 tempMovement;

    // Container to store the vehicle, which shot this projectile 
    private Vehicle owner;

    private Rigidbody2D rigibody;


    // Which vehicle shot this projectile
    public Vehicle Owner {
        get;
        set;
    }

    // Was the projectile shot by an player or enemy
    public PlayerType Type {
        get;
        set;
    }

    public float Velocity {
        get { return velocity; }
        set { velocity = value; }
    }

	// Use this for initialization
	void Start () {
        SetDirection(direction);

        rigibody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        rigibody.velocity = tempMovement;
	}

    public void SetDirection(Vector2 direction) {
        this.direction = direction.normalized;

        float xDirection = velocity * direction.x;
        float yDirection = velocity * direction.y;

        tempMovement = new Vector3(xDirection, yDirection, 0);
        
        //calculating the direction in which the projectile will fly
        tempMovement = transform.rotation * tempMovement;
    }


    /// <summary>
    /// This trigger-method checks, if the projectile collided with a
    /// vehicle
    /// </summary>
    /// <param name="collider">The collider-object this projectile collidet with</param>
    public void OnTriggerEnter2D(Collider2D collider) {
        
        Debug.Log("Projectile triggered: " + collider.name);
        Vehicle vehicle = collider.gameObject.GetComponent<Vehicle>();

        if(vehicle != null) {
            if (vehicle != this.Owner) {
                vehicle.Damage(damage);
                this.Owner.ReportEnemyDamage(vehicle, damage);
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}

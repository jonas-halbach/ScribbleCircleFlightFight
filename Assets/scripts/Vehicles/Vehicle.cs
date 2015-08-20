using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityGameBase;

/// <summary>
/// This script represents a vehicle, which can be controlled by a player or by the ai.
/// </summary>
public abstract class Vehicle : GameComponent<GameLogic> {

    // The moving velocity of this vehicle
    [SerializeField]
    protected float velocity;

    // The livepoints of this vehicle
    [SerializeField]
    private int health;

    // Weapon mounted on the vehicle by default
    [SerializeField]
    protected GameObject defaultWeapon;

    // Type of the vehicle-owner(Player or Enemy)
    [SerializeField]
    protected PlayerType type;

    [SerializeField]
    private Explosion deathAnimation;
    
    // A list which store all weapons mounted on this vehicle
    private List<Weapon> weapons;

    // Container for the weapon, which is currently active
    private Weapon activeWeapon;

    // referenz to the gamemanager
    protected GameLogic gameManager;

    // Container for the vehicles rigidbody
    protected Rigidbody2D rigidbody;

    
    public int Heatlth {
        get { return health; }
        set { health = value; }
    }

    public PlayerType PlayerType {
        get { return type; }
        set { type = value; }
    }
    
    /// <summary>
    /// Adding the default-weapon to the vehicle
    /// </summary>
    void Awake() {
        weapons = new List<Weapon>();
        activeWeapon = GetComponent<Weapon>();

        AddDefaultWeapon();
    }

    /// <summary>
    /// Method adds the default-weapon to the vehicle and sets some of its properties
    /// </summary>
    private void AddDefaultWeapon() {
        AddWeapon(defaultWeapon);

        activeWeapon.Owner = this;
        activeWeapon.Type = type;
    }


	void Start () {
        gameManager = Game;

        rigidbody = GetComponent<Rigidbody2D>();
	}

    /// <summary>
    /// This method adds a weapon to this vehicle
    /// </summary>
    /// <param name="weapon"></param>
    public void AddWeapon(GameObject weapon) {

        GameObject activeWeaponGameObject = GameObject.Instantiate<GameObject>(weapon);

        activeWeapon.Root = activeWeaponGameObject;

        // Transforming the weapon in the flying-direction of this vehicle
        activeWeaponGameObject.transform.parent = this.transform;
        activeWeaponGameObject.transform.localPosition = new Vector3(0, 0, 0);
        activeWeaponGameObject.transform.localRotation = new Quaternion();
    }
	

	void Update () {
	
	}

    /// <summary>
    /// This method executes a shoot with the current activated weapon
    /// </summary>
    public void Shoot() {
        if (activeWeapon) {
            activeWeapon.Shoot();
        }

    }

    public abstract void Move(Direction moveDirection);

    public abstract void Move(Vector2 moveDirection);

    /// <summary>
    /// This method shall be called if this vehicle gets damage
    /// </summary>
    /// <param name="damage">the damage amount</param>
    public void Damage(int damage) {
        health -= damage;
        if (health <= 0) {
            PreKill();
        }
    }

    protected virtual void PreKill() {
        Explosion preKill = GameObject.Instantiate<Explosion>(deathAnimation);
        Vector3 preKillPosition = this.transform.position;
        preKillPosition.z = -5;
        preKill.transform.position = preKillPosition;

        Kill();
    }

    /// <summary>
    /// Kill this vehicle and destroy the associated gameobject
    /// </summary>
    protected void Kill() {
        
        Destroy(this.gameObject);
    }


    /// <summary>
    /// Report to the gamemanager, that this vehicle, damaged his opponent
    /// </summary>
    /// <param name="victim">The vehicle of the opponent</param>
    /// <param name="damage">The damage-amount</param>
    public void ReportEnemyDamage(Vehicle victim, int damage) {
        if (this != null && victim != null) {
            gameManager.ReportDamage(this, victim, damage);
        }
    }

    /// <summary>
    /// This triggermethod checks, if this vehicle is collided with an other vehicle
    /// </summary>
    /// <param name="collider"></param>
    public void OnTriggerEnter2D(Collider2D collider) {
    //    Vehicle collisionVehicle = collider.gameObject.GetComponent<Vehicle>();
    //    if (collisionVehicle != null) {
            
    //        // Destroy the opponent vehicle
            
    //        gameManager.ReportDamage(collisionVehicle, this, Heatlth);
    //        collisionVehicle.Damage(collisionVehicle.Heatlth);
    //        //Destroy this vehicle
    //        Damage(Heatlth); 
    //    }
    }
}


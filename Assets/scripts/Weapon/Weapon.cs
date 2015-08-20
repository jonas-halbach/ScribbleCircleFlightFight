using UnityEngine;
using System.Collections;

/// <summary>
/// This weaponscript brings the functionality of a weapon to this game
/// </summary>
public class Weapon : MonoBehaviour {

    // The projectile which will be shoot by this wepon
    [SerializeField]
    private Projectile projectile;

    // The weapon needs some time to be able to shoot again
    [SerializeField]
    private float coolDownTime;

    // The velocity in which the projectile which was fired by this weapon will fly
    [SerializeField]
    private float projectileVelocity;

    // This weapon object needs a rootnode, where different transform-objects 
    // are added to, which represent the different canons of this weapon
    [SerializeField]
    private GameObject rootNode;

    // Is this weapon able to shoot again
    private bool canShoot = true;

    // The vehicle on which this weapon is mounted
    public Vehicle Owner {
        get;
        set;
    }

    // Does this weapon belong to a player or enemy
    public PlayerType Type {
        get;
        set;
    }

    // The rootnode of all canons
    public GameObject Root {
        get { return rootNode; }
        set { rootNode = value; }
    }

    /// <summary>
    /// Shoot
    /// </summary>
    public void Shoot() {

        // If the weapon is able to shoot again
        if (canShoot) {
            canShoot = false;

            // Iterate over all canons of the rootobject, to shoot with every canon
            Transform canons = Root.transform;
            foreach (Transform canon in canons) {
                    ShootProjectile(canon.transform);   
            }

            // Make this weapon shootable again after the coolDownTime
            Invoke("MakeShootable", coolDownTime);
        }
    }

    /// <summary>
    /// Shooting a single projectile of one canon of this weapon
    /// </summary>
    /// <param name="canon">The canon represented by a transform-object which shoots the projectile</param>
    private void ShootProjectile(Transform canon) {
        Projectile newProjectile = GameObject.Instantiate<Projectile>(projectile);
        newProjectile.Owner = this.Owner;
        newProjectile.Type = Type;

        Vector3 projectilePosition = canon.transform.position;

        // Making sure the projectile is in front of the planes
        projectilePosition.z = -1;
        newProjectile.transform.position = projectilePosition;
        newProjectile.transform.rotation = canon.transform.rotation;

        // Setting the velocity of this projectile
        newProjectile.Velocity = projectileVelocity;
    }

    /// <summary>
    /// Make this weapon shootable again
    /// </summary>
    private void MakeShootable() {
        canShoot = true;
    }
}

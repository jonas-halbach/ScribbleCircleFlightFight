using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityGameBase;

// this is a delegate to define a listener for "player has changed"-events
public delegate void PlayerStatusChangedEventListener(object sender, Player player);
public class GameLogic : Game {

    // Listener can be register for this event;
    public event PlayerStatusChangedEventListener PlayerChangeListener;

    // Setting the livepoints a player shall start with
    [SerializeField]
    private int playerStartLivepoints;

    // Setting the score-points a player shall start with
    [SerializeField]
    private int playerStartScore;

    // This transform specifies the position.y value where, the new enemies shall be spawned
    [SerializeField]
    private SpawnPoint enemySpawnPoint;

    // Specification how many score-points per damage-point the player gets
    [SerializeField]
    private float pointsPerLPDamage;

    [SerializeField]
    private GameObject playerPrototype;

    [SerializeField]
    private int maxEnemySpawnCount;

    [SerializeField]
    private int minEnemySpawnTime;

    [SerializeField]
    private int maxEnemySpawnTime;

    // Storing the Player-Script to add live and score-points
    private Player player;

    // Storing the plane controled by the player
    private GameObject playerPlane;

    private List<EnemyVehicle> existingEnemies;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    protected override void Initialize() {

        playerPlane = GameObject.Find("PlayerPlane");
        existingEnemies = new List<EnemyVehicle>();

    }

    protected override void GameSetupReady() {

        InitializePlayer();
        StartEnemySpawning();
    }

    /// <summary>
    /// Because the player-object will not be destroed after the game was restarted it has to
    /// be reinitialized
    /// </summary>
    /// <returns>The vehicle-script, which can be used to control the player avatar</returns>
    private Vehicle InitializePlayer() {

        Vehicle playerPlaneVehicle = playerPlane.GetComponent<Vehicle>();

        GameObject playerObject = GameObject.Find("Player1");

        if (playerObject == null) {
            playerObject = GameObject.Instantiate(playerPrototype);
            playerObject.name = "Player1";
            Debug.Log("Playername: " + playerObject.name);
            DontDestroyOnLoad(playerObject);
        }

        player = playerObject.GetComponent<Player>();
        player.LP = playerStartLivepoints;
        player.Score = playerStartScore;

        playerPlaneVehicle.Heatlth = player.LP;

        // Notifiying all listeners, that the player-object has changed;
        OnPlayerChanged();

        return playerPlaneVehicle;
    }

    private void StartEnemySpawning() {

        SpawnNewEnemies();
    }

    private void SpawnNewEnemies() {
        int enemySpawnCount = Random.Range(0, maxEnemySpawnCount);
        float newSpawnTime = Random.Range(minEnemySpawnTime, maxEnemySpawnTime);

        enemySpawnPoint.AddEnemies(enemySpawnCount);

        Invoke("SpawnNewEnemies", newSpawnTime);
    }

    /// <summary>
    /// Calling this method if the values of the player-object have changed to notify all
    /// listener
    /// </summary>
    protected virtual void OnPlayerChanged() {
        if (PlayerChangeListener != null) {
            PlayerChangeListener(this, player);
        }
    }

    /// <summary>
    /// This method is used to inform the gamemanager, that the offender has
    /// damaged the victim.
    /// </summary>
    /// <param name="offender">The Vehicle-script of the plane, which brought 
    /// the damage</param>
    /// <param name="victim">The vehicle-script of the plane which got the 
    /// damage</param>
    /// <param name="damage">The damage-amount</param>
    public void ReportDamage(Vehicle offender, Vehicle victim, int damage) {
        // If the player, gave damage, the player will get points
        // if the player is the victim he will loose LP
        if (offender.PlayerType == PlayerType.Player) {
            player.Score += damage * pointsPerLPDamage;
        } else if (victim.PlayerType == PlayerType.Player) {
            player.LP -= damage;
            if (player.LP <= 0) {
                GameOver();
            }
        }
        OnPlayerChanged();
    }

    public void AddEnemy(EnemyVehicle enemy) {
        existingEnemies.Add(enemy);
    }

    public void RemoveEnemy(EnemyVehicle enemy) {
        existingEnemies.Remove(enemy);
    }

    public GameObject PlayerPlane {
        get {
            return playerPlane;
        }
    }


    /// <summary>
    /// When the player lost all his LPs, than show the endscreen.
    /// </summary>
    private void GameOver() {
        Application.LoadLevel("Endscreen");
    }


}

public enum PlayerType {
    Player,
    Enemy
}

using UnityEngine;
using System.Collections;

using UnityGameBase;

public class SpawnPoint : GameComponent<GameLogic> {

    [SerializeField]
    private GameObject enemyProtoype;

    private int enemiesToSpawn;

    private int enemiesInRadius;

    private GameObject playerPlane;

	// Use this for initialization
	void Start () {
        enemiesInRadius = 0;
        enemiesToSpawn = 0;

        playerPlane = Game.PlayerPlane;
	}
	
	// Update is called once per frame
	void Update () {
        SpawnEnemies();
	}

    public void AddEnemies(int enemyNumber) {
        enemiesToSpawn += enemyNumber;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        EnemyVehicle enemyVehicle = collider.GetComponent<EnemyVehicle>();
        if (collider.GetComponent<EnemyVehicle>() != null) {
            //AddEnemyInRadius(enemyVehicle);           
        }
    }


    void OnTriggerExit2D(Collider2D collider) {
        EnemyVehicle enemyVehicle = collider.GetComponent<EnemyVehicle>();
        if (enemyVehicle != null) {
            RemoveEnemyInRadius();
            enemyVehicle.IsInSpawnPointRadius = false;
        }
    }

    public void RemoveEnemyInRadius() {
        enemiesInRadius -= 1;
    }

    public void AddEnemyInRadius(EnemyVehicle enemy) {
        enemy.IsInSpawnPointRadius = true;
        enemiesInRadius += 1;
        
    }

    private void SpawnEnemies() {
        Debug.Log("enemiesInRadius " + enemiesInRadius + " enemiesToSpawn " + enemiesToSpawn);
        if (enemiesInRadius <= 0 && enemiesToSpawn > 0) {
            GameObject newEnemy = GameObject.Instantiate(enemyProtoype);
            EnemyVehicle newEnemyVehicle = newEnemy.GetComponent<EnemyVehicle>();
            newEnemyVehicle.SpawnPoint = this;
            AddEnemyInRadius(newEnemyVehicle);
            SimpleAI enemyAI = newEnemy.GetComponent<SimpleAI>();
            enemyAI.SetPlayer(playerPlane);
        }
    }
}

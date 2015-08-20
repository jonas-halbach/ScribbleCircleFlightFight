using UnityEngine;
using System.Collections;

public class EnemyVehicle : Vehicle {

    private bool isInSpawnPointRadius = true;

    public bool IsInSpawnPointRadius {
        get { return isInSpawnPointRadius; }
        set { isInSpawnPointRadius = value;}
    }

    public SpawnPoint SpawnPoint {
        get;
        set;
    }

	// Use this for initialization
	void Start () {
        PlayerType = PlayerType.Enemy;

        Game.AddEnemy(this);

        gameManager = Game;

        rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Move(Direction moveDirection) {

    }

    public override void Move(Vector2 movedirection) {
        float xDirection = movedirection.normalized.x * velocity;
        float yDirection = movedirection.normalized.y * velocity;
        this.rigidbody.velocity = new Vector3(xDirection, yDirection, 0);
    }

    protected override void PreKill() {
        Game.RemoveEnemy(this);
        if (isInSpawnPointRadius) {
            SpawnPoint.RemoveEnemyInRadius();
        }
        base.PreKill();
    }
}

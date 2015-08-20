using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using UnityGameBase;

/// <summary>
/// The HUD shows the live and scorepoints of the player
/// </summary>
public class HUD : GameComponent<GameLogic> {

    // Where shall the player-score be shown
    [SerializeField]
    Text scoreText;

    // Where is the position to show the LPs of the player
    [SerializeField]
    GameObject livePointVisualizer;
    
    private GameLogic gameManager;
    private Canvas hud;

    private LivePointVisualizer livePointVisualizationRoot;

    void Awake() {

        // Registring as player-state-changed listener at the game-manager
        gameManager = Game;
        gameManager.PlayerChangeListener += UpdatePlayerStats;
    }


	void Start () {
           
        // Getting the position, where the players livepoints shall be shown, and 
        // which gameobject shall be used to represent a livepoint.
        livePointVisualizationRoot = GetComponentInChildren<LivePointVisualizer>();
        livePointVisualizationRoot.LivePointVisualisationObject = livePointVisualizer;
	}

    /// <summary>
    /// Listener-method to update the player-stats
    /// </summary>
    /// <param name="sender">The sender</param>
    /// <param name="player">The information of the event</param>
    public void UpdatePlayerStats(object sender, Player player) {
        scoreText.text = player.Score + "Points";
        livePointVisualizationRoot.SetLivePoints(player.LP);

    }
}

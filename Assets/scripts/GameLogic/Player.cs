using UnityEngine;
using System.Collections;

using UnityGameBase;

/// <summary>
/// Storing the LP and Score-points of the player
/// </summary>
public class Player : GameComponent<GameLogic> {
    public int LP {
        get;
        set;
    }

    public double Score {
        get;
        set;
    }
}

using UnityEngine;
using System.Collections;

/// <summary>
/// Defining the actions for the main-menu
/// </summary>
public class MenuActions : MonoBehaviour {

    public void GameQuit() {
        Application.Quit();
    }

    public void GameStart() {
        Application.LoadLevel("Gamescene");
    }
}

using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using System;
using System.IO;

/// <summary>
/// Class to store the highscoredata
/// </summary>
public class ScoreEntry {

    public ScoreEntry(string name, int score) {
        this.Name = name;
        this.Score = score;
    }

    public string Name {
        get;
        set;
    }

    public int Score {
        get;
        set;
    }
}

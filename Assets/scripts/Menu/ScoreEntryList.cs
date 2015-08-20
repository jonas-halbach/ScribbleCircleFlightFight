using UnityEngine;
using System.Collections;

/// <summary>
/// Class to store an array of score-entries. The class is used for the newtonsoft.json-library
/// </summary>
public class ScoreEntryList {
    public ScoreEntry[] Entries { get; set; }
}

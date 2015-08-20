using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// This comparator is used to sort an arraylist of ScoreEntry-Objects
/// </summary>
public class ScoreListComparer : IComparer<ScoreEntry> {

    public int Compare(ScoreEntry object1, ScoreEntry object2) {
        return object2.Score.CompareTo(object1.Score);
    }

}
   
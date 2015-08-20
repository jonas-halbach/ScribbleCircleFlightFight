using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

/// <summary>
/// This class defines the Actions for the GameOverMenu
/// </summary>
public class GameOverMenuActions : MonoBehaviour {

    // The path to the file, where the scores of the different playsessions
    // shall be stored
    [SerializeField]
    private String scoreFilePath;

    // The name of the file, where the scores of the different Playsessions
    // shall be stored
    [SerializeField]
    private String scoreFileName;
    
    // The text-object, where the score of the current playsession shall be
    // shown
    [SerializeField]
    private Text scoreDisplay;

    // The textfield where the player can type in his name, to save his score 
    [SerializeField]
    private InputField nameTextfield;

    // The text-object, where the different scores of the previous sessions 
    // will be visualized
    [SerializeField]
    private Text highscoreText;

    // The button, to click on to save the current score and name of the player
    [SerializeField]
    private GameObject saveButton;

    // List to store the previous score-entries
    private List<ScoreEntry> scoreEntries;

    // The object to store the information of the current game-session
    private Player player;

    // Absolut path to the file and filename, where the scores of the different playsessions
    // shall be stored
    private String pathToScoreFile;

    // Absolut path to the folder, where the scores of the different playsessions
    // shall be stored
    private String pathToScoreFolder;

    // Is the score yet saved
    private bool scoreYetSaved = false;

	
	void Start () {
        GameObject playerObject = GameObject.Find("Player1");
        if (playerObject != null) {
            player = playerObject.GetComponent<Player>();
        
            scoreDisplay.text = "Your score: " + player.Score;
        } else {
            Debug.Log("No player found");
        }

        pathToScoreFile = scoreFilePath + "/" + scoreFileName;
        pathToScoreFile = Application.dataPath + "/" + pathToScoreFile;

        pathToScoreFolder = Application.dataPath + "/" + scoreFilePath;

        ReadScore();
	}
	
	
	void Update () {
	
	}

    /// <summary>
    /// Restarting the game
    /// </summary>
    public void Restart() {
        Application.LoadLevel(0);
    }


    /// <summary>
    /// Quitting the game
    /// </summary>
    public void Quit() {
        Application.Quit();
    }

    /// <summary>
    /// Saving the score
    /// </summary>
    public void SaveScore() {

        // It is just possible to save the data of the current session, 
        // if it is not yet saved
        if (!scoreYetSaved) {
            try {

                // Creating all directories to the endpath, where the score
                // shall be saved
                System.IO.Directory.CreateDirectory(pathToScoreFolder);

                StreamWriter sw = new StreamWriter(pathToScoreFile, false);

                // Getting the name and score of the current game session
                String name = nameTextfield.text;
                int score = (int)player.Score;

                // Adding the data of the current game-session to the list, which 
                // contains the data of the previous sessions
                scoreEntries.Add(new ScoreEntry(name, score));

                ScoreEntryList entryList = new ScoreEntryList();
                entryList.Entries = scoreEntries.ToArray();
                
                // Parsing the list with all game-seesions into a json-String
                // to write this string into a file.
                string scoreJSON = JsonConvert.SerializeObject(entryList);

                sw.Write(scoreJSON);
                sw.Close();

                // Hiding the save button.
                saveButton.SetActive(false);

            } catch (Exception e) {
                Debug.Log("Sorry, but your score could not be saved!");
            }
        }
    }

    /// <summary>
    /// Reading the data, from the file, of the previous game-sessions
    /// </summary>
    public void ReadScore() {

        try {
            StreamReader sr = new StreamReader(pathToScoreFile);

            String content = sr.ReadToEnd();

            sr.Close();

            // Parsing the data, from the score-file inso a list-object
            ScoreEntryList scoreEntryList = JsonConvert.DeserializeObject<ScoreEntryList>(content);

            scoreEntries = new List<ScoreEntry>(scoreEntryList.Entries);

            // Sorting the scoredata by the score, so that the highscore-points are 
            // listed in descending order 
            scoreEntries.Sort(new ScoreListComparer());

            // Building a string from the array-list
            StringBuilder scoreString = new StringBuilder();
            foreach (ScoreEntry scoreEntry in scoreEntries) {
                scoreString.Append(scoreEntry.Score + " " + scoreEntry.Name + "\n");
            }

            // Visualising the text on the provided text-object
            highscoreText.text = scoreString.ToString();

        } catch (Exception e) {
            Debug.Log(e.Message);
            Debug.Log("The score file could not be read!");
        }

        if (scoreEntries == null) {
            scoreEntries = new List<ScoreEntry>();
        }
    }
}

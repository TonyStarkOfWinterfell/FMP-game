using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HighscoreTable : MonoBehaviour
{

    public Transform entryContainer;
    public Transform entryTemplate;
        private List<HighscoreEntry> highscoreEntryList;
        private List<Transform> highscoreEntryTransformList;

    private ScoreCount score7;
    private inputWindow inWind2;

    public InputField inputText;
    public string saveText;
    public int saveScore;

    public GameObject input;


    private void Awake()
    {        
        entryTemplate.gameObject.SetActive(false);

        score7 = FindObjectOfType<ScoreCount>();
        inWind2 = FindObjectOfType<inputWindow>();
        
        highscoreEntryList = new List<HighscoreEntry>()
        {
            new HighscoreEntry{ score = 8, name = "AAA" },
            new HighscoreEntry{ score = 50, name = "CAR" },
            new HighscoreEntry{ score = 31, name = "DAN" },
            new HighscoreEntry{ score = 25, name = "LJR" },
            new HighscoreEntry{ score = 13, name = "GRE" },
        };

        

        //1 string jsonString = JsonUtility.ToJson(highscoreEntryList);
        //1 Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        

        // Sort entry list by Score
        for(int i = 0; i < highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscoreEntryList.Count; j++)
            {
                if (highscoreEntryList[j].score > highscoreEntryList[i].score)
                {
                    //swap
                    HighscoreEntry tmp = highscoreEntryList[i];
                    highscoreEntryList[i] = highscoreEntryList[j];
                    highscoreEntryList[j] = tmp;
                }
            }
        }


        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }

        //Highscores highscores = new Highscores { highscoreEntryList = highscoreEntryList };
        //string json = JsonUtility.ToJson(highscores);
        /*
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("highscoreTable"));
        */

    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        //leaderboard text placement
        float templateHeight = 24f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        //ranking titles
        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }
        entryTransform.Find("posText").GetComponent<Text>().text = rankString;

        int score = highscoreEntry.score;

        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("nameText").GetComponent<Text>().text = name;

        transformList.Add(entryTransform);
    }








    //Add entry - AddHighscoreEntry(50, "NeW"); -
    public void AddHighscoreEntry(int score, string name)
    {
        //create highscore entry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        //load saved highscore
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        //add new entry to highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        //save updated highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        //PlayerPrefs.Save();
    }












    //name input window actions
    public void Show()
    {
        inWind2.input.transform.position = new Vector3(inWind2.input.transform.position.x, inWind2.input.transform.position.y - 50, 0);
    }

    public void Hide()
    {
        inWind2.input.transform.position = new Vector3(inWind2.input.transform.position.x, inWind2.input.transform.position.y + 50, 0);
    }

    public void Confirm()
    {
        saveText = inputText.text;
        saveScore = (int)score7.scoreCount;
        AddHighscoreEntry(saveScore, saveText);
        /*
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        
        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
        */
        
        Hide();       
    }
        
        
        
    
    

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }


    /*
     *  Represents a single high score entry
     *   */

    [System.Serializable]
     private class HighscoreEntry
    {
        public int score;
        public string name;
    }
}

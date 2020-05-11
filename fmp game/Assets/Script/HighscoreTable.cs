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
                

        //loading - (y = x) -      
         Highscores highscoresL = new Highscores { highscoreEntryList = highscoreEntryList };


        //Sort entry list by Score
        for (int i = 0; i < highscoresL.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscoresL.highscoreEntryList.Count; j++)
            {
                if (highscoresL.highscoreEntryList[j].score > highscoresL.highscoreEntryList[i].score)
                {
                    //swap
                    HighscoreEntry tmp = highscoresL.highscoreEntryList[i];
                    highscoresL.highscoreEntryList[i] = highscoresL.highscoreEntryList[j];
                    highscoresL.highscoreEntryList[j] = tmp;
                }
            }
        }

        
        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscoresL.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }

    }          
        



    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntryG, Transform containerG, List<Transform> transformListG)
    {
        //leaderboard text placement
        float templateHeight = 24f;        
        Transform entryTransform = Instantiate(entryTemplate, containerG);        
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformListG.Count);
        entryTransform.gameObject.SetActive(true);

        //ranking titles
        int rank = transformListG.Count + 1;
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

        int score = highscoreEntryG.score;

        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

        string name = highscoreEntryG.name;
        entryTransform.Find("nameText").GetComponent<Text>().text = name;

        transformListG.Add(entryTransform);
    }
    


    //Add entry 
    public void AddHighscoreEntry(int score, string name)
    {
        //create highscore entry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        //loads saved highscore (string to list)        
        Highscores highscoresL = new Highscores { highscoreEntryList = highscoreEntryList };

        //add new entry to highscores (list)
        highscoreEntryList.Add(highscoreEntry);

        //save updated highscores (list to string)
        highscoresL = new Highscores { highscoreEntryList = highscoreEntryList };
        
        
        
        // Sort entry list by Score
        for (int i = 0; i < highscoresL.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscoresL.highscoreEntryList.Count; j++)
            {
                if (highscoresL.highscoreEntryList[j].score > highscoresL.highscoreEntryList[i].score)
                {
                    //swap
                    HighscoreEntry tmp = highscoresL.highscoreEntryList[i];
                    highscoresL.highscoreEntryList[i] = highscoresL.highscoreEntryList[j];
                    highscoresL.highscoreEntryList[j] = tmp;
                }
            }
        }
                
        
        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry2 in highscoresL.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry2, entryContainer, highscoreEntryTransformList);
        }

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
        deleteList();     
        AddHighscoreEntry(saveScore, saveText);

        Hide();       
    }

    public void deleteList()
    {
        foreach (GameObject gos in GameObject.FindGameObjectsWithTag("highscoreEntryTemplate"))
        {
            if (gos.name == "highscoreEntryTemplate(Clone)")
            {
                Destroy(gos);
            }
        }
    }


    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }
    
    
    //Represents a single high score entry   
    [System.Serializable]
     private class HighscoreEntry
    {
        public int score;
        public string name;
    }

}

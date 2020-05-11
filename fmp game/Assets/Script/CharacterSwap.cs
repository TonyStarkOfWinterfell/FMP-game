using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSwap : MonoBehaviour
{
    public GameObject onScreen;
    public GameObject offScreen;
    public GameObject fish;
    public GameObject jellyFish;
    public GameObject turtle;
    public GameObject penguin;
    private Vector3 Displayed;
    private Vector3 OffScreen;
    public int CharacterInt = 1;
    private SpriteRenderer fishRender, jellyFishRender, turtleRender, penguinRender;

    private bool fishBool;
    private bool jellyBool;
    private bool turtBool;
    private bool pengBool;
    private bool currentBool;
    private string currentString;

    public Text selectButton;

    public int SelectInt = 1;

    public AudioSource purchase;
    public AudioSource error;

    public PlayerTap PT;



    private void Awake()
    {
        SelectInt = 1;
        fishRender = fish.GetComponent<SpriteRenderer>();
        jellyFishRender = fish.GetComponent<SpriteRenderer>();
        turtleRender = fish.GetComponent<SpriteRenderer>();
        penguinRender = fish.GetComponent<SpriteRenderer>();

        fishBool = true;
        jellyBool = false;
        turtBool = false;
        pengBool = false;

        PT = FindObjectOfType<PlayerTap>();
    }

    public void Update()
    {
        Displayed = onScreen.transform.position;
        OffScreen = offScreen.transform.position;
    }

    public void NextCharacter()
    {
        switch (CharacterInt)
        {
            case 1:
                fishRender.enabled = false;
                fish.transform.position = OffScreen;
                jellyFish.transform.position = Displayed;
                jellyFishRender.enabled = true;

                CharacterInt++;
                currentBool = jellyBool;
                currentString = "jelly";
                if (jellyBool == false)
                {
                    selectButton.text = "Buy (3)";
                }
                else
                {
                    selectButton.text = "Select";
                }
                break;
            case 2:
                jellyFishRender.enabled = false;
                jellyFish.transform.position = OffScreen;
                turtle.transform.position = Displayed;
                turtleRender.enabled = true;

                CharacterInt++;
                currentBool = turtBool;
                currentString = "turtle";

                if (turtBool == false)
                {
                    selectButton.text = "Buy (3)";
                }
                else
                {
                    selectButton.text = "Select";
                }
                break;
            case 3:
                turtleRender.enabled = false;
                turtle.transform.position = OffScreen;
                penguin.transform.position = Displayed;
                penguinRender.enabled = true;

                CharacterInt ++;
                currentBool = pengBool;
                currentString = "penguin";
                if (pengBool == false)
                {
                    selectButton.text = "Buy (3)";
                }
                else
                {
                    selectButton.text = "Select";
                }
                break;
            case 4:
                penguinRender.enabled = false;
                penguin.transform.position = OffScreen;
                fish.transform.position = Displayed;
                fishRender.enabled = true;

                CharacterInt ++;
                currentBool = fishBool;
                currentString = "fish";
                if (fishBool == false)
                {
                    selectButton.text = "Buy (3)";
                }
                else
                {
                    selectButton.text = "Select";
                }

                ResetInt();
                break;
            default:
                ResetInt();
                break;
        }
    }


    


    public void PreviousCharacter()
    {
        switch (CharacterInt)
        {
            case 1:
                fishRender.enabled = false;
                fish.transform.position = OffScreen;
                penguin.transform.position = Displayed;
                penguinRender.enabled = true;      
                
                CharacterInt--;
                currentBool = pengBool;
                currentString = "penguin";
                if (pengBool == false)
                {
                    selectButton.text = "Buy (3)";
                }
                else
                {
                    selectButton.text = "Select";
                }

                ResetInt();
                break;
            case 2:
                jellyFishRender.enabled = false;
                jellyFish.transform.position = OffScreen;
                fish.transform.position = Displayed;
                fishRender.enabled = true;

                CharacterInt--;
                currentBool = fishBool;
                currentString = "fish";
                if (fishBool == false)
                {
                    selectButton.text = "Buy (3)";
                }
                else
                {
                    selectButton.text = "Select";
                }
                break;
            case 3:
                turtleRender.enabled = false;
                turtle.transform.position = OffScreen;
                jellyFish.transform.position = Displayed;
                jellyFishRender.enabled = true;

                CharacterInt--;
                currentBool = jellyBool;
                currentString = "jelly";
                if (jellyBool == false)
                {
                    selectButton.text = "Buy (3)";
                }
                else
                {
                    selectButton.text = "Select";
                }

                break;
            case 4:
                penguinRender.enabled = false;
                penguin.transform.position = OffScreen;
                turtle.transform.position = Displayed;
                turtleRender.enabled = true;

                CharacterInt--;
                currentBool = turtBool;
                currentString = "turtle";
                if (turtBool == false)
                {
                    selectButton.text = "Buy (3)";
                }
                else
                {
                    selectButton.text = "Select";
                }

                break;
            default:
                ResetInt();
                break;
        }
    }








    public void SetInt()
    {        
        if(currentBool == false)
        {
            if(PT.ScoreP >= 3)
            {
                PT.ScoreP = PT.ScoreP - 3;
                PT.scoredPoints.text = "" + Mathf.Round(PT.ScoreP);
                purchase.Play();
                selectButton.text = "Select";
                SelectInt = CharacterInt;
                currentBool = true;                

                switch (currentString)
                {
                    case "jelly":
                        jellyBool = true;
                        break;

                    case "turtle":
                        turtBool = true;                        
                        break;

                    case "penguin":
                        pengBool = true;
                        break;

                    case "fish":
                        fishBool = true;
                        break;

                    default:
                        fishBool = true;
                        break;
                }
            }

            else
            {
                error.Play();
            }
        }

        else
        {
            SelectInt = CharacterInt;
        }
             
    }



    private void ResetInt()
    {
        if (CharacterInt >= 4)
        {
            CharacterInt = 1;
        }
        else
        {
            CharacterInt = 4;
        }
    }

}

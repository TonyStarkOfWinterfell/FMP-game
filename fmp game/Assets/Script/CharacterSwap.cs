using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public int SelectInt = 1;

    private void Awake()
    {
        SelectInt = 1;
        fishRender = fish.GetComponent<SpriteRenderer>();
        jellyFishRender = fish.GetComponent<SpriteRenderer>();
        turtleRender = fish.GetComponent<SpriteRenderer>();
        penguinRender = fish.GetComponent<SpriteRenderer>();
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
                break;
            case 2:
                jellyFishRender.enabled = false;
                jellyFish.transform.position = OffScreen;
                turtle.transform.position = Displayed;
                turtleRender.enabled = true;
                CharacterInt++;
                break;
            case 3:
                turtleRender.enabled = false;
                turtle.transform.position = OffScreen;
                penguin.transform.position = Displayed;
                penguinRender.enabled = true;
                CharacterInt++;
                break;
            case 4:
                penguinRender.enabled = false;
                penguin.transform.position = OffScreen;
                fish.transform.position = Displayed;
                fishRender.enabled = true;
                CharacterInt++;
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
                ResetInt();
                break;
            case 2:
                jellyFishRender.enabled = false;
                jellyFish.transform.position = OffScreen;
                fish.transform.position = Displayed;
                fishRender.enabled = true;
                CharacterInt--;
                break;
            case 3:
                turtleRender.enabled = false;
                turtle.transform.position = OffScreen;
                jellyFish.transform.position = Displayed;
                jellyFishRender.enabled = true;
                CharacterInt--;
                break;
            case 4:
                penguinRender.enabled = false;
                penguin.transform.position = OffScreen;
                turtle.transform.position = Displayed;
                turtleRender.enabled = true;
                CharacterInt--;
                break;
            default:
                ResetInt();
                break;
        }
    }



    public void SetInt()
    {        
        SelectInt = CharacterInt;        
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

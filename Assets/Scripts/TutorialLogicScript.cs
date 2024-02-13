using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;

public class TutorialLogicScript : MonoBehaviour
{
    private int step = 0;
    [SerializeField]
    private TMP_Text TMP_text;

    // Update is called once per frame
    void Update()
    {
        checkActions();
        updateText();
    }

    void checkActions()
    {
        switch(step)
        {
            case 0:
                if (new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) != Vector2.zero)
                    step++;
                break;
            case 1:
                if (Input.GetKeyDown("left shift"))
                    step++;
                break;
            case 2:
                if (Input.GetKeyDown("space"))
                    step++;
                break;
            case 3:
                if (!GameObject.Find("Banana"))
                    step++;
                break;
            default:
                break;
        }
    }

    void updateText()
    {
        switch (step)
        {
            case 0:
                TMP_text.text = "Welcome in the tutorial of \"You shouldn't be here !\". You will learn everything you need ! First step: move with arrows or WASD.";
                break;
            case 1:
                TMP_text.text = "Well done ! Now, press Left Shift to hide. This is an important mechanic to prevent enemies from seeing you ! But be careful, the more you stay hide, the less you see !";
                break;
            case 2:
                TMP_text.text = "You can go in the 2nd room. There is a rhinoceros, it's an enemy ! Don't go in his field of view or you lose, but if you are hide, you are safe ! Press Space to continue";
                break;
            case 3:
                TMP_text.text = "Welcome in the 3rd room ! And there is what you come to get: this fabulous banana ! Go get it by pressing E !";
                break;
            case 4:
                TMP_text.text = "Run to the red spot and press E to end the tutorial, fast !";
                break;
            default:
                break;
        }
    }
}

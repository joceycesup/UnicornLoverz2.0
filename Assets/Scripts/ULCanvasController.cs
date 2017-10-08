using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULCanvasController : MonoBehaviour
{

    private static GameObject buttonHug;
    private static bool boolbuttonHug = false;

    // Use this for initialization
    void Start()
    {
        buttonHug = GameObject.FindWithTag("buttonHug");
        buttonHug.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (boolbuttonHug)
        {
            if (ULFollowerController.gaiCount != 0)
            {
                buttonHug.SetActive(false);
                boolbuttonHug = false;
            }
        }
    }

    public static void ActiveButtonHug()
    {
        if(!boolbuttonHug && ULFollowerController.gaiCount == 0)
        {
            boolbuttonHug = true;
            buttonHug.SetActive(true);
        }
    }

}

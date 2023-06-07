using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserTutorial : MonoBehaviour
{
    public GameObject[] tutorialSteps;
    private int currTutorialStep;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < tutorialSteps.Length; i++)
        {
           if (i == currTutorialStep) {
                tutorialSteps[currTutorialStep].SetActive(true);
           } else {
                tutorialSteps[i].SetActive(false);
           }
        }
        if (currTutorialStep == 0) {
            // show first tutorial thing

        }
        
    }

    public void UpdateTutorialStep(){
        currTutorialStep++;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserTutorial : MonoBehaviour
{
    public GameObject[] tutorialSteps;
    private int currTutorialStep;
    public GameObject exitButton;
    public GameObject nextButton;
    public GameObject restartButton;
    public GameObject[] brushes;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < brushes.Length; i++) {
            brushes[i].SetActive(true);
        }
        exitButton.SetActive(false);
        nextButton.SetActive(false);
        restartButton.SetActive(false);
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
    }

    public void StartTutorial(){
        for(int i = 0; i < brushes.Length; i++) {
            brushes[i].SetActive(false);
        }
        exitButton.SetActive(true);
        nextButton.SetActive(true);
        restartButton.SetActive(true);
    }

    public void EndTutorial() {
        for(int i = 0; i < brushes.Length; i++) {
            brushes[i].SetActive(true);
        }
        exitButton.SetActive(false);
        nextButton.SetActive(false);
        restartButton.SetActive(false);
        currTutorialStep = 0;
    }

    public void RestartTutorial() {
        currTutorialStep = 0;
    }

    public void UpdateTutorialStep(){
        currTutorialStep++;
    }
}
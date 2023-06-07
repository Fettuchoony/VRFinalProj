using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserTutorial : MonoBehaviour
{
    public GameObject[] tutorialSteps;
    public GameObject[] brushStyleScreenButton;
    public GameObject[] mainMenuButton;
    public GameObject[] tutorialMenuButton;
    private bool brushStyleScreen;

    private bool tutorial;
    private int currTutorialStep;

    // Start is called before the first frame update
    void Start()
    {
        brushStyleScreen = false;
        tutorial = false;
        
        MainMenu();
    }

   

    // Update is called once per frame
    void Update()
    {
        if (tutorial) {
            for (int i = 0; i < tutorialSteps.Length; i++)
            {
                if (i == currTutorialStep) {
                        tutorialSteps[currTutorialStep].SetActive(true);
                } else {
                        tutorialSteps[i].SetActive(false);
                }
            }
        }
    }

    public void BrushStyleScreen(){
            for (int i = 0; i < mainMenuButton.Length; i++) {
            mainMenuButton[i].SetActive(false);
        }

        for (int i = 0; i < tutorialMenuButton.Length; i++) {
            tutorialMenuButton[i].SetActive(false);
        }

        for (int i = 0; i < brushStyleScreenButton.Length; i++) {
            brushStyleScreenButton[i].SetActive(true);
        }
    }

    public void MainMenu() {
        tutorial = false;
        for (int i = 0; i < mainMenuButton.Length; i++) {
            mainMenuButton[i].SetActive(true);
        }

        for (int i = 0; i < tutorialMenuButton.Length; i++) {
            tutorialMenuButton[i].SetActive(false);
        }
        for (int i = 0; i < brushStyleScreenButton.Length; i++) {
            brushStyleScreenButton[i].SetActive(false);
        }

        currTutorialStep = 0;
    }

    public void StartTutorial(){
        tutorial = true;
        for (int i = 0; i < mainMenuButton.Length; i++) {
            mainMenuButton[i].SetActive(false);
        }

        for (int i = 0; i < tutorialMenuButton.Length; i++) {
            tutorialMenuButton[i].SetActive(true);
        }
    }

    public void EndTutorial() {
        MainMenu();
    }

    public void RestartTutorial() {
        currTutorialStep = 0;
    }

    public void UpdateTutorialStep(){
        currTutorialStep++;
    }
}
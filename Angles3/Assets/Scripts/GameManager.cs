using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { set; get; }
    public GameObject mainMenu;
    public GameObject gameMenu;
    public GameObject startMenu;
    public GameObject gameRules;
    public CheckerBoard checkerboard;
    public bool isCrossJump=true;
    public bool isLineJump=true;
    public GameObject[] pieces;



    public void ToggleLineJump(bool value)
    {
        isLineJump = value;
        foreach(GameObject I in  pieces)
        {
            I.GetComponent<Piece>().isLineJumpUse = value;
        }
    }


    public void ToggleCrossJump(bool value)
    {
        foreach (GameObject I in pieces)
        {
            I.GetComponent<Piece>().isCrossJumpUse = value;
        }
    }





    public void MainMenu()
    {
        mainMenu.SetActive(true);
    }

    public void GameMenu()
    {
       gameMenu.SetActive(true);
       startMenu.SetActive(false);
       mainMenu.SetActive(false);
       gameRules.SetActive(false);

    }

    public void Resume()
    {
        mainMenu.SetActive(false);
    }

    public void StartMenu()
    {
        startMenu.SetActive(true);
        gameMenu.SetActive(false);
        mainMenu.SetActive(false);
        gameRules.SetActive(false);
        
    }

  
    public void Restart()
    {
        SceneManager.LoadScene(0);
        StartMenu();
        
    }

    void Start()
    {
        Instance = this;
        pieces = GameObject.FindGameObjectsWithTag("Pieces");
        DontDestroyOnLoad(gameObject);
        gameMenu.SetActive(false);
        mainMenu.SetActive(false);
        startMenu.SetActive(true);
        checkerboard=GetComponent<CheckerBoard>();
       
    }

    public void AgreeRules()
    {
        startMenu.SetActive(true);
        gameMenu.SetActive(false);
        mainMenu.SetActive(false);
        gameRules.SetActive(false);
    }
    public void GameRules()
    {
        startMenu.SetActive(false);
        gameMenu.SetActive(false);
        mainMenu.SetActive(false);
        gameRules.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
}

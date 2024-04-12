using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver
}
public enum GameLevel
{
    NoneGameLevel,
    Level1,
    Level2,
    Level3,
}

public class GameControl : SingletonDontDestory<GameControl>
{
    public GameState gameState;
    public GameLevel gameLevel;
    // Start is called before the first frame update
    void Start()
    {
        // gameState = GameState.MainMenu;
        // gameLevel = GameLevel.NoneGameLevel;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadNextLevel()
    {
        switch (gameLevel)
        {
            case GameLevel.Level1:
                gameLevel = GameLevel.Level2;
                SceneManager.LoadScene("GameLevel2");
                break;
            case GameLevel.Level2:
                gameLevel = GameLevel.Level3;
                SceneManager.LoadScene("GameLevel3");
                break;
            case GameLevel.Level3:
                ShowWinPanel();
                break;
        }
    }
    void ShowWinPanel()
    {

    }
}

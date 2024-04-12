using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class UIMain : MonoBehaviour
{

    public GameObject panelGamestart;
    public GameObject buttonGamestart;
    public GameObject buttonSetting;
    public GameObject buttonHelp;
    public GameObject buttonExit;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void GameStart()
    {
        GameControl.GetInstance().gameState = GameState.Playing;
        GameControl.GetInstance().gameLevel = GameLevel.Level1;
        SceneManager.LoadScene("Gaming_hzf");
    }
    public void GameSetting(GameObject Settingpanel)
    {
        Settingpanel.SetActive(true);
        panelGamestart.SetActive(false);
    }
    public void GameHelp(GameObject Helppanel)
    {
        SceneManager.LoadScene("Dialogue");
    }
    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

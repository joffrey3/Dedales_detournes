using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject Home;
    public GameObject Victory;
    public GameObject Defeat;
    public GameObject Command;
    public AudioSource AudioButton;
    public enum MenuScreen {Home,Victory,Defeat,Command};
    public static MenuScreen RequiredScreen { get; set; } = MenuScreen.Home;
    private void Start()
    {
        Home.SetActive(RequiredScreen==MenuScreen.Home);
        Victory.SetActive(RequiredScreen == MenuScreen.Victory);
        Defeat.SetActive(RequiredScreen == MenuScreen.Defeat);
        Command.SetActive(RequiredScreen == MenuScreen.Command);
    }
    public void ButtonEvent_Play()
    {
        AudioButton.Play();
        SceneManager.LoadScene("MainScene");
    }
    public void ButtonEvent_Home()
    {
        AudioButton.Play();
        Home.SetActive(true);
        Victory.SetActive(false);
        Defeat.SetActive(false);
        Command.SetActive(false);
    }
    public void ButtonEvent_Command()
    {
        AudioButton.Play();
        Home.SetActive(false);
        Victory.SetActive(false);
        Defeat.SetActive(false);
        Command.SetActive(true);
    }
    public void ButtonEvent_Retry()
    {
        AudioButton.Play();
        SceneManager.LoadScene("MainScene");
    }
    public void ButtonEvent_Quit()
    {
        AudioButton.Play();
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameScreen : MonoBehaviour
{
    public GameObject Exploration;
    public GameObject Combat;
    public enum MenuScreen { Exploration, Combat };
    public static MainGameScreen Instance { private set; get; }
    
    static MenuScreen _screen = MenuScreen.Exploration;
    public static MenuScreen RequiredScreen
    {
        get => _screen;
        set
        {
            if (value == _screen)
                return;
            _screen = value;
            Instance.Exploration.SetActive(RequiredScreen == MenuScreen.Exploration);
            Instance.Combat.SetActive(RequiredScreen == MenuScreen.Combat);
        }
    }
    private void Start()
    {
        Instance = this;
        Exploration.SetActive(RequiredScreen == MenuScreen.Exploration);
        Combat.SetActive(RequiredScreen == MenuScreen.Combat);
    }

    private void OnDestroy()
    {
        Instance = null;
    }
    /*private void Update()
    {
        Exploration.SetActive(RequiredScreen == MenuScreen.Exploration);
        Combat.SetActive(RequiredScreen == MenuScreen.Combat);
    }*/
}

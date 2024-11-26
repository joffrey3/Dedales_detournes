using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestOpen : MonoBehaviour
{
    Animator m_Animator;

    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public AudioSource audiosource;

    bool m_IsPlayerAtExit;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            m_Animator.SetBool("IsOpen", true);
            m_IsPlayerAtExit = true;
        }
    }
    void Update()
    {
        if (m_IsPlayerAtExit)
        {
            EndLevel();
        }
    }
    void EndLevel()
    {
        audiosource.Play();
        PlayerPV.setPVmax(0);
        MainMenu.RequiredScreen = MainMenu.MenuScreen.Victory;
        SceneManager.LoadScene(0);
    }
}

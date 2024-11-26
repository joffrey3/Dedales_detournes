using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerEnnemyFight : MonoBehaviour
{
    Animator m_Animator;
    public EnnemyFight script;
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public AudioSource audiosource;

    bool m_IsPlayerCaught;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            m_IsPlayerCaught = true;
        }
    }
    void Update()
    {
        if (m_IsPlayerCaught)
        {
            FightBegin();
        }
    }

    public int GetNb()
    {
        if (this.gameObject.name == "Slime1" || this.gameObject.name == "Slime2")
        {
            return 1;
        }
        if (this.gameObject.name == "Mushroom1" || this.gameObject.name == "Mushroom2")
        {
            return 2;
        }
        if (this.gameObject.name == "TurtleShell1" || this.gameObject.name == "TurtleShell2")
        {
            return 3;
        }
        if (this.gameObject.name == "Cactus1" || this.gameObject.name == "Cactus2")
        {
            return 4;
        }
        if (this.gameObject.name == "Beholder1" || this.gameObject.name == "Beholder2")
        {
            return 5;
        }
        if (this.gameObject.name == "ChestMonster1" || this.gameObject.name == "ChestMonster2")
        {
            return 6;
        }
        if (this.gameObject.name == "MaleCharacterPBR")
        {
            return 7;
        }
        return 0;
    }

    void FightBegin()
    {
        audiosource.Play();
        this.gameObject.SetActive(false);
        script.SetEnnemy_type(GetNb());
        MainGameScreen.RequiredScreen = MainGameScreen.MenuScreen.Combat;
    }
}

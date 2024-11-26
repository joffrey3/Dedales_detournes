using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnnemyFight : MonoBehaviour
{
    public GameObject[] All_Ennemy = new GameObject[7];
    public EnnemyPattern[] All_Ennemy_Pattern = new EnnemyPattern[7];
    GameObject Ennemy_encounter;
    EnnemyPattern Ennemy_pattern_encounter;
    int Ennemy_type=1;
    static bool Ennemy_turn = false;
    static bool IsEndFight = false;

    static int position_base;
    float sleep = 1f;
    bool action = false;
    new Transform transform;
    CharacterController m_Character;
    Animator m_Animator;

    void OnEnable()
    {
        GenerateEnnemy();
    }

    public void SetEnnemy_type(int nb)
    {
        Ennemy_type = nb;
    }

    public void GenerateEnnemy()
    {
        for (int i = 1; i <= 7; i++)
        {
            if (Ennemy_type == i)
            {
                Ennemy_encounter = All_Ennemy[i - 1];
                Ennemy_pattern_encounter = All_Ennemy_Pattern[i - 1];
                All_Ennemy[i - 1].SetActive(true);
            }
            else
            {
                All_Ennemy[i - 1].SetActive(false);
            }
        }
        m_Animator = Ennemy_encounter.GetComponent<Animator>();
        m_Character = Ennemy_encounter.GetComponent<CharacterController>();
        position_base = 5;
        m_Animator.SetBool("IsFight", true);
        Ennemy_pattern_encounter.InitiatePV();
        IsEndFight = false;
        Ennemy_pattern_encounter.PreviousNextAction();
    }

    public static void SetPosition(int new_position)
    {
        position_base = new_position;
    }

    public static int GetPosition()
    {
        return position_base;
    }

    public static void ChangeTurn()
    {
        Ennemy_turn = !Ennemy_turn;
    }

    void Default()
    {
        m_Animator.SetBool("IsAttack", false);
        m_Animator.SetBool("IsMoveFW", false);
        m_Animator.SetBool("IsMoveBW", false);
    }

    void Action()
    {
        string text = Ennemy_pattern_encounter.Action();
        if (text == "IsMoveFW" && position_base!=0  && PlayerFight.GetPosition()!=position_base-1)
        {
            m_Animator.SetBool(text, true);
            SetPosition(position_base - 1);
        }
        if (text == "IsMoveBW" && position_base != 5)
        {
            m_Animator.SetBool(text, true);
            SetPosition(position_base + 1);
        }
        if(text == "IsAttack")
        {
            m_Animator.SetBool(text, true);
            Ennemy_pattern_encounter.Attack();
        }
        action = true;
        sleep += Time.time;
    }

    private void Update()
    {
        if (PlayerPV.getPV() <= 0 || EnnemyPV.getPV() <= 0 && PlayerFight.EndFight()  && !IsEndFight)
        {
            for (int i = 0; i < 6; i++)
            {
                Ennemy_pattern_encounter.SetZoneColor(i, "white");
            }
            IsEndFight = true;
            Transform m_transform = m_Character.gameObject.GetComponent<Transform>();
            m_transform.Translate(Vector3.back * (5-position_base));
            SetPosition(5);
            if (PlayerPV.getPV() <= 0)
            {
                PlayerPV.setPVmax(0);
                MainGameScreen.RequiredScreen = MainGameScreen.MenuScreen.Exploration;
                MainMenu.RequiredScreen = MainMenu.MenuScreen.Defeat;
                SceneManager.LoadScene("BeginAndEndScene");
            }
            if (EnnemyPV.getPV() <= 0)
            {
                MainGameScreen.RequiredScreen = MainGameScreen.MenuScreen.Exploration;
            }
        }
        if (Ennemy_turn)
        {
            if (!action)
            {
                Action();
            }
            if (action && Time.time > sleep)
            {
                Default();
                action = false;
                sleep = 1f;
                Ennemy_pattern_encounter.PreviousNextAction();
                PlayerFight.setDefenseMode(false);
                ChangeTurn();
                PlayerFight.ChangeTurn();
            }
        }
    }
}

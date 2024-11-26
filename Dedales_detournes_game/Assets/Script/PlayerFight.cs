using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFight : MonoBehaviour
{
    public AudioSource Move_audio;
    public AudioSource Attack_audio;
    public AudioSource Defend_audio;
    static int position_base;
    CharacterController m_Character;
    new Transform transform;
    Animator m_Animator;
    float sleep =1f;
    bool action = false;
    static bool PlayerTurn = true;
    static bool DefenseMode = false;
    static bool IsEndFight = false;
    void OnEnable()
    {
        transform = GetComponent<Transform>();
        position_base = 0;
        m_Animator = GetComponent<Animator>();
        m_Character = GetComponent<CharacterController>();
        m_Animator.SetBool("IsFight",true);
        IsEndFight = false;
        action = false;
    }


    void Update()
    {
        if (PlayerPV.getPV() <= 0 || EnnemyPV.getPV() <= 0 && !IsEndFight)
        {
            Transform m_transform = m_Character.gameObject.GetComponent<Transform>();
            m_transform.Translate(Vector3.back * position_base);
            SetPosition(0);
            IsEndFight = true;
            sleep = 1f;
        }
        if (PlayerTurn)
        {
            if (Input.GetKeyUp(KeyCode.Q) && !action)
            {
                Attack_audio.Play();
                Action("IsAttack");
                if (EnnemyFight.GetPosition() == PlayerFight.GetPosition() + 1) {
                    EnnemyPV.setPV(EnnemyPV.getPV() - 10);
                }
            }
            if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D) && !action)
            {
                if (position_base != 5 && EnnemyFight.GetPosition() != position_base + 1)
                {
                    Move_audio.Play();
                    Action("IsMoveFW");
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A) && !action)
            {
                if (position_base != 0)
                {
                    Move_audio.Play();
                    Action("IsMoveBW");
                }
            }
            if (Input.GetKeyUp(KeyCode.E) && !action)
            {
                Defend_audio.Play();
                Action("IsDefend");
                DefenseMode = true;
            }
            if (action && Time.time > sleep)
            {
                Default();
                action = false;
                sleep = 1f;
                ChangeTurn();
                EnnemyFight.ChangeTurn();
            }
        }
    }

    public static bool getDefenseMode()
    {
        return DefenseMode;
    }
    public static void setDefenseMode(bool mode)
    {
        DefenseMode= mode;
    }
    static void SetPosition(int new_position)
    {
        position_base = new_position;
    }
    public static int GetPosition()
    {
        return position_base;
    }

    void Action(string text)
    {
        if (text== "IsMoveFW")
        {
            SetPosition(position_base + 1);
        }
        if (text == "IsMoveBW")
        {
            SetPosition(position_base - 1);
        }
        m_Animator.SetBool(text, true);
        action = true;
        sleep += Time.time;
    }

    void Default()
    {
        Attack_audio.Stop();
        Move_audio.Stop();
        Defend_audio.Stop();
        m_Animator.SetBool("IsAttack", false);
        m_Animator.SetBool("IsMoveFW", false);
        m_Animator.SetBool("IsMoveBW", false);
        m_Animator.SetBool("IsDefend", false);
    }

    private void OnAnimatorMove()
    {
        if (m_Animator.GetBool("IsMoveFW")) { 
            m_Character.SimpleMove(Vector3.right);
        }
        if (m_Animator.GetBool("IsMoveBW"))
        {
            m_Character.SimpleMove(Vector3.left);
        }
    }
    public static void ChangeTurn()
    {
        PlayerTurn=!PlayerTurn;
    }

    public static bool EndFight()
    {
        return IsEndFight;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnnemyPattern : MonoBehaviour
{
    public GameObject[] Zones = new GameObject[6];
    public Material[] Material = new Material[10];
    CharacterController m_Character;
    Animator m_Animator;
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Character = GetComponent<CharacterController>();
    }

    abstract public string Action();

    abstract public void InitiatePV();

    abstract public void Attack();

    abstract public void PreviousNextAction();

    private void OnAnimatorMove()
    {
        if (m_Animator.GetBool("IsMoveBW"))
        {
            m_Character.SimpleMove(Vector3.right);
        }
        if (m_Animator.GetBool("IsMoveFW"))
        {
            m_Character.SimpleMove(Vector3.left);
        }
    }

    public void SetZoneColor(int zone, string couleur="white")
    {
        GameObject ChangeColorZone = Zones[zone];
        int ChangeColor=4;
        string[] List_Color = {"green", "red", "purple", "black", "white"};
        for(int i = 0; i < 5; i++)
        {
            if (List_Color[i]==couleur)
            {
                ChangeColor = i;
            }
        }
        MeshRenderer[] Textures = ChangeColorZone.GetComponentsInChildren<MeshRenderer>();
        for(int i= 0; i < Textures.Length; i++) {
            if (Textures[i].material.name != "BordureNoir")
            {
                if (Textures[i].gameObject.name == "ZoneOpaque")
                {
                    Textures[i].material = Material[ChangeColor + 5];
                }
                else
                {
                    Textures[i].material = Material[ChangeColor];
                }
            }
        }
    }
}

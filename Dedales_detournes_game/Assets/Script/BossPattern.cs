using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern : EnnemyPattern
{
    /*public CharacterController m_Ennemy_Character;
    public CharacterController m_Player_Character;
    Transform m_Ennemy_transform;
    Transform m_Player_transform;*/

    int phase = 1;
    int etape = -1;
    string action = "";
    int[] length_phase = { 6, 10, 13, 15 };
    int[] setup_phase_1 = { 0, 3, 0, 2, 3, 0, 4};
    int[] setup_phase_2 = { 0, 3, 0, 3, 2, 1, 4};
    bool is_setup_end = false;
    bool transition_phase = false;
    int setup_count = -1;
    int[] tab_phase_1 =
    {
        0, 3, 0, 0, 0,
        1, 3, 0, 0, 0,
        2, 3, 0, 0, 0,
        3, 0, 0, 0, 0,
        3, 1, 0, 0, 0,
        3, 2, 0, 0, 0,
    };
    int[] tab_phase_2 =
    {
        0, 0, 0, 3, 0,
        2, 2, 2, 3, 0,
        0, 0, 3, 3, 0,
        2, 2, 3, 3, 0,
        0, 3, 0, 3, 0,
        2, 3, 2, 3, 0,
        3, 0, 0, 3, 0,
        3, 2, 2, 3, 0,
        0, 0, 3, 0, 0,
        2, 2, 3, 1, 0,
    };
    int[] tab_phase_3 =
    {
        0, 0, 0, 0, 3,
        0, 0, 0, 3, 3,
        0, 0, 3, 0, 3,
        0, 3, 0, 0, 3,
        3, 0, 0, 0, 3,
        0, 3, 0, 3, 3,
        0, 0, 3, 3, 3,
        3, 0, 0, 3, 3,
        0, 3, 3, 0, 3,
        3, 0, 3, 0, 3,
        3, 3, 0, 0, 3,
        3, 0, 0, 3, 0,
        0, 3, 0, 3, 1,
    };
    int[] tab_last_hp =
    {
        0, 0, 0, 0, 3,
        0, 0, 0, 3, 3,
        0, 0, 3, 3, 3,
        0, 3, 3, 3, 3,
        3, 0, 3, 3, 3,
        0, 3, 3, 3, 3,
        3, 0, 3, 3, 3,
        3, 3, 0, 3, 3,
        3, 0, 3, 3, 3,
        3, 3, 0, 3, 3,
        3, 3, 3, 0, 3,
        3, 3, 0, 3, 3,
        3, 3, 3, 0, 3,
        3, 3, 3, 3, 0,
        3, 3, 3, 3, 3,
    };
    /*private void Start()
    {
        m_Ennemy_transform = m_Ennemy_Character.gameObject.GetComponent<Transform>();
        m_Player_transform = m_Player_Character.gameObject.GetComponent<Transform>();
    }*/


    public override string Action()
    {
        cycle(true);
        if ((EnnemyPV.getPV() == 200 || EnnemyPV.getPV() == 100 || EnnemyPV.getPV() == 10) && transition_phase)
        {
            
            /*m_Ennemy_transform.Translate(Vector3.back * (5 - EnnemyFight.GetPosition()));
            EnnemyFight.SetPosition(5);
            m_Player_transform.Translate(Vector3.back * (PlayerFight.GetPosition()));
            EnnemyFight.SetPosition(0);*/
            if (EnnemyPV.getPV() == 200){
                phase = 2;
                is_setup_end = false;
            }
            else if (EnnemyPV.getPV() == 100)
            {
                phase = 3;
            }
            else if (EnnemyPV.getPV() == 10){
                phase = 4;
            }
        }
        if (!is_setup_end)
        {
            print(action);
            return action;
        }
        transition_phase = true;
        return "IsAttack";
    }

    public override void InitiatePV()
    {
        EnnemyPV.setPVmax(300);
    }

    public override void Attack()
    {
        int player_position = -1;
        if (phase == 1)
        {
            if (is_setup_end)
            {

                print(tab_phase_1[etape * 5 + PlayerFight.GetPosition()]);
                player_position = tab_phase_1[etape * 5 + PlayerFight.GetPosition()];
            }
            else if ((PlayerFight.GetPosition() == EnnemyFight.GetPosition() - 1 && setup_phase_1[setup_count] == 2) 
                || (PlayerFight.GetPosition() == EnnemyFight.GetPosition() - 1 && setup_phase_1[setup_count] == 3)
                || (PlayerFight.GetPosition() == EnnemyFight.GetPosition() - 2 && setup_phase_1[setup_count] == 3)
                || (PlayerFight.GetPosition() == 0 && setup_phase_1[setup_count] == 4))
            {
                set_count_Attack(3);
            }
            else if (PlayerFight.GetPosition() == 1 && setup_phase_1[setup_count] == 4)
            {
                set_count_Attack(1);
            }
        }
        if (phase == 2)
        {
            if (is_setup_end)
            {
                player_position = tab_phase_2[etape * 5 + PlayerFight.GetPosition()];
            }
            else if ((PlayerFight.GetPosition() == EnnemyFight.GetPosition() - 1 && setup_phase_2[setup_count] == 2)
                || (PlayerFight.GetPosition() == EnnemyFight.GetPosition() - 1 && setup_phase_2[setup_count] == 3)
                || (PlayerFight.GetPosition() == EnnemyFight.GetPosition() - 2 && setup_phase_2[setup_count] == 3)
                || (PlayerFight.GetPosition() == 0 && setup_phase_2[setup_count] == 4))
            {
                set_count_Attack(3);
            }
            else if (PlayerFight.GetPosition() == 1 && setup_phase_2[setup_count] == 4)
            {
                set_count_Attack(1);
            }
        }
        if (phase == 3)
        {
            player_position = tab_phase_3[etape * 5 + PlayerFight.GetPosition()];
        }
        if (phase == 4)
        {
            player_position = tab_last_hp[etape * 5 + PlayerFight.GetPosition()];
        }
        if (is_setup_end)
        {
            set_count_Attack(player_position);
        }
        etape = (etape + 1) % length_phase[phase - 1];
    }
    public void set_count_Attack(int player_position)
    {
        if (player_position != 0)
        {
            if (player_position == 3 || (player_position == 2 && !PlayerFight.getDefenseMode()))
            {
                PlayerPV.setPV(PlayerPV.getPV() - PlayerPV.getPV());
            }
            else if (!PlayerFight.getDefenseMode())
            {
                PlayerPV.setPV(PlayerPV.getPV() - 10);
            }
        }
    }

    public override void PreviousNextAction()
    {
        if (setup_count == 6)
        {
            setup_count = -1;
            is_setup_end = true;
        }
        if (!is_setup_end)
        {
            etape = 0;
            set_phase();
        }
        else if (EnnemyPV.getPV() == 100)
        {
            etape = 0;
        }
        else
        {
            cycle(false);
        }

    }

    public void cycle(bool is_action)
    {
        if (phase == 1)
        {
            for(int i = 0; i < 5; i++)
            {
                set_zone_color_int(i,tab_phase_1[etape * 5 + i], is_action);
            }
        }
        if (phase == 2)
        {
            for (int i = 0; i < 5; i++)
            {
                set_zone_color_int(i,tab_phase_2[etape * 5 + i], is_action);
            }
        }
        if (phase == 3)
        {
            for (int i = 0; i < 5; i++)
            {
                set_zone_color_int(i,tab_phase_3[etape * 5 + i], is_action);
            }
        }
        if (phase == 4)
        {
            for (int i = 0; i < 5; i++)
            {
                set_zone_color_int(i, tab_last_hp[etape * 5 + i], is_action);
            }
        }
    }

    public void set_zone_color_int(int position, int couleur, bool clean_zone)
    {
        if (clean_zone)
        {
            base.SetZoneColor(position, "white");
        }
        else
        {
            if (couleur == 0)
            {
                base.SetZoneColor(position, "white");
            }
            if (couleur == 1)
            {
                base.SetZoneColor(position, "red");
            }
            if (couleur == 2)
            {
                base.SetZoneColor(position, "purple");
            }
            if (couleur == 3)
            {
                base.SetZoneColor(position, "black");
            }
        }
    }

    public void set_phase()
    {
        if (phase == 1)
        {
            setup_count = (setup_count + 1) % setup_phase_1.Length;
            action = get_action_in_set_phase(setup_phase_1);
        }
        if (phase == 2)
        {
            setup_count = (setup_count + 1) % setup_phase_2.Length;
            action = get_action_in_set_phase(setup_phase_2);
        }
        if ((phase == 1 && setup_phase_1[setup_count] == 0) || (phase == 2 && setup_phase_2[setup_count] == 0))
        {
            base.SetZoneColor(EnnemyFight.GetPosition() - 1, "green");
        }
        if ((phase == 1 && setup_phase_1[setup_count] == 1) || (phase == 2 && setup_phase_2[setup_count] == 1))
        {
            base.SetZoneColor(EnnemyFight.GetPosition() + 1, "green");
        }
        if ((phase == 1 && setup_phase_1[setup_count] == 2) || (phase == 2 && setup_phase_2[setup_count] == 2))
        {
            base.SetZoneColor(EnnemyFight.GetPosition() - 1, "black");
        }
        if ((phase == 1 && setup_phase_1[setup_count] == 3) || (phase == 2 && setup_phase_2[setup_count] == 3))
        {
            base.SetZoneColor(EnnemyFight.GetPosition() - 1, "black");
            base.SetZoneColor(EnnemyFight.GetPosition() - 2, "black");
        }
        if ((phase == 1 && setup_phase_1[setup_count] == 4) || (phase == 2 && setup_phase_2[setup_count] == 4))
        {
            base.SetZoneColor(PlayerFight.GetPosition() - 1, "red");
            base.SetZoneColor(PlayerFight.GetPosition(), "black");
            base.SetZoneColor(PlayerFight.GetPosition() + 1, "red");
        }
    }

    public string get_action_in_set_phase(int[] tab)
    {
        if (tab[setup_count] == 0)
        {
            return "IsMoveFW";
        }
        else if (tab[setup_count] == 1)
        {
            return "IsMoveBW";
        }
        else
        {
            return "IsAttack";
        }
    }

    /*void OnAnimatorMove()
    {
        if (m_Animator.GetBool("IsMoveBW"))
        {
            m_Character.SimpleMove(Vector3.right);
        }
        if (m_Animator.GetBool("IsMoveFW"))
        {
            m_Character.SimpleMove(Vector3.left);
        }
    }*/
}

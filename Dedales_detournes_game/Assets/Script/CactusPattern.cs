using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusPattern : EnnemyPattern
{
    bool IsPurple = false;
    int last_position_player = 0;
    int inc = 0;
    int attack_position = 0;
    public override string Action()
    {
        base.SetZoneColor(attack_position, "white");
        return "IsAttack";
    }

    public override void InitiatePV()
    {
        EnnemyPV.setPVmax(50);
    }

    public override void Attack()
    {
        if (PlayerFight.GetPosition() == attack_position && !PlayerFight.getDefenseMode())
        {
            if (IsPurple) 
            {
                PlayerPV.setPV(PlayerPV.getPV() - PlayerPV.getPV());
            }
            PlayerPV.setPV(PlayerPV.getPV() - 10);
        }
    }

    public override void PreviousNextAction()
    {
        if (PlayerFight.GetPosition() == last_position_player)
        {
            IsPurple = !IsPurple;
        }
        else
        {
            IsPurple = true;
        }
        if (PlayerFight.GetPosition() == 0)
        {
            attack_position = 0;
            if (IsPurple)
            {
                base.SetZoneColor(0, "purple");
            }
            else
            {
                base.SetZoneColor(0, "red");
            }
        }
        else if (PlayerFight.GetPosition() == EnnemyFight.GetPosition() - 1)
        {
            attack_position = 4;
            if (IsPurple)
            {
                base.SetZoneColor(EnnemyFight.GetPosition() - 1, "purple");
            }
            else
            {
                base.SetZoneColor(EnnemyFight.GetPosition() - 1, "red");
            }
        }
        else
        {
            attack_position = PlayerFight.GetPosition();
            if (inc < 2)
            {
                attack_position += 1;
                if (IsPurple)
                {
                    base.SetZoneColor(PlayerFight.GetPosition() + 1, "purple");
                }
                else
                {
                    base.SetZoneColor(PlayerFight.GetPosition() + 1, "red");
                }
                inc++;
            }
            else
            {
                base.SetZoneColor(PlayerFight.GetPosition(), "purple");
                inc = 0;
            }
        }
        last_position_player = PlayerFight.GetPosition();
    }
}

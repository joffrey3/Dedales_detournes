using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderPattern : EnnemyPattern
{
    int[] attack_range = { -1, -1, -1 };
    int attack_turn = 0;
    int position_turn = 5;
    public override string Action()
    {
        if (position_turn != 0)
        {
            base.SetZoneColor(attack_range[0], "white");
        }
        base.SetZoneColor(attack_range[1], "white");
        if (position_turn != 5)
        {
            base.SetZoneColor(attack_range[2], "white");
        }
        return "IsAttack";
    }

    public override void InitiatePV()
    {
        EnnemyPV.setPVmax(60);
    }

    public override void Attack()
    {
        if ((PlayerFight.GetPosition() == attack_range[0] || PlayerFight.GetPosition() == attack_range[1] || PlayerFight.GetPosition() == attack_range[2]) && !PlayerFight.getDefenseMode())
        {
            if (attack_turn == 2 || (attack_turn == 1 && PlayerFight.GetPosition() == position_turn))
            {
                PlayerPV.setPV(PlayerPV.getPV() - PlayerPV.getPV());
            }
            else
            {
                PlayerPV.setPV(PlayerPV.getPV() - 10);
            }
        }
        attack_turn = (attack_turn + 1) % 3;
    }

    public override void PreviousNextAction()
    {
        position_turn--;
        if (position_turn <= PlayerFight.GetPosition() - 2 || position_turn == -1 || position_turn >= EnnemyFight.GetPosition() - 1)
        {
            position_turn = EnnemyFight.GetPosition() - 1;
        }
        attack_pattern(position_turn);
    }

    public void attack_pattern(int center)
    {
        attack_range[0] = center - 1;
        attack_range[1] = center;
        attack_range[2] = center + 1;
        if (position_turn!=0)
        {
            if (attack_turn == 2)
            {
                base.SetZoneColor(center - 1, "purple");
            }
            else
            {
                base.SetZoneColor(center - 1, "red");
            }
        }
        if (position_turn != 5)
        {
            if (attack_turn == 2)
            {
                base.SetZoneColor(center + 1, "purple");
            }
            else
            {
                base.SetZoneColor(center + 1, "red");
            }
        }
        if (attack_turn != 0)
        {
            base.SetZoneColor(center, "purple");
        }
        else
        {
            base.SetZoneColor(center, "red");
        }
    }
}

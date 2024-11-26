using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePattern : EnnemyPattern
{
    public override string Action()
    {
        if (EnnemyFight.GetPosition() <= 3)
        {
            base.SetZoneColor(EnnemyFight.GetPosition() - 1, "white");
            return "IsAttack";
        }
        base.SetZoneColor(EnnemyFight.GetPosition()-1, "white");
        return "IsMoveFW";
    }

    public override void InitiatePV()
    {
        EnnemyPV.setPVmax(20);
    }

    public override void Attack()
    {
        if (PlayerFight.GetPosition() == EnnemyFight.GetPosition() - 1 && !PlayerFight.getDefenseMode())
        {
            PlayerPV.setPV(PlayerPV.getPV()-10);
        }
    }

    public override void PreviousNextAction()
    {
        if (EnnemyFight.GetPosition() <= 3)
        {
            base.SetZoneColor(EnnemyFight.GetPosition() - 1, "red");
        }
        else
        {
            base.SetZoneColor(EnnemyFight.GetPosition() - 1, "green");
        }

    }
}

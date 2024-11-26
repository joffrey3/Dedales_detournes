using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomPattern : EnnemyPattern
{
    bool IsPurple=false;
    string previous_action = "";

    public override string Action()
    {
        if (EnnemyFight.GetPosition() <= 3)
        {
            base.SetZoneColor(EnnemyFight.GetPosition() - 1, "white");
            previous_action = "IsAttack";
            return "IsAttack";
        }
        base.SetZoneColor(EnnemyFight.GetPosition() - 1, "white");
        previous_action = "IsMoveFW";
        return "IsMoveFW";
    }

    public override void InitiatePV()
    {
        EnnemyPV.setPVmax(30);
    }

    public override void Attack()
    {
        if (PlayerFight.GetPosition() == EnnemyFight.GetPosition() - 1 && !PlayerFight.getDefenseMode())
        {
            if (IsPurple)
            {
                PlayerPV.setPV(PlayerPV.getPV() - PlayerPV.getPV());
            }
            else
            {
                PlayerPV.setPV(PlayerPV.getPV() - 10);
            }
        }
    }

    public override void PreviousNextAction()
    {
        if (EnnemyFight.GetPosition() <= 3)
        {
            if (previous_action=="IsAttack" && !IsPurple)
            {
                IsPurple = true;
            }
            else
            {
                IsPurple = false;
            }
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
            base.SetZoneColor(EnnemyFight.GetPosition() - 1, "green");
        }

    }
}

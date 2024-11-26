using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleShellPattern : EnnemyPattern
{
    static int etape=-1;
    bool Zone1;
    bool Zone2;
    bool Zone3;
    bool IsNear;
    public override string Action()
    {
        if (EnnemyFight.GetPosition() > 4)
        {
            base.SetZoneColor(EnnemyFight.GetPosition() - 1, "white");
            return "IsMoveFW";
        }
        else
        {
            if (Zone1 || IsNear)
            {
                base.SetZoneColor(EnnemyFight.GetPosition() - 1, "white");
                if (Zone2)
                {
                    base.SetZoneColor(EnnemyFight.GetPosition() - 2, "white");
                    if (Zone3)
                    {
                        base.SetZoneColor(EnnemyFight.GetPosition() - 3, "white");
                    }
                }
            }
            return "IsAttack";
        }
    }

    public override void InitiatePV()
    {
        EnnemyPV.setPVmax(40);
    }

    public override void Attack()
    {
        if (PlayerFight.GetPosition() == EnnemyFight.GetPosition() - 1 && !PlayerFight.getDefenseMode())
        {
            if (Zone1 || IsNear) {
                PlayerPV.setPV(PlayerPV.getPV() - 10);
            }
        }
        if (PlayerFight.GetPosition() == EnnemyFight.GetPosition() - 2 && !PlayerFight.getDefenseMode())
        {
            if (Zone2)
            {
                PlayerPV.setPV(PlayerPV.getPV() - 10);
            }
        }
        if (PlayerFight.GetPosition() == EnnemyFight.GetPosition() - 3 && !PlayerFight.getDefenseMode())
        {
            if (Zone3)
            {
                PlayerPV.setPV(PlayerPV.getPV() - 10);
            }
        }
    }
    public void Step()
    {
        etape=(etape+1)%6;
        if (etape == 5)
        {
            Zone1 = false;
        }
        else
        {
            Zone1 = true;
        }
        if (etape%2==1 && etape != 1)
        {
            Zone2 = false;
        }
        else
        {
            Zone2 = true;
        }
        if (etape % 2 == 1)
        {
            Zone3 = false;
        }
        else
        {
            Zone3 = true;
        }
    }
    public override void PreviousNextAction()
    {
        if (PlayerFight.GetPosition()==EnnemyFight.GetPosition() - 1)
        {
            IsNear = true;
        }
        else
        {
            IsNear = false;
        }
        if (EnnemyFight.GetPosition()>4)
        {
            base.SetZoneColor(EnnemyFight.GetPosition() - 1, "green");
        }
        else
        {
            Step();
            if (Zone1 || IsNear)
            {
                base.SetZoneColor(EnnemyFight.GetPosition() - 1, "red");
                if (Zone2)
                {
                    base.SetZoneColor(EnnemyFight.GetPosition() - 2, "red");
                    if (Zone3)
                    {
                        base.SetZoneColor(EnnemyFight.GetPosition() - 3, "red");
                    }
                }
            }
        }
    }
}

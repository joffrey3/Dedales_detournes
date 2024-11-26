using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestMonsterPattern : EnnemyPattern
{
    int etape = 5;
    bool is_black = false;
    bool Zone1 = false;
    bool Zone2 = false;
    bool Zone3 = false;
    public override string Action()
    {
        if (EnnemyFight.GetPosition() != 3)
        {
            base.SetZoneColor(EnnemyFight.GetPosition() - 1, "white");
            return "IsMoveFW";
        }
        base.SetZoneColor(EnnemyFight.GetPosition() - 1, "white");
        base.SetZoneColor(EnnemyFight.GetPosition() - 2, "white");
        base.SetZoneColor(EnnemyFight.GetPosition() - 3, "white");
        return "IsAttack";
    }

    public override void InitiatePV()
    {
        EnnemyPV.setPVmax(70);
    }

    public override void Attack()
    {
        if ((PlayerFight.GetPosition() == EnnemyFight.GetPosition() - 1 && Zone1) || (PlayerFight.GetPosition() == EnnemyFight.GetPosition() - 2 && Zone2) || (PlayerFight.GetPosition() == EnnemyFight.GetPosition() - 3 && Zone3))
        {
            if(is_black){
                PlayerPV.setPV(PlayerPV.getPV() - PlayerPV.getPV());
            }
            else if (!PlayerFight.getDefenseMode()) {
                PlayerPV.setPV(PlayerPV.getPV() - 10);
            }
        }
    }

    public void Step()
    {
        if (etape>=5)
        {
            is_black = true;
        }
        else
        {
            is_black = false;
        }
        if (etape == 3)
        {
            Zone1 = false;
        }
        else
        {
            Zone1 = true;
        }
        if (etape!=1 && etape!=2 && etape!=6)
        {
            Zone2 = false;
        }
        else
        {
            Zone2 = true;
        }
        if (etape != 2)
        {
            Zone3 = false;
        }
        else
        {
            Zone3 = true;
        }
        etape = (etape + 1) % 7;
    }

    public override void PreviousNextAction()
    {
        if (EnnemyFight.GetPosition() != 3)
        {
            base.SetZoneColor(EnnemyFight.GetPosition() - 1, "green");
        }
        else
        {
            Step();
            if (is_black)
            {
                if (Zone1)
                {
                    base.SetZoneColor(EnnemyFight.GetPosition() - 1, "black");
                    if (Zone2)
                    {
                        base.SetZoneColor(EnnemyFight.GetPosition() - 2, "black");
                    }
                }
            }
            else
            {
                if (Zone1)
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
}

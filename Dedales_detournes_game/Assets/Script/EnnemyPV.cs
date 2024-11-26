using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyPV : MonoBehaviour
{
    public static int PV_max_static = 0;

    static int PV_static = PV_max_static;
    int PV_max = PV_max_static;
    int PV = PV_max_static;
    TextMesh m_text;
    void Start()
    {
        m_text = this.GetComponent<TextMesh>();
        this.setText(m_text, PV, PV_max);
    }

    void setText(Component component_text, int nb_pv_actuel, int nb_pv_max)
    {
        m_text.text = nb_pv_actuel.ToString() + "/" + nb_pv_max.ToString();
    }
    public static int getPV()
    {
        return PV_static;
    }
    public static void setPV(int nb)
    {
        PV_static = nb;
    }

    public static int getPVmax()
    {
        return PV_max_static;
    }
    public static void setPVmax(int nb)
    {
        PV_max_static = nb;
        PV_static = PV_max_static;
    }

    private void Update()
    {
        if (PV_max_static != PV_max || PV_static != PV)
        {
            this.setText(m_text, PV_static, PV_max_static);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class battleHUD : MonoBehaviour
{
    public Slider hpslider;

    public void SetHUD(units unit)
    {
        hpslider.maxValue= unit.maxHP;
        hpslider.value = unit.currrentHP;
    }

    public void setHP(int HP)
    {
        hpslider.value = HP;

    }
}

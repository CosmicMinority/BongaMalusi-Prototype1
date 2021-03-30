using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class units : MonoBehaviour
{
    public string UnitName;

    public int unitlevel;
   public int damage;

    public int mindmg;
    public int maxdma;

    public int defence;

    public int maxHP;
    public int currrentHP;

    public bool takedamage(int hurt)
    {
        currrentHP  -= hurt; 

        if (currrentHP <= 0)
        {
            return true;
        }
        else { return false; }
    }
    public bool defense(int block)
    {
        damage -= block;

        if (damage <= 0)
        {
            return true;
        }else
        {
            return false;

        }
    }

    public void drain(int suck)
    {
        currrentHP += suck;

        if(currrentHP>= maxHP)
        {
             currrentHP= maxHP;
        }

    }



    private void Update()
    {
        damage = Random.Range(mindmg, maxdma);
    }
}

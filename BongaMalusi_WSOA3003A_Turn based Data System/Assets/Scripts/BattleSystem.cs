using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum Battle {START, PLAYERTURN, ENEMYTURN, WON ,LOST}

public class BattleSystem : MonoBehaviour
{
    public GameObject Playerpf;
    public GameObject Enemypf;

    public Transform playerbs;
    public Transform enemybs;

    public units player;
    public units enemy;

    public battleHUD playerhud;
    public battleHUD enemyhud;

    public Text battletext;

    public GameObject button;

    public Battle state;
    // Start is called before the first frame update
    void Start()
    {
        state = Battle.START;
        battlesetup();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

   void battlesetup()
    {
        GameObject playergetComp = Instantiate(Playerpf, playerbs);

        player = playergetComp.GetComponent<units>();

        GameObject enemygetComp = Instantiate(Enemypf, enemybs);
        enemy = enemygetComp.GetComponent<units>();


        battletext.text = "Start";
        playerhud.SetHUD(player);
        enemyhud.SetHUD(enemy);
        state = Battle.PLAYERTURN;
        playerturn();
        
    }

    IEnumerator playerattack()
    {
       bool isdead =  enemy.takedamage(player.damage);

        enemyhud.setHP(enemy.currrentHP);
        yield return new WaitForSeconds(2f);

        if (isdead)
        {
            state = Battle.WON;

            endbattle();
        }
        else
        {
            state = Battle.ENEMYTURN;
            StartCoroutine(enemyturn());
        }
    }

    

   IEnumerator enemyturn()
    {
        battletext.text = enemy.UnitName + "turn";

        button.SetActive(false);

        yield return new WaitForSeconds(1f);

        bool isdead = player.takedamage(enemy.damage);

        playerhud.setHP(player.currrentHP);

        yield return new WaitForSeconds(1f);

        if (isdead)
        {
            state = Battle.LOST;

            endbattle();
        }
        else
        {
            state = Battle.PLAYERTURN;
            playerturn();
        }



    }

    IEnumerator playerdefense()
    {
       bool zero = enemy.defense(player.defence);

       

        yield return new WaitForSeconds(1f);

        if (zero)
        {
            enemy.damage = 5;
        }
        else
        {

            state = Battle.ENEMYTURN;
            StartCoroutine(enemyturn());
        }
     


    }

    IEnumerator playerdrain()
    {
        player.drain(5);

        enemy.drain(-5);

        playerhud.setHP(player.currrentHP);
        enemyhud.setHP(enemy.currrentHP);

        yield return new WaitForSeconds(1f);

        state = Battle.ENEMYTURN;
        StartCoroutine(enemyturn());
    }

    void endbattle()
    {
        if ( state==Battle.WON)
        {
            battletext.text = "you won";
        }else if (state==Battle.LOST)
        {
            battletext.text = "you lost";
        }
    }

    void playerturn()
    {

        battletext.text = "player's turn";
        button.SetActive(true);

    } 

    public void onattackButton()
    {
        if (state != Battle.PLAYERTURN)
        
            return;
        StartCoroutine(playerattack());
    }

    public void ondefensebutton()
    {
        if (state != Battle.PLAYERTURN)

            return;
        StartCoroutine(playerdefense());
    }

    public void drain()
    {
        if (state != Battle.PLAYERTURN)

            return;
        StartCoroutine(playerdrain());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class GameController : MonoBehaviour
{
    private List<FighterStats> fighterStats;

    [SerializeField]
    private GameObject battleMenu;

    public void Start()
    {
        fighterStats = new List<FighterStats>();
        GameObject hero = GameObject.FindGameObjectWithTag("Hero");
        FighterStats currentFightStats = hero.GetComponent<FighterStats>();
        currentFightStats.CalculateNextTurn(0);
        fighterStats.Add(currentFightStats);

        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        FighterStats currentEnemyStats = enemy.GetComponent<FighterStats>();
        currentEnemyStats.CalculateNextTurn(0);
        fighterStats.Add(currentEnemyStats);

        fighterStats.Sort();
        battleMenu.SetActive(false);
    }


    public void NextTurn()
    {
        Debug.Log("Nextturn call");
        FighterStats currentFighterStats = fighterStats[0];
        fighterStats.Remove(currentFighterStats);

        if (!currentFighterStats.GetDead())
        {
            Debug.Log("!currentFighterStats call");
            GameObject currentUnit = currentFighterStats.gameObject;
            currentFighterStats.CalculateNextTurn(currentFighterStats.nextActTurn);
            fighterStats.Add(currentFighterStats);
            fighterStats.Sort();
            Debug.Log(fighterStats);
            if (currentUnit.tag == "Hero")
            {
                Debug.Log(currentFighterStats.gameObject.name + "Nextturn Method call");
                battleMenu.SetActive(true);
            }
            else
            {
                string attackType = Random.Range(0, 2) == 1 ? "melee" : "range";
                currentUnit.GetComponent<FighterAction>().SelectAttack(attackType);
                Debug.Log(currentFighterStats.gameObject.name + "Nextturn Method call");
            }
        }
        else
        {
            NextTurn();
            Debug.Log(currentFighterStats.gameObject.name + " is dead, skipping turn.");
        }
    }
}

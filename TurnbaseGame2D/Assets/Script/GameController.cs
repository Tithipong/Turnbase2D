using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class GameController : MonoBehaviour
{
    public Text battleText;
    private List<FighterStats> fighterStats;
    private Button[] buttons;
    [SerializeField] private GameObject battleMenu;


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

        buttons = battleMenu.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => DisableAllButtons());
        }
    }

    void DisableAllButtons()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
    }

    public void NextTurn()
    {
        battleText.gameObject.SetActive(false);
        FighterStats currentFighterStats = fighterStats[0];
        fighterStats.Remove(currentFighterStats);

        if (!currentFighterStats.GetDead())
        {
            GameObject currentUnit = currentFighterStats.gameObject;
            currentFighterStats.CalculateNextTurn(currentFighterStats.nextActTurn);
            fighterStats.Add(currentFighterStats);
            fighterStats.Sort();
            Debug.Log(fighterStats);
            if (currentUnit.tag == "Hero")
            {
                battleMenu.SetActive(true);
                foreach (Button button in buttons)
                {
                    button.interactable = true;
                }
            }
            else
            {
                battleMenu.SetActive(false);
                foreach (Button button in buttons)
                {
                    button.interactable = false;
                }
                string attackType = Random.Range(0, 2) == 1 ? "melee" : "range";
                currentUnit.GetComponent<FighterAction>().SelectAttack(attackType);
            }
        }
        else
        {
            NextTurn();
        }
    }


}

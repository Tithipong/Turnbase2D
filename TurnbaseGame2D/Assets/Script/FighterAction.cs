using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterAction : MonoBehaviour
{
    private GameObject enemy;
    private GameObject hero;

    [SerializeField]
    private GameObject meleePrefab;

    [SerializeField]
    private GameObject rangePrefab;

    [SerializeField]
    private Sprite faceIcon;

    /*private GameObject currentAttack;
    private GameObject meleeAttack;
    private GameObject rangeAttack;*/

    public void Awake()
    {
        hero = GameObject.FindGameObjectWithTag("Hero");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    public void SelectAttack(string btn)
    {
        GameObject victim = hero;

        if (tag == "Hero")
        {
            victim = enemy;
        }

        if (btn.Equals("melee"))
        {
            meleePrefab.GetComponent<ActtackScript>().Attack(victim);

        }
        else if (btn.Equals("range"))
        {
            rangePrefab.GetComponent<ActtackScript>().Attack(victim);
        }
        else
        {
            Debug.Log("Run");
        }
    }
}
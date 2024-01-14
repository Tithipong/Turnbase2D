using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeButton : MonoBehaviour
{
    [SerializeField] private bool physical;
    private GameObject hero;
    void Start()
    {
        string temp = gameObject.name;
        gameObject.GetComponent<Button>().onClick.AddListener(() => AttachCallback(temp));
        hero = GameObject.FindGameObjectWithTag("Hero");
    }

    private void AttachCallback(string btn)
    {

        if (btn.Equals("MeleeBtn"))
        {
            hero.GetComponent<FighterAction>().SelectAttack("melee");

        }
        else if (btn.Equals("RangeBtn"))
        {
            hero.GetComponent<FighterAction>().SelectAttack("range");
        }
        else
        {
            hero.GetComponent<FighterAction>().SelectAttack("run");
        }
    }

}

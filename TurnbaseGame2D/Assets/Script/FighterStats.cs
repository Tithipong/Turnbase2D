using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FighterStats : MonoBehaviour, IComparable
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject healthFill;
    [SerializeField] private GameObject magicFill;

    [Header("Stats")]
    public float health;
    public float magic;
    public float melee;
    public float magicrange;
    public float defense;
    public float speed;
    public float experience;

    private float startHealth;
    private float startMagic;

    [HideInInspector]
    public int nextActTurn;

    private bool dead = false;

    //Resize health and magic bar
    private Transform healthTranform;
    private Transform magicTranform;

    private Vector2 healthScale;
    private Vector2 magicScale;

    private float xNewHealthScale;
    private float xNewMagicScale;

    private GameObject GameController;

    void Awake()
    {
        healthTranform = healthFill.GetComponent<RectTransform>();
        healthScale = healthFill.transform.localScale;

        magicTranform = magicFill.GetComponent<RectTransform>();
        magicScale = magicFill.transform.localScale;

        startHealth = health;
        startMagic = magic;

        GameController = GameObject.Find("GameControllerOBJ");
    }

    public void ReciveDamage(float damage)
    {
        Debug.Log("Hit");
        health -= damage;
        animator.Play("injury");

        //Set damage txt

        if (health <= 0)
        {
            dead = true;
            gameObject.tag = "Dead";
            Destroy(healthFill);
            Destroy(gameObject);
        }
        else if (damage > 0)
        {
            xNewHealthScale = healthScale.x * (health / startHealth);
            healthFill.transform.localScale = new Vector2(xNewHealthScale, healthScale.y);
            GameController.GetComponent<GameController>().battleText.gameObject.SetActive(true);
            GameController.GetComponent<GameController>().battleText.text = damage.ToString();
        }
        Invoke(nameof(ContinueGame), 0.45f);
    }

    public void UpdateManaFill(float cost)
    {
        if (cost > 0)
        {
            magic -= cost;
            xNewMagicScale = magicScale.x * (magic / startMagic);
            magicFill.transform.localScale = new Vector2(xNewMagicScale, magicScale.y);
        }

    }

    public bool GetDead()
    {
        return dead;
    }

    public void ContinueGame()
    {
        Debug.Log("ContinueGame Called");
        GameObject.Find("GameControllerOBJ").GetComponent<GameController>().NextTurn();
    }

    public void CalculateNextTurn(int currentTurn)
    {

        nextActTurn = currentTurn + Mathf.CeilToInt(100f / speed);
        Debug.Log("currentTurn" + " " + currentTurn);
        Debug.Log("Mathf.CeilToInt(100f / speed)" + " " + Mathf.CeilToInt(100f / speed));
        Debug.Log("nextActTurn" + " " + nextActTurn);
    }

    public int CompareTo(object otherStats)
    {
        int next = nextActTurn.CompareTo(((FighterStats)otherStats).nextActTurn);
        return next;
    }
}

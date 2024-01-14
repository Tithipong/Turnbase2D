using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ActtackScript : MonoBehaviour
{
    public GameObject owner;
    [SerializeField] private string animationName;
    [SerializeField] private bool magicAttack;
    [SerializeField] private float magicCost;
    [SerializeField] private float minAttackMultiplier;
    [SerializeField] private float maxAttackMultiplier;
    [SerializeField] private float minDefenseMultiplier;
    [SerializeField] private float maxDefenseMultiplier;

    private FighterStats attackerStats;
    private FighterStats targetStats;
    private float damage = 0.0f;

    public void Attack(GameObject victim)
    {
        Debug.Log(owner + "Attack!!!");
        attackerStats = owner.GetComponent<FighterStats>();
        targetStats = victim.GetComponent<FighterStats>();

        if (attackerStats.magic >= magicCost)
        {
            float multiplier = Random.Range(minAttackMultiplier, maxAttackMultiplier);

            damage = multiplier * attackerStats.melee;
            if (magicAttack)
            {
                damage = multiplier * attackerStats.magicrange;
            }
            float defenseMultiplier = Random.Range(minDefenseMultiplier, maxDefenseMultiplier);
            damage = Mathf.Max(0, damage - (defenseMultiplier * targetStats.defense));
            owner.GetComponent<Animator>().Play(animationName);
            targetStats.ReciveDamage(MathF.Ceiling(damage));
            attackerStats.UpdateManaFill(magicCost);
        } 
        else
        {
            owner.GetComponent<Animator>().Play("stun");
            Invoke(nameof(SkipContinueGame), 0.45f);
        }
    }
    public void SkipContinueGame()
    {
        Debug.Log("ContinueGame Called");
        
        GameObject.Find("GameControllerOBJ").GetComponent<GameController>().NextTurn();
        
    }
}

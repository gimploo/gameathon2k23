using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType {
    ROCK = 0,
    PAPER = 1,
    SCISSORS = 2,
};

public class Player : MonoBehaviour
{
    public float health;
    public bool isDead;
    public int deathCount;
    public int xp;

    public int cooldownDuration = 4;

    private bool[] isAbilityActive = new bool[3];

    // Start is called before the first frame update
    private void Awake()
    {
        health = 50;    
        isDead = false;
        xp = 0;
        deathCount = 0;

        for (int i = 0; i < 3; i++)
        {
            isAbilityActive[i] = true;
        }
    }

    public void TakeDamage(int value)
    {
        health -= value;
        if (health <= 0)
        {
            isDead = true;
            return;
        }
    }

    public void GainXP(int value)
    {
        xp += value;
    }

    public void UseAbility(AbilityType type)
    {
        if (!isAbilityActive[(int)type]) return;

        StartCoroutine(StartCooldown(type));
    }

    private IEnumerator StartCooldown(AbilityType type)
    {
        isAbilityActive[(int)type] = false;
        yield return new WaitForSeconds(cooldownDuration);
        isAbilityActive[(int)type] = true;
    }

    void Update()
    {

    }
}

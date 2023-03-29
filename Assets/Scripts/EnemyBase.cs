using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    private int maxhealth;
    private int health;

    private bool isVulnerable = false;
    public int bulletDamageDelta = 2;
    public int abilityDamage = 3;
    public int vulnerableTimeDelta = 2;
    public int gainHealthDelta = 5;

    private void Awake()
    {
        switch(gameObject.tag)
        {
            case "Slime":
                maxhealth  = health = 10;
            break;
            case "TurtleShell":
                maxhealth  = health = 16;
            break;
            case "Origami":
                maxhealth  = health = 14;
            break;
        }
    }

    void TakeDamage()
    {
        if (!isVulnerable)
            health -= bulletDamageDelta;
        else
            health -= bulletDamageDelta + abilityDamage;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            TakeDamage();
        }
    }

    void GainHealth()
    {
        health += gainHealthDelta;
    }

    void Update()
    {
        if (isVulnerable)
            StartCoroutine(CooldownVulnerable());

        if (health <= 0)
            Destroy(gameObject);
    }

    IEnumerator CooldownVulnerable()
    {
        yield return new WaitForSeconds(vulnerableTimeDelta);
        isVulnerable = false;
    }
}

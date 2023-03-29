using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    private int maxhealth;
    private int health;

    public Level1HandlerScript l1Script;

    public bool isVulnerable = false;
    public int bulletDamageDelta = 2;
    public int abilityDamage = 3;
    public int vulnerableTimeDelta = 2;
    public int gainHealthDelta = 5;

    public GameObject killEffect;

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

        l1Script = FindObjectOfType<Level1HandlerScript>();
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
            Destroy(col.gameObject);
        }
    }

    void GainHealth()
    {
        health += gainHealthDelta;
    }

    void Update()
    {
        if (isVulnerable) {
            Debug.Log("is vulnerable");
            StartCoroutine(CooldownVulnerable());
        }

        if (health <= 0) {
            Instantiate(killEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            l1Script.enemiesLeft -= 1;
        }
    }

    IEnumerator CooldownVulnerable()
    {
        yield return new WaitForSeconds(vulnerableTimeDelta);
        isVulnerable = false;
    }
}

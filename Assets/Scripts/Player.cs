using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AbilityType {
    ROCK        = 0,
    PAPER       = 1,
    SCISSORS    = 2,
};

public class Player : MonoBehaviour
{
    private float maxHealth;
    private float currentHealth;
    public bool isDead;
    public int deathCount;
    public int xp;
    public int bulletDamage;

    [SerializeField] private Image __healthbarSprite;

    public GameObject rockAbilityUI;
    public GameObject paperAbilityUI;
    public GameObject scissorsAbilityUI;

    private RawImage rockImage;
    private RawImage paperImage;
    private RawImage scissorsImage;

    private RaycastHit hit;
    public int cooldownDuration = 4;

    private bool[] isAbilityActive = new bool[3];

    // Start is called before the first frame update
    private void Awake()
    {
        maxHealth = currentHealth = 50;    
        isDead = false;
        xp = 0;
        deathCount = 0;

        for (int i = 0; i < 3; i++)
        {
            isAbilityActive[i] = true;
        }

        rockImage = rockAbilityUI.GetComponent<RawImage>();
        paperImage = paperAbilityUI.GetComponent<RawImage>();
        scissorsImage = scissorsAbilityUI.GetComponent<RawImage>();
    }

    public void TakeDamage(int value)
    {
        currentHealth -= value;
        if (currentHealth <= 0)
        {
            isDead = true;
            return;
        }
        UpdateHealthBar();
    }

    public void GainXP(int value)
    {
        xp += value;
    }

    public void UseAbility(AbilityType type)
    {
        if (!isAbilityActive[(int)type]) return;

        switch(type)
        {
            case AbilityType.ROCK:
                rockImage.color = new Color(rockImage.color.r, rockImage.color.g, rockImage.color.b, 0.5f);
                switch(hit.transform.tag)
                {
                    case "Slime":
                        Debug.Log("hit slime");
                    break;
                    case "Origami":
                        Debug.Log("hit origami");
                    break;
                    case "TurtleShell":
                        Debug.Log("hit turtleshell");
                    break;
                }
            break;
            case AbilityType.PAPER:
                paperImage.color = new Color(paperImage.color.r, paperImage.color.g, paperImage.color.b, 0.5f);
                switch(hit.transform.tag)
                {
                    case "Slime":
                        Debug.Log("hit slime");
                    break;
                    case "Origami":
                        Debug.Log("hit origami");
                    break;
                    case "TurtleShell":
                        Debug.Log("hit turtleshell");
                    break;
                }
            break;
            case AbilityType.SCISSORS:
                scissorsImage.color = new Color(scissorsImage.color.r, scissorsImage.color.g, scissorsImage.color.b, 0.5f);
                switch(hit.transform.tag)
                {
                    case "Slime":
                        Debug.Log("hit slime");
                    break;
                    case "Origami":
                        Debug.Log("hit origami");
                    break;
                    case "TurtleShell":
                        Debug.Log("hit turtleshell");
                    break;
                }
            break;
        }
        StartCoroutine(StartCooldown(type));
    }

    private IEnumerator StartCooldown(AbilityType type)
    {
        isAbilityActive[(int)type] = false;
        yield return new WaitForSeconds(cooldownDuration);
        switch(type)
        {
            case AbilityType.ROCK:
                rockImage.color = new Color(rockImage.color.r, rockImage.color.g, rockImage.color.b, 255);
            break;
            case AbilityType.PAPER:
                paperImage.color = new Color(paperImage.color.r, paperImage.color.g, paperImage.color.b, 255);
            break;
            case AbilityType.SCISSORS:
                scissorsImage.color = new Color(scissorsImage.color.r, scissorsImage.color.g, scissorsImage.color.b, 255);
            break;
        }
        isAbilityActive[(int)type] = true;
    }

    void Update()
    {
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 500);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseAbility(AbilityType.ROCK);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseAbility(AbilityType.PAPER);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            UseAbility(AbilityType.SCISSORS);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(bulletDamage);
        }
    }

    public void UpdateHealthBar()
    {
        __healthbarSprite.fillAmount = currentHealth / maxHealth;
    }
}

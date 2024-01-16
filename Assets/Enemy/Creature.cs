using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Creature : BoardItem
{

    private Creature_SO creatureType;

    private int health;
    private int attack;

    public TextMeshProUGUI healthDisplay;
    public TextMeshProUGUI attackDisplay;
    public Image creatureImage;

    public Creature_SO CreatureType
    {
        get => creatureType;
        set
        {
            creatureType = value;
            creatureImage.sprite = creatureType.image;
            Health = creatureType.health;
            Attack = creatureType.attack;
        }
    }

    public int Health
    {
        get => health;
        set{
            health = value;
            healthDisplay.text = health.ToString();
        }
    }

    public int Attack
    {
        get => attack;
        set{
            attack = value;
            attackDisplay.text = attack.ToString();
        }
    }
}

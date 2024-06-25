using JetBrains.Annotations;
using NUnit.Framework;
using TMPro;

public class playerStats
{
    public float movementSpeed = 3.5f;
    public int maxHealth = 100;
    public int maxMana = 100;
    public int level = 1;
    public float exp = 0;
    public float manaRegeneration = 5;
    public float healthRegeneration = 2;
    public float castRecharge = 1;

    public static playerStats backup;
    public static playerStats Instance
    {
        get
        {
            if (backup == null)
            {
                backup = new playerStats();
                TargetScript.score = 0;
            }
            return backup;
        }
    }
    public void LevelUp()
    {
        level++;
        manaRegeneration += 5;
        movementSpeed += 0.1f;
        maxHealth += 5;
        maxMana += 2;
    }
    public void GainXp(float newxp)
    {
        exp += newxp;

        // Level Up
        if (exp >= level * 10)
        {
            exp -= level * 10;
            LevelUp();
        }
    }

}

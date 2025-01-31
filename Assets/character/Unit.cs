using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public string skillOneDescription;
    public string skillTwoDescription;
    public string unitName;

    public int damage;

    public int maxHP;
    public int currentHP;

    public int imobilised = 0;

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;
        return currentHP <= 0;
    }

    public void TakeImmobilisation(int nbTurn)
    {
        imobilised = nbTurn + imobilised;
    }

    public InfosSkillUsed UseSkill(Unit unit, int skillNumber)
    {
        InfosSkillUsed infosFight = new InfosSkillUsed("", false);

        if (imobilised == 0)
        {
            if (skillNumber == 0)
            {
                infosFight.opponentIsDead = SkillOne(unit);
                infosFight.messageFight = skillOneDescription;
            }
            else if (skillNumber == 1)
            {
                infosFight.opponentIsDead = SkillTwo(unit);
                infosFight.messageFight = skillTwoDescription;
            }
        }
        else
        {
            infosFight.messageFight = unitName + " est immobilisé pendant " + imobilised + " tours";
            imobilised--;
        }
        return infosFight;
    }

    public abstract bool SkillOne(Unit unit);

    public abstract bool SkillTwo(Unit unit);
}


public struct InfosSkillUsed
{
    public string messageFight;
    public bool opponentIsDead;

    public InfosSkillUsed(string messageFight, bool opponentIsDead)
    {
        this.messageFight = messageFight;
        this.opponentIsDead = opponentIsDead;
    }
}
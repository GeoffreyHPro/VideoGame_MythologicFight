public class Zeus : Unit
{
    Zeus()
    {   
        skillOneDescription = "Zeus lance un éclair";
        skillTwoDescription = "Zeus lance tempête de l'Olympe";
        unitName = "Zeus";
        damage = 10;
        maxHP = 100;
        currentHP = 100;
    }
    /* Auto-Attack */
    public override bool SkillOne(Unit unit)
    {
        bool isDead = unit.TakeDamage(damage);
        return isDead;
    }
    /* Thunder with immobilisation */
    public override bool SkillTwo(Unit unit)
    {
        unit.TakeImmobilisation(1);
        bool isDead = unit.TakeDamage(damage + 5);
        return isDead;
    }
}
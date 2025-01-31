public class Athena : Unit
{
    Athena()
    {
        skillOneDescription = "Athena lance coup froudroyant";
        skillTwoDescription = "Athena lance bénédiction d'Athénes";
        unitName = "Athena";
        damage = 5;
        maxHP = 130;
        currentHP = 130;
    }
    /* Auto-Attack */
    public override bool SkillOne(Unit unit)
    {
        bool isDead = unit.TakeDamage(damage);
        return isDead;
    }
    /* Heal */
    public override bool SkillTwo(Unit unit)
    {
        unit.currentHP += 2;
        return false;
    }
}
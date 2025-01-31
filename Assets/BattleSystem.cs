using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Button SkillOneButton;
    public Button SkillTwoButton;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;
    public Text dialogueText;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGo = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGo.GetComponent<Unit>();
        GameObject enemyGo = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGo.GetComponent<Unit>();
        dialogueText.text = "Tu te bats contre " + enemyUnit.unitName;

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYERTURN;
        dialogueText.text = "Choisis une action";
    }

    IEnumerator EnemyTurn()
    {
        InfosSkillUsed infosFight = enemyUnit.UseSkill(playerUnit, 0);
        dialogueText.text = infosFight.messageFight;
        yield return new WaitForSeconds(2F);
        playerHUD.SetHP(playerUnit.currentHP);

        if (infosFight.opponentIsDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            SkillOneButton.interactable = true;
            SkillTwoButton.interactable = true;
            dialogueText.text = "Choisis une action : ";
        }
    }

    public void OnSkillButton(int SkillNb)
    {
        SkillOneButton.interactable = false;
        SkillTwoButton.interactable = false;
        if (state != BattleState.PLAYERTURN) return;
        StartCoroutine(UseSkillUsed(SkillNb));
    }

    IEnumerator UseSkillUsed(int nb)
    {
        InfosSkillUsed infosFight = playerUnit.UseSkill(enemyUnit, nb);
        dialogueText.text = infosFight.messageFight;
        yield return new WaitForSeconds(2f);
        enemyHUD.SetHP(enemyUnit.currentHP);
        IsDeadDuringPlayerTurn(infosFight.opponentIsDead);
    }

    public void IsDeadDuringPlayerTurn(bool isDead)
    {
        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won the battle";
        }
        else
        {
            dialogueText.text = "You loose the battle";
        }
    }
}
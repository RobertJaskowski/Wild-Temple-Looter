using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static ElementalManager;

[CreateAssetMenu(menuName = "Enemy/Enemy")]
public class Enemy : ScriptableObject
{
    public Sprite Image;
    public string Name;
    public float Hp;
    public bool isBoss = false;
    public float Strenght;
    public float AttackTime;
    public ElementalType ElementalType;

    public DropTable dropTable;


    public EnemyController controller;
    //    [Space(100)]
    //    public FloatGameEvent updateEnemyHpBarEvent;
    //    public FloatVariable MaxEnemyHp;
    //    public FloatVariable difficulty;
    //    public GameEvent showPlayerHpBarEvent;
    //    public GameEvent hidePlayerHpBarEvent;
    //    public FloatGameEvent playerRuntimeHpCurrentUpdateEvent;
    //    public Equipment equipment;
    //    public EnemyDatabase enemyDatabase;

    //#if UNITY_EDITOR
    //    public void OnValidate()
    //    {
    //        updateEnemyHpBarEvent = (FloatGameEvent)SOTools.GetExistingSO<FloatGameEvent>("UpdateEnemyHpBar");
    //        MaxEnemyHp = (FloatVariable)SOTools.GetExistingSO<FloatVariable>("MaxEnemyHp");
    //        difficulty = (FloatVariable)SOTools.GetExistingSO<FloatVariable>("EnemyDifficulty");
    //        showPlayerHpBarEvent = (GameEvent)SOTools.GetExistingSO<GameEvent>("ShowPlayerHpBar");
    //        hidePlayerHpBarEvent = (GameEvent)SOTools.GetExistingSO<GameEvent>("HidePlayerHpBar"); 
    //        playerRuntimeHpCurrentUpdateEvent = (FloatGameEvent)SOTools.GetExistingSO<FloatGameEvent>("PlayerRuntimeCurrentHpUpdate");
    //        equipment = (Equipment)SOTools.GetExistingSO<Equipment>("Equipment");
    //        enemyDatabase = (EnemyDatabase)SOTools.GetExistingSO<EnemyDatabase>("Equipment");
    //    }
    //#endif


    public Enemy GenerateEnemy(EnemyController enemyController)
    {
        controller = enemyController;

        string enemyNameBase = controller.enemyDatabase.GetRandomEnemyName();
        Image = controller.enemyDatabase.GetRandomEnemyImageBasedOnEnemy(enemyNameBase, ElementalType);

        Name = enemyNameBase;
        Hp = controller.GetEnemyGeneratedHp();
        Strenght = controller.GetEnemyGeneratedStrenght();
        //L.og("Enemy str " + Strenght);

        AttackTime = controller.GetEnemyGeneratedAttackTime();
        ElementalType = controller.GetEnemyGeneratedElementalType();

        return this;
    }

    public Enemy Spawn()
    {
        FightManager.instance.currentEnemy = this;
        FightManager.instance.enemyImage.sprite = Image;
        Color color = FightManager.instance.enemyImage.color;
        color.a = 1;
        FightManager.instance.enemyImage.color = color;



        if (isBoss)
        {
            controller.hideDungeonReturnToTownButton.Raise();
            MakeBoss();
        }

        Hp *= controller.difficulty.RuntimeValue;
        controller.MaxEnemyHp.RuntimeValue = Hp;
        controller.updateEnemyHpBarEvent.SetValue(Hp).Raise();

        //L.og("enemy " + Hp + " " + controller.difficulty.RuntimeValue);

        return this;
    }

    private void MakeBoss()
    {
        controller.showPlayerHpBarEvent.Raise();

        Hp *= 2;
        controller.bossSpawned.Raise();


        StopAttackC();
        attackC = FightManager.instance.StartCoroutine(AttackCoroutine());
    }

    public bool HaveDropTable()
    {
        if (dropTable != null)
            return true;
        else
            return false;
    }

    public void Attack()
    {
        controller.enemyAttack.Raise();
        int damage = CalculateDamage(Strenght, ElementalType, controller.equipment.Chest.ElementalType, out double modifier);
        L.og("enemy dmg " + damage);
        controller.playerRuntimeHpCurrentUpdateEvent.UpdateValue(-damage).Raise();

        AudioManager.instance.Play("EnemyAttack");
    }

    public virtual void TakeDamage(Equipableitem attackerItem)
    {
        controller.enemyHit.Raise();
        double modifier;
        int damageDone = CalculateDamage(attackerItem, ElementalType, out modifier);
        FightUIManager.instance.ShowDamageOnScreen(damageDone, modifier);
        if (damageDone > 0)
        {

            Hp -= damageDone;
            controller.updateEnemyHpBarEvent.UpdateValue(-damageDone).Raise();
        }

        if (Hp <= 0)
        {
            Died();
            FightManager.instance.EnemyDied();
        }
    }
    public void Died()
    {
        StopAttackC();
        if (HaveDropTable())
            DropController.instance.Initiate(dropTable);
        else
        {
            if (!isBoss)
                controller.normalEnemyDied.Raise();
            else
                controller.bossDied.Raise();
        }



        if (isBoss)
        {
            FightManager.instance.InCombat = false;
            controller.hidePlayerHpBarEvent.Raise();
        }
        //Object.Destroy(GameObject);
        //L.og("enemy died");
    }

    Coroutine attackC;
    bool IsAttackCRunning { get { return attackC != null; } }
    IEnumerator AttackCoroutine()
    {

        while (Hp > 0)
        {
            yield return new WaitForSeconds(AttackTime);

            if (FightManager.instance.InCombat)
                Attack();
        }


        StopAttackC();
    }

    public void StopAttackC()
    {
        if (IsAttackCRunning)
        {
            FightManager.instance.StopCoroutine(attackC);

        }
        attackC = null;
    }

}

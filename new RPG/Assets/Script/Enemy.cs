using UnityEngine;

public enum EnemyQuality { Minion, Champion, Leader, Boss }

[RequireComponent(typeof(CharStats))]
public class Enemy : Interactable
{
    PlayerManager currentPlayer;
    public CharStats enemyStats;
    public EnemyQuality enemyQuality = EnemyQuality.Minion;

    public static Color[] enemyQualityColor = { Color.white, Color.cyan, Color.yellow, new Color(1f, .5f, .5f) };


    private void Start()
    {
        currentPlayer = PlayerManager.instance;
        enemyStats = GetComponent<CharStats>();
    }

    public override void Interact()
    {
        base.Interact();

        Combat playerCombat = currentPlayer.currentPlayer.GetComponent<Combat>();
        if (playerCombat != null)
        {
            playerCombat.Attack(enemyStats);
        }
        
    }
}

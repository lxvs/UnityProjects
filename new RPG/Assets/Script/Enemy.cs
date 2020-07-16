using UnityEngine;

[RequireComponent(typeof(CharStats))]
public class Enemy : Interactable
{
    PlayerManager currentPlayer;
    CharStats enemyStats;


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

using UnityEngine;

public class EnemyStats : CharStats
{
    public override void Die()
    {
        base.Die();

        Destroy(gameObject);
    }

    void Start()
    {

    }


    void Update()
    {
        
    }
}

using UnityEngine;

[RequireComponent(typeof(CharStats))]
public class Combat : MonoBehaviour
{
    CharStats statsSelf;
    float atkCoolDown = 0f;

    void Start()
    {
        statsSelf = GetComponent<CharStats>();
    }

    void Update()
    {
        if (atkCoolDown > 0) atkCoolDown -= Time.deltaTime;
    }

    public void Attack(CharStats target)
    {
        if (atkCoolDown <= 0)
        {
            target.TakeDamage(statsSelf);
            atkCoolDown = 100f / statsSelf.atkSpeed.GetValue();

        }
    }
}

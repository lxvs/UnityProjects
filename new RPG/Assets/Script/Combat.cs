using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharStats))]
public class Combat : MonoBehaviour
{
    CharStats statsSelf;
    float atkCoolDown = 0f;
    float atkDelay = .6f;

    public event System.Action OnAttack;
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
            StartCoroutine(DoDamage(target, atkDelay));

            if (OnAttack != null)
            {
                OnAttack();
            }
            atkCoolDown = 100f / statsSelf.atkSpeed.GetValue();
        }
    }

    IEnumerator DoDamage(CharStats target, float delay)
    {
        yield return new WaitForSeconds(delay);
        target.TakeDamage(statsSelf);
    }
}

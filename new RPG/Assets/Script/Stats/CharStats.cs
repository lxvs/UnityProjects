using UnityEngine;

public class CharStats : MonoBehaviour
{
    public int lvl;
    public int xp;
    public int hp { get; private set; }
    public Stat hpm;
    public Stat phyAtk;
    public Stat phyDef;

    public int phyDmgReduce;

    private void Awake()
    {
        hp = hpm.GetValue();
    }

    public void TakeDamage(int sourceAtk, int sourceLvl)
    {
        //float evadeRate = sourceAtk > phyDef.GetValue() ? .5f / ((sourceAtk - phyDef.GetValue()) ^ (sourceLvl - lvl + 1)) : .95f;
        float evadeRate = sourceAtk > phyDef.GetValue() ? ((phyDef.GetValue() / sourceAtk) ^ 3) - .05f : .95f;
        evadeRate = Mathf.Clamp(evadeRate, .05f, .95f);
        if (Random.value >= evadeRate)
        {
            int damage = (int)((float)sourceAtk / sourceLvl);
            damage -= phyDmgReduce;
            damage = Mathf.Clamp(damage, 1, int.MaxValue);
            Debug.Log("<color=magenta>" + transform.name + "</color> takes <color=magenta>" + damage + "</color> damage.");
            hp -= damage;
            if (hp <= 0)
            {
                Die();
            }
        }
    }

    public virtual void Die()
    {
        Debug.Log("<color=red>" + transform.name + "</color> DIED.");
    }
}

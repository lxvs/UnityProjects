using UnityEngine;

public class CharStats : MonoBehaviour
{
    public int lvl;
    public int xp;
    public int hp { get; private set; }
    public Stat hpm;
    public Stat phyAtk;
    public Stat phyDef;

    public Stat phyDmgReduce;

    private void Awake()
    {
        lvl = 1;
        hpm.SetBaseValue(50 + lvl * 2);
        hp = hpm.GetValue();
        phyAtk.SetBaseValue((int)Mathf.Pow(lvl, 2));
        phyDef.SetBaseValue(10 * lvl);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Debug"))
        {
            TakeDamage(15, 1);
        }
    }

    public void TakeDamage(int sourceAtk, int sourceLvl)
    {
        if (hp > 0)
        {
            //float evadeRate = sourceAtk > phyDef.GetValue() ? .5f / ((sourceAtk - phyDef.GetValue()) ^ (sourceLvl - lvl + 1)) : .95f;
            float evadeRate = sourceAtk > phyDef.GetValue() ? Mathf.Pow((float)phyDef.GetValue() / sourceAtk, 3f) - .05f : .95f;
            Debug.Log(phyDef.GetValue() + ".  eR = " + evadeRate);
            evadeRate = Mathf.Clamp(evadeRate, .05f, .95f);
            if (Random.value >= evadeRate)
            {
                int damage = (int)((float)sourceAtk / sourceLvl);
                damage -= phyDmgReduce.GetValue();
                damage = Mathf.Clamp(damage, 1, int.MaxValue);
                Debug.Log("<color=magenta>" + transform.name + "</color> takes <color=magenta>" + damage + "</color> damage.");
                hp -= damage;
                if (hp <= 0)
                {
                    hp = 0;
                    Die();
                }
            }
        }

    }

    public virtual void Die()
    {
        Debug.Log("<color=red>" + transform.name + "</color> DIED.");
    }
}

using UnityEngine;

public class CharStats : MonoBehaviour
{
    public int lvl = 1;
    public int xp;
    public int hp;
    public Stat hpm;
    public Stat phyAtk;
    public Stat phyDef;
    public Stat phyDmgReduce;
    public Stat phyDmgRdcRate;

    public Stat hitRate;
    public Stat evadeRate;
    public Stat critRate;

    public Stat atkSpeed;
    public Stat movSpeed;   // movement speed

    private void Awake()
    {
        lvl = 1;
        hpm.SetBaseValue(50 + lvl * 2);
        hp = hpm.GetValue();
        phyAtk.SetBaseValue(10 * lvl);
        phyDef.SetBaseValue(10 * lvl);
        hitRate.SetBaseValue(0);
        evadeRate.SetBaseValue(0);
        critRate.SetBaseValue(5);
        phyDmgReduce.SetBaseValue(0);
        atkSpeed.SetBaseValue(100);
        movSpeed.SetBaseValue(600);
    }

    public void TakeDamage(CharStats sourceStats)
    {
        if (hp > 0)
        {
            int sourceAtk = sourceStats.phyAtk.GetValue();
            int sourceLvl = sourceStats.lvl;
            int sourceHitRate = sourceStats.hitRate.GetValue();
            float lvlDifferenceCoefficient = Mathf.Pow(1.1f, lvl - sourceLvl);
            int defConverted = (int)(phyDef.GetValue() * lvlDifferenceCoefficient);
            float evadeRateConverted = sourceAtk < (defConverted * 2) ? (sourceAtk / defConverted / 2f * .9f + .05f) : .95f;
            evadeRateConverted = Mathf.Clamp(evadeRateConverted + (evadeRate.GetValue() - sourceHitRate) * .01f, .05f, .95f);

            if (Random.value >= evadeRateConverted)
            {
                int damage;
                if (Random.value <= critRate.GetValue() * .01f)
                {
                    damage = sourceAtk / sourceLvl * 2;

                }
                else
                {
                    damage = (int)(sourceAtk / sourceLvl * Random.Range(.5f, 1.5f));

                }

                damage -= phyDmgReduce.GetValue();
                damage = (int)(damage * (1f - phyDmgRdcRate.GetValue() * .01f));
                damage = Mathf.Clamp(damage, (sourceLvl + 1) / 2, int.MaxValue);
                Debug.Log("<color=magenta>" + transform.name + "</color> takes <color=magenta>" + damage + "</color> damage. eR = " + evadeRateConverted + ".  " + hp + "/" + hpm.GetValue());
                hp -= damage;
                if (hp <= 0)
                {
                    hp = 0;
                    Die();
                }
            }
            else
            {
                Debug.Log(transform.name + " <color=green>evaded successfully</color>." + evadeRateConverted);
            }
        }

    }

    public virtual void Die()
    {
        Debug.Log("<color=red>" + transform.name + "</color> DIED.");
    }
}

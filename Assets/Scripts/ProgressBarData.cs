[System.Serializable]
public enum Stat
{
    damage, 
    passiveDamage, 
    health
}
[System.Serializable]
public class ProgressBarData
{
    public Stat stat;
    public float maximum;
    public float current;
    public int level;

    public ProgressBarData(ProgressBar bar)
    {
        stat = (Stat)bar.statToIncrease;
        maximum = bar.maximum;
        current = bar.current;
        level = bar.level;
    }
}

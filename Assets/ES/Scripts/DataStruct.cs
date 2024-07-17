
[System.Serializable]
public struct MonsterData
{
    public string name { get; private set; }
    public float hp { get; private set; }
    public float moveSpeed { get; private set; }
    public float attackDamage { get; private set; }
    public float attackRange { get; private set; }
    public string projectile { get; private set; }
    public string sound { get; private set; }

    public void SetData(string name, float hp, float moveSpeed, float attackDamage, 
        float attackRange, string projectile, string sound)
    {
        this.name = name;
        this.hp = hp;
        this.moveSpeed = moveSpeed;
        this.attackDamage = attackDamage;
        this.attackRange = attackRange;
        this.projectile = projectile;
        this.sound = sound;
    }
}
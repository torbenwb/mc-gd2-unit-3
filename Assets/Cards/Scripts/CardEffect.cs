using UnityEngine;

[System.Serializable]
public class CardEffect
{
    [SerializeField] public int strength;
    [SerializeField] public TargetType targetType;
    [SerializeField] public EffectType effectType;

    [Header("Visual Effects")]
    public GameObject FXPrefab;
    public Vector2 cameraShake;

    public enum TargetType
    {
        Player, Creature, AllCreatures
    }

    public enum EffectType
    {
        ChangeHealth, ChangeAttack, ChangeMana, DrawCard
    }

    public void ResolveEffect(Player player)
    {
        if (FXPrefab) MonoBehaviour.Instantiate(FXPrefab,Vector3.zero, Quaternion.identity);
        CameraShake.Shake(cameraShake.x, cameraShake.y);

        switch(effectType)
        {
            case EffectType.ChangeHealth: player.Health += strength; break;
            case EffectType.ChangeMana: player.Mana += strength; break;
            case EffectType.DrawCard: for(int i = 0; i < strength; i++) player.DrawCard(); break;
        }
    }

    public void ResolveEffect(Creature targetCreature)
    {
        if (FXPrefab) MonoBehaviour.Instantiate(FXPrefab,targetCreature.transform.position, Quaternion.identity);
        CameraShake.Shake(cameraShake.x, cameraShake.y);

        switch(effectType)
        {
            case EffectType.ChangeHealth: targetCreature.Health += strength; break;
            case EffectType.ChangeAttack: targetCreature.Attack += strength; break;
        }
    }

    public void ResolveEffect(Creature[] creatures)
    {
        foreach(Creature targetCreature in creatures)
        {
            ResolveEffect(targetCreature);
        }
    }

}

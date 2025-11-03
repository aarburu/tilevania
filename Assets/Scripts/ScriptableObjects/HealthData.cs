using UnityEngine;

[CreateAssetMenu(fileName = "NewHealthData", menuName = "Game/Health Data")]
public class HealthData : ScriptableObject
{
    public int baseMaxHealth = 3;

    // En el caso de que se añadan NPCs que no deberían morir.
    public bool isImmortal = false;
}

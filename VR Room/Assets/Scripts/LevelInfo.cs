using UnityEngine;

[CreateAssetMenu(fileName = "New Level Info", menuName = "ScriptableObject/LevelInfo", order = 1)]
public class LevelInfo : ScriptableObject
{
    public GameObject carPrefab;
    public CarPartType brokenPartType;
    public string levelDescription;
}

using UnityEngine;

[CreateAssetMenu( fileName = "GameData", menuName = "Data/GameData")]
public class GameData : ScriptableObject
{
    public float crystals;


    public void Reset()
    {
        crystals = 0;
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
   public float movementSpeed;
   public float distanceForFollow;
   public float followSpeed;
}

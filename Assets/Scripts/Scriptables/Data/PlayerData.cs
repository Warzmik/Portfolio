using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player")]
public class PlayerData : ScriptableObject
{
    public bool canBeControlled;
    public bool canMove;
    public bool canJump;
    public bool inAir;
    public float movementSpeed;
    public float jumpForce;
    [Range(0f, 0.5f)] public float coyoteTime;
}

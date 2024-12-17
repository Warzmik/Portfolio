using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player")]
public class PlayerData : ScriptableObject
{
    [Header("Settings"), Space]

    public GroundTypes groundType = GroundTypes.Floor;
    public CameraModes cameraMode = CameraModes.Normal;

    public float life;
    public float moveForce;
    public float jumpForce;
    public float attackForce;

    [Space, Header("Internal data"), Space]

    public float moveValue;
    public Vector3 moveDirection;
    public Vector3 currentPosition;
    public bool makingDamage;

    [Space, Header("Constraints"), Space]

    public bool canMove;
    public bool canJump;
    public bool canAttack;

    [Space, Header("Actions"), Space]

    public bool isMoving;
    public bool isJumping;
    public bool isTargeting;
    public bool isAttacking;
}
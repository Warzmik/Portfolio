using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player")]
public class PlayerData : ScriptableObject
{
    [Header("Settings"), Space]

    public float life;
    public float moveForce;
    public float jumpForce;
    public float attackForce;
    public float powerForce;
    public float powerDuration;

    [Space, Header("Internal data"), Space]

    public GroundTypes groundType = GroundTypes.Floor;
    public CameraModes cameraMode = CameraModes.Normal;
    public float moveValue;
    public int attackCount;
    public Vector3 moveDirection;
    public Vector3 currentPosition;
    public Vector3 targetPosition;

    [Space, Header("Constraints"), Space]

    public bool canMove;
    public bool canJump;
    public bool canAttack;
    public bool canUsePower;

    [Space, Header("Actions"), Space]

    public bool isMoving;
    public bool isJumping;
    public bool isTargeting;
    public bool isAttacking;
    public bool isUsingPower;
}
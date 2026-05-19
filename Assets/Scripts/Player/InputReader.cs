using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Jump = "Jump";
    private const string Attack = "Fire1";

    public bool IsJumpPressed { get; private set; }
    public bool IsAttackPressed { get; private set; }

    private void Update()
    {
        IsJumpPressed = Input.GetButtonDown(Jump);
        IsAttackPressed = Input.GetButtonDown(Attack);
    }
}

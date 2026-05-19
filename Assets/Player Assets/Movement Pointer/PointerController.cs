using UnityEngine;

public class PointerController : GameplayElement
{
    [SerializeField] private float rotationSpeed = 0.5f;
    private Animator animator;
    Vector3 mousePosition = new();
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public override void GameUpdate()
    {
        gameObject.transform.Rotate(Vector3.forward, rotationSpeed);
    }

    public void SetPosition(float newPos)
    {
        transform.position = new(newPos, transform.position.y, transform.position.z);
    }

    public void Move(float speed)
    {
        animator.SetBool("Idle", speed == 0f);
    }

}

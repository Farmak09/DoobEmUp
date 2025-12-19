using UnityEngine;

[System.Serializable]
public class MovementVariables
{
    [SerializeField]
    private float maxSpeed;
    private float speed = 0f;

    [SerializeField]
    private float accel;

    public void ResetVariables()
    {
        speed = 0f;
    }

    public float GetSpeed(float target)
    {
        if (Mathf.Abs(target) - Time.deltaTime * Mathf.Abs(speed) < Global.MOVEMENT_DEADZONE)
        {
            speed = 0f;
            return 0f;
        }

        int direction = target < 0 ? -1 : 1;

        float ret = maxSpeed * direction;

        if (Mathf.Abs(speed) < maxSpeed)
        {
            ret = speed;
            speed += Time.deltaTime * accel * direction;
        }
        return ret;
    }
}

[System.Serializable]
public class HealthVariables
{
    [SerializeField]
    private float maxHP;
    private float currentHP;
}

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/Dooby")]
public class PlayerStats : ScriptableObject
{
    [SerializeField]
    private MovementVariables movement;

    [SerializeField]
    private HealthVariables health;

    private bool _controlled = false;
    public bool Controlled
    {
        get { return _controlled; }
        set { _controlled = value; }
    }

    public float GetSpeed(float direction)
    {
        return movement.GetSpeed(direction);
    }

    public void ResetVariables()
    {
        movement.ResetVariables();
    }
}

using System;

public class StateMashineData
{
    public float XVelosity;
    public float YVelosity;

    private float speed;
    private float xInput;

    public float XInput
    {
        get => xInput;
        set
        {
            if (xInput < -1 || xInput > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            xInput = value;
        }
    }
    public float Speed
    {
        get => speed;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            speed = value;
        }
    }
}

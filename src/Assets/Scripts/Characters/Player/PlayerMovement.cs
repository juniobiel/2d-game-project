using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigPlayer;
    private Animator animator;

    [SerializeField, Header("Player Attributes")] private float Speed;

    [SerializeField, Header("Player Joystick")] private FixedJoystick joystick;


    void Start()
    {
        _rigPlayer = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }



    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        float _horizontal = joystick.Horizontal;
        float _vertical = joystick.Vertical;

        Vector2 dir = new(_horizontal, _vertical);
        _rigPlayer.MovePosition(_rigPlayer.position + Speed * Time.fixedDeltaTime * dir);

        if (_vertical == 0 || _horizontal == 0)
        {
            animator.SetFloat("Speed", 0);
        }
        else
        {
            animator.SetFloat("Speed", 1);
        }

        VerifyMovimentAnimation(dir, Speed);

    }

    private const float Y_UP = 0.50f;
    private const float Y_DOWN = -0.50f;

    private const float Y_UP_TOLERANCE = 0.1f;
    private const float Y_DOWN_TOLERANCE = -0.1f;

    private const float X_RIGHT_TOLERANCE = 0.1f;
    private const float X_LEFT_TOLERANCE = -0.1f;

    private const float X_RIGHT = 0.50f;
    private const float X_LEFT = -0.50f;

    public void VerifyMovimentAnimation(Vector2 direction, float speed)
    {
        var xPosition = direction.x;
        var yPosition = direction.y;

        if (speed > 0)
        {

            if (xPosition >= X_RIGHT && (yPosition >= Y_DOWN_TOLERANCE || yPosition <= Y_UP_TOLERANCE))
                AnimationMovimentRight();

            if (xPosition <= X_LEFT && (yPosition >= Y_DOWN_TOLERANCE || yPosition <= Y_UP_TOLERANCE))
                AnimationMovimentLeft();

            if (yPosition >= Y_UP && (xPosition >= X_LEFT_TOLERANCE || yPosition <= X_RIGHT_TOLERANCE))
                AnimationMovimentUp();

            if (yPosition <= Y_DOWN && (xPosition >= X_LEFT_TOLERANCE || yPosition <= X_RIGHT_TOLERANCE))
                AnimationMovimentDown();

            if (xPosition >= X_RIGHT_TOLERANCE && yPosition >= Y_UP_TOLERANCE)
                AnimationMovimentRightUp();

            if (xPosition >= X_RIGHT_TOLERANCE && yPosition <= Y_DOWN_TOLERANCE)
                AnimationMovimentRightDown();

            if (xPosition <= X_LEFT_TOLERANCE && yPosition >= Y_UP_TOLERANCE)
                AnimationMovimentLeftUp();

            if (xPosition <= X_LEFT_TOLERANCE && yPosition <= Y_DOWN_TOLERANCE)
                AnimationMovimentLeftDown();
        }
    }


    public void SetAnimatorParameters(float horizontal, float vertical)
    {
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

    }

    private void AnimationMovimentUp() => SetAnimatorParameters(0, 1);

    private void AnimationMovimentDown() => SetAnimatorParameters(0, -1);

    private void AnimationMovimentLeft() => SetAnimatorParameters(-1, 0);

    private void AnimationMovimentRight() => SetAnimatorParameters(1, 0);

    private void AnimationMovimentRightUp() => SetAnimatorParameters(1, 1);

    private void AnimationMovimentRightDown() => SetAnimatorParameters(1, -1);

    private void AnimationMovimentLeftUp() => SetAnimatorParameters(-1, 1);

    private void AnimationMovimentLeftDown() => SetAnimatorParameters(-1, -1);

}

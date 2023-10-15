using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    protected Animator _animator;
    protected Transform _playerTransform;
    
    [SerializeField]
    protected float Speed;

    protected const float Y_UP = 0.95f;
    protected const float Y_DOWN = -0.95f;

    protected const float Y_UP_TOLERANCE = 0.3f;
    protected const float Y_DOWN_TOLERANCE = -0.3f;

    protected const float X_RIGHT_TOLERANCE = 0.3f;
    protected const float X_LEFT_TOLERANCE = -0.3f;

    protected const float X_RIGHT = 0.95f;
    protected const float X_LEFT = -0.95f;

    protected void VerifyMovimentAnimation(Vector2 direction, float speed)
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

    protected Vector3 GetEnemyMoveDirection()
    {
        var heading = _playerTransform.position - transform.position;
        var direction = heading / heading.magnitude;
        
        return direction;
    }

    protected void EnemyMoviment()
    {
        var moveTowards = Vector2.MoveTowards(transform.position, _playerTransform.position, Speed * Time.deltaTime);

        transform.position = moveTowards;
    }

    protected void SetAnimatorParameters(float horizontal, float vertical)
    {
        _animator.SetFloat("Horizontal", horizontal);
        _animator.SetFloat("Vertical", vertical);
    }

    protected void AnimationMovimentUp() => SetAnimatorParameters(0, 1);

    protected void AnimationMovimentDown() => SetAnimatorParameters(0, -1);

    protected void AnimationMovimentLeft() => SetAnimatorParameters(-1, 0);

    protected void AnimationMovimentRight() => SetAnimatorParameters(1, 0);

    protected void AnimationMovimentRightUp() => SetAnimatorParameters(1, 1);

    protected void AnimationMovimentRightDown() => SetAnimatorParameters(1, -1);

    protected void AnimationMovimentLeftUp() => SetAnimatorParameters(-1, 1);

    protected void AnimationMovimentLeftDown() => SetAnimatorParameters(-1, -1);
}

using UnityEngine;

public class BelzebuController : MonoBehaviour
{
    private Transform _playerTransform;
    private Animator _animator;

    [SerializeField]
    private float Speed;
    private bool CanMove;

    private const float Y_UP = 0.95f;
    private const float Y_DOWN = -0.95f;

    private const float Y_UP_TOLERANCE = 0.03f;
    private const float Y_DOWN_TOLERANCE = - 0.3f;

    private const float X_RIGHT_TOLERANCE = 0.3f;
    private const float X_LEFT_TOLERANCE = - 0.3f;

    private const float X_RIGHT = 0.95f;
    private const float X_LEFT = - 0.95f;

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _animator = gameObject.GetComponent<Animator>();
        CanMove = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var playerDistance = Vector2.Distance(_playerTransform.position, gameObject.transform.position);

        if(playerDistance > 2.0f && !CanMove) 
            CanMove = true;
        
        if(CanMove)
        {
            var heading = _playerTransform.position - transform.position;
            var direction = heading / heading.magnitude;

            var moveTowards = Vector2.MoveTowards(transform.position, _playerTransform.position, Speed * Time.deltaTime);

            transform.position = moveTowards;

            VerifyMovimentAnimation(direction, Speed);
        }

    }

    private void OnCollisionEnter2D( Collision2D collision )
    {
        CanMove = false;
        SetAnimatorParameters(0, 0);
    }

    private void VerifyMovimentAnimation(Vector2 direction, float speed)
    {
        var xPosition = direction.x;
        var yPosition = direction.y;

        if(speed > 0)
        {

            if (xPosition >= X_RIGHT && (yPosition >= Y_DOWN_TOLERANCE || yPosition <= Y_UP_TOLERANCE))
                AnimationMovimentRight();

            if (xPosition <= X_LEFT && (yPosition >= Y_DOWN_TOLERANCE || yPosition <= Y_UP_TOLERANCE))
                AnimationMovimentLeft();

            if (yPosition >= Y_UP && (xPosition >= X_LEFT_TOLERANCE || yPosition <= X_RIGHT_TOLERANCE))
                AnimationMovimentUp();

            if (yPosition <= Y_DOWN && (xPosition >= X_LEFT_TOLERANCE || yPosition <= X_RIGHT_TOLERANCE))
                AnimationMovimentDown();

            if(xPosition >= X_RIGHT_TOLERANCE && yPosition >= Y_UP_TOLERANCE)
                AnimationMovimentRightUp();

            if (xPosition >= X_RIGHT_TOLERANCE && yPosition <= Y_DOWN_TOLERANCE)
                AnimationMovimentRightDown();

            if (xPosition <= X_LEFT_TOLERANCE && yPosition >= Y_UP_TOLERANCE)
                AnimationMovimentLeftUp();

            if (xPosition <= X_LEFT_TOLERANCE && yPosition <= Y_DOWN_TOLERANCE)
                AnimationMovimentLeftDown();
        }
    }


    private void SetAnimatorParameters(float horizontal, float vertical)
    {
        _animator.SetFloat("Horizontal", horizontal);
        _animator.SetFloat("Vertical", vertical);
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

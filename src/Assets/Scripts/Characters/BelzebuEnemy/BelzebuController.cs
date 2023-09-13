using UnityEngine;

public class BelzebuController : MonoBehaviour
{
    private Transform _playerTransform;

    [SerializeField]
    private float Speed;
    private bool CanMove;

    private Vector2 auxActualPosition;

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        CanMove = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var playerDistance = Vector2.Distance(_playerTransform.position, gameObject.transform.position);

        if(playerDistance > 1.0f && !CanMove) 
            CanMove = true;
        

        if(CanMove)
        {
            auxActualPosition = transform.position;
            var moveTowards = Vector2.MoveTowards(transform.position, _playerTransform.position, Speed * Time.deltaTime);

            transform.position = moveTowards;

            float positionX = auxActualPosition.x - transform.position.x;
            float positionY = auxActualPosition.y - transform.position.y;
            Debug.LogError($"{positionX} e {positionY}");

            var heading = _playerTransform.position - transform.position;
            var direction = heading / heading.magnitude;

            //Debug.DrawLine(transform.position, _playerTransform.position, Color.red);
            Debug.DrawRay(transform.position, direction, Color.red);
            Debug.LogWarning(direction);
            //VerifyMovimentAnimation(positionX, positionY, Speed);
        }

    }

    private void OnCollisionEnter2D( Collision2D collision )
    {
        CanMove = false;

    }

    private void VerifyMovimentAnimation(float positionX, float positionY, float speed)
    {

        if(speed > 0)
        {
            //Right moviments
            if (positionX > 0 && positionY > 0)
                AnimationMovimentRightUp();

            if (positionX > 0 && positionY < 0)
                AnimationMovimentRightDown();

            if (positionX > 0 && positionY == 0)
                AnimationMovimentRight();
        }
    }

    private void AnimationMovimentUp()
    {

    }

    private void AnimationMovimentDown()
    {

    }

    private void AnimationMovimentLeft()
    {

    }

    private void AnimationMovimentRight()
    {

    }

    private void AnimationMovimentRightUp()
    {

    }

    private void AnimationMovimentRightDown()
    {

    }

    private void AnimationMovimentLeftUp()
    {

    }

    private void AnimationMovimentoLeftDown()
    {

    }

}

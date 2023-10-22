using UnityEngine;

public class BelzebuController : EnemyController
{
    private bool CanMove;

    [SerializeField]
    private EnemyLifeBarHUD _lifeHUD;

    private void OnEnable()
    {
        _lifeHUD = gameObject.GetComponentInChildren<EnemyLifeBarHUD>();
        Life = 100f;
        _lifeHUD.SetEnemyLife(Life);
    }

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _animator = gameObject.GetComponent<Animator>();
        CanMove = true;

        _lifeHUD.RemoveHealthPoint(62);
    }

    void FixedUpdate()
    {
        var playerDistance = Vector2.Distance(_playerTransform.position, gameObject.transform.position);

        if(playerDistance > 2.0f && !CanMove) 
            CanMove = true;
        
        if(CanMove)
        {
            Vector3 direction = GetEnemyMoveDirection();
            EnemyMoviment();

            VerifyMovimentAnimation(direction, Speed);
        }
    }

    private void OnCollisionEnter2D( Collision2D collision )
    {
        CanMove = false;
        SetAnimatorParameters(0, 0);
    }
}

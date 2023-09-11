using UnityEngine;

public class BelzebuController : MonoBehaviour
{
    private Transform _playerTransform;

    [SerializeField]
    private float Speed;

    private bool CanMove;

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        CanMove = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(CanMove)
        {
            Debug.Log($"Diff - 1:{transform.position}; 2 - {Vector2.MoveTowards(transform.position, _playerTransform.position, Speed * Time.deltaTime)}");
            var moveTowards = Vector2.MoveTowards(transform.position, _playerTransform.position, Speed * Time.deltaTime);

            float positionX = transform.position.x - moveTowards.x;
            float positionY = transform.position.y - moveTowards.y;
            Debug.LogError($"{positionX} e {positionY}");
            transform.position = Vector2.MoveTowards(transform.position, _playerTransform.position, Speed * Time.deltaTime);

            Debug.Log($"Angle - {Vector2.Angle(moveTowards, _playerTransform.position)}");
            Debug.Log($"Dot - {Vector2.Dot(moveTowards, _playerTransform.position)}");
            Debug.LogWarning($"Clamp Magnitude - {Vector2.ClampMagnitude(moveTowards, 0.1f)}");
  
        }

    }

    private void OnCollisionEnter2D( Collision2D collision )
    {
        CanMove = false;

        CanMove = true;
    }

}

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
            transform.position = Vector2.MoveTowards(transform.position, _playerTransform.position, Speed * Time.deltaTime);
        }

    }

    private void OnCollisionEnter2D( Collision2D collision )
    {
        CanMove = false;

        CanMove = true;
    }

}

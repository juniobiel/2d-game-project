using UnityEngine;

namespace Assets.Character.Enemy.Scripts.Fear
{
    public class FearEnemyController : EnemyController
    {
        Rigidbody2D Rb;

        [SerializeField] bool IsRightPatrol;
        [SerializeField] bool IsEcholocationActive;
        [SerializeField] bool IsIddleActive;
        [SerializeField] float timer;

        GameObject playerObject;

        private void Start()
        {
            Rb = GetComponent<Rigidbody2D>();
            playerObject = GameObject.FindGameObjectWithTag("Player");
            IsRightPatrol = false;   
            IsEcholocationActive = false;
            timer = 0;
        }

        private void FixedUpdate()
        {
            Patrol();
        }

        protected override void Patrol()
        {
            if(!IsRightPatrol)
            {
                MoveEnemyLeft();

                VerifyPatrolLeft();

                Echolocatization();

                IddleAnimation(IsRightPatrol);
            }

            else
            {
                MoveEnemyRight();

                VerifyPatrolRight();

                Echolocatization();

                IddleAnimation(IsRightPatrol);
            }

        }

        private void MoveEnemyLeft()
        {
            if (gameObject.transform.position.x >= PointsPatrol.InitialPoint.x)
                Rb.velocity = -(Speed * Time.deltaTime * PointsPatrol.InitialPoint);
        }
        private void MoveEnemyRight()
        {
            if (gameObject.transform.position.x <= PointsPatrol.FinalPoint.x)
                Rb.velocity = (Speed * Time.deltaTime * PointsPatrol.InitialPoint);
        }
        private void VerifyPatrolLeft()
        {
            if (gameObject.transform.position.x <= PointsPatrol.InitialPoint.x && !IsIddleActive)
                IsEcholocationActive = true;
        }
        private void VerifyPatrolRight()
        {
            if (gameObject.transform.position.x >= PointsPatrol.FinalPoint.x && !IsIddleActive)
                IsEcholocationActive = true;
        }

        private void Echolocatization()
        {
            if (IsEcholocationActive)
            {
                timer += Time.deltaTime;
                if (timer <= 2f)
                {
                    float playerDistance = Vector3.Distance(gameObject.transform.position, playerObject.GetComponent<Transform>().position);

                    Debug.Log("Echolocalizating");

                    if (playerDistance <= 3)
                    {
                        Debug.Log("Attack the player");
                    }
                }
                else
                {
                    IsEcholocationActive = false;
                    IsIddleActive = true;
                    timer -= timer;
                }
            }
        }

        private void IddleAnimation(bool isRightPatrol)
        {
            if (IsIddleActive)
            {
                timer += Time.deltaTime;
                if (timer <= 2f)
                    Debug.Log("Iddle Animation");
                else
                {
                    IsIddleActive = false;
                    IsRightPatrol = !isRightPatrol;
                    timer -= timer;
                }
            }
        }

    }

}

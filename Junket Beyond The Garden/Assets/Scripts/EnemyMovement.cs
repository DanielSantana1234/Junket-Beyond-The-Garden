using UnityEngine;

        public class ChaseEnemy : MonoBehaviour
        {
            public Transform playerTransform;
            public float chaseSpeed = 3f;

            void Update()
            {
                if (playerTransform == null) return;

                Vector2 direction = (playerTransform.position - transform.position).normalized;
                transform.Translate(direction * chaseSpeed * Time.deltaTime);
            }
        }
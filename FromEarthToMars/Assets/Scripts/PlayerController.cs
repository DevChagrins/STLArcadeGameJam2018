using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chagrins
{
    using Utility;
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : BaseController
    {
        public float speed;
        public ContactFilter2D contactFilter;
        public float colliderError = 0.001f;

        Vector3 velocity = Vector3.zero;

        void Start()
        {
            _Initialize();
        }

        void FixedUpdate()
        {
            _Update();
        }

        protected override void _Step()
        {
            Vector3 tempVelocity = new Vector3();

            if(Input.GetKey(KeyCode.A))
            {
                tempVelocity.x -= speed;
            }

            if (Input.GetKey(KeyCode.D))
            {
                tempVelocity.x += speed;
            }

            if (Input.GetKey(KeyCode.W))
            {
                tempVelocity.y += speed;
            }

            if (Input.GetKey(KeyCode.S))
            {
                tempVelocity.y -= speed;
            }

            float distance = speed * Time.deltaTime;
            Vector2 direction = tempVelocity.normalized;
            velocity = direction * distance;

            CheckCollision(new Vector2(direction.x, 0f), velocity.x);
            CheckCollision(new Vector2(0f, direction.y), velocity.y);

            transform.position += velocity;
        }

        bool CheckCollision(Vector2 _direction, float _distance)
        {
            bool impactCollision = false;
            Vector2 newVelocity = Vector2.zero;

            RaycastHit2D[] collisionResults = new RaycastHit2D[10];

            // General collision checks
            int colliderCount = Physics2D.BoxCastNonAlloc(new Vector2(transform.position.x, transform.position.y) + collider2D.offset, boxCollider2D.size, 0f, _direction, collisionResults, _distance, contactFilter.layerMask);
            //int colliderCount = collider2D.Cast(_direction, contactFilter, collisionResults, _distance);

            Vector2 leftOverTravel = Vector2.zero;
            if(colliderCount > 0)
            {
                Debug.Log("Collider Count: " + colliderCount);
                for(int colliderIndex = 0; colliderIndex < colliderCount; colliderIndex++)
                {
                    if(HandleCollision(collisionResults[colliderIndex]))
                    {
                        impactCollision = true;
                    }
                }
            }

            return impactCollision;
        }

        // Overridable to make adjustments to the reaction
        protected virtual bool HandleCollision(RaycastHit2D _hit)
        {
            int collisionCount = 0;
            bool collisionHappened = false;
            Vector3 position = transform.position;
            if(!Maths.EqualZero(_hit.normal.x))
            {
                float moveSign = Mathf.Sign(_hit.normal.x);
                if (!Maths.EqualZero(velocity.x) && (moveSign != Mathf.Sign(velocity.x)))
                {
                    velocity.x = 0;
                    position.x = _hit.point.x - (((boxCollider2D.size.x * 0.5f) + boxCollider2D.offset.x + colliderError) * -moveSign);
                    collisionHappened = true;
                    collisionCount++;
                }
            }

            // Figure out why the Y is all fucky. Maybe check with no offset
            if (!Maths.EqualZero(_hit.normal.y))
            {
                float moveSign = Mathf.Sign(_hit.normal.y);
                if (!Maths.EqualZero(velocity.y) && (moveSign != Mathf.Sign(velocity.y)))
                {
                    velocity.y = 0;
                    position.y = _hit.point.y - (((boxCollider2D.size.y * 0.5f) + boxCollider2D.offset.y + colliderError) * -moveSign);
                    collisionHappened = true;
                    collisionCount++;
                }
            }

            if (collisionHappened)
            {
                if (!Maths.EqualZero(velocity.x))
                {
                    velocity.x = transform.position.x - position.x;
                }

                if (!Maths.EqualZero(velocity.y))
                {
                    velocity.y = transform.position.y - position.y;
                }
                transform.position = position;
            }

            return collisionHappened;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
        }
    }
}
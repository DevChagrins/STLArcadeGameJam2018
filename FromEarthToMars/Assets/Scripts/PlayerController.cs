﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chagrins
{
    using Utility;
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : BaseController
    {
        public bool Player1 = true;
		public GameObject DrillIcon;
        public float speed;
        public ContactFilter2D contactFilter;
        public ContactFilter2D pointOfInterestFilter;
        public float colliderError = 0.001f;
        public float inputDelay = 1.0f;
        public float actionTime = 1.0f;

        Vector3 velocity = Vector3.zero;
        bool firstCall = true;
        private List<InputCommand> m_pendingInputs;
        public InputCommand m_lastInput;

        private float m_pauseInputTime = 0.0f;
        private CountDownTimer countDownTimer = null;

		private float m_bonusTimeValue = 0.0f;

        void Start()
        {
            _Initialize();
            m_pendingInputs = new List<InputCommand>();
            countDownTimer = GameObject.FindGameObjectWithTag("Timer")?.GetComponent<CountDownTimer>();
        }

        void Update() { }

        void FixedUpdate()
        {
            _Update();
        }

        private void IssueInputs()
        {
            Vector3 targetVel = new Vector3();
            if (Player1)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    targetVel.x -= speed;
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    targetVel.x += speed;
                }

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    targetVel.y += speed;
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    targetVel.y -= speed;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.A))
                {
                    targetVel.x -= speed;
                }

                if (Input.GetKey(KeyCode.D))
                {
                    targetVel.x += speed;
                }

                if (Input.GetKey(KeyCode.W))
                {
                    targetVel.y += speed;
                }

                if (Input.GetKey(KeyCode.S))
                {
                    targetVel.y -= speed;
                }
            }
            if (targetVel != Vector3.zero)
            {
                if (m_lastInput == null ||
                    m_lastInput.direction != targetVel)
                {
                    InputCommand newCommand = new InputCommand();
                    newCommand.ActiveTime = Time.timeSinceLevelLoad + inputDelay;
                    newCommand.duration = 0f;
                    newCommand.direction = targetVel;
                    newCommand.Delay = inputDelay;
                    m_pendingInputs.Add(newCommand);
                    m_lastInput = newCommand;
                    FindObjectOfType<ArrowManager>().CreateArrow(newCommand, Player1);
                }
                else
                {
                    m_lastInput.duration += Time.fixedDeltaTime;
                }
            }
            else
            {
                m_lastInput = null;
            }
        }

        private Vector3 calculateTempVelocity()
        {
            List<InputCommand> m_nextInputs = new List<InputCommand>();
            Vector3 tempVelocity = new Vector3();
            foreach (InputCommand ic in m_pendingInputs)
            {
                if (ic.IsActive(Time.timeSinceLevelLoad))
                {
                    tempVelocity += ic.direction;
                }
                if (!ic.IsDead(Time.timeSinceLevelLoad))
                {
                    m_nextInputs.Add(ic);
                }
            }
            m_pendingInputs = m_nextInputs;
            return tempVelocity;
        }

        protected override void _Step()
        {
			if (m_pauseInputTime <= 0.0f)
				IssueInputs ();
			else {
				countDownTimer?.AddTime(m_bonusTimeValue * Time.deltaTime);
				m_pauseInputTime -= Time.deltaTime;
			}
            Vector3 tempVelocity = calculateTempVelocity();

            // Collision!
            float distance = speed * Time.deltaTime;
            Vector2 direction = tempVelocity.normalized;
            velocity = direction * distance;

            CheckLeftRight(direction.x, velocity.x);
            CheckUpDown(direction.y, velocity.y);

            transform.position += velocity;

            CheckPointOfInterests();

            // Sprite stuff!!!
            if (Maths.EqualZero(direction.y) && !Maths.EqualZero(direction.x))
            {
                if (Maths.EqualZero(animator.speed))
                {
                    animator.speed = 1f;
                }

                if (direction.x >= 0f)
                {
                    spriteRenderer.flipX = false;
                    animator.Play("Side");
                }
                else
                {
                    spriteRenderer.flipX = true;
                    animator.Play("Side");
                }
            }
            else if (!Maths.EqualZero(direction.y))
            {
                if (Maths.EqualZero(animator.speed))
                {
                    animator.speed = 1f;
                }

                if (direction.y >= 0f)
                {
                    animator.Play("Down");
                }
                else
                {
                    animator.Play("Up");
                }
            }
            else
            {
                animator.speed = 0f;
            }

        }

        void CheckPointOfInterests()
        {
            Collider2D[] colliderResults = new Collider2D[10];
            int results = Physics2D.OverlapCollider(collider2D, pointOfInterestFilter, colliderResults);

            if (results > 0)
            {
                CollectPointOfInterest(colliderResults[0]);
            }
        }

        void CollectPointOfInterest(Collider2D collider)
        {
            PointOfInterest poi = collider.gameObject.GetComponent<PointOfInterest>();

            if (poi == null)
                return;
            // Add time
            float? timeValue = poi?.GetTimeValue();

            if (timeValue.HasValue)
            {
                //Debug.Log("Collecting!");
                // Actually add the time to the overall counter
                
            }
            // Delay input
			m_bonusTimeValue = timeValue.Value / actionTime;
            FreezeInput(actionTime, true);
			FindObjectOfType<CountDownTimer> ().AddPickUp (1, Player1);

			Destroy (Instantiate (DrillIcon, transform.position + new Vector3 (0.0f, 0.25f, 0f), Quaternion.identity), actionTime);

            // Disable collision on point of interest
            poi?.EnableSelfDestruction(actionTime);
        }

        public void FreezeInput(float f, bool ClearCurrentInputs)
        {
            if (m_pauseInputTime > 0f) 
                return;
            m_pauseInputTime = f;
            foreach (InputCommand i in m_pendingInputs)
                i.duration = 0f;
            if (ClearCurrentInputs)
                m_pendingInputs.Clear();
        }

        bool CheckLeftRight(float _xDirection, float _xSpeed)
        {
            bool impactCollision = false;
            Vector2 newVelocity = Vector2.zero;

            RaycastHit2D[] collisionResults = new RaycastHit2D[10];
            Vector2 direction = new Vector2(_xDirection, 0f);

            // General collision checks
            int colliderCount = Physics2D.BoxCastNonAlloc(new Vector2(transform.position.x, transform.position.y) + collider2D.offset, boxCollider2D.size, 0f, direction, collisionResults, _xSpeed, contactFilter.layerMask);
            Vector3 position = transform.position;

            Vector2 leftOverTravel = Vector2.zero;
            if (colliderCount > 0)
            {
                for (int colliderIndex = 0; colliderIndex < colliderCount; colliderIndex++)
                {
                    RaycastHit2D hit = collisionResults[colliderIndex];
                    if (!hit.collider.isTrigger && !Maths.EqualZero(hit.normal.x))
                    {
                        float moveSign = Mathf.Sign(hit.normal.x);
                        if (!Maths.EqualZero(_xSpeed) && (moveSign != Mathf.Sign(_xSpeed)))
                        {
                            velocity.x = 0;
                            position.x = hit.point.x - (((boxCollider2D.size.x * 0.5f) + boxCollider2D.offset.x + colliderError) * -moveSign);
                            impactCollision = true;
                        }
                    }
                }
            }

            return impactCollision;
        }

        bool CheckUpDown(float _yDirection, float _ySpeed)
        {
            bool impactCollision = false;
            Vector2 newVelocity = Vector2.zero;

            RaycastHit2D[] collisionResults = new RaycastHit2D[10];
            Vector2 direction = new Vector2(0f, _yDirection);

            // General collision checks
            int colliderCount = Physics2D.BoxCastNonAlloc(new Vector2(transform.position.x, transform.position.y) + collider2D.offset, boxCollider2D.size, 0f, direction, collisionResults, _ySpeed, contactFilter.layerMask);
            Vector3 position = transform.position;

            Vector2 leftOverTravel = Vector2.zero;
            if (colliderCount > 0)
            {

                for (int colliderIndex = 0; colliderIndex < colliderCount; colliderIndex++)
                {
                    RaycastHit2D hit = collisionResults[colliderIndex];
                    //Debug.Log (hit.collider.gameObject);
                    if (!hit.collider.isTrigger && !Maths.EqualZero(hit.normal.y))
                    {
                        float moveSign = Mathf.Sign(hit.normal.y);
                        if (!Maths.EqualZero(_ySpeed) && (moveSign != Mathf.Sign(_ySpeed)))
                        {

                            velocity.y = 0;
                            position.y = hit.point.y - (((boxCollider2D.size.y * 0.5f) + boxCollider2D.offset.y + colliderError) * -moveSign);
                            impactCollision = true;
                        }
                    }
                }
            }

            return impactCollision;
        }

        // Overridable to make adjustments to the reaction
        protected virtual bool HandleCollision(RaycastHit2D _hit)
        {
            bool collisionHappened = true;

            return collisionHappened;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Hazard"))
            {
                // Lose command here
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player{
    public class PlayerController : MonoBehaviour
    {
        [Header("X Movement")]
        public float playerSpeed;

        [Space]
        [Header("Jumping")]
        public LayerMask groundLayer;
        public float jumpForce;
        public float gravity;
        public float maxJumpDuration;

        private float timeInAir;
        
        [Space]
        [Header("Components / Objects")]
        [HideInInspector] public Rigidbody2D m_rigidbody;
        [HideInInspector] public SpriteRenderer m_renderer;

        void ConfigureComponents(){
            m_rigidbody = gameObject.GetComponent<Rigidbody2D>();
            m_rigidbody.gravityScale *= gravity;
            m_renderer = gameObject.GetComponent<SpriteRenderer>();
        }

        void Start(){
            ConfigureComponents();
        }

        void Update(){
            LateralMovement();
            JumpMovement();
        }

        void LateralMovement(){

            int direction = (int)Input.GetAxisRaw("Horizontal");

            SetRigidbodySpeed(playerSpeed * direction * Time.deltaTime, m_rigidbody.velocity.y);

            if (direction != 0){
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y);
            }
        }

        bool TouchingFloor(){

            const float EXTRA_LENGTH = 1f;
            
            RaycastHit2D rayCenter = Physics2D.Raycast(transform.position, Vector3.down, (transform.localScale.y / 2) + EXTRA_LENGTH, groundLayer);
            RaycastHit2D rayLeft = Physics2D.Raycast(new Vector2((float)transform.position.x + (float)(transform.localScale.x / 2 - 0.1), transform.position.y) , Vector3.down, (transform.localScale.y / 2) + EXTRA_LENGTH, groundLayer);
            RaycastHit2D rayRight = Physics2D.Raycast(new Vector2((float)transform.position.x - (float)(transform.localScale.x / 2 - 0.1), transform.position.y),  Vector3.down, (transform.localScale.y / 2) + EXTRA_LENGTH, groundLayer);

            return rayCenter || rayLeft || rayRight;
        }

        void JumpMovement(){

            bool JUMP_BUTTON = Input.GetKey(KeyCode.Space);

            bool canJump = false;

            if (JUMP_BUTTON && timeInAir < maxJumpDuration && canJump){
                timeInAir += Time.deltaTime;
                SetRigidbodySpeed(m_rigidbody.velocity.x, jumpForce * Time.deltaTime);
            }

            if (TouchingFloor()){
                timeInAir = 0;

                canJump = true;

                if (JUMP_BUTTON){
                    canJump = false;
                }
            }
        }

        void SetRigidbodySpeed(float x, float y){
            m_rigidbody.velocity = new Vector2(x, y);
        }
    }
}
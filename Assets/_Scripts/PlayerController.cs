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
        public float jumpForce;
        public float gravity;
        
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
        }

        void LateralMovement(){

            int direction = (int)Input.GetAxisRaw("Horizontal");

            SetRigidbodySpeed(playerSpeed * direction, m_rigidbody.velocity.y);
        }

        void SetRigidbodySpeed(float x, float y){
            m_rigidbody.velocity = new Vector2(x * Time.deltaTime, y * Time.deltaTime);
        }
    }
}
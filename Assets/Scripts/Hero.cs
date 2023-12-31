using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _damageJumpForce;

    [SerializeField] private LayerCheck _groundCheck;
    [SerializeField] private float _interactionRadius;
    [SerializeField] private LayerMask _interactionLayer;

    [SerializeField] private SpawnComponents _footStepParticles;
    [SerializeField] private SpawnComponents _jumpParticles;
    
    private Collider2D[] _interactionResult = new Collider2D[1];
    private Vector2 _direction;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private bool _isGrounded;
    private bool _allowDoubleJump;

    private static readonly int IsGroundedKey = Animator.StringToHash("IsGrounded");
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int VerticalVelocity = Animator.StringToHash("VerticalVelocity");
    private static readonly int Hit = Animator.StringToHash("Hit");

    private void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update() 
    {
        _isGrounded = _groundCheck.IsTouchingLayer;
    }

    private void FixedUpdate() 
    {
        var xVelocity = _direction.x * _speed;
        var yVelocity = CalculateYVelocity();
        _rigidbody.velocity = new Vector2(xVelocity, yVelocity);

        _animator.SetBool(IsGroundedKey, _isGrounded);
        _animator.SetFloat(VerticalVelocity, _rigidbody.velocity.y);
        _animator.SetBool(IsRunning, _direction.x != 0);

        UpdateSpriteDirection();
    }

    private float CalculateYVelocity()
    {
        var yVelocity = _rigidbody.velocity.y;
        var isJumpPressing = _direction.y > 0;

        if(_isGrounded) _allowDoubleJump = true;
        if(isJumpPressing)
        {
            yVelocity = CalculateJumpVelocity(yVelocity);
        }
        else if(_rigidbody.velocity.y > 0)
        {
            yVelocity *= 0.5f;
        }

        return yVelocity;
    }

    private float CalculateJumpVelocity(float yVelocity)
    {
        var isFalling = _rigidbody.velocity.y <= 0.001f;
        if(!isFalling) return yVelocity;

        if(_isGrounded)
        {
            SpawnJumpDust();
            yVelocity += _jumpForce;
        }
        else if(_allowDoubleJump)
        {
            yVelocity = _jumpForce;
            _allowDoubleJump = false;
        }

        return yVelocity;
    }

    private void UpdateSpriteDirection()
    {
        if(_direction.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if(_direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    public void TakeDamage()
    {
        _animator.SetTrigger(Hit);
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damageJumpForce);
    }

    public void Interact()
    {
        var size = Physics2D.OverlapCircleNonAlloc(transform.position, _interactionRadius, _interactionResult, _interactionLayer);

        for(int i = 0; i < size; i++)
        {
            var interactable = _interactionResult[i].GetComponent<InteractableComponent>();
            if(interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    public void SpawnFootDust()
    {
        _footStepParticles.Spawn();
    }

    public void SpawnJumpDust()
    {
        _jumpParticles.Spawn();
    }
}

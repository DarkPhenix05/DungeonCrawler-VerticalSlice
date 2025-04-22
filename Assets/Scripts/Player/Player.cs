using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [Header("Input")] 
    public PlayerInput _playerInput;
    public PlayerInputActions _playerInputActions;


    [Header("Movement")]
    public Rigidbody _rb;
    public Camera _mainCamera;
    public Vector3 _cameraFoward;
    public Vector3 _cameraRight;

    public float _speed;
    public float _holdingSpeed;
    public float _rotationSpeed;
    public bool _snapping;
    public bool _rotating = true;
    
    private Vector2 _movement;

    private float _tempSpeed;

    [Header("HP")]
    public bool _isDead = false;
    public int _hp;
    public int _maxHp = 10;
    public HealthBar _healthBar;

    [Header("Attack")]
    public bool _attacking = false;
    public bool _holding = false;
    public bool _heaviAttak = false;
    public float _holdTime = 1.5f;
    public Collider _attackCollider;
    private float _attackDuration = .5f;
    private Sword _sword;

    [Header("Animation")]
    [SerializeField] private Animator _anim;
    Vector3 _rbSpeed;

    [Header("Audio")]
    [SerializeField] private AudioManager _audioManager;
    
    [Header("Inventory - Interactions")]
    [SerializeField] private GameObject _interactionObject;
    [SerializeField] private Inventorry _inventorry;
    public bool canCollect = false;
    public bool inRange = false;
    public bool chest = false;

    [Header("UI")]
    public GameObject _hud;
    public GameObject _GOUI;

    void Awake()
    {
        _playerInput = this.gameObject.GetComponent<PlayerInput>();
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Movement.performed += Movement_performed;
        
        _inventorry = FindObjectOfType<Inventorry>();
        _rb = this.gameObject.GetComponent<Rigidbody>();
        _anim = this.gameObject.GetComponent<Animator>();
        _audioManager =  this.gameObject.GetComponent<AudioManager>();
        _attackCollider = FindObjectOfType<Sword>().GetComponent<Collider>();
        _sword = _attackCollider.GetComponent<Sword>();
        _attackCollider.gameObject.SetActive(false);
        _hp = _maxHp;
        _healthBar.SetMaxHealth(_maxHp);
    }

    void Update()
    {
        if(!_attacking && !_isDead)
        {
            _movement = _playerInputActions.Player.Movement.ReadValue<Vector2>();
            Movement();
        }
    }

    void Movement()
    {
        if (!_mainCamera)
        {
            return;
        }

        //WASD();
        
        _cameraFoward = _mainCamera.transform.forward;
        _cameraRight = _mainCamera.transform.right;

        _cameraFoward.y = 0;
        _cameraRight.y = 0;

        Vector3 rightRelative = _movement.x * _cameraRight;
        Vector3 forwardRelative = _movement.y * _cameraFoward;

        Vector3 moveDir = (forwardRelative + rightRelative).normalized * _tempSpeed;
        _rb.velocity = new Vector3(moveDir.x, _rb.velocity.y, moveDir.z);
        
        if(_rotating)
            Rotate();

        Animate();
        
        if(_rbSpeed != Vector3.zero)
        {
            if (_rbSpeed.y != 0)
            {
                //Debug.Log("FALLING");
            }
            else
            {
                _audioManager.PlayWalkSound();
            }
        }
        else
        {
                _audioManager.StopWalkSound();
        }
    }

    public void Movement_performed(InputAction.CallbackContext context)
    {
        Debug.Log(context);

       _movement = context.ReadValue<Vector2>();

        if(context.canceled)
        {
            _audioManager.StopWalkSound();
        }
    }

    private void Rotate()
    {
        if(_rbSpeed != Vector3.zero)
        {
            if(_snapping)
            {
                transform.forward = new Vector3(_rbSpeed.x, 0 , _rbSpeed.z);
            }
            else
            {
                Quaternion toRotation = Quaternion.LookRotation(new Vector3(_rbSpeed.x, 0, _rbSpeed.z), Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
            }
        }
    }

    public void Rotate(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _rotating = false;
        }
        else if (context.canceled)
        {
            _rotating = true;
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            TryToInteract();
            Debug.Log("Interact");
        }
    }

    private void TryToInteract()
    {
        if (inRange && canCollect)
        {
            if (chest)
            {
                _interactionObject.GetComponent<Chest>().DropReward();
                _inventorry.UseKey();
            }
        }
        else
        {
            if (chest)
            {
                _interactionObject.GetComponent<Chest>().Loked();
            }
        }
    }

    public bool HaveKey(GameObject key, GameObject interactingObject)
    {
        _interactionObject = interactingObject;
        return key == null || _inventorry.HaveNeededItem(key);
    }

    public void NormalAttack(InputAction.CallbackContext context)
    {
        
        if(_attacking || inRange || _isDead)
            return;
        
        if (context.started)
        {
            _holding = true;
            _tempSpeed = _holdingSpeed;
        }
        else if (context.canceled && !_heaviAttak)
        {
            _anim.SetTrigger("Attack");
            _anim.SetFloat("AttackNum", Random.Range(1, 3));
            _sword.SetPosition(0);
            StartCoroutine(TurnAttackCollider(_attackDuration));

            _audioManager.PlayAttackSound(); 
            
            _tempSpeed = _speed;
            _holding = false;
        }
    }

    public void HeavyAttack(InputAction.CallbackContext context)
    {
        if(_attacking || inRange || _isDead)
            return;
        
        if (context.started)
        {
            _holding = true;
            _tempSpeed = _holdingSpeed;
        }
        else if (context.performed)
        {
            _heaviAttak = true;
            _audioManager.PlayChargedAtackSound();
        }
        else if (context.canceled && _heaviAttak)
        {
            _anim.SetTrigger("Attack"); 
            _anim.SetFloat("AttackNum", 4);
            _sword.SetPosition(1);
            StartCoroutine(TurnAttackCollider(_attackDuration));

            _audioManager.PlayAttackSound();
            
            _tempSpeed = _speed;
            _heaviAttak = false;
            _holding = false;
        }
    }


    private IEnumerator TurnAttackCollider(float attackDuration)
    {
        _attacking = true;
        _attackCollider.gameObject.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        _attacking = false;
        _attackCollider.gameObject.SetActive(false);
    }

    public void TakeDamage(int dmg)
    {
        if(!_isDead)
        {
            _hp -= dmg;
            _healthBar.SetHealth(_hp);

            if (_hp <= 0)
            {
                StartCoroutine(Death());
            }
        }
    }

    private IEnumerator Death()
    {
        _isDead = true;
        _anim.SetTrigger("Dead");

        yield return new WaitForSeconds(0.8f);

        gameObject.SetActive(false);
        GameOver();
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        _hud.SetActive(false);
        _GOUI.SetActive(true);
    }

    private void Animate()
    {
        _rbSpeed = (_rb.velocity).normalized;

        if (_rbSpeed != Vector3.zero)
        {
            _anim.SetBool("Moving", true);
        }
        else
        {
            _anim.SetBool("Moving", false);
        }

        _anim.SetFloat("Ver", Vector3.Dot(_rbSpeed,transform.forward));
        _anim.SetFloat("Hor", Vector3.Dot(_rbSpeed,transform.right));
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        if (_isDead)
            return;

        if (context.started)
        {
            _rb.AddForce(Vector2.up * 5f, ForceMode.Impulse);
            Debug.Log("JUMP");
        }
    }
}

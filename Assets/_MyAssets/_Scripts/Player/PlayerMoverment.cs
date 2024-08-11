using UnityEngine;

public class PlayerMoverment : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _moveSpeed = 2f;
    private PlayerManager _manager;

    void Start()
    {
        _manager = this.GetComponentInParent<PlayerManager>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        var joystickDirection = InputManager.Singleton.JoystikcDiretion;
        if (joystickDirection == Vector2.zero)
        {
            _manager.Anim.SetWalk(false);
            return;
        }

        var gameDirection = new Vector3(joystickDirection.x, 0, joystickDirection.y);
        _rigidbody.velocity = gameDirection * _moveSpeed;
        _manager.Model.rotation = Quaternion.LookRotation(gameDirection);
        _manager.Anim.SetWalk(true);
    }
}
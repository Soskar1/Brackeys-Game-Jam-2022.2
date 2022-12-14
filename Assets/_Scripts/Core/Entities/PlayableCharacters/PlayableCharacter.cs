using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Entities.PlayableCharacters
{
    [RequireComponent(typeof(Animator))]
    public abstract class PlayableCharacter : Entity
    {
        [SerializeField] protected Input _input;
        [SerializeField] private Level _level;
        [SerializeField] private Animator _animator;
        [SerializeField] private bool _isActive = false;
        public bool IsActive => _isActive;

        private Vector2 _movementInput;

        private readonly int _speedAnimProp = Animator.StringToHash("Speed");

        public virtual void Update()
        {
            _movementInput = _input.GetMovementInput(this);
            _animator.SetFloat(_speedAnimProp, Mathf.Abs(_movementInput.magnitude));

            if (_flipping.FacingRight && _movementInput.x < 0 ||
                !_flipping.FacingRight && _movementInput.x > 0)
                _flipping.Flip();
        }

        private void FixedUpdate() => Move(_movementInput);
        public void Activate() => _isActive = true;
        public void Deactivate() => _isActive = false;

        public override void Die()
        {
            gameObject.SetActive(false);
            _level.SetRestartTrigger();
        }
    }
}
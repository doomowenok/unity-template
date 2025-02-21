using Infrastructure.Pool;
using Infrastructure.Time;
using UnityEngine;
using VContainer;

namespace Gameplay.Core
{
    public abstract class BaseCharacter : MonoBehaviour, IPoolable
    {
        [SerializeField] private Rigidbody _rigidbody;

        private ITimeService _time;
        private float _moveSpeed;

        GameObject IPoolable.PoolObject => gameObject;

        [Inject]
        private void Construct(ITimeService time)
        {
            _time = time;
        }

        public virtual void Initialize(in CharacterData data)
        {
            _moveSpeed = data.MoveSpeed;
        }

        public virtual void Move(Vector3 direction)
        {
            _rigidbody.linearVelocity = direction * _moveSpeed * _time.DeltaTime;
        }

        public virtual void Release()
        {
            gameObject.SetActive(false);
        }
    }
}
using Gameplay.Services.Physics;
using Infrastructure.Pool;
using Infrastructure.Time;
using UnityEngine;
using VContainer;

namespace Gameplay.Core
{
    public abstract class BaseCharacter : MonoBehaviour, IPoolable
    {
        [SerializeField] private Rigidbody _rigidbody;

        protected IObjectCollisionController ObjectCollisionController { get; private set; }
        protected ITimeService Time { get; private set; }
        
        private float _moveSpeed;

        GameObject IPoolable.PoolObject => gameObject;


        [Inject]
        private void Construct(ITimeService time, IObjectCollisionController objectCollisionController)
        {
            Time = time;
            ObjectCollisionController = objectCollisionController;
        }

        public virtual void Initialize(in CharacterData data)
        {
            _moveSpeed = data.MoveSpeed;
        }

        public virtual void Move(Vector3 direction)
        {
            Vector3 velocity = direction * _moveSpeed * Time.DeltaTime;
            Debug.Log($"[CHARACTER]::Move {direction} and velocity {velocity}.");
            _rigidbody.linearVelocity = velocity;
        }

        public virtual void Release()
        {
            gameObject.SetActive(false);
        }
    }
}
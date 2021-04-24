using UnityEngine;

public class PoliceDog : Actor
{
   [SerializeField] private Transform _playerTransform;
   [SerializeField] private float _speed;
   [SerializeField] private float _followDistance;
   [SerializeField] private float _attackDistance;
   [SerializeField] private float _AttackColdown;
   [SerializeField] private ScoreCounter _counter;
   
   //private Timer _timer;
   private bool _isHaveAttackPlace;
   private Transform _attackPlace;
   private Player _player;
   private Transform _transform;
   protected override void StartGame()
   {
      //_timer = gameObject.AddComponent<Timer>();
      _playerTransform = GameObject.FindWithTag("Player").transform;
      _player = _playerTransform.GetComponent<Player>();
      _transform = transform;
   }

   private void Update()
   {
      if (_takeDamage) return;
      var distance = _isHaveAttackPlace?Vector2.Distance(_attackPlace.position,_transform.position):
         Vector2.Distance(_playerTransform.position,_transform.position);
      
      if (distance <= _followDistance)
      {
         RotateToPlayer();
         if (_isHaveAttackPlace == false)
         {
            _attackPlace = _player.GetAttackPlace();
            if (_attackPlace)
            {
               _isHaveAttackPlace = true;
               //print("get Place"+_attackPlace.position);
            }
            else
            {
               _attackPlace = _playerTransform;
            }
         }
         
         if (distance <= _attackDistance)
         {
            if (_isHaveAttackPlace)
            {
               if (Timer())return;
               AnimaBeh.PlayAnim(AnimationsType.hit);
               time = _AttackColdown;
            }
            return;
         }
         AnimaBeh.PlayAnim(AnimationsType.walk);
         _rigidBody.MovePosition(Vector2.MoveTowards(_rigidBody.position,_attackPlace.position, _speed));
      }
   }
   
   private void RotateToPlayer()
   {
      _transform.localScale = new Vector3(Mathf.Sign(_playerTransform.position.x-_transform.position.x), 1, 1);
   }

   protected override void Death()
   {
      _counter.AddCount(5,_transform.position);
      _player.FreeAttackPlace(transform);
      Toolbox.Get<EndLevelChecker>().UpdateKillCount();
      base.Death();
   }

   private void OnDrawGizmosSelected()
   {
      Gizmos.color=Color.black;
      Gizmos.DrawWireSphere(transform.position,_followDistance);
      Gizmos.DrawWireSphere(transform.position,_attackDistance);
      Gizmos.DrawWireCube(_attackPos.position,Data.attackSize);
   }
}

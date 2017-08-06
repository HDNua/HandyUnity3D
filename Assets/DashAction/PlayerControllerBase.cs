using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 플레이어 대쉬를 구현합니다.
/// </summary>
namespace Assets.DashAction
{
    /// <summary>
    /// 플레이어 컨트롤러 인터페이스입니다.
    /// </summary>
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
    public class PlayerControllerBase : MonoBehaviour
    {
        #region Unity 컴포넌트에 대한 참조를 보관합니다.
        /// <summary>
        /// Rigidbody 컴포넌트입니다.
        /// </summary>
        Rigidbody _rigidbody;
        /// <summary>
        /// Rigidbody 컴포넌트를 가져옵니다.
        /// </summary>
        protected Rigidbody _Rigidbody
        {
            get
            {
                if (_rigidbody == null)
                    _rigidbody = GetComponent<Rigidbody>();
                return _rigidbody;
            }
        }
        /// <summary>
        /// Rigidbody의 속도입니다.
        /// </summary>
        protected Vector3 _Velocity
        {
            get { return _Rigidbody.velocity; }
            set { _Rigidbody.velocity = value; }
        }

        #endregion





        #region Unity에서 접근 가능한 공용 필드를 정의합니다.

        #endregion





        #region 필드를 정의합니다.
        /// <summary>
        /// 플레이어가 오른쪽을 보고 있다면 참입니다.
        /// </summary>
        bool _facingRight = true;

        /// <summary>
        /// 
        /// </summary>
        float _lastKeyDownTime = 0;
        /// <summary>
        /// 
        /// </summary>
        float _lastKeyPressTime = 0;
        /// <summary>
        /// 
        /// </summary>
        Stack<InputKeyInfo> _keyDownInfoStack = new Stack<InputKeyInfo>();

        //
        [Obsolete()]
        protected Stack<InputKeyInfo> _KeyDownInfoStack { get { return _keyDownInfoStack; } }

        #endregion





        #region 프로퍼티를 정의합니다.
        /// <summary>
        /// 플레이어가 오른쪽을 보고 있다면 참입니다.
        /// </summary>
        public bool FacingRight
        {
            get { return _facingRight; }
            protected set { _facingRight = value; }
        }

        /// <summary>
        /// 가장 최근에 눌린 키를 반환합니다.
        /// </summary>
        protected InputKeyInfo PrevDownKey
        {
            get
            {
                if (_keyDownInfoStack.Count == 0)
                    return InputKeyInfo.Null;
                return _keyDownInfoStack.Peek();
            }
        }

        /// <summary>
        /// KeyDown 이벤트가 발생할 때까지 걸린 시간입니다.
        /// </summary>
        protected float LastKeyDownTime { get { return _lastKeyDownTime; } }
        /// <summary>
        /// KeyPress 이벤트가 발생할 때까지 걸린 시간입니다.
        /// </summary>
        protected float LastKeyPressTime { get { return _lastKeyPressTime; } }

        #endregion





        #region MonoBehaviour 기본 메서드를 재정의합니다.
        /// <summary>
        /// MonoBehaviour 개체를 초기화합니다.
        /// </summary>
        protected virtual void Start()
        {

        }
        /// <summary>
        /// 프레임이 갱신될 때 MonoBehaviour 개체 정보를 업데이트 합니다.
        /// </summary>
        protected virtual void Update()
        {
            // 
            _lastKeyDownTime += Time.deltaTime;

            // 
            if (Input.anyKeyDown)
            {
                // 
                PushKey(Input.inputString, _lastKeyDownTime);
                _lastKeyDownTime = 0;
            }
        }
        /// <summary>
        /// FixedTimestep에 설정된 값에 따라 일정한 간격으로 업데이트 합니다.
        /// 물리 효과가 적용된 오브젝트를 조정할 때 사용됩니다.
        /// (Update는 불규칙한 호출이기 때문에 물리엔진 충돌검사가 제대로 되지 않을 수 있습니다.)
        /// </summary>
        protected virtual void FixedUpdate()
        {
            _lastKeyPressTime += Time.fixedDeltaTime;
        }
        /// <summary>
        /// 모든 Update 함수가 호출된 후 마지막으로 호출됩니다.
        /// 주로 오브젝트를 따라가게 설정한 카메라는 LastUpdate를 사용합니다.
        /// </summary>
        protected virtual void LateUpdate()
        {

        }

        #endregion





        #region 행동 메서드를 정의합니다.
        /// <summary>
        /// 플레이어의 방향을 전환합니다.
        /// </summary>
        protected void Flip()
        {
            var scale = transform.localScale;
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
        }

        /// <summary>
        /// 플레이어를 왼쪽으로 이동합니다.
        /// </summary>
        /// <param name="movingSpeed">이동 속력입니다.</param>
        protected void MoveLeft(float movingSpeed)
        {
            if (FacingRight)
                Flip();
            _Velocity = new Vector2(-movingSpeed, _Velocity.y);
        }
        /// <summary>
        /// 플레이어를 오른쪽으로 이동합니다.
        /// </summary>
        /// <param name="movingSpeed">이동 속력입니다.</param>
        protected void MoveRight(float movingSpeed)
        {
            if (FacingRight == false)
                Flip();
            _Velocity = new Vector2(movingSpeed, _Velocity.y);
        }
        /// <summary>
        /// 플레이어의 이동을 멈춥니다.
        /// </summary>
        protected void StopMoving()
        {
            _Velocity = Vector2.zero;
        }

        #endregion





        #region 보조 메서드를 정의합니다.
        /// <summary>
        /// 키 입력 스택에 키를 넣습니다.
        /// </summary>
        /// <param name="keyName">입력된 키의 이름입니다.</param>
        /// <param name="interval">키가 입력되기까지 걸린 시간입니다.</param>
        void PushKey(string keyName, float interval)
        {
            _keyDownInfoStack.Push(new InputKeyInfo(keyName, interval));
        }

        /// <summary>
        /// 왼쪽 화살표 키가 눌렸다면 참입니다.
        /// </summary>
        protected static bool IsLeftKeyDown()
        {
            return Input.GetKeyDown(KeyCode.LeftArrow);
        }
        /// <summary>
        /// 왼쪽 화살표 키가 눌린 상태라면 참입니다.
        /// </summary>
        protected static bool IsRightKeyDown()
        {
            return Input.GetKeyDown(KeyCode.RightArrow);
        }
        /// <summary>
        /// 오른쪽 화살표 키가 눌렸다면 참입니다.
        /// </summary>
        protected static bool IsLeftKeyPressed()
        {
            return Input.GetKey(KeyCode.LeftArrow);
        }
        /// <summary>
        /// 오른쪽 화살표 키가 눌린 상태라면 참입니다.
        /// </summary>
        protected static bool IsRightKeyPressed()
        {
            return Input.GetKey(KeyCode.RightArrow);
        }

        #endregion
    }
}

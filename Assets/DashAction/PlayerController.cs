using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



/// <summary>
/// 플레이어 대쉬를 구현합니다.
/// </summary>
namespace Assets.DashAction
{
    /// <summary>
    /// 플레이어 컨트롤러의 구현입니다.
    /// </summary>
    public class PlayerController : PlayerControllerBase
    {
        #region Unity에서 접근 가능한 공용 필드를 정의합니다.
        /// <summary>
        /// 
        /// </summary>
        public float _walkingSpeed = 5;
        /// <summary>
        /// 
        /// </summary>
        public float _runningSpeed = 10;

        /// <summary>
        /// 
        /// </summary>
        public float _dashInterval = 1f;

        #endregion





        #region 필드를 정의합니다.
        /// <summary>
        /// 
        /// </summary>
        public bool _isWalking = false;
        /// <summary>
        /// 
        /// </summary>
        public bool _isRunning = false;

        #endregion




        #region 테스트용 필드를 정의합니다.
        /// <summary>
        /// 현재 X축 이동 속도입니다.
        /// </summary>
        public float _movingSpeed = 0;
        /// <summary>
        /// 현재 X축 이동 속도입니다.
        /// </summary>
        public float _MovingSpeed { set { _movingSpeed = value; } }


        /// <summary>
        /// 키 입력 스택에 삽입된 키의 수입니다.
        /// </summary>
        public int _keyCount;
		/// <summary>
        /// 키 입력 스택에 삽입된 키의 수입니다.
        /// </summary>
		public int _KeyCount { set { _keyCount = value; } }

        /// <summary>
        /// 가장 최근에 입력된 키의 이름입니다.
        /// </summary>
        public string _keyName;
		/// <summary>
        /// 가장 최근에 입력된 키의 이름입니다.
        /// </summary>
		public string _KeyName { set { _keyName = value; } }

        #endregion





        #region MonoBehaviour 기본 메서드를 재정의합니다.
        /// <summary>
        /// FixedTimestep에 설정된 값에 따라 일정한 간격으로 업데이트 합니다.
        /// 물리 효과가 적용된 오브젝트를 조정할 때 사용됩니다.
        /// (Update는 불규칙한 호출이기 때문에 물리엔진 충돌검사가 제대로 되지 않을 수 있습니다.)
        /// </summary>
        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            // 
            _MovingSpeed = _Velocity.x;

            // 
            if (IsLeftKeyPressed())
            {
                if (_isRunning)
                {

                }
                else if (_isWalking)
                {
                    // 
                    if (PrevDownKey.interval < _dashInterval)
                    {
                        RunLeft();
                    }
                    else
                    {
                        StopMoving();
                        StopWalking();
                        StopRunning();

                        WalkLeft();
                    }
                }
                else
                {
                    WalkLeft();
                }
            }
            else if (IsRightKeyPressed())
            {
                if (_isRunning)
                {
                    
                }
                else if (_isWalking)
                {
                    // 
                    if (PrevDownKey.interval < _dashInterval)
                    {
                        RunRight();
                    }
                    else
                    {
                        StopMoving();
                        StopWalking();
                        StopRunning();

                        WalkRight();
                    }
                }
                else
                {
                    WalkRight();
                }
            }
            else
            {
                StopMoving();
                StopWalking();
                StopRunning();
            }
        }

        #endregion





        #region 행동 메서드를 정의합니다.
        /// <summary>
        /// 플레이어를 걷기 상태로 만듭니다.
        /// </summary>
        void Walk()
        {
            _isWalking = true;
        }
        /// <summary>
        /// 플레이어를 왼쪽으로 걷게 합니다.
        /// </summary>
        void WalkLeft()
        {
            Walk();
            MoveLeft(_walkingSpeed);
        }
        /// <summary>
        /// 플레이어를 오른쪽으로 걷게 합니다.
        /// </summary>
        void WalkRight()
        {
            Walk();
            MoveRight(_walkingSpeed);
        }
        /// <summary>
        /// 플레이어의 걷기를 멈춥니다.
        /// </summary>
        void StopWalking()
        {
            _isWalking = false;
        }
        /// <summary>
        /// 플레이어를 달리기 상태로 만듭니다.
        /// </summary>
        void Run()
        {
            _isRunning = true;
        }
        /// <summary>
        /// 플레이어를 왼쪽으로 달리게 합니다.
        /// </summary>
        void RunLeft()
        {
            Run();
            MoveLeft(_runningSpeed);
        }
        /// <summary>
        /// 플레이어를 오른쪽으로 달리게 합니다.
        /// </summary>
        void RunRight()
        {
            Run();
            MoveRight(_runningSpeed);
        }
        /// <summary>
        /// 플레이어의 달리기를 멈춥니다.
        /// </summary>
        void StopRunning()
        {
            _isRunning = false;
        }

        #endregion





        #region 구형 정의를 보관합니다.
        [Obsolete("FixedUpdate() 메서드를 참조하십시오.")]
        /// <summary>
        /// FixedTimestep에 설정된 값에 따라 일정한 간격으로 업데이트 합니다.
        /// 물리 효과가 적용된 오브젝트를 조정할 때 사용됩니다.
        /// (Update는 불규칙한 호출이기 때문에 물리엔진 충돌검사가 제대로 되지 않을 수 있습니다.)
        /// </summary>
        protected /* override */ void FixedUpdate_dep()
        {
            base.FixedUpdate();

            // 
            _MovingSpeed = _Velocity.x;
            _KeyCount = _KeyDownInfoStack.Count;
            _KeyName = PrevDownKey.keyName;

            // 
            if (IsLeftKeyPressed())
            {
                if (_isRunning)
                {

                }
                else if (_isWalking)
                {
                    // 
                    if (PrevDownKey.keyName == "LeftArrow" && (PrevDownKey.interval < _dashInterval))
                    {
                        RunLeft();
                    }
                    else
                    {
                        StopMoving();
                        StopWalking();
                        StopRunning();

                        WalkLeft();
                    }
                }
                else
                {
                    WalkLeft();
                }
            }
            else if (IsRightKeyPressed())
            {
                // 달리는 상태라면
                if (_isRunning)
                {

                }
                // 걷고 있는 상태라면
                else if (_isWalking)
                {
                    // 이전 키 입력과 
                    if (PrevDownKey.keyName == "RightArrow" && (PrevDownKey.interval < _dashInterval))
                    {
                        RunRight();
                    }
                    else
                    {
                        StopMoving();
                        StopWalking();
                        StopRunning();

                        WalkRight();
                    }
                }
                else
                {
                    WalkRight();
                }
            }
            else
            {
                StopMoving();
                StopWalking();
                StopRunning();
            }
        }

        #endregion
    }
}

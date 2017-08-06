using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    #region Unity 컴포넌트에 대한 참조를 보관합니다.
    /// <summary>
    /// 
    /// </summary>
    Rigidbody _rigidbody;
    /// <summary>
    /// 
    /// </summary>
    Rigidbody _Rigidbody
    {
        get
        {
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody>();
            return _rigidbody;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    Vector3 _Velocity
    {
        get { return _Rigidbody.velocity; }
        set { _Rigidbody.velocity = value; }
    }

    #endregion





    #region Unity에서 접근 가능한 공용 필드를 정의합니다.
    /// <summary>
    /// 
    /// </summary>
    public float _movingSpeed = 0;
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
    public float _dashInterval = 0.5f;

    #endregion





    #region 필드를 정의합니다.
    /// <summary>
    /// 
    /// </summary>
    bool _isWalking;
    /// <summary>
    /// 
    /// </summary>
    bool _isRunning;

    /// <summary>
    /// 
    /// </summary>
    bool _facingRight = true;
    /// <summary>
    /// 
    /// </summary>
    public bool FacingRight
    {
        get { return _facingRight; }
        private set { _facingRight = value; }
    }

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
    Stack<KeyPressInfo> _keyPressInfoStack = new Stack<KeyPressInfo>();

    #endregion





    #region MonoBehaviour 기본 메서드를 재정의합니다.
    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        
    }
    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        // 
        _lastKeyDownTime += Time.deltaTime;

        // 
        if (Input.anyKeyDown)
        {
            //
            KeyCode keyCode;
            float interval = _lastKeyDownTime;

            // 
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //
                keyCode = KeyCode.LeftArrow;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                // 
                keyCode = KeyCode.RightArrow;
            }
            else
            {
                // 
                throw new Exception();
            }

            // 
            _keyPressInfoStack.Push(new KeyPressInfo(keyCode, interval));
            _lastKeyDownTime = 0;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    void FixedUpdate()
    {
        _lastKeyPressTime += Time.fixedDeltaTime;

        // 
        if (IsLeftKeyPressed())
        {
            if (_isRunning)
            {

            }
            else if (_isWalking)
            {
                KeyPressInfo kpInfo = _keyPressInfoStack.Peek();

                // 
                if (kpInfo.interval < _dashInterval)
                {
                    RunLeft();
                }
                else
                {
                    StopMoving();
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
                KeyPressInfo kpInfo = _keyPressInfoStack.Peek();

                // 
                if (kpInfo.interval < _dashInterval)
                {
                    RunRight();
                }
                else
                {
                    StopMoving();
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
        }
    }

    #endregion





    #region 행동 메서드를 정의합니다.
    /// <summary>
    /// 
    /// </summary>
    void Flip()
    {
        var scale = transform.localScale;
        transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="movingSpeed"></param>
    void MoveLeft(float movingSpeed)
    {
        if (FacingRight)
            Flip();
        _Velocity = new Vector2(-movingSpeed, _Velocity.y);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="movingSpeed"></param>
    void MoveRight(float movingSpeed)
    {
        if (FacingRight == false)
            Flip();
        _Velocity = new Vector2(movingSpeed, _Velocity.y);
    }
    /// <summary>
    /// 
    /// </summary>
    void StopMoving()
    {
        _Velocity = Vector2.zero;
        StopWalking();
        StopRunning();
    }
    /// <summary>
    /// 
    /// </summary>
    void Walk()
    {
        _isWalking = true;
    }
    /// <summary>
    /// 
    /// </summary>
    void WalkLeft()
    {
        Walk();
        MoveLeft(_walkingSpeed);
    }
    /// <summary>
    /// 
    /// </summary>
    void WalkRight()
    {
        Walk();
        MoveRight(_walkingSpeed);
    }
    /// <summary>
    /// 
    /// </summary>
    void StopWalking()
    {
        _isWalking = false;
    }
    /// <summary>
    /// 
    /// </summary>
    void Run()
    {
        _isRunning = true;
    }
    /// <summary>
    /// 
    /// </summary>
    void RunLeft()
    {
        Run();
        MoveLeft(_runningSpeed);
    }
    /// <summary>
    /// 
    /// </summary>
    void RunRight()
    {
        Run();
        MoveRight(_runningSpeed);
    }
    /// <summary>
    /// 
    /// </summary>
    void StopRunning()
    {
        _isRunning = false;
    }

    #endregion





    #region 보조 메서드를 정의합니다.
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    static bool IsLeftKeyDown()
    {
        return Input.GetKeyDown(KeyCode.LeftArrow);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    static bool IsRightKeyDown()
    {
        return Input.GetKeyDown(KeyCode.RightArrow);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    static bool IsLeftKeyPressed()
    {
        return Input.GetKey(KeyCode.LeftArrow);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    static bool IsRightKeyPressed()
    {
        return Input.GetKey(KeyCode.RightArrow);
    }

    #endregion
}

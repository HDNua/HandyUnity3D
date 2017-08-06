using UnityEngine;



/// <summary>
/// 플레이어 대쉬를 구현합니다.
/// </summary>
namespace Assets.DashAction
{
    /// <summary>
    /// 입력 키 정보입니다.
    /// </summary>
    public struct InputKeyInfo
    {
        /// <summary>
        /// 입력된 키의 이름입니다.
        /// </summary>
        public string keyName;
        /// <summary>
        /// 키가 입력되기까지 걸린 시간입니다.
        /// </summary>
        public float interval;



        /// <summary>
        /// 입력 키 정보입니다.
        /// </summary>
        /// <param name="keyCode">입력된 키의 이름입니다.</param>
        /// <param name="interval">키가 입력되기까지 걸린 시간입니다.</param>
        public InputKeyInfo(string keyName, float interval)
        {
            this.keyName = keyName;
            this.interval = interval;
        }



        /// <summary>
        /// InputKeyInfo가 정보를 갖지 않는 경우를 위한 개체입니다.
        /// </summary>
        static InputKeyInfo _Null;
        /// <summary>
        /// InputKeyInfo Null 개체입니다.
        /// </summary>
        public static InputKeyInfo Null { get { return _Null; } }
        /// <summary>
        /// InputKeyInfo에 대한 static 생성자입니다.
        /// </summary>
        static InputKeyInfo()
        {
            _Null = new InputKeyInfo(null, 10000);
        }
    }
}

using UnityEngine;



/// <summary>
/// 
/// </summary>
struct KeyPressInfo
{
    /// <summary>
    /// 
    /// </summary>
    public KeyCode keyCode;
    /// <summary>
    /// 
    /// </summary>
    public float interval;


    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="keyCode"></param>
    /// <param name="interval"></param>
    public KeyPressInfo(KeyCode keyCode, float interval)
    {
        this.keyCode = keyCode;
        this.interval = interval;
    }
}
using UnityEngine;

[CreateAssetMenu]
public class IntegerSO : ScriptableObject
{
    // this is a scriptable object (SO) that will store integers e.g. player's health & kill count
    // reference: https://www.youtube.com/watch?v=MBM_4zrQHao 
    [SerializeField]
    private int _value;
    public int Value
    {
        get { return _value; }
        set { _value = value; }
    }
   
}

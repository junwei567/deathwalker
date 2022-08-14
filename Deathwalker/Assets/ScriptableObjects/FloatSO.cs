using UnityEngine;

[CreateAssetMenu]
public class FloatSO : ScriptableObject
{
    // this is a scriptable object (SO) that will store integers e.g. player's health & kill count
    // reference: https://www.youtube.com/watch?v=MBM_4zrQHao 
    [SerializeField]
    private float _value;
    public float Value
    {
        get { return _value; }
        set { _value = value; }
    }
   
}

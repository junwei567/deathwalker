using UnityEngine;

[CreateAssetMenu]
public class StringSO : ScriptableObject
{
    // this is a scriptable object (SO) that will store integers e.g. player's health & kill count
    // reference: https://www.youtube.com/watch?v=MBM_4zrQHao 
    [SerializeField]
    private string _value;
    public string Value
    {
        get { return _value; }
        set { _value = value; }
    }
   
}

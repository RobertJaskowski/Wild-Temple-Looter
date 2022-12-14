using System;

[Serializable]
public class IntReference
{
    public bool UseConstant = true;
    public int ConstantValue;
    public IntVariable Variable;

    public IntReference()
    { }

    public IntReference(int value)
    {
        UseConstant = true;
        ConstantValue = value;
    }

    public int Value
    {
        get { return UseConstant ? ConstantValue : Variable.RuntimeValue; }
        set
        {
            if (UseConstant)
                ConstantValue = value;
            else
                Variable.RuntimeValue = value;
        }
    }

    public static implicit operator float(IntReference reference)
    {
        return reference.Value;
    }
}

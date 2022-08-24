using System;

[Serializable]
public class DropTableReference
{
    public bool UseConstant = true;
    public DropTable ConstantValue;
    public DropTableVariable Variable;

    public DropTableReference()
    { }

    public DropTableReference(DropTable value)
    {
        UseConstant = true;
        ConstantValue = value;
    }

    public DropTable Value
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

    public static implicit operator DropTable(DropTableReference reference)
    {
        return reference.Value;
    }
}

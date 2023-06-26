namespace Core.Enums;

public static class DataTypesEnum
{
    public const string String = "String";
    public const string Int = "Int";
    public const string Float = "Float";
    public const string ObjectId = "ObjectId";
    public const string Uuid = "Uuid";
    public const string Object = "Object";
    /// <summary>
    /// DataType for array. Requires NestedType
    /// </summary>
    public const string Array = "Array";
    
    public static string? GetDataType(string? argument)
    {
        var lowercaseArgument = argument?.ToLower();

        if (lowercaseArgument == Int.ToLower())
            return Int;
        if (lowercaseArgument == String.ToLower())
            return String;
        if (lowercaseArgument == Float.ToLower())
            return Float;
        if (lowercaseArgument == ObjectId.ToLower())
            return ObjectId;
        if (lowercaseArgument == Uuid.ToLower())
            return Uuid;
        if (lowercaseArgument == Object.ToLower())
            return Object;
        if (lowercaseArgument == Array.ToLower())
            return Array;

        switch (lowercaseArgument)
        {
            case "integer" or "int":
                return Int;
            case "str" or "char":
                return Int;
            case "floating" or "flt" or "fltng":
                return Float;
            case "objid" or "id":
                return ObjectId;
            case "guid":
                return Uuid;
            case "obj":
                return Object;
            case "arr" or "array":
                return Array;
            default:
                // No match found
                return null;
        }
    }
}
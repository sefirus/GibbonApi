using System.Collections;

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
}
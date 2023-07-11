using Core.Entities;
using Core.ViewModels.Schema;

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

    public static readonly List<DataType> DataTypes = new()
    {
        new DataType { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = String,  },
        new DataType { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = Int,  },
        new DataType { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = Float,  },
        new DataType { Id = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = ObjectId,  },
        new DataType { Id = Guid.Parse("00000000-0000-0000-0000-000000000005"), Name = Uuid,  },
        new DataType { Id = Guid.Parse("00000000-0000-0000-0000-000000000006"), Name = Object,  },
        new DataType { Id = Guid.Parse("00000000-0000-0000-0000-000000000007"), Name = Array, }
    };

    public static DataType GetDataTypeObject(SchemaFieldViewModel fieldViewModel)
    {
        var parentType = GetDataType(fieldViewModel.Type);
        return DataTypes.Single(dt => dt.Name == parentType);
    }

    public static DataType GetDataTypeObject(string? argument)
    {
        var parentType = GetDataType(argument)!;
        return DataTypes.Single(dt => dt.Name == parentType);
    }
    
    public static DataType GetDataTypeObjectById(Guid id)
    {
        return DataTypes.Single(dt => dt.Id == id);
    }
}

public static class DataTypeIdsEnum
{
    public static readonly Guid StringId = Guid.Parse("00000000-0000-0000-0000-000000000001");
    public static readonly Guid IntId = Guid.Parse("00000000-0000-0000-0000-000000000002");
    public static readonly Guid FloatId = Guid.Parse("00000000-0000-0000-0000-000000000003");
    public static readonly Guid ObjectId_Id = Guid.Parse("00000000-0000-0000-0000-000000000004");
    public static readonly Guid UuidId = Guid.Parse("00000000-0000-0000-0000-000000000005");
    public static readonly Guid ObjectId = Guid.Parse("00000000-0000-0000-0000-000000000006");
    public static readonly Guid ArrayId = Guid.Parse("00000000-0000-0000-0000-000000000007");
}
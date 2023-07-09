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
        new DataType { Id = Guid.Parse("00000000-0000-0000-0000-000000000007"), Name = Array, NestedTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"), NestedTypeName = String},
        new DataType { Id = Guid.Parse("00000000-0000-0000-0000-000000000008"), Name = Array, NestedTypeId = Guid.Parse("00000000-0000-0000-0000-000000000002"), NestedTypeName = Int},
        new DataType { Id = Guid.Parse("00000000-0000-0000-0000-000000000009"), Name = Array, NestedTypeId = Guid.Parse("00000000-0000-0000-0000-000000000003"), NestedTypeName = Float},
        new DataType { Id = Guid.Parse("00000000-0000-0000-0000-000000000010"), Name = Array, NestedTypeId = Guid.Parse("00000000-0000-0000-0000-000000000004"), NestedTypeName = ObjectId},
        new DataType { Id = Guid.Parse("00000000-0000-0000-0000-000000000011"), Name = Array, NestedTypeId = Guid.Parse("00000000-0000-0000-0000-000000000005"), NestedTypeName = Uuid},
        new DataType { Id = Guid.Parse("00000000-0000-0000-0000-000000000012"), Name = Array, NestedTypeId = Guid.Parse("00000000-0000-0000-0000-000000000006"), NestedTypeName = Object},

    };

    public static DataType GetDataTypeObject(SchemaFieldViewModel fieldViewModel)
    {
        var parentType = GetDataType(fieldViewModel.Type)!;
        if (parentType != Array)
        {
            return DataTypes.Single(dt => dt.Name == parentType);
        }

        var nestedType = GetDataType(fieldViewModel.ArrayElement?.Type)!;
        return DataTypes.Single(dt => dt.Name == Array && dt.NestedTypeName == nestedType);
    }

    public static DataType GetDataTypeObject(string? argument)
    {
        var parentType = GetDataType(argument)!;
        if (parentType != Array)
        {
            return DataTypes.Single(dt => dt.Name == parentType);
        }

        throw new InvalidOperationException();
    }
    
    public static DataType GetArrayDataTypeObject(string nestedType)
    {
        var arrayNestedType = GetDataType(nestedType)!;
        if (arrayNestedType != Array)
        {
            return DataTypes.Single(dt => dt.Name == Array && dt.NestedTypeName == arrayNestedType);
        }

        throw new InvalidOperationException();
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
    public static readonly Guid StringArrayId = Guid.Parse("00000000-0000-0000-0000-000000000007");
    public static readonly Guid IntArrayId = Guid.Parse("00000000-0000-0000-0000-000000000008");
    public static readonly Guid FloatArrayId = Guid.Parse("00000000-0000-0000-0000-000000000009");
    public static readonly Guid ObjectIdArrayId = Guid.Parse("00000000-0000-0000-0000-000000000010");
    public static readonly Guid UuidArrayId = Guid.Parse("00000000-0000-0000-0000-000000000011");
    public static readonly Guid ObjectArrayId = Guid.Parse("00000000-0000-0000-0000-000000000012");
}
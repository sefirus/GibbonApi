﻿using Core.Entities;
using Core.Enums;
using Core.ViewModels.Schema;

namespace ApplicationTests.Fixtures;

public class SchemaFieldMappingFixture
{
    public Dictionary<string, SchemaFieldViewModel> PrimitiveTypesSource => new()
    {
        { "IntField", new SchemaFieldViewModel { Type = DataTypesEnum.Int, Min = 1, Max = 10 } },
        { "FloatField", new SchemaFieldViewModel { Type = DataTypesEnum.Float, Min = 1.0f, Max = 5.5f } },
        { "StringField", new SchemaFieldViewModel { Type = DataTypesEnum.String, Length = 50, Pattern = "^[a-z]*$" } },
        { "ObjectIdField", new SchemaFieldViewModel { Type = DataTypesEnum.ObjectId } },
        { "UuidField", new SchemaFieldViewModel { Type = DataTypesEnum.Uuid } },
    };

    public List<SchemaField> PrimitiveTypesExpected { get; } = new()
    {
        new SchemaField { FieldName = "IntField", DataTypeId = DataTypesEnum.GetDataTypeObject("Int").Id, Min = 1, Max = 10 },
        new SchemaField { FieldName = "FloatField", DataTypeId = DataTypesEnum.GetDataTypeObject("Float").Id, Min = 1.0f, Max = 5.5f },
        new SchemaField { FieldName = "StringField", DataTypeId = DataTypesEnum.GetDataTypeObject("String").Id, Length = 50, Pattern = "^[a-z]*$" },
        new SchemaField { FieldName = "ObjectIdField", DataTypeId = DataTypesEnum.GetDataTypeObject("ObjectId").Id },
        new SchemaField { FieldName = "UuidField", DataTypeId = DataTypesEnum.GetDataTypeObject("Uuid").Id },
    };

    public Dictionary<string, SchemaFieldViewModel> MixedTypesWithNestedTypesSource => new()
    {
        { "IntField", new SchemaFieldViewModel { Type = DataTypesEnum.Int, Min = -50, Max = 50 } },
        { "FloatField", new SchemaFieldViewModel { Type = DataTypesEnum.Float, Min = -50.0f, Max = 50.0f } },
        { "StringField", new SchemaFieldViewModel { Type = DataTypesEnum.String, Length = 20 } },
        { "ObjectIdField", new SchemaFieldViewModel { Type = DataTypesEnum.ObjectId } },
        { "UuidField", new SchemaFieldViewModel { Type = DataTypesEnum.Uuid } },
        { "StringArrayField", new SchemaFieldViewModel { Type = DataTypesEnum.Array, ArrayElement = new SchemaFieldViewModel { Type = DataTypesEnum.String, Length = 15 } } },
        { "IntArrayField", new SchemaFieldViewModel { Type = DataTypesEnum.Array, ArrayElement = new SchemaFieldViewModel { Type = DataTypesEnum.Int, Min = 0, Max = 100 } } },
    };

    public List<SchemaField> MixedTypesWithNestedTypesExpected => new()
    {
        new () { FieldName = "IntField", DataTypeId = DataTypesEnum.GetDataTypeObject("Int").Id, Min = -50, Max = 50 },
        new () { FieldName = "FloatField", DataTypeId = DataTypesEnum.GetDataTypeObject("Float").Id, Min = -50.0f, Max = 50.0f },
        new () { FieldName = "StringField", DataTypeId = DataTypesEnum.GetDataTypeObject("String").Id, Length = 20 },
        new () { FieldName = "ObjectIdField", DataTypeId = DataTypesEnum.GetDataTypeObject("ObjectId").Id },
        new () { FieldName = "UuidField", DataTypeId = DataTypesEnum.GetDataTypeObject("Uuid").Id },
        new ()
        {
            FieldName = "StringArrayField", 
            DataTypeId = DataTypeIdsEnum.ArrayId, 
            IsArray = true, 
            ChildFields = new()
            {
                new (){ FieldName = "StringArrayField", DataTypeId = DataTypesEnum.GetDataTypeObject("String").Id, Length = 15 }
            },
        },
        new () { 
            FieldName = "IntArrayField", 
            DataTypeId = DataTypeIdsEnum.ArrayId, 
            IsArray = true, 
            ChildFields = new()
            {
                new (){ FieldName = "IntArrayField", DataTypeId = DataTypesEnum.GetDataTypeObject("Int").Id, Min = 0, Max = 100}
            } 
        },
    };

    public Dictionary<string, SchemaFieldViewModel> ArrayOfPrimitivesSource => new()
    {
        { "IntArrayField", new SchemaFieldViewModel { Type = DataTypesEnum.Array, ArrayElement = new SchemaFieldViewModel { Type = DataTypesEnum.Int, Min = -10, Max = 10 } } },
        { "FloatArrayField", new SchemaFieldViewModel { Type = DataTypesEnum.Array, ArrayElement = new SchemaFieldViewModel { Type = DataTypesEnum.Float, Min = -10.0f, Max = 10.0f } } },
        { "StringArrayField", new SchemaFieldViewModel { Type = DataTypesEnum.Array, ArrayElement = new SchemaFieldViewModel { Type = DataTypesEnum.String, Length = 20 } } },
        { "ObjectIdArrayField", new SchemaFieldViewModel { Type = DataTypesEnum.Array, ArrayElement = new SchemaFieldViewModel { Type = DataTypesEnum.ObjectId } } },
        { "UuidArrayField", new SchemaFieldViewModel { Type = DataTypesEnum.Array, ArrayElement = new SchemaFieldViewModel { Type = DataTypesEnum.Uuid } } }
    };

    public List<SchemaField> ArrayOfPrimitivesExpected => new()
    {
        new () { 
            FieldName = "IntArrayField", 
            DataTypeId = DataTypeIdsEnum.ArrayId, 
            IsArray = true, 
            ChildFields = new()
            {
                new SchemaField(){ FieldName = "IntArrayField", DataTypeId = DataTypesEnum.GetDataTypeObject("Int").Id, Min = -10, Max = 10}
            } 
        },
        new () { 
            FieldName = "FloatArrayField", 
            DataTypeId = DataTypeIdsEnum.ArrayId, 
            IsArray = true, 
            ChildFields = new()
            {
                new (){ FieldName = "FloatArrayField", DataTypeId = DataTypesEnum.GetDataTypeObject("Float").Id, Min = -10.0f, Max = 10.0f}
            } 
        },
        new ()
        {
            FieldName = "StringArrayField", 
            DataTypeId = DataTypeIdsEnum.ArrayId, 
            IsArray = true, 
            ChildFields = new()
            {
                new SchemaField(){ FieldName = "StringArrayField", DataTypeId = DataTypesEnum.GetDataTypeObject("String").Id, Length = 20 }
            },
        },
        new ()
        {
            FieldName = "ObjectIdArrayField", 
            DataTypeId = DataTypeIdsEnum.ArrayId, 
            IsArray = true, 
            ChildFields = new()
            {
                new (){ FieldName = "ObjectIdArrayField", DataTypeId = DataTypesEnum.GetDataTypeObject("ObjectId").Id }
            },
        },
        new () { 
            FieldName = "UuidArrayField", 
            DataTypeId = DataTypeIdsEnum.ArrayId, 
            IsArray = true, 
            ChildFields = new()
            {
                new (){ FieldName = "UuidArrayField", DataTypeId = DataTypesEnum.GetDataTypeObject("Uuid").Id }
            } 
        }
    };
    
    public Dictionary<string, SchemaFieldViewModel> ArrayOfMixedTypesSource => new()
    {
        { "IntField", new SchemaFieldViewModel { Type = DataTypesEnum.Array, ArrayElement = new SchemaFieldViewModel { Type = DataTypesEnum.Int, Min = -50, Max = 50 } } },
        { "FloatField", new SchemaFieldViewModel { Type = DataTypesEnum.Array, ArrayElement = new SchemaFieldViewModel { Type = DataTypesEnum.Float, Min = -50, Max = 50 } } },
        { "StringField", new SchemaFieldViewModel { Type = DataTypesEnum.Array, ArrayElement = new SchemaFieldViewModel { Type = DataTypesEnum.String, Length = 30 } } },
        { "ObjectIdField", new SchemaFieldViewModel { Type = DataTypesEnum.Array, ArrayElement = new SchemaFieldViewModel { Type = DataTypesEnum.ObjectId } } },
        { "ObjectField", new SchemaFieldViewModel 
            { 
                Type = DataTypesEnum.Array, 
                ArrayElement = new SchemaFieldViewModel 
                { 
                    Type = DataTypesEnum.Object, 
                    Fields = new()
                    {
                        new SchemaFieldViewModel { FieldName = "NestedIntField", Type = DataTypesEnum.Int, Min = -100, Max = 500 } ,
                        new SchemaFieldViewModel { FieldName = "NestedFloatField", Type = DataTypesEnum.Float, Min = 0, Max = 1 } ,
                        new SchemaFieldViewModel { FieldName = "NestedStringField", Type = DataTypesEnum.String, Length = 15 } ,
                        new SchemaFieldViewModel { FieldName = "NestedObjectIdField", Type = DataTypesEnum.ObjectId } ,
                        new SchemaFieldViewModel { FieldName = "NestedUuidField", Type = DataTypesEnum.Uuid } ,
                    }
                } 
            }
        }
    };

    public List<SchemaField> ArrayOfMixedTypesExpected => new()
    {
        new SchemaField()
        {
            FieldName = "IntField", 
            DataTypeId = DataTypeIdsEnum.ArrayId, 
            IsArray = true, 
            ChildFields = new()
            {
                new SchemaField(){ FieldName = "IntField", DataTypeId = DataTypesEnum.GetDataTypeObject("Int").Id, Min = -50, Max = 50 }
            }
        },
        new SchemaField()
        {
            FieldName = "FloatField", 
            DataTypeId = DataTypeIdsEnum.ArrayId, 
            IsArray = true, 
            ChildFields = new()
            {
                new SchemaField(){ FieldName = "FloatField", DataTypeId = DataTypesEnum.GetDataTypeObject("Float").Id, Min = -50, Max = 50 }
            }
        },
        new SchemaField()
        {
            FieldName = "StringField", 
            DataTypeId = DataTypeIdsEnum.ArrayId, 
            IsArray = true, ChildFields = new()
            {
                new SchemaField(){ FieldName = "StringField", DataTypeId = DataTypesEnum.GetDataTypeObject("String").Id, Length = 30 }
            }
        },
        new SchemaField()
        {
            FieldName = "ObjectIdField", 
            DataTypeId = DataTypeIdsEnum.ArrayId, 
            IsArray = true, 
            ChildFields = new()
            {
                new SchemaField(){ FieldName = "ObjectIdField", DataTypeId = DataTypesEnum.GetDataTypeObject("ObjectId").Id }
            }
        },
        new SchemaField() { 
            FieldName = "ObjectField", 
            DataTypeId = DataTypeIdsEnum.ArrayId, 
            IsArray = true, 
            ChildFields = new()
            { 
                new SchemaField()
                {
                    FieldName = "ObjectField",
                    DataTypeId = DataTypesEnum.GetDataTypeObject("Object").Id,
                    IsArray = false,
                    ChildFields = new()
                    {
                        new SchemaField() { FieldName = "NestedIntField", DataTypeId = DataTypesEnum.GetDataTypeObject("Int").Id, Min = -100, Max = 500 },
                        new SchemaField() { FieldName = "NestedFloatField", DataTypeId = DataTypesEnum.GetDataTypeObject("Float").Id, Min = 0, Max = 1 },
                        new SchemaField() { FieldName = "NestedStringField", DataTypeId = DataTypesEnum.GetDataTypeObject("String").Id, Length = 15 },
                        new SchemaField() { FieldName = "NestedObjectIdField", DataTypeId = DataTypesEnum.GetDataTypeObject("ObjectId").Id },
                        new SchemaField() { FieldName = "NestedUuidField", DataTypeId = DataTypesEnum.GetDataTypeObject("Uuid").Id },
                    }
                } 
            } 
        },
    };
    
    public Dictionary<string, SchemaFieldViewModel> ObjectOfObjectsSource => new()
    {
        { "IntObjectField", new SchemaFieldViewModel 
            { 
                Type = DataTypesEnum.Object, 
                Fields = new List<SchemaFieldViewModel> 
                { 
                    new SchemaFieldViewModel { FieldName = "IntObjectNestedField", Type = DataTypesEnum.Int, Min = -50, Max = 50 } 
                } 
            } 
        },
        { "FloatObjectField", new SchemaFieldViewModel 
            { 
                Type = DataTypesEnum.Object, 
                Fields = new List<SchemaFieldViewModel> 
                { 
                    new SchemaFieldViewModel { FieldName = "FloatObjectNestedField", Type = DataTypesEnum.Float, Min = -50, Max = 50 } 
                } 
            } 
        },
        { "StringObjectField", new SchemaFieldViewModel 
            { 
                Type = DataTypesEnum.Object, 
                Fields = new List<SchemaFieldViewModel> 
                { 
                    new SchemaFieldViewModel { FieldName = "StringObjectNestedField", Type = DataTypesEnum.String, Length = 30 } 
                } 
            } 
        }
    };

    public List<SchemaField> ObjectOfObjectsExpected => new()
    {
        new SchemaField()
        {
            FieldName = "IntObjectField",
            DataTypeId = DataTypesEnum.GetDataTypeObject("Object").Id,
            ChildFields = new()
            {
                new SchemaField()
                {
                    FieldName = "IntObjectNestedField",
                    DataTypeId = DataTypesEnum.GetDataTypeObject("Int").Id,
                    Min = -50,
                    Max = 50
                }
            }
        },
        new SchemaField()
        {
            FieldName = "FloatObjectField",
            DataTypeId = DataTypesEnum.GetDataTypeObject("Object").Id,
            ChildFields = new()
            {
                new SchemaField()
                {
                    FieldName = "FloatObjectNestedField",
                    DataTypeId = DataTypesEnum.GetDataTypeObject("Float").Id,
                    Min = -50,
                    Max = 50
                }
            }
        },
        new SchemaField()
        {
            FieldName = "StringObjectField",
            DataTypeId = DataTypesEnum.GetDataTypeObject("Object").Id,
            ChildFields = new()
            {
                new SchemaField()
                {
                    FieldName = "StringObjectNestedField",
                    DataTypeId = DataTypesEnum.GetDataTypeObject("String").Id,
                    Length = 30
                }
            }
        }
    };
    
    public Dictionary<string, SchemaFieldViewModel> ArrayOfObjectsWithNestedObjectsSource => new()
    {
        {
            "ArrayOfObjectsField", new SchemaFieldViewModel
            {
                Type = DataTypesEnum.Array,
                ArrayElement = new SchemaFieldViewModel
                {
                    Type = DataTypesEnum.Object,
                    Fields = new List<SchemaFieldViewModel>
                    {
                        {
                            new SchemaFieldViewModel
                            {
                                FieldName = "IntField",
                                Type = DataTypesEnum.Int,
                                Min = -50,
                                Max = 50,
                            }
                        },
                        {
                            new SchemaFieldViewModel
                            {
                                FieldName = "FloatField", 
                                Type = DataTypesEnum.Float,
                                Min = -50,
                                Max = 50,
                            }
                        },
                        {
                            new SchemaFieldViewModel
                            {
                                FieldName = "StringField", 
                                Type = DataTypesEnum.String,
                                Length = 30,
                            }
                        },
                        {
                            new SchemaFieldViewModel
                            {
                                FieldName = "NestedObjectField", 
                                Type = DataTypesEnum.Object,
                                Fields = new List<SchemaFieldViewModel>
                                {
                                    {
                                        new SchemaFieldViewModel
                                        {
                                            FieldName = "NestedIntField",
                                            Type = DataTypesEnum.Int,
                                            Min = -100,
                                            Max = 100,
                                        }
                                    },
                                    {
                                        new SchemaFieldViewModel
                                        {
                                            FieldName = "NestedFloatField", 
                                            Type = DataTypesEnum.Float,
                                            Min = -100,
                                            Max = 100,
                                        }
                                    },
                                    {
                                        new SchemaFieldViewModel
                                        {
                                            FieldName = "NestedStringField", 
                                            Type = DataTypesEnum.String,
                                            Length = 50,
                                        }
                                    },
                                }
                            }
                        },
                    }
                }
            }
        },
    };

    public List<SchemaField> ArrayOfObjectsWithNestedObjectsExpected => new()
    {
        new SchemaField
        {
            FieldName = "ArrayOfObjectsField",
            DataTypeId = DataTypeIdsEnum.ArrayId,
            IsArray = true,
            ChildFields = new List<SchemaField>
            {
                new SchemaField
                {
                    FieldName = "ArrayOfObjectsField",
                    DataTypeId = DataTypesEnum.GetDataTypeObject("Object").Id,
                    IsArray = false,
                    ChildFields = new List<SchemaField>
                    {
                        new SchemaField
                        {
                            FieldName = "IntField",
                            DataTypeId = DataTypesEnum.GetDataTypeObject("Int").Id,
                            Min = -50,
                            Max = 50,
                        },
                        new SchemaField
                        {
                            FieldName = "FloatField",
                            DataTypeId = DataTypesEnum.GetDataTypeObject("Float").Id,
                            Min = -50,
                            Max = 50,
                        },
                        new SchemaField
                        {
                            FieldName = "StringField",
                            DataTypeId = DataTypesEnum.GetDataTypeObject("String").Id,
                            Length = 30,
                        },
                        new SchemaField
                        {
                            FieldName = "NestedObjectField",
                            DataTypeId = DataTypesEnum.GetDataTypeObject("Object").Id,
                            ChildFields = new List<SchemaField>
                            {
                                new SchemaField
                                {
                                    FieldName = "NestedIntField",
                                    DataTypeId = DataTypesEnum.GetDataTypeObject("Int").Id,
                                    Min = -100,
                                    Max = 100,
                                },
                                new SchemaField
                                {
                                    FieldName = "NestedFloatField",
                                    DataTypeId = DataTypesEnum.GetDataTypeObject("Float").Id,
                                    Min = -100,
                                    Max = 100,
                                },
                                new SchemaField
                                {
                                    FieldName = "NestedStringField",
                                    DataTypeId = DataTypesEnum.GetDataTypeObject("String").Id,
                                    Length = 50,
                                },
                            }
                        }
                    }
                }
            }
        },
    };
    
    public Dictionary<string, SchemaFieldViewModel> ComplexNestedArraysSource => new()
    {
        {
            "ArrayOfArraysOfObjects", new SchemaFieldViewModel
            {
                Type = DataTypesEnum.Array,
                ArrayElement = new SchemaFieldViewModel
                {
                    Type = DataTypesEnum.Array,
                    ArrayElement = new SchemaFieldViewModel
                    {
                        Type = DataTypesEnum.Object,
                        Fields = new List<SchemaFieldViewModel>
                        {
                            new SchemaFieldViewModel
                            {
                                FieldName = "IntField",
                                Type = DataTypesEnum.Int,
                                Min = -50,
                                Max = 50
                            },
                            new SchemaFieldViewModel
                            {
                                FieldName = "FloatField",
                                Type = DataTypesEnum.Float,
                                Min = -50,
                                Max = 50
                            },
                            new SchemaFieldViewModel
                            {
                                FieldName = "StringField",
                                Type = DataTypesEnum.String,
                                Length = 30
                            }
                        }
                    }
                }
            }
        },
        {
            "ArrayOfArraysOfArraysOfInts", new SchemaFieldViewModel
            {
                Type = DataTypesEnum.Array,
                ArrayElement = new SchemaFieldViewModel
                {
                    Type = DataTypesEnum.Array,
                    ArrayElement = new SchemaFieldViewModel
                    {
                        Type = DataTypesEnum.Array,
                        ArrayElement = new SchemaFieldViewModel
                        {
                            Type = DataTypesEnum.Int,
                            Min = 0,
                            Max = 100
                        }
                    }
                }
            }
        }
    };

    public List<SchemaField> ComplexNestedArraysExpected => new()
    {
        new SchemaField
        {
            FieldName = "ArrayOfArraysOfObjects",
            DataTypeId = DataTypeIdsEnum.ArrayId,
            IsArray = true,
            ChildFields = new List<SchemaField>
            {
                new SchemaField
                {
                    FieldName = "ArrayOfArraysOfObjects",
                    DataTypeId = DataTypeIdsEnum.ArrayId,
                    IsArray = true,
                    ChildFields = new List<SchemaField>
                    {
                        new SchemaField
                        {
                            FieldName = "IntField",
                            DataTypeId = DataTypesEnum.GetDataTypeObject("Int").Id,
                            Min = -50,
                            Max = 50
                        },
                        new SchemaField
                        {
                            FieldName = "FloatField",
                            DataTypeId = DataTypesEnum.GetDataTypeObject("Float").Id,
                            Min = -50,
                            Max = 50
                        },
                        new SchemaField
                        {
                            FieldName = "StringField",
                            DataTypeId = DataTypesEnum.GetDataTypeObject("String").Id,
                            Length = 30
                        }
                    }
                }
            }
        },
        new SchemaField
        {
            FieldName = "ArrayOfArraysOfArraysOfInts",
            DataTypeId = DataTypeIdsEnum.ArrayId,
            IsArray = true,
            ChildFields = new List<SchemaField>
            {
                new SchemaField
                {
                    FieldName = "ArrayOfArraysOfArraysOfInts",
                    DataTypeId = DataTypeIdsEnum.ArrayId,
                    IsArray = true,
                    ChildFields = new List<SchemaField>
                    {
                        new SchemaField
                        {
                            FieldName = "ArrayOfArraysOfArraysOfInts",
                            DataTypeId = DataTypeIdsEnum.ArrayId,
                            IsArray = true,
                            ChildFields = new List<SchemaField>
                            {
                                new SchemaField
                                {
                                    FieldName = "ArrayOfArraysOfArraysOfInts",
                                    DataTypeId = DataTypesEnum.GetDataTypeObject("Int").Id,
                                    Min = 0,
                                    Max = 100
                                }
                            }
                        }
                    }
                }
            }
        }
    };
    
    public Dictionary<string, SchemaFieldViewModel> ObjectWithPatternSource => new()
    {
        {
            "StringPatternField1", new SchemaFieldViewModel
            {
                Type = DataTypesEnum.String,
                Length = 20,
                Pattern = @"^\d{5}$"
            }
        },
        {
            "ArrayOfStringsPatternField", new SchemaFieldViewModel
            {
                Type = DataTypesEnum.Array,
                ArrayElement = new SchemaFieldViewModel
                {
                    Type = DataTypesEnum.String,
                    Length = 20,
                    Pattern = @"^\d{5}$"
                }
            }
        },
        {
            "ObjectOfStringsPatternField", new SchemaFieldViewModel
            {
                Type = DataTypesEnum.Object,
                Fields = new List<SchemaFieldViewModel>
                {
                    new SchemaFieldViewModel
                    {
                        FieldName = "StringField",
                        Type = DataTypesEnum.String,
                        Length = 20,
                        Pattern = @"^\d{5}$"
                    }
                }
            }
        },
        {
            "StringPatternField2", new SchemaFieldViewModel
            {
                Type = DataTypesEnum.String,
                Length = 10,
                Pattern = @"^[a-z]{5}$"
            }
        },
        {
            "ArrayOfStringsPatternField2", new SchemaFieldViewModel
            {
                Type = DataTypesEnum.Array,
                ArrayElement = new SchemaFieldViewModel
                {
                    Type = DataTypesEnum.String,
                    Length = 10,
                    Pattern = @"^[a-z]{5}$"
                }
            }
        },
    };

    public List<SchemaField> ObjectWithPatternExpected => new()
    {
        new SchemaField
        {
            FieldName = "StringPatternField1",
            DataTypeId = DataTypesEnum.GetDataTypeObject("String").Id,
            Length = 20,
            Pattern = @"^\d{5}$"
        },
        new SchemaField
        {
            FieldName = "ArrayOfStringsPatternField",
            DataTypeId = DataTypesEnum.GetDataTypeObject("Array").Id,
            IsArray = true,
            ChildFields = new List<SchemaField>
            {
                new SchemaField
                {
                    FieldName = "ArrayOfStringsPatternField",
                    DataTypeId = DataTypesEnum.GetDataTypeObject("String").Id,
                    Length = 20,
                    Pattern = @"^\d{5}$"
                }
            }
        },
        new SchemaField
        {
            FieldName = "ObjectOfStringsPatternField",
            DataTypeId = DataTypesEnum.GetDataTypeObject("Object").Id,
            ChildFields = new List<SchemaField>
            {
                new SchemaField
                {
                    FieldName = "StringField",
                    DataTypeId = DataTypesEnum.GetDataTypeObject("String").Id,
                    Length = 20,
                    Pattern = @"^\d{5}$"
                }
            }
        },
        new SchemaField
        {
            FieldName = "StringPatternField2",
            DataTypeId = DataTypesEnum.GetDataTypeObject("String").Id,
            Length = 10,
            Pattern = @"^[a-z]{5}$"
        },
        new SchemaField
        {
            FieldName = "ArrayOfStringsPatternField2",
            DataTypeId = DataTypesEnum.GetDataTypeObject("Array").Id,
            IsArray = true,
            ChildFields = new List<SchemaField>
            {
                new SchemaField
                {
                    FieldName = "ArrayOfStringsPatternField2",
                    DataTypeId = DataTypesEnum.GetDataTypeObject("String").Id,
                    Length = 10,
                    Pattern = @"^[a-z]{5}$"
                }
            }
        }
    };
    
    public Dictionary<string, SchemaFieldViewModel> ObjectWithPrimaryKeySource => new()
    {
        {
            "StringPKField", new SchemaFieldViewModel
            {
                Type = DataTypesEnum.String,
                Length = 20,
                IsPrimaryKey = true
            }
        },
        {
            "ArrayOfStringsField", new SchemaFieldViewModel
            {
                Type = DataTypesEnum.Array,
                ArrayElement = new SchemaFieldViewModel
                {
                    Type = DataTypesEnum.String,
                    Length = 20
                }
            }
        },
        {
            "ObjectOfStringsField", new SchemaFieldViewModel
            {
                Type = DataTypesEnum.Object,
                Fields = new List<SchemaFieldViewModel>
                {
                    new SchemaFieldViewModel
                    {
                        FieldName = "StringField",
                        Type = DataTypesEnum.String,
                        Length = 20
                    }
                }
            }
        },
        {
            "IntField", new SchemaFieldViewModel
            {
                Type = DataTypesEnum.Int,
                Min = -50,
                Max = 50
            }
        },
        {
            "ArrayOfIntsField", new SchemaFieldViewModel
            {
                Type = DataTypesEnum.Array,
                ArrayElement = new SchemaFieldViewModel
                {
                    Type = DataTypesEnum.Int,
                    Min = -50,
                    Max = 50
                }
            }
        },
        {
            "ObjectOfIntsField", new SchemaFieldViewModel
            {
                Type = DataTypesEnum.Object,
                Fields = new List<SchemaFieldViewModel>
                {
                    new SchemaFieldViewModel
                    {
                        FieldName = "IntField",
                        Type = DataTypesEnum.Int,
                        Min = -50,
                        Max = 50
                    }
                }
            }
        },
    };

    public List<SchemaField> ObjectWithPrimaryKeyExpected => new()
    {
        new SchemaField
        {
            FieldName = "StringPKField",
            DataTypeId = DataTypeIdsEnum.StringId,
            Length = 20,
            IsPrimaryKey = true
        },
        new SchemaField
        {
            FieldName = "ArrayOfStringsField",
            DataTypeId = DataTypeIdsEnum.ArrayId,
            IsArray = true,
            ChildFields = new List<SchemaField>
            {
                new SchemaField
                {
                    FieldName = "ArrayOfStringsField",
                    DataTypeId = DataTypeIdsEnum.StringId,
                    Length = 20
                }
            }
        },
        new SchemaField
        {
            FieldName = "ObjectOfStringsField",
            DataTypeId = DataTypeIdsEnum.ObjectId,
            ChildFields = new List<SchemaField>
            {
                new SchemaField
                {
                    FieldName = "StringField",
                    DataTypeId = DataTypeIdsEnum.StringId,
                    Length = 20
                }
            }
        },
        new SchemaField
        {
            FieldName = "IntField",
            DataTypeId = DataTypeIdsEnum.IntId,
            Min = -50,
            Max = 50
        },
        new SchemaField
        {
            FieldName = "ArrayOfIntsField",
            DataTypeId = DataTypeIdsEnum.ArrayId,
            IsArray = true,
            ChildFields = new List<SchemaField>
            {
                new SchemaField
                {
                    FieldName = "ArrayOfIntsField",
                    DataTypeId = DataTypeIdsEnum.IntId,
                    Min = -50,
                    Max = 50
                }
            }
        },
        new SchemaField
        {
            FieldName = "ObjectOfIntsField",
            DataTypeId = DataTypeIdsEnum.ObjectId,
            ChildFields = new List<SchemaField>
            {
                new SchemaField
                {
                    FieldName = "IntField",
                    DataTypeId = DataTypeIdsEnum.IntId,
                    Min = -50,
                    Max = 50
                }
            }
        }
    };
    public Dictionary<string, SchemaFieldViewModel> ObjectWithSummarySource => new()
    {
        {
            "StringField", new SchemaFieldViewModel
            {
                Type = DataTypesEnum.String,
                Length = 20,
                Summary = "string field"
            }
        },
        {
            "IntField", new SchemaFieldViewModel
            {
                Type = DataTypesEnum.Int,
                Min = -50,
                Max = 50,
                Summary = "int field"
            }
        },
        {
            "FloatField", new SchemaFieldViewModel
            {
                Type = DataTypesEnum.Float,
                Min = -50.0f,
                Max = 50.0f,
                Summary = "float field"
            }
        },
        {
            "ObjectIdField", new SchemaFieldViewModel
            {
                Type = DataTypesEnum.ObjectId,
                Summary = "ObjectId field"
            }
        },
        {
            "UuidField", new SchemaFieldViewModel
            {
                Type = DataTypesEnum.Uuid,
                Summary = "Uuid field"
            }
        }
    };

    public List<SchemaField> ObjectWithSummaryExpected => new()
    {
        new SchemaField
        {
            FieldName = "StringField",
            DataTypeId = DataTypeIdsEnum.StringId,
            Length = 20,
            Summary = "string field"
        },
        new SchemaField
        {
            FieldName = "IntField",
            DataTypeId = DataTypeIdsEnum.IntId,
            Min = -50,
            Max = 50,
            Summary = "int field"
        },
        new SchemaField
        {
            FieldName = "FloatField",
            DataTypeId = DataTypeIdsEnum.FloatId,
            Min = -50.0f,
            Max = 50.0f,
            Summary = "float field"
        },
        new SchemaField
        {
            FieldName = "ObjectIdField",
            DataTypeId = DataTypeIdsEnum.ObjectId_Id,
            Summary = "ObjectId field"
        },
        new SchemaField
        {
            FieldName = "UuidField",
            DataTypeId = DataTypeIdsEnum.UuidId,
            Summary = "Uuid field"
        }
    };
}

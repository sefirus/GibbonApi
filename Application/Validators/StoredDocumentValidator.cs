﻿using System.Text.RegularExpressions;
using Core.Entities;
using Core.Enums;
using FluentValidation;

namespace Application.Validators;

public class StoredDocumentValidator : AbstractValidator<StoredDocument>
{
    public StoredDocumentValidator()
    {
        RuleFor(x => x.FieldValues)
            .Must(list => list != null && list.Count > 0)
            .WithMessage("FieldValues must not be null or empty");

        RuleForEach(x => x.FieldValues)
            .SetValidator(new FieldValueValidator());
    }
}

public class FieldValueValidator : AbstractValidator<FieldValue>
{
    public FieldValueValidator()
    {
        // RuleFor(x => x.Value)
        //     .NotEmpty()
        //     .When(fieldValue => fieldValue.SchemaField.IsRequired)
        //     .WithMessage("Value must not be empty");

        RuleFor(x => x)
            .Must(fieldValue =>
            {
                if (fieldValue.SchemaField == null)
                    return false;

                var schemaField = fieldValue.SchemaField;
                switch (schemaField.DataTypeId)
                {
                    case var g when g == DataTypeIdsEnum.IntId:
                        return ValidateInt(fieldValue, schemaField);
                    case var g when g == DataTypeIdsEnum.FloatId:
                        return ValidateFloat(fieldValue, schemaField);
                    case var g when g == DataTypeIdsEnum.StringId:
                        return ValidateString(fieldValue, schemaField);
                    case var g when g == DataTypeIdsEnum.UuidId:
                        return ValidateUuid(fieldValue);
                    case var g when g == DataTypeIdsEnum.ObjectId_Id:
                        // Add logic to validate ObjectId
                        return true;
                    default:
                        return false;
                }
            })
            .WithMessage("Field value does not match the field schema");
    }

    private static bool ValidateInt(FieldValue fieldValue, SchemaField schemaField)
    {
        if (!int.TryParse(fieldValue.Value, out var value))
            return false;

        return (!schemaField.Min.HasValue || value >= schemaField.Min) &&
               (!schemaField.Max.HasValue || value <= schemaField.Max);
    }

    private static bool ValidateFloat(FieldValue fieldValue, SchemaField schemaField)
    {
        if (!double.TryParse(fieldValue.Value, out var value))
            return false;

        return (!schemaField.Min.HasValue || value >= schemaField.Min) &&
               (!schemaField.Max.HasValue || value <= schemaField.Max);
    }

    private static bool ValidateString(FieldValue fieldValue, SchemaField schemaField)
    {
        if (schemaField.Length.HasValue && fieldValue.Value.Length > schemaField.Length)
            return false;

        return string.IsNullOrEmpty(schemaField.Pattern) ||
               Regex.IsMatch(fieldValue.Value, schemaField.Pattern);
    }

    private static bool ValidateUuid(FieldValue fieldValue)
    {
        return Guid.TryParse(fieldValue.Value, out _);
    }
}
namespace Movilissa.core.DTOs.Shared;

public record struct Item (int Id, string Description) {
    public static Item From<TEnum>(TEnum enumValue)
        where TEnum : struct, Enum {
        return new Item((int)(object)enumValue, enumValue.ToString().PascalCaseToTitleCase());
    }
};

using Luau.Objects;

namespace Luau
{
    public readonly struct LuauValue(LuauType type, double number, long integer, object? reference)
    {
        public LuauType Type { get; } = type;

        public double Number { get; } = number;
        public long Integer { get; } = integer;

        public object? Reference { get; } = reference;

        public object? ToObject()
        {
            return Type switch
            {
                LuauType.Nil => null,
                LuauType.Boolean => Number != 0,
                LuauType.Integer => Integer,
                LuauType.Number => Number,
                LuauType.String => Reference is string s
                    ? s
                    : throw new InvalidOperationException("Luau string value has no backing string."),
                LuauType.Function => new LuauFunction(
                    Reference as Luau
                        ?? throw new InvalidOperationException("Luau function value has no owning Luau instance."),
                    (int)Number),
                _ => throw new NotSupportedException($"Unsupported Luau type: {Type}")
            };
        }

        public override string ToString()
        {
            return ToObject()?.ToString() ?? "nil";
        }

        public static implicit operator LuauValue(double value) => new(LuauType.Number, value, 0, null);
        public static implicit operator LuauValue(string value) => new(LuauType.String, 0, 0, value);
        public static implicit operator LuauValue(bool value) => new(LuauType.Boolean, value ? 1 : 0, 0, null);

        public static implicit operator LuauValue(long value) => new(LuauType.Integer, 0, value, null);
        public static implicit operator LuauValue(int value) => new(LuauType.Integer, 0, value, null);

        public static LuauValue Nil => new(LuauType.Nil, 0, 0, null);
    }
}
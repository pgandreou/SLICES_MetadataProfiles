using System.Diagnostics;

namespace Slices.V1.Standard;

public struct LanguageIso639_3
{
    private string _code;

    [DebuggerHidden]
    public string Code
    {
        get => _code;
        set
        {
            if (value.Length != 3)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            _code = value;
        }
    }
}

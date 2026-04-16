public static class NumberFormatter
{
    /// <summary>
    /// digits take no effect if rawValue is < 1k
    /// </summary>
    /// <param name="rawValue"></param>
    /// <param name="digits"></param>
    /// <returns></returns>
    public static string FormatLargeNumber(float rawValue, int digits = 1)
    {
        if (digits < 0) digits = 0;
        if (rawValue < 1e+3) return rawValue.ToString();
        else if (rawValue < 1e+6) return $"{(rawValue / 1e+3).ToString($"F{digits}")}k";
        else return $"{(rawValue / 1e+6).ToString($"F{digits}")}M";
    }
}

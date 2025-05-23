namespace Cheryl.Uno.Controls.Background;

public readonly struct FastNoiseRendererOptionsWinUI3
{
    public FastNoiseLite.NoiseType Type { get; }
    public float NoiseScale { get; }
    public float XAnimSpeed { get; }
    public float YAnimSpeed { get; }
    public float PrimaryAlpha { get; }
    public float AccentAlpha { get; }

    public FastNoiseRendererOptionsWinUI3(
        FastNoiseLite.NoiseType type,
        float noiseScale = 2f,
        float xAnimSpeed = 2f,
        float yAnimSpeed = 1f,
        float primaryAlpha = 0.7f,
        float accentAlpha = 0.04f,
        float animSeedScale = 0.1f)
    {
        Type = type;
        NoiseScale = noiseScale;
        XAnimSpeed = xAnimSpeed * animSeedScale;
        YAnimSpeed = yAnimSpeed * animSeedScale;
        PrimaryAlpha = primaryAlpha;
        AccentAlpha = accentAlpha;
    }
}

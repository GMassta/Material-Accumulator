using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Settings/Settings", order = 0)]
public class Settings : ScriptableObject, IRadiusOscillatorSettings, IZoneMotionSettings
{
    [Header("Half Sphere Settings")]
    [Tooltip("Базовый радиус")]
    [SerializeField] private float baseRadius;
    [Tooltip("Амплитуда изменения радиуса")]
    [SerializeField] private float amplitude;
    [Tooltip("Частота изменения радиуса")]
    [SerializeField] private float frequency;
    [Tooltip("Настройка Animation curve изменения радиуса")]
    [SerializeField] private AnimationCurve curve;

    [Header("Zone Settings")] 
    [Tooltip("Скорость перемещения зоны")]
    [SerializeField] private float zoneMove;
    [Tooltip("Скорость накопления материала")]
    [SerializeField] private float accumulationRate;

    public float BaseRadius => baseRadius;
    public float Amplitude => amplitude;
    public float Frequency => frequency;
    public AnimationCurve Curve => curve;
    
    public float ZoneMove => zoneMove;
    public float AccumulationRate => accumulationRate;
}

public interface IRadiusOscillatorSettings
{
    float BaseRadius { get; }
    float Amplitude { get; }
    float Frequency { get; }
    AnimationCurve Curve { get; }
}

public interface IZoneMotionSettings
{
    float ZoneMove { get; }
    float AccumulationRate { get; }
}
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Settings/Settings", order = 0)]
public class Settings : ScriptableObject
{
    [Header("Half Sphere Settings")]
    [Tooltip("Базовый радиус")]
    public float baseRadius;
    [Tooltip("Амплитуда изменения радиуса")]
    public float amplitude;
    [Tooltip("Частота изменения радиуса")]
    public float frequency;
    [Tooltip("Настройка Animation curve изменения радиуса")]
    public AnimationCurve curve;

    [Header("Zone Settings")] 
    [Tooltip("Скорость перемещения зоны")]
    public float zoneMove;
    [Tooltip("Скорость накопления материала")]
    public float accumulationRate;
}
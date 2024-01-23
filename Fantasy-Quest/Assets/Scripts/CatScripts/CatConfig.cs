using UnityEngine;

[CreateAssetMenu(fileName ="CatConfig", menuName ="Configs/CatConfig")]
public class CatConfig : ScriptableObject
{
    [SerializeField] private RunStateConfig runStateConfig;
    [SerializeField] private AirConfig airConfig;

    public RunStateConfig RunStateConfig => runStateConfig;
    public AirConfig AirConfig => airConfig;
}

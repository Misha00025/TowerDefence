

public interface IWavesGenerator
{
    bool WavesListIsEmpty { get; }

    Wave GetNextWave();
    Wave GenerateWave(WavePrefab prefab);
}
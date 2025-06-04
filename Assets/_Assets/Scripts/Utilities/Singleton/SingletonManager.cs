public static class SingletonManager
{
    public static InGameVisualHandler InGameVisualHandler => InGameVisualHandler.Instance;
    public static MapManager MapManager => MapManager.Instance;
    public static CameraManager CameraManager => CameraManager.Instance;
    public static GameEffectManager GameEffectManager => GameEffectManager.Instance;
    public static PoolingSystem PoolingSystem => PoolingSystem.Instance;
    public static GameToolManager GameToolManager => GameToolManager.Instance;
}
public static class SingletonManager
{
    public static InGameVisualHandler InGameVisualHandler => InGameVisualHandler.Instance;
    public static MapManager MapManager => MapManager.Instance;
    public static CameraManager CameraManager => CameraManager.Instance;
}
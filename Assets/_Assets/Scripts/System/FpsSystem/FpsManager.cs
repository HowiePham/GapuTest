using UnityEngine;

public class FpsManager : MonoBehaviour
{
    [SerializeField] private int limitedFps;

    private void Awake()
    {
        LimitFps();
    }

    private void LimitFps()
    {
        Application.targetFrameRate = limitedFps;
    }
}
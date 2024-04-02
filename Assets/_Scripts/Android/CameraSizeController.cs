using UnityEngine;

public class CameraSizeController : MonoBehaviour
{
    public Camera mainCamera;
    public float defaultSize = 6.4f;
    public float minSize = 4f; // ����������� ������ ��������� ������
    public float maxSize = 8f; // ������������ ������ ��������� ������

    void Start()
    {
        // ������������� ����������� ������ ��������� ������
        mainCamera.orthographicSize = defaultSize;
        AdjustCameraSize();
    }

    void Update()
    {
        AdjustCameraSize();
    }

    void AdjustCameraSize()
    {
        // �������� ������� ���������� ������
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);

        // ��������� ���������������� ������ ��������� ������ � ����������� �� �������� ���������� ������
        float proportionalSize = defaultSize * (screenSize.y / screenSize.x);

        // ������������ ������ ��������� ������ � ��������� �� minSize �� maxSize
        float clampedSize = Mathf.Clamp(proportionalSize, minSize, maxSize);

        // �������� ������ ��������� ������
        mainCamera.orthographicSize = clampedSize;
    }
}

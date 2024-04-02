using UnityEngine;

public class ObjectSizeController : MonoBehaviour
{
    public Camera mainCamera;
    public Transform objectTransform;
    public float defaultWidth = 12.8f; // ������ ������� �� ���������
    public float defaultHeight = 7.2f; // ������ ������� �� ���������
    public float minWidth = 8f; // ����������� ������ �������
    public float minHeight = 4.5f; // ����������� ������ �������
    public float maxWidth = 16f; // ������������ ������ �������
    public float maxHeight = 9f; // ������������ ������ �������

    private float defaultAspect;

    void Start()
    {
        defaultAspect = defaultWidth / defaultHeight;
        AdjustObjectSize();
    }

    void Update()
    {
        AdjustObjectSize();
    }

    void AdjustObjectSize()
    {
        // �������� ������� ���������� ������
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // ��������� ���������������� ������ ������� � ����������� �� �������� ���������� ������
        float proportionalWidth = defaultWidth;
        float proportionalHeight = defaultHeight;

        float screenAspect = screenWidth / screenHeight;

        if (screenAspect > defaultAspect)
        {
            // ��������� ������ �������, ����� ������������ ���������
            proportionalWidth = defaultHeight * screenAspect;
        }
        else if (screenAspect < defaultAspect)
        {
            // ��������� ������ �������, ����� ������������ ���������
            proportionalHeight = defaultWidth / screenAspect;
        }

        // ������������ ������ ������� � ��������� �� minWidth �� maxWidth � �� minHeight �� maxHeight
        float clampedWidth = Mathf.Clamp(proportionalWidth, minWidth, maxWidth);
        float clampedHeight = Mathf.Clamp(proportionalHeight, minHeight, maxHeight);

        // �������� ������ � ������� �������
        float scale = clampedWidth / defaultWidth;
        objectTransform.localScale = new Vector3(scale, scale, scale);
        mainCamera.orthographicSize = clampedHeight / 2f;
    }
}

using UnityEngine;
using System.IO;

public class VideoLoader : MonoBehaviour
{
    public string videoFileName = "Nonsensica.mp4";

    private void Start()
    {
        // ���� � ����� � StreamingAssets
        string sourcePath = Path.Combine(Application.streamingAssetsPath, videoFileName);

        // ����, ���� �� ��������� ����
        string destinationPath = Path.Combine(Application.persistentDataPath, videoFileName);

        // ���� ���� �� ���������� � persistentDataPath, ��������� ��� ����
        if (!File.Exists(destinationPath))
        {
            byte[] videoBytes;

            // ������� �� ���������, ��� ��������� ���� (��� WebGL ���������� UnityWebRequest)
#if UNITY_WEBGL && !UNITY_EDITOR
                UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(sourcePath);
                www.SendWebRequest();
                while (!www.isDone) { }
                videoBytes = www.downloadHandler.data;
#else
            videoBytes = File.ReadAllBytes(sourcePath);
#endif

            File.WriteAllBytes(destinationPath, videoBytes);
        }

        // ������ ���������� destinationPath ��� ��������������� �����
        //YourPlugin.PlayVideo(destinationPath);
    }
}

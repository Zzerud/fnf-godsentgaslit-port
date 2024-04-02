using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fsaasda : MonoBehaviour
{
    private bool isTouching = false;
    private int touchCount = 0;

    private void Update()
    {
        // ���������, ��� ����������� ����� 2 �������.
        if (touchCount > 2)
        {
            // ��� ��� ��� ��������� ����� ��� ���� �������.
        }

        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);

                    // �������� RaycastHit2D ��� ��������� ���������� � ������������ � ������������.
                    RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

                    if (hit.collider != null)
                    {
                        // ���������, ��� ������������ ��������� � Collider2D ������ �������.
                        if (hit.collider.CompareTag("YourTag"))
                        {
                            //isTouching[touch.fingerId] = true;
                            // ����� ����� �������� ���, ����������� ��� ������� UI ��������.
                            Debug.Log("����� �������� UI �������� " + hit.collider.gameObject.name);
                        }
                    }
                    else
                    {
                        Debug.Log("hui");
                    }
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    isTouching = false;
                    touchCount--;

                    // ����� ����� �������� ���, ����������� ��� ���������� ������ � ������.

                    Debug.Log("����� ������� � ������.");
                }
            }
        }
    }
}

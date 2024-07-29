using System.Collections;
using TreeEditor;
using UnityEngine;

public class WeaponAutoLazer : MonoBehaviour
{

    [SerializeField] private Transform pos1; // �ѱ��� ��ġ
    [SerializeField] private Transform pos2; // ������ ��ġ

    private void OnEnable()
    {
        pos1 = transform.GetChild(1);
        pos2 = transform.GetChild(4);
    }

    public void SetPositions(Vector3 start, Vector3 end)
    {
            pos1.position = start;
            pos2.position = end;
    }
}
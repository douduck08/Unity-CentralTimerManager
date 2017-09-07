using System.Collections;
using System.Collections.Generic;
using DouduckGame.Math;
using UnityEngine;

public class EasingTest : MonoBehaviour {

    [System.Serializable]
    public struct EasingData {
        public GameObject go;
        public Easing easing;
    }

    public Easing m_easing;
    public EasingData m_inStuct;
    public List<Easing> m_inList;
    public List<EasingData> m_inStuctList;

    public float height = 10f;
    public float durability = 2f;
    public float m_time = 0f;

    private void Update () {
        m_time += Time.deltaTime;
        for (int i = 0; i < m_inStuctList.Count; i++) {
            if (m_inStuctList[i].go != null) {
                float x = m_inStuctList[i].go.transform.position.x;
                m_inStuctList[i].go.transform.position = new Vector2 (0, m_inStuctList[i].easing.Ease (m_time / durability) * height) * 10f;
            }
        }
        if (m_time > durability) {
            m_time -= durability;
        }
    }

    //[SerializeField]
    //private int m_pointNumber;
    //[SerializeField]
    //private int m_scale;
    //void OnDrawGizmos () {
    //    float deltaX_ = 1f / m_pointNumber;
    //    for (int i = 0; i < m_pointNumber; i++) {
    //        float x1 = deltaX_ * i;
    //        float x2 = deltaX_ * (i + 1);
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawLine (new Vector2 (x1, Easing.EaseInBounce (x1)) * m_scale, new Vector2 (x2, Easing.EaseInBounce (x2)) * m_scale);
    //        Gizmos.color = Color.yellow;
    //        Gizmos.DrawLine (new Vector2 (x1, Easing.EaseOutBounce (x1)) * m_scale, new Vector2 (x2, Easing.EaseOutBounce (x2)) * m_scale);
    //        Gizmos.color = Color.green;
    //        Gizmos.DrawLine (new Vector2 (x1, Easing.EaseInOutBounce (x1)) * m_scale, new Vector2 (x2, Easing.EaseInOutBounce (x2)) * m_scale);
    //        Gizmos.color = Color.blue;
    //        Gizmos.DrawLine (new Vector2 (x1, Easing.EaseOutInBounce (x1)) * m_scale, new Vector2 (x2, Easing.EaseOutInBounce (x2)) * m_scale);
    //    }
    //}
}
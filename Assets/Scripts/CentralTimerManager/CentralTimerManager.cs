using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace DouduckGame.TimerManager {
    public sealed class CentralTimerManager : MonoBehaviour {

        private const bool hideInHierarchy = false;

        private static object m_oLock = new object ();
        private static CentralTimerManager m_oInstance = null;
        private static CentralTimerManager Instance {
            get {
                if (m_applicationIsQuitting) {
                    return null;
                }

                lock (m_oLock) {
                    if (m_oInstance == null) {
                        GameObject container_ = new GameObject ();
                        GameObject.DontDestroyOnLoad (container_);
                        m_oInstance = container_.AddComponent<CentralTimerManager> ();
                        container_.name = "[Singleton] " + m_oInstance.GetType ().Name;
                        if (hideInHierarchy) {
                            container_.hideFlags = HideFlags.HideInHierarchy;
                        }
                    }
                    return m_oInstance;
                }
            }
        }

        private static bool m_applicationIsQuitting = false;
        public void OnDestroy () {
            m_applicationIsQuitting = true;
        }

        private List<DTimer> m_timerList = new List<DTimer> ();

        void Update () {
            for (int i = m_timerList.Count - 1; i >= 0; i--) {
                m_timerList[i].Update ();
            }
        }

        public static DTimer CreateDTimer (float second, Action<float> callback) {
            return Instance.AddTimer (DTimer.Generate (second, callback));
        }

        private DTimer AddTimer (DTimer timer) {
            m_timerList.Add (timer);
            return timer;
        }

        public static void RemoveTimer (DTimer timer) {
            Instance.m_timerList.Remove (timer);
        }
    }
}

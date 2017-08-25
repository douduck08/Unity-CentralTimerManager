using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace DouduckGame.TimerManager {
    public class DTimer {

        protected event Action m_OnStart;
        protected event Action m_OnComplete;
        protected event Action m_OnStop;
        protected event Action<float> m_OnUpdate;

        protected ProgressStatus m_progress;
        protected LoopCounter m_loopCounter;
        protected TimerStatus m_timerStatus;

        public bool paused {
            get {
                return m_timerStatus.paused;
            }
        }
        public bool started {
            get {
                return m_timerStatus.started;
            }
        }
        public bool stopped {
            get {
                return m_timerStatus.stopped;
            }
        }

        public static DTimer Generate (float second, System.Action<float> callback) {
            return new DTimer (second, callback);
        }

        private DTimer (float second, System.Action<float> callback) {
            Initialize (second);
            m_OnUpdate = callback;
        }

        protected void Initialize (float period) {
            m_progress.period = period;
            m_progress.scale = 1f;
            m_progress.resumeTime = Time.time;
            m_progress.pausedProgress = 0f;

            m_loopCounter.loopTime = 1;
            m_loopCounter.loopCount = 0;

            m_timerStatus.paused = false;
            m_timerStatus.started = false;
            m_timerStatus.stopped = false;
        }

        protected void Repeat () {
            m_progress.resumeTime = Time.time;
            if (m_progress.scale > 0) {
                m_loopCounter.loopCount += 1;
                m_progress.pausedProgress = 0f;
            } else {
                m_loopCounter.loopCount -= 1;
                m_progress.pausedProgress = 1f;
            }

            if (m_loopCounter.loopCount >= m_loopCounter.loopTime || m_loopCounter.loopCount < 0) {
                Stop ();
            }
        }

        public void Update () {
            if (m_timerStatus.needUpdate) {
            }
            float progress = m_progress.progress;
            if (m_OnUpdate != null) {
                m_OnUpdate (Mathf.Clamp01 (progress));
            }
            if (progress > 1 || progress < 0) {
                Repeat ();
            }
        }

        public DTimer SetLoop (int loopTime, LoopType loopType = LoopType.Repeat) {
            if (m_timerStatus.stopped) {
                throw new InvalidOperationException ("This DTimer was stopped");
            }
            m_loopCounter.loopTime = loopTime;
            // TODO: loopType
            return this;
        }

        public DTimer SetDelay (float delay) {
            if (m_timerStatus.stopped) {
                throw new InvalidOperationException ("This DTimer was stopped");
            }
            m_progress.pausedProgress = m_progress.progress;
            m_progress.resumeTime = m_progress.resumeTime + delay;
            return this;
        }

        public DTimer SetScale (float scale) {
            if (m_timerStatus.stopped) {
                throw new InvalidOperationException ("This DTimer was stopped");
            }
            if (m_timerStatus.paused) {
                m_progress.pauseScale = scale;
            } else {
                m_progress.pausedProgress = m_progress.progress;
                m_progress.resumeTime = Time.time;
                m_progress.scale = scale;
            }
            return this;
        }

        public DTimer Pause () {
            if (m_timerStatus.stopped) {
                throw new InvalidOperationException ("This DTimer was stopped");
            }

            if (m_timerStatus.paused) {
                return this;
            }
            // TODO
            m_progress.pauseScale = m_progress.scale;
            SetScale (0f);
            m_timerStatus.paused = true;
            return this;
        }

        public DTimer Resume () {
            if (m_timerStatus.stopped) {
                throw new InvalidOperationException ("This DTimer was stopped");
            }

            if (!m_timerStatus.paused) {
                return this;
            }
            m_timerStatus.paused = false;
            SetScale (m_progress.pauseScale);
            return this;
        }

        public DTimer Stop () {
            m_timerStatus.stopped = true;
            CentralTimerManager.RemoveTimer (this);
            return this;
        }

        public DTimer OnStart (Action callback) {
            m_OnStart += callback;
            return this;
        }

        public DTimer OnComplete (Action callback) {
            m_OnComplete += callback;
            return this;
        }
        public DTimer OnStop (Action callback) {
            m_OnStop += callback;
            return this;
        }
        public DTimer OnUpdate (Action<float> callback) {
            m_OnUpdate += callback;
            return this;
        }
    }
}

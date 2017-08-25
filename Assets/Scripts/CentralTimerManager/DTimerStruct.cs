using UnityEngine;

namespace DouduckGame.TimerManager {

    public struct ProgressStatus {
        public float period;
        public float scale;
        public float resumeTime;
        public float pausedProgress;
        public float pauseScale;

        public float progress {
            get {
                if (Time.time > resumeTime) {
                    return pausedProgress + (Time.time - resumeTime) * scale;
                } else {
                    return pausedProgress;
                }
            }
        }
    }

    public struct LoopCounter {
        public int loopTime;
        public int loopCount;
    }

    public struct TimerStatus {
        public bool paused;
        public bool started;
        public bool stopped;

        public bool needUpdate {
            get {
                return !paused && !stopped;
            }
        }
    }

    public enum LoopType {
        Repeat,
        Yoyo
    }
}

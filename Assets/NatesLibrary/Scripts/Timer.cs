using UnityEngine;
using UnityEngine.Events;


namespace NatesLibrary //if in subfolder, write .subfoldername
{
    public class Timer : MonoBehaviour
    {
        private float m_timeLeft = 0.0f;
        public float timeLeft {  get {  return m_timeLeft; }  } //allows other scripts to read timeLeft but not to modify it

        public UnityEvent timeout;

        public bool autoStart = false;  //if the timer starts on awake or not

        public bool autoRestart = false; //if the time automattically restarts when complete or not

        public bool useScaledTime = true;

        public float countDownTime = 1.0f;

        public float timeScale = 1.0f;

        public bool countingDown {  get {  return m_timeLeft > 0.0f; } }


        void Start()
        {
            if (autoStart)
                StartTimer();
        }

        void Update()
        {
            if (m_timeLeft > 0.0)
            {
                if (useScaledTime)
                    m_timeLeft -= (Time.deltaTime * timeScale);
                else
                    m_timeLeft -= (Time.unscaledDeltaTime * timeScale);


                if (m_timeLeft < 0.0f)
                {
                    timeout.Invoke(); //call unity events. EventName.Invoke

                    if (autoRestart)
                        StartTimer();
                }
            }
        }
        public void StartTimerFromEvent()
        {
            StartTimer();
        }

        public void StopTimer()
        {
            m_timeLeft = 0.0f;
        }

        public void StartTimer(float? _countDown = null, bool _autoRestart = false)
        {
            if (_countDown != null && _countDown > 0.0f)
                countDownTime = (float)_countDown;

            autoRestart = _autoRestart;

            m_timeLeft = countDownTime;
        }
    }

}



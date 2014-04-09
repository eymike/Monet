using System;
using System.Diagnostics;

namespace MonetCore
{
    [ServiceAtribute(Name = "Frame Timer")]
    public class FrameTimer
    {
        public FrameTimer(MonetServiceProvider services)
        {

        }

        private Stopwatch m_timer = new Stopwatch();
        public TimeSpan TotalTime
        {
            get { return m_currentTime; }
        }

        public TimeSpan ElapsedTime
        {
            get { return m_elapsedTime; }
        }

        private TimeSpan m_currentTime;
        private TimeSpan m_elapsedTime;

        public void Start()
        {
            m_timer.Start();
        }

        public void Stop()
        {
            m_timer.Stop();
        }

        public void Tick()
        {
            var currentTime = m_timer.Elapsed;
            m_elapsedTime = currentTime - m_currentTime;
            m_currentTime = currentTime;
        }
    }
}

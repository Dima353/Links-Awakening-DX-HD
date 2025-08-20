using System;
using Microsoft.Xna.Framework.Audio;

namespace GbsPlayer
{
    public class CDynamicEffectInstance
    {
        private readonly DynamicSoundEffectInstance _instance;

        public SoundState State => _instance.State;

        public CDynamicEffectInstance(int sampleRate)
        {
            // Mono, 16-bit PCM
            _instance = new DynamicSoundEffectInstance(sampleRate, AudioChannels.Mono);
        }

        public int GetPendingBufferCount()
        {
            return _instance.PendingBufferCount;
        }

        public void Play()
        {
            if (State != SoundState.Playing)
                _instance.Play();
        }

        public void Pause()
        {
            if (State == SoundState.Playing)
                _instance.Pause();
        }

        public void Resume()
        {
            if (State == SoundState.Paused)
                _instance.Play();
        }

        public void Stop()
        {
            _instance.Stop();
        }

        public void SetVolume(float volume)
        {
            _instance.Volume = volume;
        }

        public void SubmitBuffer(byte[] buffer, int offset, int count)
        {
            var data = new byte[count];
            Buffer.BlockCopy(buffer, offset, data, 0, count);
            _instance.SubmitBuffer(data);
        }
    }
}

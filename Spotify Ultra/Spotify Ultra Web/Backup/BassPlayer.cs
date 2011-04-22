using System;
using System.Collections.Generic;
using System.Text;
using Un4seen.Bass;

namespace SpofityRuntime
{
	public class BassPlayer
	{
		private BASSBuffer basbuffer = null;
		private STREAMPROC streamproc = null;

		public int EnqueueSamples(AudioData audioData)
		{
			return EnqueueSamples(audioData.Channels, audioData.Rate, audioData.Samples, audioData.Frames);
		}

		public int EnqueueSamples(int channels, int rate, byte[] samples, int frames)
		{
			int consumed = 0;
			if (basbuffer == null)
			{
				Bass.BASS_Init(-1, rate, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
				basbuffer = new BASSBuffer(2, rate, channels, 2);
				streamproc = new STREAMPROC(Reader);
				int bassId = Bass.BASS_StreamCreate(rate, channels, BASSFlag.BASS_DEFAULT, streamproc, IntPtr.Zero);
				Bass.BASS_ChannelPlay(bassId, false);
			}

			if (basbuffer.Space(0) > samples.Length)
			{
				basbuffer.Write(samples, samples.Length);
				consumed = frames;
			}

			return consumed;
		}

		private int Reader(int handle, IntPtr buffer, int length, IntPtr user)
		{
			return basbuffer.Read(buffer, length, user.ToInt32());
		}

		public void Stop()
		{ 
			//Bass.BASS_SampleStop
			Bass.BASS_Stop();
		}
	}
}

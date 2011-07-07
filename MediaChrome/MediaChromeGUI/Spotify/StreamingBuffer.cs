using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spotify;
using MediaChrome;
namespace Spotify
{
    /// <summary>
    /// Class for dealing with streaming buffer
    /// </summary>
    public class StreamingBuffer
    {
        /// <summary>
        /// Track the buffer is bound to
        /// </summary>
        public Track Track { get; set; }

        /// <summary>
        /// Buffer of audio data
        /// </summary>
        public List<MCRuntime.AudioData> Buffer { get; set; }
    }
}

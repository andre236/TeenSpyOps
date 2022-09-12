using System;
using System.Collections;
using UnityEngine;


// https: //www.youtube.com/watch?v=3hsBFxrIgQI - Referencia do código.
namespace Controllers
{
    public class AudioController : MonoBehaviour
    {
        private Hashtable _audioTable;
        private Hashtable _jobTable;

        public static AudioController Instance;

        public bool IsDebugging;

        public AudioTrack[] Tracks;


        [System.Serializable]
        public class AudioObject
        {
            public AudioType Type;
            public AudioClip Clip;
        }

        [System.Serializable]
        public class AudioTrack
        {
            public AudioSource Source;
            public AudioSource[] Audio;

        }

        private class AudioJob
        {
            public AudioAction Action;
            public AudioType Type;

            public AudioJob(AudioAction action, AudioType type)
            {
                Action = action;
                Type = type;
            }
        }

        private enum AudioAction
        {
            START,
            STOP,
            RESTART
        }

        #region Unity Functions

        private void Awake()
        {
            if (!Instance)
            {
                Configure();
            }
        }

        private void OnDisable()
        {
            Dispose();
        }

        #endregion

        #region Public Functions

        public void PlayAudio(AudioType type)
        {
            AddJob(new AudioJob(AudioAction.START, type));
        }

        public void StopAudio(AudioType type)
        {
            AddJob(new AudioJob(AudioAction.STOP, type));

        }

        public void RestartAudio(AudioType type)
        {
            AddJob(new AudioJob(AudioAction.RESTART, type));

        }

        #endregion

        #region Private Functions

        private void Configure()
        {
            Instance = this;
            _audioTable = new Hashtable();
            _jobTable = new Hashtable();
            GenerateAudioTable();

        }

        private void Dispose()
        {
            foreach(DictionaryEntry entry in _jobTable)
            {
                IEnumerator job = (IEnumerator)entry.Value;
                StopCoroutine(job);
            }
        }

        public AudioClip GetAudioClipFromAudioTrack(AudioType type, AudioTrack track)
        {
            //foreach (AudioObject audioObject in track.Audio)
            //{
            //    if (audioObject.Type == type)
            //    {
            //        return audioObject.Clip;
            //    }
            //}

            return null;
        }

        private void Log(string msg)
        {
            if (!IsDebugging)
                return;
            Debug.Log("[Audio Controller]: " + msg);
        }

        private void LogWarning(string msg)
        {
            if (!IsDebugging) return;
            Debug.LogWarning("[Audio Controller]: " + msg);
        }

        private void GenerateAudioTable()
        {
            foreach (AudioTrack track in Tracks)
            {
                //foreach (AudioObject audioObject in track.Audio)
                //{
                //    // Do not duplicate keys
                //    if (_audioTable.ContainsKey(audioObject.Type))
                //    {
                //        LogWarning("You are trying to register audio [" + audioObject.Type + "] that has already been registered");

                //    }
                //    else
                //    {
                //        _audioTable.Add(audioObject, track);
                //        Log("Registering audio [" + audioObject.Type + "].");
                //    }

                //}
            }
        }

        private IEnumerator RunAudioJob(AudioJob job)
        {
            AudioTrack track = (AudioTrack)_audioTable[job.Type];
            track.Source.clip = GetAudioClipFromAudioTrack(job.Type, track);

            switch (job.Action)
            {
                case AudioAction.START:
                    track.Source.Play();
                    break;
                case AudioAction.STOP:
                    track.Source.Stop();
                    break;
                case AudioAction.RESTART:
                    track.Source.Stop();
                    track.Source.Play();
                    break;
            }

            _jobTable.Remove(job.Type);
            Log("Job count:" + _jobTable.Count);
            yield return null;
        }

        private void AddJob(AudioJob job)
        {
            // Removing conflicting jobs
            RemoveConflictingJobs(job.Type);

            // Start Job
            IEnumerator jobRunner = RunAudioJob(job);
            _jobTable.Add(job.Type, jobRunner);
            Log("Starting job on [" + job.Type + "] with operation " + job.Action);
        }

        private void RemoveJob(AudioType type)
        {
            if (!_jobTable.ContainsKey(type))
            {
                LogWarning("Trying to stop a job [" + type + "] that is not running.");
                return;
            }

            IEnumerator runningJob = (IEnumerator)_jobTable[type];
            StopCoroutine(runningJob);
            _jobTable.Remove(type);
        }

        private void RemoveConflictingJobs(AudioType type)
        {
            if(_jobTable.ContainsKey(type))
            {
                RemoveJob(type);
            }

            AudioType conflictAudio = AudioType.None;

            foreach(DictionaryEntry entry in _jobTable)
            {
                AudioType audioType = (AudioType)entry.Key;
                AudioTrack audioTrackInUse = (AudioTrack)_audioTable[audioType];
                AudioTrack audioTrackNeeded = (AudioTrack)_audioTable[type];

                if(audioTrackNeeded.Source == audioTrackInUse.Source)
                {
                    //Conflict 
                    conflictAudio = audioType;
                }
            }
            if(conflictAudio != AudioType.None)
            {
                RemoveJob(conflictAudio);
            }
        }



        #endregion
    }
}
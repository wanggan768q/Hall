using UnityEngine;
using System.Collections;

public class GameSound : MonoBehaviour
{


    public static bool SoundOnFlying;

    public static bool MusicOnFlying;

    private void Start()
    {
        string s = DateFile.GetSingleton().ReadVoice("SoundSource");
        if (int.Parse(s) == 0)
        {
            SoundOnFlying = false;
            SoundManager.GetSingleton().StopAudioSource(SoundManager.GetSingleton().SoundSource);
        }
        else if (int.Parse(s) == 1)
        {
            SoundOnFlying = true;
            SoundManager.GetSingleton().PlayMusicSource();
        }
        s = DateFile.GetSingleton().ReadVoice("MusicSource");
        if (int.Parse(s) == 0)
        {
            MusicOnFlying = false;
            SoundManager.GetSingleton().StopAudioSource(SoundManager.GetSingleton().MusicSource);
        }
        else if (int.Parse(s) == 1)
        {
            MusicOnFlying = true;
            SoundManager.GetSingleton().PlayMusicSource();
        }
    }
}

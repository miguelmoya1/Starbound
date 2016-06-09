using System;
using Tao.Sdl;

class Sound
{
    IntPtr Int;
    
    public Sound(string name)
    {
        Int = SdlMixer.Mix_LoadMUS(name);
    }

    public void SetSound(string name)
    {
        Int = SdlMixer.Mix_LoadMUS(name);
    }
    
    public void Play1()
    {
        SdlMixer.Mix_PlayMusic(Int, 1);
    }
    
    public void PlayAll()
    {
        SdlMixer.Mix_PlayMusic(Int, -1);
    }
    
    public void Stop()
    {
        SdlMixer.Mix_HaltMusic();
    }

}

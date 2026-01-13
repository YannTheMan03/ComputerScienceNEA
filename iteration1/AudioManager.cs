using WMPLib;

public static class AudioManager
{
    public static readonly WindowsMediaPlayer MusicPlayer;

    static AudioManager()
    {
        MusicPlayer = new WindowsMediaPlayer();
        MusicPlayer.URL = @"C:\Users\yb.2415248\OneDrive - Hereford Sixth Form College\Computer Science\C03 - Project\Assets\Space Arcade Sci-Fi Theme (Royalty Free).mp3";
        MusicPlayer.settings.setMode("loop", true);
        MusicPlayer.controls.play();
    }

    public static void SetVolume(int volume)
    {
        MusicPlayer.settings.volume = volume;
    }

    public static void SetMute(bool mute)
    {
        MusicPlayer.settings.mute = mute;
    }
}

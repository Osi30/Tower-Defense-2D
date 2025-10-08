using System;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    Music,
    SFX
}

[Serializable]
public class SoundEntry
{
    public string key;           // ví dụ: "coin", "win", "bgm_main"
    public AudioClip clip;
    public SoundType type = SoundType.SFX;
    [Range(0f, 1f)] public float volume = 1f;
    [Range(-3f, 3f)] public float pitch = 1f;
    public bool loop = false;    // chủ yếu hữu ích cho Music
}

[CreateAssetMenu(menuName = "Audio/Audio Library", fileName = "AudioLibrary")]
public class AudioLibrary : ScriptableObject
{
    public List<SoundEntry> sounds = new List<SoundEntry>();

    private Dictionary<string, SoundEntry> _map;

    public bool TryGet(string key, out SoundEntry entry)
    {
        if (_map == null)
        {
            _map = new Dictionary<string, SoundEntry>(StringComparer.OrdinalIgnoreCase);
            foreach (var s in sounds)
            {
                if (!string.IsNullOrWhiteSpace(s.key) && !_map.ContainsKey(s.key))
                    _map.Add(s.key, s);
            }
        }
        return _map.TryGetValue(key, out entry);
    }

    public IEnumerable<SoundEntry> All(SoundType type)
    {
        foreach (var s in sounds)
            if (s.type == type) yield return s;
    }
}

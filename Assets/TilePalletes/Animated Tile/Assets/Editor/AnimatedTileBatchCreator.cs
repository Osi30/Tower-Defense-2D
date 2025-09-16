#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AnimatedTileBatchCreator : EditorWindow
{
    // ==== PATHS (đúng theo project của bạn) ====
    [SerializeField] private string frame1Path = "Assets/TilePalletes/Animated Tile/Assets/Frame1";
    [SerializeField] private string frame2Path = "Assets/TilePalletes/Animated Tile/Assets/Frame2";
    [SerializeField] private string frame3Path = "Assets/TilePalletes/Animated Tile/Assets/Frame3";
    [SerializeField] private string frame4Path = "Assets/TilePalletes/Animated Tile/Assets/Frame4";
    [SerializeField] private string outputPath = "Assets/TilePalletes/Animated Tile/Assets/AnimatedTiles";

    [SerializeField] private float fps = 8f;
    [SerializeField] private bool overwrite = true;

    [MenuItem("Tools/Tilemap/Animated Tile Builder")]
    public static void ShowWindow() => GetWindow<AnimatedTileBatchCreator>("Animated Tile Builder");

    void OnGUI()
    {
        EditorGUILayout.LabelField("Folders", EditorStyles.boldLabel);
        frame1Path = FolderField("Frame1 (TileFrame1_XX*)", frame1Path);
        frame2Path = FolderField("Frame2 (TileFrame2_XX*)", frame2Path);
        frame3Path = FolderField("Frame3 (TileFrame3_XX*)", frame3Path);
        frame4Path = FolderField("Frame4 (TileFrame4_XX*)", frame4Path);
        outputPath = FolderField("Output (AnimatedTiles)", outputPath);

        EditorGUILayout.Space(6);
        EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);
        fps = EditorGUILayout.FloatField("FPS", Mathf.Max(0.01f, fps));
        overwrite = EditorGUILayout.ToggleLeft("Overwrite existing", overwrite);

        EditorGUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Scan Only", GUILayout.Height(26))) Scan();
        if (GUILayout.Button("Generate AnimatedTiles", GUILayout.Height(26))) Generate();
        EditorGUILayout.EndHorizontal();
    }

    string FolderField(string label, string path)
    {
        EditorGUILayout.BeginHorizontal();
        path = EditorGUILayout.TextField(label, path);
        if (GUILayout.Button("Select", GUILayout.Width(70)))
        {
            string p = EditorUtility.OpenFolderPanel("Select folder (inside Assets/)", Application.dataPath, "");
            if (!string.IsNullOrEmpty(p))
            {
                if (p.StartsWith(Application.dataPath))
                    path = "Assets" + p.Substring(Application.dataPath.Length);
                else
                    EditorUtility.DisplayDialog("Invalid", "Folder must be inside Assets/.", "OK");
            }
        }
        EditorGUILayout.EndHorizontal();
        return path;
    }

    void Scan()
    {
        var m1 = LoadByKey(frame1Path, 1, out var c1);
        var m2 = LoadByKey(frame2Path, 2, out var c2);
        var m3 = LoadByKey(frame3Path, 3, out var c3);
        var m4 = LoadByKey(frame4Path, 4, out var c4);

        Debug.Log($"[Scan] Sprites found  F1:{c1}  F2:{c2}  F3:{c3}  F4:{c4}");
        Debug.Log($"[Scan] Keys F1: {string.Join(", ", m1.Keys.Take(40))}");
        Debug.Log($"[Scan] Keys F2: {string.Join(", ", m2.Keys.Take(40))}");
        Debug.Log($"[Scan] Keys F3: {string.Join(", ", m3.Keys.Take(40))}");
        Debug.Log($"[Scan] Keys F4: {string.Join(", ", m4.Keys.Take(40))}");

        var all = new HashSet<string>(m1.Keys);
        all.UnionWith(m2.Keys); all.UnionWith(m3.Keys); all.UnionWith(m4.Keys);
        var miss = all.Where(k => !m1.ContainsKey(k) || !m2.ContainsKey(k) || !m3.ContainsKey(k) || !m4.ContainsKey(k)).ToList();
        if (miss.Count == 0) Debug.Log("[Scan] OK: All key sets complete.");
        else Debug.LogWarning("[Scan] Incomplete keys: " + string.Join(", ", miss.Take(60)));
    }

    void Generate()
    {
        EnsureFolder(outputPath);

        var m1 = LoadByKey(frame1Path, 1, out _);
        var m2 = LoadByKey(frame2Path, 2, out _);
        var m3 = LoadByKey(frame3Path, 3, out _);
        var m4 = LoadByKey(frame4Path, 4, out _);

        var keys = m1.Keys.Intersect(m2.Keys).Intersect(m3.Keys).Intersect(m4.Keys)
                          .Where(k => int.TryParse(k, out _))
                          .OrderBy(k => int.Parse(k))
                          .ToList();

        if (keys.Count == 0)
        {
            Debug.LogWarning("[AnimatedTile] No common keys found. Run Scan to inspect naming.");
            return;
        }

        int created = 0, skipped = 0;
        foreach (var key in keys)
        {
            var tile = ScriptableObject.CreateInstance<AnimatedTile>();
            tile.m_AnimatedSprites = new[] { m1[key], m2[key], m3[key], m4[key] };
            tile.m_MinSpeed = fps;
            tile.m_MaxSpeed = fps;
            tile.m_TileColliderType = Tile.ColliderType.None;

            string assetPath = $"{outputPath}/TileAnim_{key}.asset";
            if (!overwrite && File.Exists(assetPath)) { skipped++; continue; }

            AssetDatabase.CreateAsset(tile, assetPath);
            EditorUtility.SetDirty(tile);
            created++;
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"[AnimatedTile] Created {created}, Skipped {skipped}. Output: {outputPath}");
    }

    // --- Phân key đúng cho cả "TileFrameN_01" và "TileFrameN_64_0" ---
    Dictionary<string, Sprite> LoadByKey(string folder, int frameNumber, out int totalSprites)
    {
        var dict = new Dictionary<string, Sprite>();
        totalSprites = 0;

        if (!AssetDatabase.IsValidFolder(folder))
        {
            Debug.LogWarning($"[AnimatedTile] Folder not found: {folder}");
            return dict;
        }

        var guids = AssetDatabase.FindAssets("t:Sprite", new[] { folder });
        totalSprites = guids.Length;

        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
            if (!sprite) continue;

            string key = ExtractKey(sprite.name, frameNumber);
            if (key != null) dict[key] = sprite; // nếu trùng key, lấy cái cuối (ổn)
        }
        return dict;
    }

    // Lấy cụm số đầu tiên sau "TileFrameN_". Nếu không có, lấy nhóm số kế cuối; nếu vẫn không có thì lấy nhóm số cuối.
    string ExtractKey(string spriteName, int frameNumber)
    {
        // 1) Ưu tiên: bắt chữ số ngay SAU prefix "TileFrameN_"
        var prefix = "TileFrame" + frameNumber + "_";
        if (spriteName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
        {
            var rest = spriteName.Substring(prefix.Length);     // ví dụ "64_0" hoặc "01"
            var m = Regex.Match(rest, @"^(\d+)");
            if (m.Success) return m.Groups[1].Value;           // "64" hoặc "01"
        }

        // 2) Fallback thông minh: lấy nhóm số KẾ CUỐI nếu tồn tại (TileFrame1_64_0 -> "64")
        var ms = Regex.Matches(spriteName, @"\d+");
        if (ms.Count >= 2) return ms[ms.Count - 2].Value;
        if (ms.Count == 1) return ms[0].Value;

        return null;
    }

    void EnsureFolder(string path)
    {
        if (AssetDatabase.IsValidFolder(path)) return;
        var parent = Path.GetDirectoryName(path).Replace("\\", "/");
        var leaf = Path.GetFileName(path);
        if (!AssetDatabase.IsValidFolder(parent)) EnsureFolder(parent);
        AssetDatabase.CreateFolder(parent, leaf);
    }
}
#endif

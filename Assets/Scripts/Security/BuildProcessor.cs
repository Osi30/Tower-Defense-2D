using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Assets.Scripts.Security
{
    public class BuildProcessor : IPreprocessBuildWithReport, IPostprocessBuildWithReport
    {
        // Run Firstly
        public int callbackOrder { get { return 1; } }

        private const string API_FILE_PATH = "Assets/Scripts/Security/APICaller.cs";
        private const string PLACEHOLDER = "@@API_URL_PRODUCTION_@@";

        public void OnPreprocessBuild(BuildReport report)
        {
            Debug.Log("🔨 Bắt đầu thay thế URL API Production...");

            // 1. Lấy URL Production từ BuildConstants
            string productionUrl = BuildConstants.PRODUCTION_URL;

            // 2. Đọc nội dung file APICaller.cs
            string apiContent = File.ReadAllText(API_FILE_PATH);

            // 3. Thực hiện thay thế chuỗi Placeholder bằng URL thật
            if (apiContent.Contains(PLACEHOLDER))
            {
                // Thay thế tất cả các lần xuất hiện của placeholder
                string newApiContent = apiContent.Replace(PLACEHOLDER, productionUrl);

                // 4. Ghi nội dung đã thay thế trở lại vào file
                File.WriteAllText(API_FILE_PATH, newApiContent);

                // Bắt Unity refresh Asset Database để sử dụng code mới
                AssetDatabase.Refresh();

                Debug.Log("✅ Thay thế thành công. URL Production đã được nhúng.");
            }
            else
            {
                // Cảnh báo nếu không tìm thấy placeholder
                Debug.LogError("🔴 Không tìm thấy Placeholder trong file API. Đã bị thay thế trước đó?");
            }
        }

        public void OnPostprocessBuild(BuildReport report)
        {
            Debug.Log("🔄 Bắt đầu hoàn tác (Restore) Placeholder URL...");

            // 1. Đọc nội dung file đã được nhúng URL
            string apiContent = File.ReadAllText(API_FILE_PATH);

            // 2. Chuẩn bị chuỗi Placeholder cần Restore
            string productionUrl = BuildConstants.PRODUCTION_URL;

            // 3. Thực hiện hoàn tác (Thay URL thật bằng Placeholder)
            // Dùng Regex.Escape để đảm bảo các ký tự đặc biệt trong URL không gây lỗi
            string pattern = Regex.Escape(productionUrl);
            string newApiContent = Regex.Replace(apiContent, pattern, PLACEHOLDER);

            // 4. Ghi nội dung đã hoàn tác trở lại file
            File.WriteAllText(API_FILE_PATH, newApiContent);

            // Bắt Unity refresh
            AssetDatabase.Refresh();

            Debug.Log("✅ Hoàn tác thành công. File API đã trở lại trạng thái Placeholder.");
        }
    }
}

using Dapper;
using QuanLyDangVien.Helper;
using QuanLyDangVien.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace QuanLyDangVien.Services
{
    public class SystemConfigService
    {
        /// <summary>
        /// Lấy tất cả cấu hình
        /// </summary>
        public List<SystemConfig> GetAll()
        {
            using (var conn = DbHelper.GetConnection())
            {
                return conn.Query<SystemConfig>(
                    "SystemConfig_GetAll",
                    commandType: CommandType.StoredProcedure
                ).ToList();
            }
        }

        /// <summary>
        /// Lấy giá trị cấu hình theo key
        /// </summary>
        public string GetValue(string configKey)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ConfigKey", configKey);
                var config = conn.Query<SystemConfig>(
                    "SystemConfig_GetByKey",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();
                return config?.ConfigValue;
            }
        }

        /// <summary>
        /// Đặt giá trị cấu hình
        /// </summary>
        public bool SetValue(string configKey, string configValue, string description = null)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var user = AuthorizationHelper.GetCurrentUser();
                var parameters = new DynamicParameters();
                parameters.Add("@ConfigKey", configKey);
                parameters.Add("@ConfigValue", configValue);
                parameters.Add("@Description", description);
                parameters.Add("@NguoiCapNhat", user?.TenDangNhap ?? "System");

                int result = conn.Execute("SystemConfig_SetValue", parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        /// <summary>
        /// Lấy số ngày backup (mặc định 7)
        /// </summary>
        public int GetBackupIntervalDays()
        {
            string value = GetValue("BackupIntervalDays");
            if (int.TryParse(value, out int days))
                return days;
            return 7; // Mặc định 7 ngày
        }

        /// <summary>
        /// Đặt số ngày backup
        /// </summary>
        public void SetBackupIntervalDays(int days)
        {
            SetValue("BackupIntervalDays", days.ToString(), "Số ngày giữa các lần backup tự động");
        }
    }
}



using Dapper;
using QuanLyDangVien.Helper;
using QuanLyDangVien.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace QuanLyDangVien.Services
{
    public class AuditLogService
    {
        /// <summary>
        /// Ghi lại thao tác vào Audit Log
        /// </summary>
        public void LogAction(string action, string tableName, int? recordID = null, string oldValues = null, string newValues = null)
        {
            try
            {
                var user = AuthorizationHelper.GetCurrentUser();
                if (user == null)
                    return;

                using (var conn = DbHelper.GetConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@NguoiDungID", user.NguoiDungID);
                    parameters.Add("@TenDangNhap", user.TenDangNhap);
                    parameters.Add("@Action", action);
                    parameters.Add("@TableName", tableName);
                    parameters.Add("@RecordID", recordID);
                    parameters.Add("@OldValues", oldValues);
                    parameters.Add("@NewValues", newValues);
                    parameters.Add("@IPAddress", System.Net.IPAddress.Loopback.ToString());
                    parameters.Add("@UserAgent", "WinForms Application");

                    conn.Execute("AuditLog_Insert", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                // Log lỗi nhưng không throw để không ảnh hưởng đến flow chính
                System.Diagnostics.Debug.WriteLine($"Lỗi khi ghi Audit Log: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy tất cả Audit Log
        /// </summary>
        public List<AuditLog> GetAll(DateTime? tuNgay = null, DateTime? denNgay = null, string action = null, string tableName = null)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TuNgay", tuNgay);
                parameters.Add("@DenNgay", denNgay);
                parameters.Add("@Action", action);
                parameters.Add("@TableName", tableName);

                return conn.Query<AuditLog>(
                    "AuditLog_GetAll",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();
            }
        }

        /// <summary>
        /// Lấy Audit Log theo NguoiDungID
        /// </summary>
        public List<AuditLog> GetByNguoiDungID(int nguoiDungID, DateTime? tuNgay = null, DateTime? denNgay = null)
        {
            using (var conn = DbHelper.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@NguoiDungID", nguoiDungID);
                parameters.Add("@TuNgay", tuNgay);
                parameters.Add("@DenNgay", denNgay);

                return conn.Query<AuditLog>(
                    "AuditLog_GetByNguoiDungID",
                    parameters,
                    commandType: CommandType.StoredProcedure
                ).ToList();
            }
        }
    }
}



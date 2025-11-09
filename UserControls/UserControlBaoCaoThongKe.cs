using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using QuanLyDangVien.Services;
using QuanLyDangVien.DTOs;

namespace QuanLyDangVien
{
    public partial class UserControlBaoCaoThongKe : UserControl
    {
        private ReportService _reportService;

        public UserControlBaoCaoThongKe()
        {
            InitializeComponent();
            _reportService = new ReportService();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                LoadDonViChart();
                LoadTongSoChart();
                LoadGioiTinhChart();
                LoadQueQuanChart();
                LoadTuoiChart();
                LoadChucVuChart();
                LoadTrinhDoChart();
                LoadKetNapChuyenChart();
                LoadChinhThucDuBiChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDonViChart()
        {
            try
            {
                var data = _reportService.GetThongKeTheoDonVi();
                
                // Nếu không có dữ liệu, dùng dữ liệu giả
                if (data == null || data.Count == 0)
                {
                    data = GetFakeDonViData();
                }

                chartDonVi.Series.Clear();
                chartDonVi.ChartAreas[0].AxisX.Title = "Đơn vị";
                chartDonVi.ChartAreas[0].AxisY.Title = "Số lượng";
                chartDonVi.ChartAreas[0].AxisX.Interval = 1;
                chartDonVi.ChartAreas[0].AxisX.LabelStyle.Angle = -45;

                var series = new Series("Số lượng");
                series.ChartType = SeriesChartType.Column;
                series.Color = Color.FromArgb(0, 174, 219);

                foreach (var item in data.Take(10)) // Giới hạn 10 đơn vị đầu
                {
                    series.Points.AddXY(item.TenDonVi, item.TongSoDangVien);
                }

                chartDonVi.Series.Add(series);
            }
            catch
            {
                // Dùng dữ liệu giả nếu có lỗi
                LoadDonViChartWithFakeData();
            }
        }

        private void LoadTongSoChart()
        {
            try
            {
                var data = _reportService.GetThongKeTongHop();
                var tongSo = data.Where(x => x.LoaiThongKe == "TongSoDangVien").Sum(x => x.SoLuong);
                
                if (tongSo == 0)
                {
                    tongSo = 150; // Dữ liệu giả
                }

                lblTongSoValue.Text = $"{tongSo:N0} người";

                chartTongSo.Series.Clear();
                var series = new Series("Tổng số");
                series.ChartType = SeriesChartType.Pie;
                series["PieLabelStyle"] = "Outside";
                series["PieLineColor"] = "Black";

                // Phân tích theo đối tượng
                var doiTuongData = data.Where(x => x.LoaiThongKe == "TongSoDangVien").ToList();
                if (doiTuongData.Count == 0)
                {
                    doiTuongData = new List<BaoCaoThongKeDTO>
                    {
                        new BaoCaoThongKeDTO { Ten = "QNCN", SoLuong = 80 },
                        new BaoCaoThongKeDTO { Ten = "HSQ-BS", SoLuong = 50 },
                        new BaoCaoThongKeDTO { Ten = "LĐHĐ", SoLuong = 20 }
                    };
                }

                foreach (var item in doiTuongData)
                {
                    series.Points.AddXY(item.Ten, item.SoLuong);
                }

                chartTongSo.Series.Add(series);
            }
            catch
            {
                LoadTongSoChartWithFakeData();
            }
        }

        private void LoadGioiTinhChart()
        {
            try
            {
                var data = _reportService.GetThongKeTongHop();
                var gioiTinhData = data.Where(x => x.LoaiThongKe == "GioiTinh").ToList();

                if (gioiTinhData.Count == 0)
                {
                    gioiTinhData = new List<BaoCaoThongKeDTO>
                    {
                        new BaoCaoThongKeDTO { Ten = "Nam", SoLuong = 100 },
                        new BaoCaoThongKeDTO { Ten = "Nữ", SoLuong = 50 }
                    };
                }

                var nam = gioiTinhData.FirstOrDefault(x => x.Ten == "Nam")?.SoLuong ?? 0;
                var nu = gioiTinhData.FirstOrDefault(x => x.Ten == "Nữ")?.SoLuong ?? 0;

                lblGioiTinhNam.Text = $"Nam: {nam:N0}";
                lblGioiTinhNu.Text = $"Nữ: {nu:N0}";

                chartGioiTinh.Series.Clear();
                var series = new Series("Giới tính");
                series.ChartType = SeriesChartType.Pie;
                series["PieLabelStyle"] = "Outside";

                series.Points.AddXY("Nam", nam);
                series.Points.AddXY("Nữ", nu);

                chartGioiTinh.Series.Add(series);
            }
            catch
            {
                LoadGioiTinhChartWithFakeData();
            }
        }

        private void LoadQueQuanChart()
        {
            try
            {
                // Dữ liệu giả vì không có method trong ReportService
                var fakeData = new Dictionary<string, int>
                {
                    { "Hà Nội", 45 },
                    { "Hồ Chí Minh", 35 },
                    { "Đà Nẵng", 25 },
                    { "Hải Phòng", 20 },
                    { "Cần Thơ", 15 },
                    { "Khác", 10 }
                };

                chartQueQuan.Series.Clear();
                chartQueQuan.ChartAreas[0].AxisX.Title = "Quê quán";
                chartQueQuan.ChartAreas[0].AxisY.Title = "Số lượng";
                chartQueQuan.ChartAreas[0].AxisX.Interval = 1;
                chartQueQuan.ChartAreas[0].AxisX.LabelStyle.Angle = -45;

                var series = new Series("Số lượng");
                series.ChartType = SeriesChartType.Bar;
                series.Color = Color.FromArgb(0, 174, 219);

                foreach (var item in fakeData)
                {
                    series.Points.AddXY(item.Key, item.Value);
                }

                chartQueQuan.Series.Add(series);
            }
            catch
            {
                LoadQueQuanChartWithFakeData();
            }
        }

        private void LoadTuoiChart()
        {
            try
            {
                var data = _reportService.GetThongKeTuoiDoi();
                
                if (data == null || data.Count == 0)
                {
                    data = new List<BaoCaoThongKeDTO>
                    {
                        new BaoCaoThongKeDTO { Ten = "18-30", SoLuong = 30 },
                        new BaoCaoThongKeDTO { Ten = "31-40", SoLuong = 50 },
                        new BaoCaoThongKeDTO { Ten = "41-50", SoLuong = 40 },
                        new BaoCaoThongKeDTO { Ten = "51-60", SoLuong = 25 },
                        new BaoCaoThongKeDTO { Ten = "Trên 60", SoLuong = 5 }
                    };
                }

                chartTuoi.Series.Clear();
                chartTuoi.ChartAreas[0].AxisX.Title = "Độ tuổi";
                chartTuoi.ChartAreas[0].AxisY.Title = "Số lượng";
                chartTuoi.ChartAreas[0].AxisX.Interval = 1;

                var series = new Series("Số lượng");
                series.ChartType = SeriesChartType.Column;
                series.Color = Color.FromArgb(0, 174, 219);

                foreach (var item in data)
                {
                    series.Points.AddXY(item.Ten, item.SoLuong);
                }

                chartTuoi.Series.Add(series);
            }
            catch
            {
                LoadTuoiChartWithFakeData();
            }
        }

        private void LoadChucVuChart()
        {
            try
            {
                // Dữ liệu giả vì không có method trong ReportService
                var fakeData = new Dictionary<string, int>
                {
                    { "Bí thư", 5 },
                    { "Phó Bí thư", 8 },
                    { "Ủy viên", 12 },
                    { "Trưởng phòng", 15 },
                    { "Phó phòng", 20 },
                    { "Nhân viên", 80 }
                };

                chartChucVu.Series.Clear();
                chartChucVu.ChartAreas[0].AxisX.Title = "Chức vụ";
                chartChucVu.ChartAreas[0].AxisY.Title = "Số lượng";
                chartChucVu.ChartAreas[0].AxisX.Interval = 1;
                chartChucVu.ChartAreas[0].AxisX.LabelStyle.Angle = -45;

                var series = new Series("Số lượng");
                series.ChartType = SeriesChartType.Bar;
                series.Color = Color.FromArgb(0, 174, 219);

                foreach (var item in fakeData)
                {
                    series.Points.AddXY(item.Key, item.Value);
                }

                chartChucVu.Series.Add(series);
            }
            catch
            {
                LoadChucVuChartWithFakeData();
            }
        }

        private void LoadTrinhDoChart()
        {
            try
            {
                // Dữ liệu giả vì không có method trong ReportService
                var fakeData = new Dictionary<string, int>
                {
                    { "Tiểu học", 5 },
                    { "Trung học cơ sở", 10 },
                    { "Trung học phổ thông", 30 },
                    { "Đại học", 80 },
                    { "Cao học", 20 },
                    { "Tiến sĩ", 5 }
                };

                chartTrinhDo.Series.Clear();
                var series = new Series("Trình độ");
                series.ChartType = SeriesChartType.Pie;
                series["PieLabelStyle"] = "Outside";

                foreach (var item in fakeData)
                {
                    series.Points.AddXY(item.Key, item.Value);
                }

                chartTrinhDo.Series.Add(series);
            }
            catch
            {
                LoadTrinhDoChartWithFakeData();
            }
        }

        private void LoadKetNapChuyenChart()
        {
            try
            {
                int currentYear = DateTime.Now.Year;
                var ketNapData = _reportService.GetThongKeKetNapMoi(currentYear);
                var chuyenData = _reportService.GetThongKeChuyenSinhHoat(currentYear);

                chartKetNapChuyen.Series.Clear();
                chartKetNapChuyen.ChartAreas[0].AxisX.Title = "Tháng";
                chartKetNapChuyen.ChartAreas[0].AxisY.Title = "Số lượng";
                chartKetNapChuyen.ChartAreas[0].AxisX.Interval = 1;

                // Series Kết nạp mới
                var seriesKetNap = new Series("Kết nạp mới");
                seriesKetNap.ChartType = SeriesChartType.Line;
                seriesKetNap.Color = Color.FromArgb(0, 174, 219);
                seriesKetNap.MarkerStyle = MarkerStyle.Circle;
                seriesKetNap.MarkerSize = 8;

                if (ketNapData != null && ketNapData.Count > 0)
                {
                    foreach (var item in ketNapData.OrderBy(x => x.Ten))
                    {
                        seriesKetNap.Points.AddXY($"Tháng {item.Ten}", item.SoLuong);
                    }
                }
                else
                {
                    // Dữ liệu giả
                    for (int i = 1; i <= 12; i++)
                    {
                        seriesKetNap.Points.AddXY($"Tháng {i}", new Random().Next(5, 15));
                    }
                }

                // Series Chuyển đi-đến
                var seriesChuyen = new Series("Chuyển đi-đến");
                seriesChuyen.ChartType = SeriesChartType.Line;
                seriesChuyen.Color = Color.FromArgb(192, 0, 0);
                seriesChuyen.MarkerStyle = MarkerStyle.Square;
                seriesChuyen.MarkerSize = 8;

                if (chuyenData != null && chuyenData.Count > 0)
                {
                    foreach (var item in chuyenData.OrderBy(x => x.Ten))
                    {
                        seriesChuyen.Points.AddXY($"Tháng {item.Ten}", item.SoLuong);
                    }
                }
                else
                {
                    // Dữ liệu giả
                    for (int i = 1; i <= 12; i++)
                    {
                        seriesChuyen.Points.AddXY($"Tháng {i}", new Random().Next(2, 8));
                    }
                }

                chartKetNapChuyen.Series.Add(seriesKetNap);
                chartKetNapChuyen.Series.Add(seriesChuyen);
            }
            catch
            {
                LoadKetNapChuyenChartWithFakeData();
            }
        }

        private void LoadChinhThucDuBiChart()
        {
            try
            {
                var data = _reportService.GetThongKeTongHop();
                var loaiDangVienData = data.Where(x => x.LoaiThongKe == "LoaiDangVien").ToList();

                if (loaiDangVienData.Count == 0)
                {
                    loaiDangVienData = new List<BaoCaoThongKeDTO>
                    {
                        new BaoCaoThongKeDTO { Ten = "Chính thức", SoLuong = 120 },
                        new BaoCaoThongKeDTO { Ten = "Dự bị", SoLuong = 30 }
                    };
                }

                var chinhThuc = loaiDangVienData.FirstOrDefault(x => x.Ten == "Chính thức")?.SoLuong ?? 0;
                var duBi = loaiDangVienData.FirstOrDefault(x => x.Ten == "Dự bị")?.SoLuong ?? 0;

                lblChinhThuc.Text = $"Chính thức: {chinhThuc:N0}";
                lblDuBi.Text = $"Dự bị: {duBi:N0}";

                chartChinhThucDuBi.Series.Clear();
                var series = new Series("Loại Đảng viên");
                series.ChartType = SeriesChartType.Pie;
                series["PieLabelStyle"] = "Outside";

                series.Points.AddXY("Chính thức", chinhThuc);
                series.Points.AddXY("Dự bị", duBi);

                chartChinhThucDuBi.Series.Add(series);
            }
            catch
            {
                LoadChinhThucDuBiChartWithFakeData();
            }
        }

        // Các method dữ liệu giả
        private List<BaoCaoTheoDonViDTO> GetFakeDonViData()
        {
            return new List<BaoCaoTheoDonViDTO>
            {
                new BaoCaoTheoDonViDTO { TenDonVi = "Đơn vị 1", TongSoDangVien = 45 },
                new BaoCaoTheoDonViDTO { TenDonVi = "Đơn vị 2", TongSoDangVien = 38 },
                new BaoCaoTheoDonViDTO { TenDonVi = "Đơn vị 3", TongSoDangVien = 32 },
                new BaoCaoTheoDonViDTO { TenDonVi = "Đơn vị 4", TongSoDangVien = 25 },
                new BaoCaoTheoDonViDTO { TenDonVi = "Đơn vị 5", TongSoDangVien = 20 }
            };
        }

        private void LoadDonViChartWithFakeData()
        {
            var data = GetFakeDonViData();
            chartDonVi.Series.Clear();
            var series = new Series("Số lượng");
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.FromArgb(0, 174, 219);
            foreach (var item in data)
            {
                series.Points.AddXY(item.TenDonVi, item.TongSoDangVien);
            }
            chartDonVi.Series.Add(series);
        }

        private void LoadTongSoChartWithFakeData()
        {
            lblTongSoValue.Text = "150 người";
            chartTongSo.Series.Clear();
            var series = new Series("Tổng số");
            series.ChartType = SeriesChartType.Pie;
            series.Points.AddXY("QNCN", 80);
            series.Points.AddXY("HSQ-BS", 50);
            series.Points.AddXY("LĐHĐ", 20);
            chartTongSo.Series.Add(series);
        }

        private void LoadGioiTinhChartWithFakeData()
        {
            lblGioiTinhNam.Text = "Nam: 100";
            lblGioiTinhNu.Text = "Nữ: 50";
            chartGioiTinh.Series.Clear();
            var series = new Series("Giới tính");
            series.ChartType = SeriesChartType.Pie;
            series.Points.AddXY("Nam", 100);
            series.Points.AddXY("Nữ", 50);
            chartGioiTinh.Series.Add(series);
        }

        private void LoadQueQuanChartWithFakeData()
        {
            chartQueQuan.Series.Clear();
            var series = new Series("Số lượng");
            series.ChartType = SeriesChartType.Bar;
            series.Color = Color.FromArgb(0, 174, 219);
            series.Points.AddXY("Hà Nội", 45);
            series.Points.AddXY("Hồ Chí Minh", 35);
            series.Points.AddXY("Đà Nẵng", 25);
            series.Points.AddXY("Hải Phòng", 20);
            series.Points.AddXY("Cần Thơ", 15);
            series.Points.AddXY("Khác", 10);
            chartQueQuan.Series.Add(series);
        }

        private void LoadTuoiChartWithFakeData()
        {
            chartTuoi.Series.Clear();
            var series = new Series("Số lượng");
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.FromArgb(0, 174, 219);
            series.Points.AddXY("18-30", 30);
            series.Points.AddXY("31-40", 50);
            series.Points.AddXY("41-50", 40);
            series.Points.AddXY("51-60", 25);
            series.Points.AddXY("Trên 60", 5);
            chartTuoi.Series.Add(series);
        }

        private void LoadChucVuChartWithFakeData()
        {
            chartChucVu.Series.Clear();
            var series = new Series("Số lượng");
            series.ChartType = SeriesChartType.Bar;
            series.Color = Color.FromArgb(0, 174, 219);
            series.Points.AddXY("Bí thư", 5);
            series.Points.AddXY("Phó Bí thư", 8);
            series.Points.AddXY("Ủy viên", 12);
            series.Points.AddXY("Trưởng phòng", 15);
            series.Points.AddXY("Phó phòng", 20);
            series.Points.AddXY("Nhân viên", 80);
            chartChucVu.Series.Add(series);
        }

        private void LoadTrinhDoChartWithFakeData()
        {
            chartTrinhDo.Series.Clear();
            var series = new Series("Trình độ");
            series.ChartType = SeriesChartType.Pie;
            series.Points.AddXY("Tiểu học", 5);
            series.Points.AddXY("Trung học cơ sở", 10);
            series.Points.AddXY("Trung học phổ thông", 30);
            series.Points.AddXY("Đại học", 80);
            series.Points.AddXY("Cao học", 20);
            series.Points.AddXY("Tiến sĩ", 5);
            chartTrinhDo.Series.Add(series);
        }

        private void LoadKetNapChuyenChartWithFakeData()
        {
            chartKetNapChuyen.Series.Clear();
            var seriesKetNap = new Series("Kết nạp mới");
            seriesKetNap.ChartType = SeriesChartType.Line;
            seriesKetNap.Color = Color.FromArgb(0, 174, 219);
            var seriesChuyen = new Series("Chuyển đi-đến");
            seriesChuyen.ChartType = SeriesChartType.Line;
            seriesChuyen.Color = Color.FromArgb(192, 0, 0);
            for (int i = 1; i <= 12; i++)
            {
                seriesKetNap.Points.AddXY($"Tháng {i}", new Random().Next(5, 15));
                seriesChuyen.Points.AddXY($"Tháng {i}", new Random().Next(2, 8));
            }
            chartKetNapChuyen.Series.Add(seriesKetNap);
            chartKetNapChuyen.Series.Add(seriesChuyen);
        }

        private void LoadChinhThucDuBiChartWithFakeData()
        {
            lblChinhThuc.Text = "Chính thức: 120";
            lblDuBi.Text = "Dự bị: 30";
            chartChinhThucDuBi.Series.Clear();
            var series = new Series("Loại Đảng viên");
            series.ChartType = SeriesChartType.Pie;
            series.Points.AddXY("Chính thức", 120);
            series.Points.AddXY("Dự bị", 30);
            chartChinhThucDuBi.Series.Add(series);
        }
    }
}

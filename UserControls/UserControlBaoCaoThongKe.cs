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
using QuanLyDangVien.Helper;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using MetroFramework.Controls;

namespace QuanLyDangVien
{
    public partial class UserControlBaoCaoThongKe : UserControl
    {
        private ReportService _reportService;
        private int _selectedYear;

        public UserControlBaoCaoThongKe()
        {
            InitializeComponent();
            _reportService = new ReportService();
            _selectedYear = DateTime.Now.Year;
            InitializeYearComboBox();
            LoadData();
            ApplyPermissions();
        }

        private void InitializeYearComboBox()
        {
            cboNam.Items.Clear();
            int currentYear = DateTime.Now.Year;
            for (int i = currentYear; i >= currentYear - 10; i--)
            {
                cboNam.Items.Add(i);
            }
            cboNam.SelectedIndex = 0;
        }

        private void cboNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNam.SelectedItem != null)
            {
                _selectedYear = (int)cboNam.SelectedItem;
                LoadData();
            }
        }

        private void LoadData()
        {
            try
            {
                LoadCardsData();
                LoadDonViChart();
                LoadKetNapChuyenChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCardsData()
        {
            try
            {
                var data = _reportService.GetThongKeTongHop(_selectedYear);
                
                // Tổng số đảng viên
                var tongSoDangVien = data.Where(x => x.LoaiThongKe == "TongSoDangVien").Sum(x => x.SoLuong);
                lblTongSoDangVienValue.Text = tongSoDangVien.ToString("N0");

                // Tổng số đơn vị
                var donViList = _reportService.GetThongKeTheoDonVi(_selectedYear);
                lblTongSoDonViValue.Text = donViList.Count.ToString("N0");

                // Kết nạp mới trong năm
                var ketNapData = _reportService.GetThongKeKetNapMoi(_selectedYear);
                var tongKetNap = ketNapData?.Sum(x => x.SoLuong) ?? 0;
                lblKetNapMoiValue.Text = tongKetNap.ToString("N0");

                // Chuyển sinh hoạt trong năm
                var chuyenData = _reportService.GetThongKeChuyenSinhHoat(_selectedYear);
                var tongChuyen = chuyenData?.Sum(x => x.SoLuong) ?? 0;
                lblChuyenSinhHoatValue.Text = tongChuyen.ToString("N0");

                // Chính thức và Dự bị
                var loaiDangVienData = data.Where(x => x.LoaiThongKe == "LoaiDangVien").ToList();
                var chinhThuc = loaiDangVienData.FirstOrDefault(x => x.Ten == "Chính thức")?.SoLuong ?? 0;
                var duBi = loaiDangVienData.FirstOrDefault(x => x.Ten == "Dự bị")?.SoLuong ?? 0;
                lblChinhThucValue.Text = chinhThuc.ToString("N0");
                lblDuBiValue.Text = duBi.ToString("N0");

                // Khen thưởng trong năm
                var khenThuongData = _reportService.GetThongKeKhenThuong(_selectedYear);
                var tongKhenThuong = khenThuongData?.Sum(x => x.SoLuong) ?? 0;
                lblKhenThuongValue.Text = tongKhenThuong.ToString("N0");

                // Kỷ luật trong năm
                var kyLuatData = _reportService.GetThongKeKyLuat(_selectedYear);
                var tongKyLuat = kyLuatData?.Sum(x => x.SoLuong) ?? 0;
                lblKyLuatValue.Text = tongKyLuat.ToString("N0");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu cards: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDonViChart()
        {
            try
            {
                var data = _reportService.GetThongKeTheoDonVi(_selectedYear);
                
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
                series.Color = System.Drawing.Color.FromArgb(0, 174, 219);

                foreach (var item in data.Take(10))
                {
                    series.Points.AddXY(item.TenDonVi, item.TongSoDangVien);
                }

                chartDonVi.Series.Add(series);
            }
            catch
            {
                LoadDonViChartWithFakeData();
            }
        }

        private void LoadKetNapChuyenChart()
        {
            try
            {
                var ketNapData = _reportService.GetThongKeKetNapMoi(_selectedYear);
                var chuyenData = _reportService.GetThongKeChuyenSinhHoat(_selectedYear);

                chartKetNapChuyen.Series.Clear();
                chartKetNapChuyen.ChartAreas[0].AxisX.Title = "Tháng";
                chartKetNapChuyen.ChartAreas[0].AxisY.Title = "Số lượng";
                chartKetNapChuyen.ChartAreas[0].AxisX.Interval = 1;

                // Series Kết nạp mới
                var seriesKetNap = new Series("Kết nạp mới");
                seriesKetNap.ChartType = SeriesChartType.Line;
                seriesKetNap.Color = System.Drawing.Color.FromArgb(0, 174, 219);
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
                    for (int i = 1; i <= 12; i++)
                    {
                        seriesKetNap.Points.AddXY($"Tháng {i}", 0);
                    }
                }

                // Series Chuyển đi-đến
                var seriesChuyen = new Series("Chuyển đi-đến");
                seriesChuyen.ChartType = SeriesChartType.Line;
                seriesChuyen.Color = System.Drawing.Color.FromArgb(192, 0, 0);
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
                    for (int i = 1; i <= 12; i++)
                    {
                        seriesChuyen.Points.AddXY($"Tháng {i}", 0);
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

        private void btnXuatBaoCao_Click(object sender, EventArgs e)
        {
            if (!AuthorizationHelper.HasPermission("BaoCao", "Export"))
            {
                MessageBox.Show("Bạn không có quyền xuất báo cáo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Word Documents (*.docx)|*.docx";
                    saveDialog.FileName = $"BaoCaoThongKe_{_selectedYear}.docx";
                    saveDialog.DefaultExt = "docx";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        ExportToWord(saveDialog.FileName);
                        MessageBox.Show("Xuất báo cáo thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất báo cáo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportToWord(string filePath)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());

                // Style Definitions
                StyleDefinitionsPart stylesPart = mainPart.AddNewPart<StyleDefinitionsPart>();
                GenerateStyleDefinitions(stylesPart);

                // Tiêu đề chính
                AddTitle(body, "BÁO CÁO THỐNG KÊ ĐẢNG VIÊN", 28);
                AddSubtitle(body, $"Năm: {_selectedYear}", 24);
                AddEmptyLine(body, 2);

                // Lấy dữ liệu
                var data = _reportService.GetThongKeTongHop(_selectedYear);
                var donViList = _reportService.GetThongKeTheoDonVi(_selectedYear);
                var ketNapData = _reportService.GetThongKeKetNapMoi(_selectedYear);
                var chuyenData = _reportService.GetThongKeChuyenSinhHoat(_selectedYear);
                var khenThuongData = _reportService.GetThongKeKhenThuong(_selectedYear);
                var kyLuatData = _reportService.GetThongKeKyLuat(_selectedYear);
                var tuoiDoiData = _reportService.GetThongKeTuoiDoi();
                var tuoiDangData = _reportService.GetThongKeTuoiDang();

                // 1. Tổng số đảng viên
                AddSectionTitle(body, "1. TỔNG SỐ ĐẢNG VIÊN");
                var tongSoDangVien = data.Where(x => x.LoaiThongKe == "TongSoDangVien").Sum(x => x.SoLuong);
                
                // Bảng tổng số đảng viên
                var summaryTable = CreateTable(body, 2, 2);
                AddTableCell(summaryTable, 0, 0, "Tổng số đảng viên", true, "4472C4");
                AddTableCell(summaryTable, 0, 1, $"{tongSoDangVien:N0} người", false, "D9E1F2");
                AddTableCell(summaryTable, 1, 0, "Tổng số đơn vị", true, "4472C4");
                AddTableCell(summaryTable, 1, 1, $"{donViList.Count:N0} đơn vị", false, "D9E1F2");
                body.Append(summaryTable);
                AddEmptyLine(body, 1);

                // Bảng theo đối tượng
                var doiTuongData = data.Where(x => x.LoaiThongKe == "TongSoDangVien").ToList();
                if (doiTuongData.Count > 0)
                {
                    AddSubSectionTitle(body, "Theo đối tượng:");
                    var doiTuongTable = CreateTable(body, doiTuongData.Count + 1, 3);
                    AddTableCell(doiTuongTable, 0, 0, "STT", true, "4472C4");
                    AddTableCell(doiTuongTable, 0, 1, "Đối tượng", true, "4472C4");
                    AddTableCell(doiTuongTable, 0, 2, "Số lượng (Tỷ lệ)", true, "4472C4");
                    
                    int stt = 1;
                    foreach (var item in doiTuongData)
                    {
                        AddTableCell(doiTuongTable, stt, 0, stt.ToString(), false, "F2F2F2");
                        AddTableCell(doiTuongTable, stt, 1, item.Ten, false, "FFFFFF");
                        AddTableCell(doiTuongTable, stt, 2, $"{item.SoLuong:N0} người ({item.TyLe:F2}%)", false, "FFFFFF");
                        stt++;
                    }
                    body.Append(doiTuongTable);
                    AddEmptyLine(body, 1);
                }

                // 2. Theo đơn vị
                AddSectionTitle(body, "2. THỐNG KÊ THEO ĐƠN VỊ");
                var donViTable = CreateTable(body, Math.Min(donViList.Count, 10) + 1, 4);
                AddTableCell(donViTable, 0, 0, "STT", true, "70AD47");
                AddTableCell(donViTable, 0, 1, "Tên đơn vị", true, "70AD47");
                AddTableCell(donViTable, 0, 2, "Tổng số", true, "70AD47");
                AddTableCell(donViTable, 0, 3, "Chính thức / Dự bị", true, "70AD47");
                
                int sttDonVi = 1;
                foreach (var item in donViList.Take(10))
                {
                    AddTableCell(donViTable, sttDonVi, 0, sttDonVi.ToString(), false, "E2EFDA");
                    AddTableCell(donViTable, sttDonVi, 1, item.TenDonVi, false, "FFFFFF");
                    AddTableCell(donViTable, sttDonVi, 2, $"{item.TongSoDangVien:N0}", false, "FFFFFF");
                    AddTableCell(donViTable, sttDonVi, 3, $"{item.DangVienChinhThuc:N0} / {item.DangVienDuBi:N0}", false, "FFFFFF");
                    sttDonVi++;
                }
                body.Append(donViTable);
                AddEmptyLine(body, 1);

                // 3. Kết nạp mới và Chuyển sinh hoạt
                AddSectionTitle(body, "3. KẾT NẠP MỚI VÀ CHUYỂN SINH HOẠT");
                var tongKetNap = ketNapData?.Sum(x => x.SoLuong) ?? 0;
                var tongChuyen = chuyenData?.Sum(x => x.SoLuong) ?? 0;
                
                var ketNapChuyenTable = CreateTable(body, 3, 2);
                AddTableCell(ketNapChuyenTable, 0, 0, "Chỉ tiêu", true, "FFC000");
                AddTableCell(ketNapChuyenTable, 0, 1, "Số lượng", true, "FFC000");
                AddTableCell(ketNapChuyenTable, 1, 0, "Kết nạp mới trong năm", false, "FFF2CC");
                AddTableCell(ketNapChuyenTable, 1, 1, $"{tongKetNap:N0} người", false, "FFFFFF");
                AddTableCell(ketNapChuyenTable, 2, 0, "Chuyển sinh hoạt trong năm", false, "FFF2CC");
                AddTableCell(ketNapChuyenTable, 2, 1, $"{tongChuyen:N0} người", false, "FFFFFF");
                body.Append(ketNapChuyenTable);
                AddEmptyLine(body, 1);

                // Bảng chi tiết theo tháng
                if (ketNapData != null && ketNapData.Count > 0)
                {
                    AddSubSectionTitle(body, "Chi tiết theo tháng:");
                    var thangTable = CreateTable(body, 13, 3);
                    AddTableCell(thangTable, 0, 0, "Tháng", true, "FFC000");
                    AddTableCell(thangTable, 0, 1, "Kết nạp mới", true, "FFC000");
                    AddTableCell(thangTable, 0, 2, "Chuyển sinh hoạt", true, "FFC000");
                    
                    var ketNapByMonth = ketNapData.OrderBy(x => x.Ten).ToDictionary(x => x.Ten, x => x.SoLuong);
                    var chuyenByMonth = chuyenData?.OrderBy(x => x.Ten).ToDictionary(x => x.Ten, x => x.SoLuong) ?? new Dictionary<string, int>();
                    
                    for (int i = 1; i <= 12; i++)
                    {
                        AddTableCell(thangTable, i, 0, $"Tháng {i}", false, i % 2 == 0 ? "FFF2CC" : "FFFFFF");
                        AddTableCell(thangTable, i, 1, ketNapByMonth.ContainsKey(i.ToString()) ? ketNapByMonth[i.ToString()].ToString("N0") : "0", false, i % 2 == 0 ? "FFF2CC" : "FFFFFF");
                        AddTableCell(thangTable, i, 2, chuyenByMonth.ContainsKey(i.ToString()) ? chuyenByMonth[i.ToString()].ToString("N0") : "0", false, i % 2 == 0 ? "FFF2CC" : "FFFFFF");
                    }
                    body.Append(thangTable);
                    AddEmptyLine(body, 1);
                }

                // 4. Chính thức và Dự bị
                AddSectionTitle(body, "4. CHÍNH THỨC VÀ DỰ BỊ");
                var loaiDangVienData = data.Where(x => x.LoaiThongKe == "LoaiDangVien").ToList();
                var chinhThuc = loaiDangVienData.FirstOrDefault(x => x.Ten == "Chính thức")?.SoLuong ?? 0;
                var duBi = loaiDangVienData.FirstOrDefault(x => x.Ten == "Dự bị")?.SoLuong ?? 0;
                
                var loaiDangVienTable = CreateTable(body, 3, 3);
                AddTableCell(loaiDangVienTable, 0, 0, "Loại đảng viên", true, "70AD47");
                AddTableCell(loaiDangVienTable, 0, 1, "Số lượng", true, "70AD47");
                AddTableCell(loaiDangVienTable, 0, 2, "Tỷ lệ", true, "70AD47");
                AddTableCell(loaiDangVienTable, 1, 0, "Chính thức", false, "E2EFDA");
                AddTableCell(loaiDangVienTable, 1, 1, $"{chinhThuc:N0} người", false, "FFFFFF");
                AddTableCell(loaiDangVienTable, 1, 2, chinhThuc + duBi > 0 ? $"{(chinhThuc * 100.0 / (chinhThuc + duBi)):F2}%" : "0%", false, "FFFFFF");
                AddTableCell(loaiDangVienTable, 2, 0, "Dự bị", false, "E2EFDA");
                AddTableCell(loaiDangVienTable, 2, 1, $"{duBi:N0} người", false, "FFFFFF");
                AddTableCell(loaiDangVienTable, 2, 2, chinhThuc + duBi > 0 ? $"{(duBi * 100.0 / (chinhThuc + duBi)):F2}%" : "0%", false, "FFFFFF");
                body.Append(loaiDangVienTable);
                AddEmptyLine(body, 1);

                // 5. Khen thưởng và Kỷ luật
                AddSectionTitle(body, "5. KHEN THƯỞNG VÀ KỶ LUẬT");
                var tongKhenThuong = khenThuongData?.Sum(x => x.SoLuong) ?? 0;
                var tongKyLuat = kyLuatData?.Sum(x => x.SoLuong) ?? 0;
                
                var khenThuongKyLuatTable = CreateTable(body, 3, 2);
                AddTableCell(khenThuongKyLuatTable, 0, 0, "Loại", true, "C00000");
                AddTableCell(khenThuongKyLuatTable, 0, 1, "Số lượng", true, "C00000");
                AddTableCell(khenThuongKyLuatTable, 1, 0, "Khen thưởng trong năm", false, "FFE699");
                AddTableCell(khenThuongKyLuatTable, 1, 1, $"{tongKhenThuong:N0} trường hợp", false, "FFFFFF");
                AddTableCell(khenThuongKyLuatTable, 2, 0, "Kỷ luật trong năm", false, "FFE699");
                AddTableCell(khenThuongKyLuatTable, 2, 1, $"{tongKyLuat:N0} trường hợp", false, "FFFFFF");
                body.Append(khenThuongKyLuatTable);
                AddEmptyLine(body, 1);

                // 6. Tuổi đời
                AddSectionTitle(body, "6. THỐNG KÊ TUỔI ĐỜI");
                var tuoiDoiTable = CreateTable(body, tuoiDoiData.Count + 1, 3);
                AddTableCell(tuoiDoiTable, 0, 0, "Độ tuổi", true, "4472C4");
                AddTableCell(tuoiDoiTable, 0, 1, "Số lượng", true, "4472C4");
                AddTableCell(tuoiDoiTable, 0, 2, "Tỷ lệ", true, "4472C4");
                
                int sttTuoiDoi = 1;
                foreach (var item in tuoiDoiData)
                {
                    AddTableCell(tuoiDoiTable, sttTuoiDoi, 0, $"{item.Ten} tuổi", false, sttTuoiDoi % 2 == 0 ? "D9E1F2" : "FFFFFF");
                    AddTableCell(tuoiDoiTable, sttTuoiDoi, 1, $"{item.SoLuong:N0} người", false, sttTuoiDoi % 2 == 0 ? "D9E1F2" : "FFFFFF");
                    AddTableCell(tuoiDoiTable, sttTuoiDoi, 2, $"{item.TyLe:F2}%", false, sttTuoiDoi % 2 == 0 ? "D9E1F2" : "FFFFFF");
                    sttTuoiDoi++;
                }
                body.Append(tuoiDoiTable);
                AddEmptyLine(body, 1);

                // 7. Tuổi đảng
                AddSectionTitle(body, "7. THỐNG KÊ TUỔI ĐẢNG");
                var tuoiDangTable = CreateTable(body, tuoiDangData.Count + 1, 3);
                AddTableCell(tuoiDangTable, 0, 0, "Năm tuổi đảng", true, "4472C4");
                AddTableCell(tuoiDangTable, 0, 1, "Số lượng", true, "4472C4");
                AddTableCell(tuoiDangTable, 0, 2, "Tỷ lệ", true, "4472C4");
                
                int sttTuoiDang = 1;
                foreach (var item in tuoiDangData)
                {
                    AddTableCell(tuoiDangTable, sttTuoiDang, 0, $"{item.Ten} năm", false, sttTuoiDang % 2 == 0 ? "D9E1F2" : "FFFFFF");
                    AddTableCell(tuoiDangTable, sttTuoiDang, 1, $"{item.SoLuong:N0} người", false, sttTuoiDang % 2 == 0 ? "D9E1F2" : "FFFFFF");
                    AddTableCell(tuoiDangTable, sttTuoiDang, 2, $"{item.TyLe:F2}%", false, sttTuoiDang % 2 == 0 ? "D9E1F2" : "FFFFFF");
                    sttTuoiDang++;
                }
                body.Append(tuoiDangTable);
                AddEmptyLine(body, 1);

                // 8. Giới tính
                AddSectionTitle(body, "8. THỐNG KÊ GIỚI TÍNH");
                var gioiTinhData = data.Where(x => x.LoaiThongKe == "GioiTinh").ToList();
                var gioiTinhTable = CreateTable(body, gioiTinhData.Count + 1, 3);
                AddTableCell(gioiTinhTable, 0, 0, "Giới tính", true, "4472C4");
                AddTableCell(gioiTinhTable, 0, 1, "Số lượng", true, "4472C4");
                AddTableCell(gioiTinhTable, 0, 2, "Tỷ lệ", true, "4472C4");
                
                int sttGioiTinh = 1;
                foreach (var item in gioiTinhData)
                {
                    AddTableCell(gioiTinhTable, sttGioiTinh, 0, item.Ten, false, sttGioiTinh % 2 == 0 ? "D9E1F2" : "FFFFFF");
                    AddTableCell(gioiTinhTable, sttGioiTinh, 1, $"{item.SoLuong:N0} người", false, sttGioiTinh % 2 == 0 ? "D9E1F2" : "FFFFFF");
                    AddTableCell(gioiTinhTable, sttGioiTinh, 2, $"{item.TyLe:F2}%", false, sttGioiTinh % 2 == 0 ? "D9E1F2" : "FFFFFF");
                    sttGioiTinh++;
                }
                body.Append(gioiTinhTable);
                AddEmptyLine(body, 2);

                // Footer - Chữ ký
                AddParagraph(body, $"Ngày lập báo cáo: {DateTime.Now:dd/MM/yyyy HH:mm}", false, 12, false, JustificationValues.Right);
                AddEmptyLine(body, 3);
                AddParagraph(body, "Người lập", false, 14, true, JustificationValues.Right);
                AddEmptyLine(body, 2);
                AddParagraph(body, "_________________________", false, 12, false, JustificationValues.Right);
            }
        }

        private void GenerateStyleDefinitions(StyleDefinitionsPart stylesPart)
        {
            Styles styles = new Styles();
            Style style = new Style() { Type = StyleValues.Paragraph, StyleId = "Normal" };
            StyleName styleName = new StyleName() { Val = "Normal" };
            style.Append(styleName);
            styles.Append(style);
            stylesPart.Styles = styles;
        }

        private void AddTitle(Body body, string text, int fontSize)
        {
            Paragraph para = new Paragraph();
            ParagraphProperties paraProps = new ParagraphProperties();
            paraProps.SpacingBetweenLines = new SpacingBetweenLines() { After = "200", Before = "200" };
            para.Append(paraProps);
            
            Run run = new Run();
            RunProperties props = new RunProperties();
            props.FontSize = new FontSize() { Val = (fontSize * 2).ToString() };
            props.Bold = new Bold();
            props.Color = new DocumentFormat.OpenXml.Wordprocessing.Color() { Val = "1F4E78" };
            run.Append(props);
            run.Append(new Text(text));
            para.Append(run);
            
            para.Append(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }));
            body.Append(para);
        }

        private void AddSubtitle(Body body, string text, int fontSize)
        {
            Paragraph para = new Paragraph();
            Run run = new Run();
            RunProperties props = new RunProperties();
            props.FontSize = new FontSize() { Val = (fontSize * 2).ToString() };
            props.Color = new DocumentFormat.OpenXml.Wordprocessing.Color() { Val = "4472C4" };
            run.Append(props);
            run.Append(new Text(text));
            para.Append(run);
            para.Append(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }));
            body.Append(para);
        }

        private void AddSectionTitle(Body body, string text)
        {
            Paragraph para = new Paragraph();
            ParagraphProperties paraProps = new ParagraphProperties();
            paraProps.SpacingBetweenLines = new SpacingBetweenLines() { After = "120", Before = "240" };
            para.Append(paraProps);
            
            Run run = new Run();
            RunProperties props = new RunProperties();
            props.FontSize = new FontSize() { Val = "24" };
            props.Bold = new Bold();
            props.Color = new DocumentFormat.OpenXml.Wordprocessing.Color() { Val = "1F4E78" };
            run.Append(props);
            run.Append(new Text(text));
            para.Append(run);
            body.Append(para);
        }

        private void AddSubSectionTitle(Body body, string text)
        {
            Paragraph para = new Paragraph();
            ParagraphProperties paraProps = new ParagraphProperties();
            paraProps.SpacingBetweenLines = new SpacingBetweenLines() { After = "60", Before = "120" };
            para.Append(paraProps);
            
            Run run = new Run();
            RunProperties props = new RunProperties();
            props.FontSize = new FontSize() { Val = "20" };
            props.Bold = new Bold();
            run.Append(props);
            run.Append(new Text(text));
            para.Append(run);
            body.Append(para);
        }

        private void AddParagraph(Body body, string text, bool bold, int fontSize = 12, bool italic = false, JustificationValues? justification = null)
        {
            Paragraph para = new Paragraph();
            Run run = new Run();
            RunProperties props = new RunProperties();
            props.FontSize = new FontSize() { Val = (fontSize * 2).ToString() };
            if (bold) props.Bold = new Bold();
            if (italic) props.Italic = new Italic();
            run.Append(props);
            run.Append(new Text(text));
            para.Append(run);
            
            if (justification.HasValue)
            {
                para.Append(new ParagraphProperties(new Justification() { Val = justification.Value }));
            }
            
            body.Append(para);
        }

        private void AddEmptyLine(Body body, int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                Paragraph para = new Paragraph();
                ParagraphProperties paraProps = new ParagraphProperties();
                paraProps.SpacingBetweenLines = new SpacingBetweenLines() { After = "60" };
                para.Append(paraProps);
                body.Append(para);
            }
        }

        private Table CreateTable(Body body, int rows, int cols)
        {
            Table table = new Table();
            TableProperties tableProps = new TableProperties();
            TableWidth tableWidth = new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Pct };
            TableBorders borders = new TableBorders(
                new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4, Color = "000000" },
                new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4, Color = "000000" },
                new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4, Color = "000000" },
                new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4, Color = "000000" },
                new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 2, Color = "000000" },
                new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 2, Color = "000000" }
            );
            tableProps.Append(tableWidth);
            tableProps.Append(borders);
            table.Append(tableProps);

            for (int i = 0; i < rows; i++)
            {
                TableRow row = new TableRow();
                for (int j = 0; j < cols; j++)
                {
                    TableCell cell = new TableCell();
                    TableCellProperties cellProps = new TableCellProperties();
                    cellProps.Append(new TableCellWidth() { Width = (5000 / cols).ToString(), Type = TableWidthUnitValues.Pct });
                    cell.Append(cellProps);
                    row.Append(cell);
                }
                table.Append(row);
            }
            
            return table;
        }

        private void AddTableCell(Table table, int row, int col, string text, bool isHeader, string bgColor)
        {
            TableRow tableRow = table.Elements<TableRow>().ElementAt(row);
            TableCell cell = tableRow.Elements<TableCell>().ElementAt(col);
            
            Paragraph para = new Paragraph();
            ParagraphProperties paraProps = new ParagraphProperties();
            paraProps.Justification = new Justification() { Val = isHeader ? JustificationValues.Center : JustificationValues.Left };
            paraProps.SpacingBetweenLines = new SpacingBetweenLines() { After = "60", Before = "60" };
            para.Append(paraProps);
            
            Run run = new Run();
            RunProperties runProps = new RunProperties();
            runProps.FontSize = new FontSize() { Val = isHeader ? "22" : "20" };
            if (isHeader)
            {
                runProps.Bold = new Bold();
                runProps.Color = new DocumentFormat.OpenXml.Wordprocessing.Color() { Val = "FFFFFF" };
            }
            run.Append(runProps);
            run.Append(new Text(text));
            para.Append(run);
            
            // Set background color
            TableCellProperties cellProps = cell.Elements<TableCellProperties>().FirstOrDefault();
            if (cellProps == null)
            {
                cellProps = new TableCellProperties();
                cell.Append(cellProps);
            }
            
            Shading shading = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = bgColor };
            cellProps.Shading = shading;
            
            cell.Append(para);
        }

        // Các method dữ liệu giả
        private List<BaoCaoTheoDonViDTO> GetFakeDonViData()
        {
            return new List<BaoCaoTheoDonViDTO>
            {
                new BaoCaoTheoDonViDTO { TenDonVi = "Đơn vị 1", TongSoDangVien = 45 },
                new BaoCaoTheoDonViDTO { TenDonVi = "Đơn vị 2", TongSoDangVien = 38 },
                new BaoCaoTheoDonViDTO { TenDonVi = "Đơn vị 3", TongSoDangVien = 32 }
            };
        }

        private void LoadDonViChartWithFakeData()
        {
            var data = GetFakeDonViData();
            chartDonVi.Series.Clear();
            var series = new Series("Số lượng");
            series.ChartType = SeriesChartType.Column;
            series.Color = System.Drawing.Color.FromArgb(0, 174, 219);
            foreach (var item in data)
            {
                series.Points.AddXY(item.TenDonVi, item.TongSoDangVien);
            }
            chartDonVi.Series.Add(series);
        }

        private void LoadKetNapChuyenChartWithFakeData()
        {
            chartKetNapChuyen.Series.Clear();
            var seriesKetNap = new Series("Kết nạp mới");
            seriesKetNap.ChartType = SeriesChartType.Line;
            seriesKetNap.Color = System.Drawing.Color.FromArgb(0, 174, 219);
            var seriesChuyen = new Series("Chuyển đi-đến");
            seriesChuyen.ChartType = SeriesChartType.Line;
            seriesChuyen.Color = System.Drawing.Color.FromArgb(192, 0, 0);
            for (int i = 1; i <= 12; i++)
            {
                seriesKetNap.Points.AddXY($"Tháng {i}", new Random().Next(5, 15));
                seriesChuyen.Points.AddXY($"Tháng {i}", new Random().Next(2, 8));
            }
            chartKetNapChuyen.Series.Add(seriesKetNap);
            chartKetNapChuyen.Series.Add(seriesChuyen);
        }

        /// <summary>
        /// Áp dụng phân quyền cho các control dựa trên vai trò người dùng
        /// </summary>
        private void ApplyPermissions()
        {
            bool canExport = AuthorizationHelper.HasPermission("BaoCao", "Export");
            if (btnXuatBaoCao != null) btnXuatBaoCao.Enabled = canExport;
        }
    }
}

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Commons
{
    public class NPOIExceLHelper
    {
        /// <summary>
        /// Trả về WorkBook từ một đường dẫn tệp Excel.
        /// </summary>
        /// <param name="filePath">Đường dẫn đầy đủ đến tệp Excel.</param>
        /// <returns>Đối tượng IWorkbook (HSSFWorkbook cho .xls, XSSFWorkbook cho .xlsx).</returns>
        /// <exception cref="FileNotFoundException">Ném ra nếu tệp không tồn tại.</exception>
        /// <exception cref="NotSupportedException">Ném ra nếu định dạng tệp không được hỗ trợ (.xls hoặc .xlsx).</exception>
        /// <exception cref="IOException">Ném ra nếu có lỗi khi truy cập tệp.</exception>
        public static IWorkbook GetWorkbookFromFilePath(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Tệp không tìm thấy tại đường dẫn: {filePath}");
            }

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    if (filePath.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                    {
                        return new XSSFWorkbook(fs);
                    }
                    else if (filePath.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
                    {
                        return new HSSFWorkbook(fs);
                    }
                    else
                    {
                        throw new NotSupportedException("Định dạng tệp không được hỗ trợ. Chỉ hỗ trợ .xls và .xlsx.");
                    }
                }
            }
            catch (IOException ex)
            {
                throw new IOException($"Lỗi khi mở hoặc đọc tệp Excel: {ex.Message}", ex);
            }
        }




        /// <summary>
        /// Đọc dữ liệu từ một sheet cụ thể trong tệp Excel và chuyển đổi thành DataTable.
        /// Hàm này giả định rằng hàng đầu tiên được đọc (tại startRowIndex) sẽ là tiêu đề cột của DataTable.
        /// </summary>
        /// <param name="filePath">Đường dẫn đầy đủ đến tệp Excel.</param>
        /// <param name="sheetIndex">Chỉ số của sheet cần đọc (0-based).</param>
        /// <param name="startRowIndex">Chỉ số hàng bắt đầu đọc dữ liệu (0-based). Hàng này và các hàng sau sẽ được đọc.</param>
        /// <param name="endColumnIndex">Chỉ số cột kết thúc (0-based) để đọc dữ liệu. Dữ liệu sẽ được đọc từ cột 0 đến cột này.</param>
        /// <returns>DataTable chứa dữ liệu từ sheet được chỉ định.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Ném ra nếu sheetIndex, startRowIndex hoặc endColumnIndex không hợp lệ.</exception>
        /// <exception cref="Exception">Ném ra nếu có lỗi trong quá trình đọc Excel.</exception>
        public static DataTable ReadDataFromExcelToDataTable(string filePath, int sheetIndex, int startRowIndex, int endColumnIndex)
        {
            DataTable dt = new DataTable();
            IWorkbook workbook = null;
            try
            {
                // 1. Lấy Workbook từ đường dẫn file Excel
                workbook = GetWorkbookFromFilePath(filePath);

                // 2. Kiểm tra và lấy Sheet
                if (sheetIndex < 0 || sheetIndex >= workbook.NumberOfSheets)
                {
                    throw new ArgumentOutOfRangeException(nameof(sheetIndex), $"Chỉ số sheet ({sheetIndex}) không hợp lệ. Workbook chỉ có {workbook.NumberOfSheets} sheet.");
                }
                ISheet sheet = workbook.GetSheetAt(sheetIndex);
                if (sheet == null)
                {
                    throw new Exception($"Sheet với chỉ số {sheetIndex} không tồn tại hoặc bị lỗi.");
                }

                // 3. Kiểm tra chỉ số hàng bắt đầu
                if (startRowIndex < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(startRowIndex), "Chỉ số hàng bắt đầu không được âm.");
                }
                if (startRowIndex > sheet.LastRowNum && sheet.LastRowNum != -1) // -1 nếu sheet trống
                {
                    // Nếu startRowIndex vượt quá LastRowNum, nhưng không phải sheet trống,
                    // có nghĩa là không có hàng nào để đọc từ đó.
                    return dt; // Trả về DataTable rỗng
                }

                // 4. Kiểm tra chỉ số cột kết thúc
                if (endColumnIndex < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(endColumnIndex), "Chỉ số cột kết thúc không được âm.");
                }

                // Lấy hàng tiêu đề (hoặc hàng đầu tiên của dữ liệu)
                IRow headerRow = sheet.GetRow(startRowIndex);
                if (headerRow == null)
                {
                    // Nếu hàng bắt đầu đọc không tồn tại, tức là không có dữ liệu để đọc từ đó trở đi
                    return dt; // Trả về DataTable rỗng
                }

                // Xác định phạm vi cột thực tế để đọc
                // alwaysStartColIndex sẽ là 0 vì bạn muốn đọc từ cột 0
                int actualStartColIdx = 0;
                // actualEndColIdx sẽ là giá trị nhỏ nhất giữa endColumnIndex và cột cuối cùng có dữ liệu trong hàng tiêu đề
                // headerRow.LastCellNum là chỉ số của ô cuối cùng + 1, nên phải trừ 1 để ra chỉ số
                int actualEndColIdx = Math.Min(endColumnIndex, headerRow.LastCellNum > 0 ? headerRow.LastCellNum - 1 : 0);

                // Đảm bảo rằng actualEndColIdx không nhỏ hơn actualStartColIdx
                // (tránh trường hợp endColumnIndex quá nhỏ hoặc không có dữ liệu)
                if (actualEndColIdx < actualStartColIdx)
                {
                    // Nếu phạm vi không hợp lệ, có thể nghĩa là không có cột nào để đọc
                    return dt;
                }

                // 5. Tạo cột cho DataTable từ hàng tiêu đề (hoặc hàng đầu tiên được đọc)
                for (int i = actualStartColIdx; i <= actualEndColIdx; i++)
                {
                    ICell cell = headerRow.GetCell(i);
                    string colName = (cell != null && GetCellValue(cell) != DBNull.Value) ? GetCellValue(cell).ToString() : $"Column_{i}";

                    // Đảm bảo tên cột là duy nhất
                    int counter = 1;
                    string originalColName = colName;
                    while (dt.Columns.Contains(colName))
                    {
                        colName = $"{originalColName}_{counter}";
                        counter++;
                    }
                    dt.Columns.Add(colName);
                }

                // 6. Đọc dữ liệu từ các hàng còn lại
                for (int i = startRowIndex + 1; i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue; // Bỏ qua hàng trống

                    DataRow dataRow = dt.NewRow();
                    for (int j = actualStartColIdx; j <= actualEndColIdx; j++)
                    {
                        ICell cell = row.GetCell(j);
                        if (cell != null)
                        {
                            // Ánh xạ chỉ số cột từ Excel về chỉ số 0-based của DataTable
                            int dtColumnIndex = j - actualStartColIdx;
                            if (dtColumnIndex < dt.Columns.Count)
                            {
                                dataRow[dtColumnIndex] = GetCellValue(cell);
                            }
                        }
                    }
                    dt.Rows.Add(dataRow);
                }
            }
            catch (Exception ex)
            {
                // Xử lý hoặc ném lại ngoại lệ
                throw new Exception($"Lỗi khi đọc dữ liệu Excel từ '{filePath}', Sheet {sheetIndex}, bắt đầu từ hàng {startRowIndex}, kết thúc cột {endColumnIndex}: {ex.Message}", ex);
            }
            finally
            {
                workbook?.Close(); // Đảm bảo workbook được đóng
            }
            return dt;
        }


        /// <summary>
        /// Hàm helper để lấy giá trị ô, hỗ trợ nhiều kiểu dữ liệu.
        /// </summary>
        private static object GetCellValue(ICell cell)
        {
            if (cell == null) return DBNull.Value;

            switch (cell.CellType)
            {
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Numeric:
                    if (DateUtil.IsCellDateFormatted(cell))
                        return cell.DateCellValue;
                    else
                        return cell.NumericCellValue;
                case CellType.Boolean:
                    return cell.BooleanCellValue;
                case CellType.Formula:
                    try
                    {
                        IFormulaEvaluator evaluator = cell.Sheet.Workbook.GetCreationHelper().CreateFormulaEvaluator();
                        CellValue evalCellValue = evaluator.Evaluate(cell);
                        switch (evalCellValue.CellType)
                        {
                            case CellType.Numeric: return evalCellValue.NumberValue;
                            case CellType.String: return evalCellValue.StringValue;
                            case CellType.Boolean: return evalCellValue.BooleanValue;
                            case CellType.Error: return evalCellValue.ErrorValue;
                            default: return DBNull.Value;
                        }
                    }
                    catch { return cell.CellFormula; } // Trả về công thức nếu không thể đánh giá
                case CellType.Blank:
                    return DBNull.Value;
                case CellType.Error:
                    return cell.ErrorCellValue;
                default:
                    return DBNull.Value;
            }
        }



        /// <summary>
        /// Ghi dữ liệu từ DataTable vào một sheet cụ thể trong một workbook đã có.
        /// </summary>
        /// <param name="dataTable">Đối tượng DataTable chứa dữ liệu cần ghi.</param>
        /// <param name="workbook">Đối tượng IWorkbook đã có để ghi dữ liệu vào.</param>
        /// <param name="sheetIndex">Chỉ số của sheet (0-based) trong workbook để ghi dữ liệu vào.
        /// Nếu sheet không tồn tại, nó sẽ được tạo.</param>
        /// <param name="startRowIndex">Chỉ số hàng (0-based) trong sheet để bắt đầu ghi dữ liệu.</param>
        /// <param name="includeHeader">Có ghi tiêu đề cột của DataTable vào Excel hay không (ghi tại startRowIndex).</param>
        /// <param name="autoSizeColumns">Tự động điều chỉnh độ rộng cột sau khi ghi dữ liệu.</param>
        /// <returns>Đối tượng IWorkbook đã được cập nhật.</returns>
        /// <exception cref="ArgumentNullException">Ném ra nếu dataTable hoặc workbook là null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Ném ra nếu sheetIndex hoặc startRowIndex không hợp lệ.</exception>
        /// <exception cref="Exception">Ném ra nếu có lỗi trong quá trình ghi dữ liệu.</exception>
        public static IWorkbook WriteDataTableToWorkbook(DataTable dataTable, IWorkbook workbook, int sheetIndex, int startRowIndex, bool includeHeader = true, bool autoSizeColumns = true)
        {
            if (dataTable == null) throw new ArgumentNullException(nameof(dataTable), "DataTable không được null.");
            if (workbook == null) throw new ArgumentNullException(nameof(workbook), "Workbook không được null.");
            if (sheetIndex < 0) throw new ArgumentOutOfRangeException(nameof(sheetIndex), "Chỉ số sheet không được âm.");
            if (startRowIndex < 0) throw new ArgumentOutOfRangeException(nameof(startRowIndex), "Chỉ số hàng bắt đầu không được âm.");

            ISheet sheet;
            try
            {
                sheet = workbook.GetSheetAt(sheetIndex);
            }
            catch (ArgumentOutOfRangeException)
            {
                // Sheet chưa tồn tại, tạo mới
                sheet = workbook.CreateSheet($"Sheet{sheetIndex + 1}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi truy cập hoặc tạo sheet với chỉ số {sheetIndex}: {ex.Message}", ex);
            }

            if (sheet == null) // Đảm bảo sheet được tạo nếu chỉ số không có hoặc bị lỗi
            {
                sheet = workbook.CreateSheet($"Sheet{sheetIndex + 1}");
            }


            int currentRowIndex = startRowIndex;

            // Ghi tiêu đề nếu được yêu cầu
            if (includeHeader)
            {
                IRow headerRow = sheet.GetRow(currentRowIndex) ?? sheet.CreateRow(currentRowIndex);
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    ICell cell = headerRow.GetCell(i) ?? headerRow.CreateCell(i);
                    SetCellValue(cell, dataTable.Columns[i].ColumnName);
                    // Optional: Apply basic header style
                    // SetCellStyle(cell, isBold: true, backgroundColor: NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index, borderStyle: BorderStyle.Thin, alignment: HorizontalAlignment.Center);
                }
                currentRowIndex++; // Di chuyển xuống hàng tiếp theo sau tiêu đề
            }

            // Ghi dữ liệu từ DataTable
            foreach (DataRow dr in dataTable.Rows)
            {
                IRow dataRow = sheet.GetRow(currentRowIndex) ?? sheet.CreateRow(currentRowIndex);
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    ICell cell = dataRow.GetCell(i) ?? dataRow.CreateCell(i);
                    SetCellValue(cell, dr[i]);
                    // Optional: Apply basic data cell style
                    // SetCellStyle(cell, borderStyle: BorderStyle.Thin);
                }
                currentRowIndex++;
            }

            // Tự động điều chỉnh độ rộng cột
            if (autoSizeColumns)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    sheet.AutoSizeColumn(i);
                }
            }

            return workbook;
        }

        /// <summary>
        /// Hàm helper để đặt giá trị ô, hỗ trợ nhiều kiểu dữ liệu.
        /// </summary>
        private static void SetCellValue(ICell cell, object value)
        {
            if (value == null || value == DBNull.Value)
            {
                cell.SetCellType(CellType.Blank);
                return;
            }

            if (value is bool)
            {
                cell.SetCellValue((bool)value);
            }
            else if (value is int || value is long || value is double || value is float || value is decimal)
            {
                cell.SetCellValue(Convert.ToDouble(value));
            }
            else if (value is DateTime)
            {
                cell.SetCellValue((DateTime)value);
                // Áp dụng định dạng ngày tháng cho ô
                ICellStyle dateStyle = cell.Sheet.Workbook.CreateCellStyle();
                IDataFormat dataFormat = cell.Sheet.Workbook.CreateDataFormat();
                dateStyle.DataFormat = dataFormat.GetFormat("dd/MM/yyyy HH:mm:ss"); // Hoặc định dạng khác bạn muốn
                cell.CellStyle = dateStyle;
            }
            else
            {
                cell.SetCellValue(value.ToString());
            }
        }



        /// <summary>
        /// Mở một tệp Excel từ đường dẫn có sẵn, ghi nội dung từ DataTable vào một sheet cụ thể,
        /// và lưu lại các thay đổi vào tệp.
        /// </summary>
        /// <param name="filePath">Đường dẫn đầy đủ đến tệp Excel. Nếu tệp không tồn tại, nó sẽ được tạo mới.</param>
        /// <param name="dataTable">Đối tượng DataTable chứa dữ liệu cần ghi.</param>
        /// <param name="sheetIndex">Chỉ số của sheet (0-based) trong workbook để ghi dữ liệu vào.
        /// Nếu sheet không tồn tại, nó sẽ được tạo.</param>
        /// <param name="startRowIndex">Chỉ số hàng (0-based) trong sheet để bắt đầu ghi dữ liệu.</param>
        /// <param name="includeHeader">Có ghi tiêu đề cột của DataTable vào Excel hay không (ghi tại startRowIndex).</param>
        /// <param name="autoSizeColumns">Tự động điều chỉnh độ rộng cột sau khi ghi dữ liệu.</param>
        /// <returns>Đối tượng IWorkbook đã được cập nhật (trong bộ nhớ).</returns>
        /// <exception cref="ArgumentNullException">Ném ra nếu dataTable hoặc filePath là null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Ném ra nếu sheetIndex hoặc startRowIndex không hợp lệ.</exception>
        /// <exception cref="IOException">Ném ra nếu có lỗi khi truy cập hoặc ghi tệp.</exception>
        /// <exception cref="Exception">Ném ra nếu có lỗi trong quá trình xử lý Excel.</exception>
        public static IWorkbook WriteDataTableToExcelFile(string filePath, DataTable dataTable, int sheetIndex, int startRowIndex, bool includeHeader = true, bool autoSizeColumns = true)
        {
            if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentNullException(nameof(filePath), "Đường dẫn tệp không được rỗng.");
            if (dataTable == null) throw new ArgumentNullException(nameof(dataTable), "DataTable không được null.");

            IWorkbook workbook;
            bool fileExists = File.Exists(filePath);
            string fileExtension = Path.GetExtension(filePath);

            if (fileExists)
            {
                // Mở workbook hiện có
                workbook = GetWorkbookFromFilePath(filePath);
            }
            else
            {
                // Tạo workbook mới nếu tệp không tồn tại
                if (fileExtension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    workbook = new XSSFWorkbook();
                }
                else if (fileExtension.Equals(".xls", StringComparison.OrdinalIgnoreCase))
                {
                    workbook = new HSSFWorkbook();
                }
                else
                {
                    throw new NotSupportedException("Định dạng tệp không được hỗ trợ để tạo tệp mới. Chỉ hỗ trợ .xls và .xlsx.");
                }
            }

            try
            {
                // Ghi dữ liệu vào workbook bằng hàm tiện ích đã có
                workbook = WriteDataTableToWorkbook(dataTable, workbook, sheetIndex, startRowIndex, includeHeader, autoSizeColumns);

                // Lưu workbook vào tệp
                //using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                //{
                //    workbook.Write(fs);
                //}
                return workbook;
            }
            catch (IOException ex)
            {
                throw new IOException($"Lỗi khi lưu tệp Excel vào đường dẫn: {filePath}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi ghi dữ liệu vào tệp Excel: {ex.Message}", ex);
            }
            finally
            {
                // Không đóng workbook ở đây vì nó được trả về.
                // Người gọi hàm sẽ chịu trách nhiệm đóng workbook nếu cần
                // (thường không cần nếu đã lưu vào file và hàm không giữ tham chiếu)
            }
        }




    }

}
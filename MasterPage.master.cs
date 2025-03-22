using HtmlAgilityPack;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{

    public string FormClientId => this.form1.ClientID;
    private int admissionNumber = 0; // 在类的顶部定义
    private string processedHtmlContent; // 用于保存处理后的HTML内容
    
    public Literal DataContent
    {
        get { return dataContent; }
    }
    public event EventHandler StartButtonClicked;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // 清除之前的临时文件
            ClearTemporaryFiles();

            //从 Session 中恢复学校名称
            string selectedSchoolName = Session["SelectedSchoolName"] as string;
            if (!string.IsNullOrEmpty(selectedSchoolName))
            {
                Literal selectedSchoolNameLiteral = (Literal)FindControl("selectedSchoolName");
                if (selectedSchoolNameLiteral != null)
                {
                    selectedSchoolNameLiteral.Text = $" {HttpUtility.HtmlEncode(selectedSchoolName)}";
                }
            }


        }
        else
        {

            string twinBinding = Request.Form["hfTwinBinding"];
            if (!string.IsNullOrEmpty(twinBinding))
            {
                // 存储到 ViewState 或 Session 中
                ViewState["TwinBinding"] = twinBinding;
                // 或者
                // Session["TwinBinding"] = twinBinding;
            }
            string selectedSchoolName = Session["SelectedSchoolName"] as string;
            if (!string.IsNullOrEmpty(selectedSchoolName) && ViewState["SchoolNameInserted"]?.ToString() != "true")
            {
                Literal dataContent = (Literal)FindControl("dataContent");
                if (dataContent != null)
                {
                    var doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(dataContent.Text);

                    var thead = doc.DocumentNode.SelectSingleNode("//thead");
                    if (thead != null)
                    {
                        // 检查学校名称行是否已经存在
                        var existingSchoolNameRow = thead.SelectSingleNode("tr[th[@colspan='5' and @style='text-align: center; padding: 10px;']]"); // 假设 colspan 为 5
                        if (existingSchoolNameRow == null)
                        {
                            // 插入学校名称行（如果不存在）
                            var schoolNameRow = doc.CreateElement("tr");
                            var schoolNameCell = doc.CreateElement("th");
                            schoolNameCell.SetAttributeValue("colspan", "5"); // 或者使用实际的列数
                            schoolNameCell.SetAttributeValue("style", "text-align: center; padding: 10px;");
                            schoolNameCell.InnerHtml = HttpUtility.HtmlEncode(selectedSchoolName);
                            schoolNameRow.AppendChild(schoolNameCell);
                            thead.InsertBefore(schoolNameRow, thead.FirstChild);
                            // 标记学校名称行已插入
                            
                        }
                    }

                    dataContent.Text = doc.DocumentNode.OuterHtml;
                }
            }

        }

    }

    private void ClearTemporaryFiles()
    {
        string tempPath = Server.MapPath("~/Temp/");
        if (Directory.Exists(tempPath))
        {
            foreach (string file in Directory.GetFiles(tempPath))
            {
                File.Delete(file);
            }
        }
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default2.aspx");
    }


    //protected void btnImport_Click(object sender, EventArgs e)
    //{
    //     if (fileUpload.HasFile)
    //{
    //    try
    //    {
    //        // 保存上传的文件到临时目录
    //        string tempPath = Server.MapPath("~/Temp/");
    //        if (!Directory.Exists(tempPath))
    //        {
    //            Directory.CreateDirectory(tempPath);
    //        }

    //        string filePath = Path.Combine(tempPath, Path.GetFileName(fileUpload.PostedFile.FileName));
    //        fileUpload.SaveAs(filePath);
    //            using (var package = new ExcelPackage(new FileInfo(filePath)))
    //        {
    //            var worksheet = package.Workbook.Worksheets[0]; // 获取第一个工作表  
    //            int rowCount = worksheet.Dimension.Rows; // 行数  
    //            int colCount = worksheet.Dimension.Columns; // 列数  

    //            StringBuilder sb = new StringBuilder();
    //            sb.Append("<table class='min-w-full bg-white'>");
    //            sb.Append("<thead><tr>");

    //                // 将生成的HTML内容插入到数据框
    //                dataContent.Text = sb.ToString();
    //                // 读取表头  
    //                for (int col = 1; col <= colCount; col++)
    //            {
    //                var header = worksheet.Cells[1, col].Text; // 获取表头单元格的文本  
    //                //sb.Append($"<th class='py-2 px-4 border-b'>{header}</th>");
    //                sb.Append($"<th style='text-align: center;' class='py-2 px-4 border-b'>{header}</th>");
    //                }
    //            sb.Append("</tr></thead><tbody>");

    //            // 读取数据行  
    //            for (int row = 2; row <= rowCount; row++) // 从第二行开始，第一行为表头  
    //            {
    //                sb.Append("<tr>");
    //                for (int col = 1; col <= colCount; col++)
    //                {
    //                    var value = worksheet.Cells[row, col].Text; // 获取单元格的文本  
    //                                                                //sb.Append($"<td class='py-2 px-4 border-b'>{value}</td>");
    //                        sb.Append($"<td style='text-align: center;' class='py-2 px-4 border-b'>{value}</td>");
    //                    }
    //                sb.Append("</tr>");
    //            }
    //            sb.Append("</tbody></table>");

    //            // 将生成的HTML内容插入到数据框  
    //            dataContent.Text = sb.ToString();
    //            ScriptManager.RegisterStartupScript(this, GetType(), "HideSchoolName", "document.getElementById('selectedSchoolName').style.display = 'none';", true);
    //            }
    //    }
    //    catch (Exception ex)
    //    {
    //        // 处理异常(记录日志或显示错误信息)  
    //        Response.Write($"<script>alert('发生错误: {ex.Message}');</script>");
    //    }
    //}
    //}

    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (fileUpload.HasFile)
        {
            try
            {
                // 保存上传的文件到临时目录  
                string tempPath = Server.MapPath("~/Temp/");
                if (!Directory.Exists(tempPath))
                {
                    Directory.CreateDirectory(tempPath);
                }

                string filePath = Path.Combine(tempPath, Path.GetFileName(fileUpload.PostedFile.FileName));
                fileUpload.SaveAs(filePath);

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0]; // 获取第一个工作表  
                    int rowCount = worksheet.Dimension.Rows; // 行数  
                    int colCount = worksheet.Dimension.Columns; // 列数  

                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table class='min-w-full bg-white'>");

                    // 读取表头  
                    sb.Append("<thead><tr>");
                    for (int col = 1; col <= colCount; col++)
                    {
                        var header = worksheet.Cells[1, col].Text; // 获取表头单元格的文本  
                        sb.Append($"<th style='text-align: center;' class='py-2 px-4 border-b'>{header}</th>");
                    }
                    sb.Append("</tr></thead><tbody>");

                    // 读取数据行  
                    for (int row = 2; row <= rowCount; row++) // 从第二行开始，第一行为表头  
                    {
                        sb.Append("<tr>");
                        for (int col = 1; col <= colCount; col++)
                        {
                            var value = worksheet.Cells[row, col].Text; // 获取单元格的文本  
                            sb.Append($"<td style='text-align: center;' class='py-2 px-4 border-b'>{value}</td>");
                        }
                        sb.Append("</tr>");
                    }
                    sb.Append("</tbody></table>");

                    // 将生成的HTML内容插入到数据框  
                    Literal dataContent = (Literal)FindControl("dataContent");
                    if (dataContent != null)
                    {
                        dataContent.Text = sb.ToString();
                    }

                    // Store the imported file name in ViewState for use in export  
                    ViewState["ImportedFileName"] = Path.GetFileName(fileUpload.PostedFile.FileName);
                }
            }
            catch (Exception ex)
            {
                // 处理异常(记录日志或显示错误信息)  
                Response.Write($"<script>alert('发生错误: {ex.Message}');</script>");
            }
        }
    }


    protected void btnStart_Click(object sender, EventArgs e)
    {
        string twinBinding = ViewState["TwinBinding"] as string;
        System.Diagnostics.Debug.WriteLine("btnStart_Click - twinBinding: " + twinBinding);
        //if (string.IsNullOrEmpty(twinBinding))
        //{
        //    twinBinding = "twinBinding"; // 默认为捆绑
        //}

        System.Diagnostics.Debug.WriteLine("Received twinBinding: " + twinBinding);

        if (string.IsNullOrEmpty(twinBinding))
        {
            Response.Write("<script>alert('请先选择双胎是否捆绑上学的派位方式');</script>");
            return;
        }

        var admissionInputControl = (TextBox)this.FindControl("txt招生数");
        string admissionInput = admissionInputControl != null ? admissionInputControl.Text : string.Empty;

        if (string.IsNullOrEmpty(admissionInput))
        {
            Response.Write("<script>alert('输入招生数为空');</script>");
            return;
        }

        int admissionNumber;
        if (!int.TryParse(admissionInput, out admissionNumber) || admissionNumber <= 0)
        {
            Response.Write("<script>alert('输入的招生数无效');</script>");
            return;
        }

        var doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(dataContent.Text);

        //先确保学校名称行存在并正确插入（如果不存在的话）
        // 从 ViewState 中恢复学校名称
        string selectedSchoolName = Session["SelectedSchoolName"] as string;
       
        if (!string.IsNullOrEmpty(selectedSchoolName))
        {
            var thead = doc.DocumentNode.SelectSingleNode("//thead");
            if (thead != null)
            {
                // 检查学校名称行是否已经存在
                var existingSchoolNameRow = thead.SelectSingleNode("tr[th[@colspan and @style]]");
                if (existingSchoolNameRow == null && !ViewState["SchoolNameInserted"].ToString().Equals("true"))
                {
                    // 插入学校名称行（如果不存在）
                    var schoolNameRow = doc.CreateElement("tr");
                    var schoolNameCell = doc.CreateElement("th");
                    schoolNameCell.SetAttributeValue("colspan", doc.DocumentNode.SelectNodes("//thead/tr/th").Count.ToString());
                    schoolNameCell.SetAttributeValue("style", "text-align: center; padding: 10px;");
                    schoolNameCell.InnerHtml = HttpUtility.HtmlEncode(selectedSchoolName);
                    schoolNameRow.AppendChild(schoolNameCell);
                    thead.InsertBefore(schoolNameRow, thead.FirstChild);

                    // 标记学校名称行已插入
                   
                }
            }
        }

        // 后续原来的代码逻辑保持不变，比如添加随机号、处理不同绑定模式下的行筛选等操作
        // 添加“随机号”列到表头
        var headerRow = doc.DocumentNode.SelectSingleNode("//thead/tr[2]"); // 表头在第一行
        if (headerRow != null)
        {
            var randomHeader = doc.CreateElement("th");
            randomHeader.SetAttributeValue("style", "text-align: center; padding: 10px;");
            randomHeader.InnerHtml = "随机号";
            // 找到表头行的最后一个<th>元素
            var lastHeader = headerRow.LastChild;
            while (lastHeader.NextSibling != null)
            {
                lastHeader = lastHeader.NextSibling;
            }

            // 将“随机号”插入到表头行的最后一列之后
            headerRow.InsertAfter(randomHeader, lastHeader);
        }

        var dataRows = doc.DocumentNode.SelectNodes("//tbody/tr");
        if (dataRows == null || dataRows.Count == 0)
        {
            Response.Write("<script>alert('未找到数据行');</script>");
            return;
        }

        Random rand = new Random();
        var rowsWithRandomNumbers = new List<Tuple<int, HtmlNode>>();

        foreach (var row in dataRows)
        {
            var randomNumber = rand.Next(1, 10000);
            var randomCell = doc.CreateElement("td");

            // 检查该行是否包含被选中的字母
            var cells = row.SelectNodes("td");
            bool containsSelectedLetter = false;
            if (cells != null && cells.Count > 1)
            {
                var secondColumnValue = cells[1].InnerText;
                foreach (var letter in new[] { 'A', 'B', 'C', 'D' })
                {
                    if (secondColumnValue.Contains(letter))
                    {
                        containsSelectedLetter = true;
                        break;
                    }
                }
            }

            // 如果包含被选中的字母，在随机号后面加上星号
            randomCell.InnerHtml = containsSelectedLetter ? $"{randomNumber}*" : randomNumber.ToString();
            if (cells != null && cells.Count > 0)
            {
                row.InsertAfter(randomCell, row.LastChild);
            }
            rowsWithRandomNumbers.Add(new Tuple<int, HtmlNode>(randomNumber, row));
        }
        if (twinBinding == "noTwinBinding")
        {
            System.Diagnostics.Debug.WriteLine("Entering noTwinBinding logic");
            // 不捆绑逻辑
            var selectedRows = rowsWithRandomNumbers.OrderBy(x => x.Item1).ToList();

            //处理选中的行
           var tbody = doc.DocumentNode.SelectSingleNode("//tbody");
            tbody.RemoveAllChildren();

            foreach (var selectedRow in selectedRows)
            {
                // 设置前N行的字体颜色为绿色
                if (selectedRows.IndexOf(selectedRow) < admissionNumber)
                {
                    selectedRow.Item2.SetAttributeValue("style", "color: green;");
                }
                tbody.AppendChild(selectedRow.Item2); // 添加每一行到tbody中
            }

            // 更新HTML内容
            dataContent.Text = doc.DocumentNode.OuterHtml;
            processedHtmlContent = dataContent.Text;
            ViewState["ProcessedHtmlContent"] = processedHtmlContent;
            return; // 确保这里返回，避免执行捆绑逻辑
        }

        // 捆绑模式的逻辑
        var sortedRows = rowsWithRandomNumbers.OrderBy(x => x.Item1).ToList();

        var selectedLetters = new HashSet<char>();
        var selectedLetterPositions = new Dictionary<char, List<int>>();

        for (int i = 0; i < Math.Min(sortedRows.Count, admissionNumber); i++)
        {
            var cells = sortedRows[i].Item2.SelectNodes("td");
            if (cells != null && cells.Count > 1)
            {
                var secondColumnValue = cells[1].InnerText;
                foreach (var letter in new[] { 'A', 'B', 'C', 'D' })
                {
                    if (secondColumnValue.Contains(letter))
                    {
                        selectedLetters.Add(letter);
                        if (!selectedLetterPositions.ContainsKey(letter))
                        {
                            selectedLetterPositions[letter] = new List<int>();
                        }
                        selectedLetterPositions[letter].Add(i);
                    }
                }
            }
        }

        // 创建一个新的行列表，用于最终的排序
        var finalRows = new List<Tuple<int, HtmlNode>>(sortedRows);

        // 将其他相同字母的行聚集在最初被选中的字母行的后面
        foreach (var letter in selectedLetters)
        {
            var initialPositions = selectedLetterPositions[letter];
            var otherPositions = new List<int>();

            for (int i = 0; i < sortedRows.Count; i++)
            {
                if (!initialPositions.Contains(i))
                {
                    var cells = sortedRows[i].Item2.SelectNodes("td");
                    if (cells != null && cells.Count > 1 && cells[1].InnerText.Contains(letter))
                    {
                        otherPositions.Add(i);
                    }
                }
            }

            // 将其他相同字母的行插入到最初被选中的字母行后面
            foreach (var initialPosition in initialPositions)
            {
                var insertIndex = initialPosition + 1;
                foreach (var otherPosition in otherPositions)
                {
                    if (insertIndex < finalRows.Count)
                    {
                        var rowToInsert = sortedRows[otherPosition];
                        finalRows.Insert(insertIndex, rowToInsert);
                        insertIndex++;
                    }
                }
            }
        }

        // 去除重复的行
        finalRows = finalRows.Distinct().ToList();

        var htmlTbody = doc.DocumentNode.SelectSingleNode("//tbody");
        if (htmlTbody == null)
        {
            Response.Write("<script>alert('未找到 tbody');</script>");
            return;
        }
        htmlTbody.RemoveAllChildren();

        // 遍历所有行，而不仅仅是前N行
        foreach (var finalRow in finalRows)
        {
            // 对前N行进行颜色设置
            if (finalRows.IndexOf(finalRow) < admissionNumber)
            {
                finalRow.Item2.SetAttributeValue("style", "color: green;"); // 设置字体颜色为绿色
            }
            htmlTbody.AppendChild(finalRow.Item2); // 添加每一行到tbody中
        }
        dataContent.Text = doc.DocumentNode.OuterHtml;
        processedHtmlContent = dataContent.Text;
        ViewState["ProcessedHtmlContent"] = processedHtmlContent;
        
    }


    protected void btnTest_Click(object sender, EventArgs e)
    {
        string twinBinding = ViewState["TwinBinding"] as string;
        System.Diagnostics.Debug.WriteLine("btnStart_Click - twinBinding: " + twinBinding);
        //if (string.IsNullOrEmpty(twinBinding))
        //{
        //    twinBinding = "twinBinding"; // 默认为捆绑
        //}

        System.Diagnostics.Debug.WriteLine("Received twinBinding: " + twinBinding);

        if (string.IsNullOrEmpty(twinBinding))
        {
            Response.Write("<script>alert('请先选择双胎是否捆绑上学的派位方式');</script>");
            return;
        }

        var admissionInputControl = (TextBox)this.FindControl("txt招生数");
        string admissionInput = admissionInputControl != null ? admissionInputControl.Text : string.Empty;

        if (string.IsNullOrEmpty(admissionInput))
        {
            Response.Write("<script>alert('输入招生数为空');</script>");
            return;
        }

        int admissionNumber;
        if (!int.TryParse(admissionInput, out admissionNumber) || admissionNumber <= 0)
        {
            Response.Write("<script>alert('输入的招生数无效');</script>");
            return;
        }

        var doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(dataContent.Text);

        //先确保学校名称行存在并正确插入（如果不存在的话）
        // 从 ViewState 中恢复学校名称
        string selectedSchoolName = Session["SelectedSchoolName"] as string;

        if (!string.IsNullOrEmpty(selectedSchoolName))
        {
            var thead = doc.DocumentNode.SelectSingleNode("//thead");
            if (thead != null)
            {
                // 检查学校名称行是否已经存在
                var existingSchoolNameRow = thead.SelectSingleNode("tr[th[@colspan and @style]]");
                if (existingSchoolNameRow == null && !ViewState["SchoolNameInserted"].ToString().Equals("true"))
                {
                    // 插入学校名称行（如果不存在）
                    var schoolNameRow = doc.CreateElement("tr");
                    var schoolNameCell = doc.CreateElement("th");
                    schoolNameCell.SetAttributeValue("colspan", doc.DocumentNode.SelectNodes("//thead/tr/th").Count.ToString());
                    schoolNameCell.SetAttributeValue("style", "text-align: center; padding: 10px;");
                    schoolNameCell.InnerHtml = HttpUtility.HtmlEncode(selectedSchoolName);
                    schoolNameRow.AppendChild(schoolNameCell);
                    thead.InsertBefore(schoolNameRow, thead.FirstChild);

                    // 标记学校名称行已插入

                }
            }
        }

        // 后续原来的代码逻辑保持不变，比如添加随机号、处理不同绑定模式下的行筛选等操作
        // 添加“随机号”列到表头
        var headerRow = doc.DocumentNode.SelectSingleNode("//thead/tr[2]"); // 表头在第一行
        if (headerRow != null)
        {
            var randomHeader = doc.CreateElement("th");
            randomHeader.SetAttributeValue("style", "text-align: center; padding: 10px;");
            randomHeader.InnerHtml = "随机号";
            // 找到表头行的最后一个<th>元素
            var lastHeader = headerRow.LastChild;
            while (lastHeader.NextSibling != null)
            {
                lastHeader = lastHeader.NextSibling;
            }

            // 将“随机号”插入到表头行的最后一列之后
            headerRow.InsertAfter(randomHeader, lastHeader);
        }

        var dataRows = doc.DocumentNode.SelectNodes("//tbody/tr");
        if (dataRows == null || dataRows.Count == 0)
        {
            Response.Write("<script>alert('未找到数据行');</script>");
            return;
        }

        Random rand = new Random();
        var rowsWithRandomNumbers = new List<Tuple<int, HtmlNode>>();

        foreach (var row in dataRows)
        {
            var randomNumber = rand.Next(1, 10000);
            var randomCell = doc.CreateElement("td");

            // 检查该行是否包含被选中的字母
            var cells = row.SelectNodes("td");
            bool containsSelectedLetter = false;
            if (cells != null && cells.Count > 1)
            {
                var secondColumnValue = cells[1].InnerText;
                foreach (var letter in new[] { 'A', 'B', 'C', 'D' })
                {
                    if (secondColumnValue.Contains(letter))
                    {
                        containsSelectedLetter = true;
                        break;
                    }
                }
            }

            // 如果包含被选中的字母，在随机号后面加上星号
            randomCell.InnerHtml = containsSelectedLetter ? $"{randomNumber}*" : randomNumber.ToString();
            if (cells != null && cells.Count > 0)
            {
                row.InsertAfter(randomCell, row.LastChild);
            }
            rowsWithRandomNumbers.Add(new Tuple<int, HtmlNode>(randomNumber, row));
        }
        if (twinBinding == "noTwinBinding")
        {
            System.Diagnostics.Debug.WriteLine("Entering noTwinBinding logic");
            // 不捆绑逻辑
            var selectedRows = rowsWithRandomNumbers.OrderBy(x => x.Item1).ToList();

            //处理选中的行
            var tbody = doc.DocumentNode.SelectSingleNode("//tbody");
            tbody.RemoveAllChildren();

            foreach (var selectedRow in selectedRows)
            {
                // 设置前N行的字体颜色为绿色
                if (selectedRows.IndexOf(selectedRow) < admissionNumber)
                {
                    selectedRow.Item2.SetAttributeValue("style", "color: green;");
                }
                tbody.AppendChild(selectedRow.Item2); // 添加每一行到tbody中
            }

            // 更新HTML内容
            dataContent.Text = doc.DocumentNode.OuterHtml;
            processedHtmlContent = dataContent.Text;
            ViewState["ProcessedHtmlContent"] = processedHtmlContent;
            return; // 确保这里返回，避免执行捆绑逻辑
        }

        // 捆绑模式的逻辑
        var sortedRows = rowsWithRandomNumbers.OrderBy(x => x.Item1).ToList();

        var selectedLetters = new HashSet<char>();
        var selectedLetterPositions = new Dictionary<char, List<int>>();

        for (int i = 0; i < Math.Min(sortedRows.Count, admissionNumber); i++)
        {
            var cells = sortedRows[i].Item2.SelectNodes("td");
            if (cells != null && cells.Count > 1)
            {
                var secondColumnValue = cells[1].InnerText;
                foreach (var letter in new[] { 'A', 'B', 'C', 'D' })
                {
                    if (secondColumnValue.Contains(letter))
                    {
                        selectedLetters.Add(letter);
                        if (!selectedLetterPositions.ContainsKey(letter))
                        {
                            selectedLetterPositions[letter] = new List<int>();
                        }
                        selectedLetterPositions[letter].Add(i);
                    }
                }
            }
        }

        // 创建一个新的行列表，用于最终的排序
        var finalRows = new List<Tuple<int, HtmlNode>>(sortedRows);

        // 将其他相同字母的行聚集在最初被选中的字母行的后面
        foreach (var letter in selectedLetters)
        {
            var initialPositions = selectedLetterPositions[letter];
            var otherPositions = new List<int>();

            for (int i = 0; i < sortedRows.Count; i++)
            {
                if (!initialPositions.Contains(i))
                {
                    var cells = sortedRows[i].Item2.SelectNodes("td");
                    if (cells != null && cells.Count > 1 && cells[1].InnerText.Contains(letter))
                    {
                        otherPositions.Add(i);
                    }
                }
            }

            // 将其他相同字母的行插入到最初被选中的字母行后面
            foreach (var initialPosition in initialPositions)
            {
                var insertIndex = initialPosition + 1;
                foreach (var otherPosition in otherPositions)
                {
                    if (insertIndex < finalRows.Count)
                    {
                        var rowToInsert = sortedRows[otherPosition];
                        finalRows.Insert(insertIndex, rowToInsert);
                        insertIndex++;
                    }
                }
            }
        }

        // 去除重复的行
        finalRows = finalRows.Distinct().ToList();

        var htmlTbody = doc.DocumentNode.SelectSingleNode("//tbody");
        if (htmlTbody == null)
        {
            Response.Write("<script>alert('未找到 tbody');</script>");
            return;
        }
        htmlTbody.RemoveAllChildren();

        // 遍历所有行，而不仅仅是前N行
        foreach (var finalRow in finalRows)
        {
            // 对前N行进行颜色设置
            if (finalRows.IndexOf(finalRow) < admissionNumber)
            {
                finalRow.Item2.SetAttributeValue("style", "color: green;"); // 设置字体颜色为绿色
            }
            htmlTbody.AppendChild(finalRow.Item2); // 添加每一行到tbody中
        }
        dataContent.Text = doc.DocumentNode.OuterHtml;
        processedHtmlContent = dataContent.Text;
        ViewState["ProcessedHtmlContent"] = processedHtmlContent;

    }




    //protected void btnExport_Click(object sender, EventArgs e)
    //{
    //    string processedHtmlContent = ViewState["ProcessedHtmlContent"] as string;

    //    if (string.IsNullOrEmpty(processedHtmlContent))
    //    {
    //        Response.Write("<script>alert('没有处理过的数据');</script>");
    //        return;
    //    }

    //    try
    //    {
    //        var doc = new HtmlAgilityPack.HtmlDocument();
    //        doc.LoadHtml(processedHtmlContent);

    //        // 在解析HTML内容之后，移除学校名称行
    //        var schoolNameRow = doc.DocumentNode.SelectSingleNode("//thead/tr[th[@colspan='5' and @style='text-align: center; padding: 10px;']]");
    //        if (schoolNameRow != null)
    //        {
    //            schoolNameRow.Remove();
    //        }

    //        StringBuilder csvOutput = new StringBuilder();

    //        // 添加表头  
    //        var headerCells = doc.DocumentNode.SelectNodes("//thead/tr/th");
    //        if (headerCells != null)
    //        {
    //            for (int i = 0; i < headerCells.Count; i++)
    //            {
    //                var cell = headerCells[i];
    //                csvOutput.Append(cell.InnerText.Trim());
    //                if (i < headerCells.Count - 1) // 不在最后一个单元格后面添加逗号
    //                {
    //                    csvOutput.Append(",");
    //                }
    //            }
    //            csvOutput.AppendLine(); // End the header row  
    //        }

    //        // 导出所有设置了绿色字体的行  
    //        var dataRows = doc.DocumentNode.SelectNodes("//tbody/tr[@style='color: green;']");
    //        if (dataRows != null)
    //        {
    //            foreach (var row in dataRows)
    //            {
    //                var cells = row.SelectNodes("td");
    //                if (cells != null)
    //                {
    //                    for (int i = 0; i < cells.Count; i++)
    //                    {
    //                        var cell = cells[i];
    //                        csvOutput.Append(cell.InnerText.Trim().Replace("\"", "\"\""));
    //                        if (i < cells.Count - 1) // 不在最后一个单元格后面添加逗号
    //                        {
    //                            csvOutput.Append(",");
    //                        }
    //                    }
    //                    csvOutput.AppendLine(); // End the current data row  
    //                }
    //            }
    //        }

    //        // Get the imported file name from ViewState for the exported file name  
    //        string exportedFileName = ViewState["ImportedFileName"] != null
    //            ? ViewState["ImportedFileName"].ToString().Replace(".xlsx", ".csv") // Change the extension to .csv  
    //            : "exported_data.csv"; // Fallback name  

    //        // 将 CSV 内容写入 Memory Stream  
    //        using (var stream = new MemoryStream())
    //        using (var writer = new StreamWriter(stream, Encoding.UTF8))
    //        {
    //            writer.Write(csvOutput.ToString());
    //            writer.Flush();
    //            stream.Position = 0;

    //            // 设置响应头以便浏览器下载文件  
    //            Response.Clear();
    //            Response.ContentType = "application/csv";
    //            Response.AddHeader("content-disposition", $"attachment; filename={exportedFileName}");
    //            Response.BinaryWrite(stream.ToArray());
    //            Response.End();
    //        }

    //        // 清除学校名称，防止下次导出时再次出现重复  
    //        // Session["SelectedSchoolName"] = null; // 这里可以保留，但其实不需要，因为已经不在导出时使用
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write($"");
    //    }
    //}




    protected void btnExport_Click(object sender, EventArgs e)
    {
        string processedHtmlContent = ViewState["ProcessedHtmlContent"] as string;

        if (string.IsNullOrEmpty(processedHtmlContent))
        {
            Response.Write("<script>alert('没有处理过的数据');</script>");
            return;
        }

        try
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(processedHtmlContent);

            // 在解析HTML内容之后，移除学校名称行  
            var schoolNameRow = doc.DocumentNode.SelectSingleNode("//thead/tr[th[@colspan='5' and @style='text-align: center; padding: 10px;']]");
            string schoolName = string.Empty;

            if (schoolNameRow != null)
            {
                schoolName = schoolNameRow.InnerText.Trim(); // 获取学校名称  
                schoolNameRow.Remove(); // 移除学校名称行  
            }

            StringBuilder csvOutput = new StringBuilder();

            // 添加学校名称行  
            if (!string.IsNullOrEmpty(schoolName))
            {
                csvOutput.AppendLine($"学校名称,{schoolName}"); // 将学校名称添加到CSV的第一行  
            }

            // 添加表头  
            var headerCells = doc.DocumentNode.SelectNodes("//thead/tr/th");
            if (headerCells != null)
            {
                for (int i = 0; i < headerCells.Count; i++)
                {
                    var cell = headerCells[i];
                    csvOutput.Append(cell.InnerText.Trim());
                    if (i < headerCells.Count - 1) // 不在最后一个单元格后面添加逗号  
                    {
                        csvOutput.Append(",");
                    }
                }
                csvOutput.AppendLine(); // End the header row  
            }

            // 导出所有设置了绿色字体的行  
            var dataRows = doc.DocumentNode.SelectNodes("//tbody/tr[@style='color: green;']");
            if (dataRows != null)
            {
                foreach (var row in dataRows)
                {
                    var cells = row.SelectNodes("td");
                    if (cells != null)
                    {
                        for (int i = 0; i < cells.Count; i++)
                        {
                            var cell = cells[i];
                            csvOutput.Append(cell.InnerText.Trim().Replace("\"", "\"\""));
                            if (i < cells.Count - 1) // 不在最后一个单元格后面添加逗号  
                            {
                                csvOutput.Append(",");
                            }
                        }
                        csvOutput.AppendLine(); // End the current data row  
                    }
                }
            }

            // Get the imported file name from ViewState for the exported file name  
            string exportedFileName = ViewState["ImportedFileName"] != null
                ? ViewState["ImportedFileName"].ToString().Replace(".xlsx", ".csv") // Change the extension to .csv  
                : "exported_data.csv"; // Fallback name  

            // 将 CSV 内容写入 Memory Stream  
            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream, Encoding.UTF8))
            {
                writer.Write(csvOutput.ToString());
                writer.Flush();
                stream.Position = 0;

                // 设置响应头以便浏览器下载文件  
                Response.Clear();
                Response.ContentType = "application/csv";
                Response.AddHeader("content-disposition", $"attachment; filename={exportedFileName}");
                Response.BinaryWrite(stream.ToArray());
                Response.End();
            }

            // 清除学校名称，防止下次导出时再次出现重复  
            // Session["SelectedSchoolName"] = null; // 这里可以保留，但其实不需要，因为已经不在导出时使用  
        }
        catch (Exception ex)
        {
            Response.Write($" ");
        }
    }
    
}

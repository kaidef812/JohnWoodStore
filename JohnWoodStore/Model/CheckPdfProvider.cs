using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JohnWoodStore.Model.Database;
using JohnWoodStore.View;
using iTextSharp.text.pdf;
using JohnWoodStore.ViewModel;
using MigraDoc.DocumentObjectModel.Tables;

namespace JohnWoodStore.Model
{
    class CheckPdfProvider
    {
        private readonly Document _document;
        private readonly Section _section;

        public CheckPdfProvider()
        {
            _document = new();
            _section = _document.AddSection();
            InitStyleSection();
        }
        private void InitStyleSection()
        {
            _section.PageSetup.PageFormat = PageFormat.B5;
            _section.PageSetup.Orientation = Orientation.Portrait;
            _section.PageSetup.BottomMargin = 10;
            _section.PageSetup.TopMargin = 10;
        }

        public void AddTitle(string title)
        {
            Paragraph paragraph = new Paragraph();
            _section.Add(paragraph);
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Format.Font.Size = 36;
            paragraph.Format.Font.Bold = true;
            paragraph.AddText(title);
        }

        public void AddParagraph(string text)
        {
            Paragraph paragraph = new Paragraph();
            _section.Add(paragraph);
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Format.Font.Size = 20;
            paragraph.AddText(text);
        }

        public void AddFooter(string text)
        {
            Paragraph paragraph = new Paragraph();
            _section.Add(paragraph);
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Format.Font.Size = 14;
            paragraph.AddText(text);
        }

        public void AddTable()
        {
            Table table = new Table();
            table.Rows.Alignment = RowAlignment.Center;
            table.Format.Alignment = ParagraphAlignment.Center;
            table.Format.Font.Size = 16;

            Column column = table.AddColumn(Unit.FromCentimeter(4));
            column.Format.Alignment = ParagraphAlignment.Center;

            table.AddColumn(Unit.FromCentimeter(4));
            table.AddColumn(Unit.FromCentimeter(4));
            table.AddColumn(Unit.FromCentimeter(3));

            Row row = table.AddRow();
            row.Shading.Color = Colors.PaleGoldenrod;
            Cell cell = row.Cells[0];
            cell.AddParagraph("Категория");
            cell = row.Cells[1];
            cell.AddParagraph("Название");
            cell = row.Cells[2];
            cell.AddParagraph("Количество");
            cell = row.Cells[3];
            cell.AddParagraph("Цена");


            foreach (var product in CatalogViewModel.SelectedProducts)
            {
                row = table.AddRow();
                cell = row.Cells[0];
                cell.AddParagraph(product.CategoryName);
                cell = row.Cells[1];
                cell.AddParagraph(product.Name);
                cell = row.Cells[2];
                cell.AddParagraph(product.Count.ToString());
                cell = row.Cells[3];
                cell.AddParagraph((product.GetPrice() * product.Count).ToString());
            }

            _document.LastSection.Add(table);
        }

        public void Save(string fileName)
        {
            string path = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString()).ToString();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);
            renderer.Document = _document;
            renderer.RenderDocument();
            renderer.Save(Path.Combine(path + (fileName + ".pdf")));
        }
    }
}

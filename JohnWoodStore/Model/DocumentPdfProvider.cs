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
    class DocumentPdfProvider
    {
        private readonly Document _document;
        private readonly Section _section;

        public DocumentPdfProvider()
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
            paragraph.Format.Font.Size = 14;
            paragraph.AddText(text);
        }

        public void AddNameOrganizations(string text)
        {
            Paragraph paragraph = new Paragraph();
            _section.Add(paragraph);
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.Format.Font.Size = 16;
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

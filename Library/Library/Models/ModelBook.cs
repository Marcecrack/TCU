namespace Library.Models
{
    using GoogleModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ModelBook
    {
        #region Attributes
        public bool BookAvailable { get; set; }
        public int BookPageCount { get; set; }
        public Pdf BookPdf { get; set; }
        public string BookAuthor { get; set; }
        public string BookCategory { get; set; }
        public string BookDescription { get; set; }
        public string BookLink { get { return this.BookPdf.acsTokenLink; } }
        public string BookTitle { get; set; }
        #endregion

    }
}

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
        public string BookDescription
        {
            get { return this.BookDescription == null ? "Este libro no cuenta con una descripción" : this.BookDescription; }
            set { this.BookDescription = value; }
        }
        public string BookLink { get; set; }
        public string BookTitle { get; set; }
        #endregion

    }
}

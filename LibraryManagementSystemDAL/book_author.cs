//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace LibraryManagementSystemDAL
{
    public partial class book_author
    {
        #region Primitive Properties
    
        public virtual int id
        {
            get;
            set;
        }
    
        public virtual int authors_id
        {
            get { return _authors_id; }
            set
            {
                if (_authors_id != value)
                {
                    if (author != null && author.id != value)
                    {
                        author = null;
                    }
                    _authors_id = value;
                }
            }
        }
        private int _authors_id;
    
        public virtual int books_id
        {
            get { return _books_id; }
            set
            {
                if (_books_id != value)
                {
                    if (book != null && book.id != value)
                    {
                        book = null;
                    }
                    _books_id = value;
                }
            }
        }
        private int _books_id;

        #endregion
        #region Navigation Properties
    
        public virtual author author
        {
            get { return _author; }
            set
            {
                if (!ReferenceEquals(_author, value))
                {
                    var previousValue = _author;
                    _author = value;
                    Fixupauthor(previousValue);
                }
            }
        }
        private author _author;
    
        public virtual book book
        {
            get { return _book; }
            set
            {
                if (!ReferenceEquals(_book, value))
                {
                    var previousValue = _book;
                    _book = value;
                    Fixupbook(previousValue);
                }
            }
        }
        private book _book;

        #endregion
        #region Association Fixup
    
        private void Fixupauthor(author previousValue)
        {
            if (previousValue != null && previousValue.book_author.Contains(this))
            {
                previousValue.book_author.Remove(this);
            }
    
            if (author != null)
            {
                if (!author.book_author.Contains(this))
                {
                    author.book_author.Add(this);
                }
                if (authors_id != author.id)
                {
                    authors_id = author.id;
                }
            }
        }
    
        private void Fixupbook(book previousValue)
        {
            if (previousValue != null && previousValue.book_author.Contains(this))
            {
                previousValue.book_author.Remove(this);
            }
    
            if (book != null)
            {
                if (!book.book_author.Contains(this))
                {
                    book.book_author.Add(this);
                }
                if (books_id != book.id)
                {
                    books_id = book.id;
                }
            }
        }

        #endregion
    }
}

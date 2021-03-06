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

namespace LibraryManagementSystemDAL.Data
{
    public partial class department
    {
        #region Primitive Properties
    
        public virtual int Id
        {
            get;
            set;
        }
    
        public virtual string Name
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<book> books
        {
            get
            {
                if (_books == null)
                {
                    var newCollection = new FixupCollection<book>();
                    newCollection.CollectionChanged += Fixupbooks;
                    _books = newCollection;
                }
                return _books;
            }
            set
            {
                if (!ReferenceEquals(_books, value))
                {
                    var previousValue = _books as FixupCollection<book>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixupbooks;
                    }
                    _books = value;
                    var newValue = value as FixupCollection<book>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixupbooks;
                    }
                }
            }
        }
        private ICollection<book> _books;
    
        public virtual ICollection<student> students
        {
            get
            {
                if (_students == null)
                {
                    var newCollection = new FixupCollection<student>();
                    newCollection.CollectionChanged += Fixupstudents;
                    _students = newCollection;
                }
                return _students;
            }
            set
            {
                if (!ReferenceEquals(_students, value))
                {
                    var previousValue = _students as FixupCollection<student>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixupstudents;
                    }
                    _students = value;
                    var newValue = value as FixupCollection<student>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixupstudents;
                    }
                }
            }
        }
        private ICollection<student> _students;

        #endregion
        #region Association Fixup
    
        private void Fixupbooks(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (book item in e.NewItems)
                {
                    item.department = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (book item in e.OldItems)
                {
                    if (ReferenceEquals(item.department, this))
                    {
                        item.department = null;
                    }
                }
            }
        }
    
        private void Fixupstudents(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (student item in e.NewItems)
                {
                    item.department = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (student item in e.OldItems)
                {
                    if (ReferenceEquals(item.department, this))
                    {
                        item.department = null;
                    }
                }
            }
        }

        #endregion
    }
}

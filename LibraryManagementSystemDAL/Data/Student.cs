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
    public partial class student
    {
        #region Primitive Properties
    
        public virtual string RegistrationID
        {
            get;
            set;
        }
    
        public virtual string FirstName
        {
            get;
            set;
        }
    
        public virtual string LastName
        {
            get;
            set;
        }
    
        public virtual string Email
        {
            get;
            set;
        }
    
        public virtual string Telephone
        {
            get;
            set;
        }
    
        public virtual string Address
        {
            get;
            set;
        }
    
        public virtual System.DateTime DateOfBirth
        {
            get;
            set;
        }
    
        public virtual System.DateTime EnrolmentDate
        {
            get;
            set;
        }
    
        public virtual Nullable<bool> CurrentlyRegistered
        {
            get;
            set;
        }
    
        public virtual int SexId
        {
            get { return _sexId; }
            set
            {
                if (_sexId != value)
                {
                    if (sex != null && sex.Id != value)
                    {
                        sex = null;
                    }
                    _sexId = value;
                }
            }
        }
        private int _sexId;
    
        public virtual int DepartmentId
        {
            get { return _departmentId; }
            set
            {
                if (_departmentId != value)
                {
                    if (department != null && department.Id != value)
                    {
                        department = null;
                    }
                    _departmentId = value;
                }
            }
        }
        private int _departmentId;
    
        public virtual int Id
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual department department
        {
            get { return _department; }
            set
            {
                if (!ReferenceEquals(_department, value))
                {
                    var previousValue = _department;
                    _department = value;
                    Fixupdepartment(previousValue);
                }
            }
        }
        private department _department;
    
        public virtual sex sex
        {
            get { return _sex; }
            set
            {
                if (!ReferenceEquals(_sex, value))
                {
                    var previousValue = _sex;
                    _sex = value;
                    Fixupsex(previousValue);
                }
            }
        }
        private sex _sex;
    
        public virtual ICollection<studentbookloan> studentbookloans
        {
            get
            {
                if (_studentbookloans == null)
                {
                    var newCollection = new FixupCollection<studentbookloan>();
                    newCollection.CollectionChanged += Fixupstudentbookloans;
                    _studentbookloans = newCollection;
                }
                return _studentbookloans;
            }
            set
            {
                if (!ReferenceEquals(_studentbookloans, value))
                {
                    var previousValue = _studentbookloans as FixupCollection<studentbookloan>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixupstudentbookloans;
                    }
                    _studentbookloans = value;
                    var newValue = value as FixupCollection<studentbookloan>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixupstudentbookloans;
                    }
                }
            }
        }
        private ICollection<studentbookloan> _studentbookloans;

        #endregion
        #region Association Fixup
    
        private void Fixupdepartment(department previousValue)
        {
            if (previousValue != null && previousValue.students.Contains(this))
            {
                previousValue.students.Remove(this);
            }
    
            if (department != null)
            {
                if (!department.students.Contains(this))
                {
                    department.students.Add(this);
                }
                if (DepartmentId != department.Id)
                {
                    DepartmentId = department.Id;
                }
            }
        }
    
        private void Fixupsex(sex previousValue)
        {
            if (previousValue != null && previousValue.students.Contains(this))
            {
                previousValue.students.Remove(this);
            }
    
            if (sex != null)
            {
                if (!sex.students.Contains(this))
                {
                    sex.students.Add(this);
                }
                if (SexId != sex.Id)
                {
                    SexId = sex.Id;
                }
            }
        }
    
        private void Fixupstudentbookloans(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (studentbookloan item in e.NewItems)
                {
                    item.student = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (studentbookloan item in e.OldItems)
                {
                    if (ReferenceEquals(item.student, this))
                    {
                        item.student = null;
                    }
                }
            }
        }

        #endregion
    }
}

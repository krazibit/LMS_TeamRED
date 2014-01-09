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
    public partial class user_role
    {
        #region Primitive Properties
    
        public virtual int id
        {
            get;
            set;
        }
    
        public virtual string description
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<system_users> system_users
        {
            get
            {
                if (_system_users == null)
                {
                    var newCollection = new FixupCollection<system_users>();
                    newCollection.CollectionChanged += Fixupsystem_users;
                    _system_users = newCollection;
                }
                return _system_users;
            }
            set
            {
                if (!ReferenceEquals(_system_users, value))
                {
                    var previousValue = _system_users as FixupCollection<system_users>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixupsystem_users;
                    }
                    _system_users = value;
                    var newValue = value as FixupCollection<system_users>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixupsystem_users;
                    }
                }
            }
        }
        private ICollection<system_users> _system_users;

        #endregion
        #region Association Fixup
    
        private void Fixupsystem_users(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (system_users item in e.NewItems)
                {
                    item.user_role = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (system_users item in e.OldItems)
                {
                    if (ReferenceEquals(item.user_role, this))
                    {
                        item.user_role = null;
                    }
                }
            }
        }

        #endregion
    }
}
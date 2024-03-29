﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsConstatcs
{
    public partial class ContactDetails : Form
    {

        private BusinessLogicLayer _businessLogicLayer;
        private Contacts _contact;
        public ContactDetails()
        {
            InitializeComponent();
            _businessLogicLayer = new BusinessLogicLayer();
        }

        #region EVENTS
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveContact();
            this.Close();
            ((Main)this.Owner).PopulateContacts();
        }

        #endregion

        #region PUBLIC METHOD

        public void LoadContact(Contacts contacts)
        {
            _contact = contacts;
            if(contacts != null)
            {
                ClearForm();

                txtFirstName.Text = contacts.FirstName;
                txtLastName.Text = contacts.LastName;
                txtPhone.Text = contacts.Phone;
                txtAddress.Text = contacts.Address;

            }
        }


        #endregion

        #region PRIVATE METHOD

        private void SaveContact()
        {
            Contacts contact = new Contacts();
            contact.FirstName = txtFirstName.Text;
            contact.LastName = txtLastName.Text;
            contact.Phone = txtPhone.Text;
            contact.Address = txtAddress.Text;

            contact.Id = _contact != null ? _contact.Id : 0;
            _businessLogicLayer.SaveContact(contact);
        }

        private void ClearForm()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtAddress.Text = string.Empty;
        }

        #endregion
    }
}

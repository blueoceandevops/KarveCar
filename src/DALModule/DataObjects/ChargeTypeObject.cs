﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataObjects
{
    public class ChargeTypeObject: BaseAuxDataObject
    {
       
        private object numero;
        private object _cuenta;

        public object Numero
        {
            get { return numero; }
            set
            {
                numero =  value;
                OnPropertyChanged("Numero");
            }
        }
        public object Cuenta
        {
            get { return _cuenta; }
            set
            {
                _cuenta = value;
                OnPropertyChanged("Cuenta");
            }
        }
    }
}

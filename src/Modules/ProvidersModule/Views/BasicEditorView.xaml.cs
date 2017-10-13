﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MasterModule.ViewModels;

namespace MasterModule.Views
{
    /// <summary>
    /// Interaction logic for BasicEditorView.xaml
    /// </summary>
    public partial class BasicEditorView : UserControl
    {
        public BasicEditorView()
        {
            InitializeComponent();
        }
        public string Header
        {
            get

            {
                TabViewModelBase tvm = this.DataContext as TabViewModelBase;
                return tvm.Header;

            }

            
        }
    }
}

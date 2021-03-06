﻿using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KarveCar.Utility;
using MahApps.Metro.Controls;
using Syncfusion.Windows.Shared;
using Syncfusion.Windows.Tools.Controls;

namespace KarveCar.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");
            InitializeComponent();
        
          //  this.NewTabControl.TabClosed += NewTabControl_TabClosed;
         //   Navigation.Navigation.Frame = new Frame() {NavigationUIVisibility = NavigationUIVisibility.Hidden};
          //  Navigation.Navigation.Frame.Navigated += SplitViewFrame_OnNavigated;

            // Navigate to the home page.
          //  this.Loaded += (sender, args) => Navigation.Navigation.Navigate(new Uri("Views/MainPage.xaml", UriKind.RelativeOrAbsolute));
        }

       
        private IUnityContainer _container;


        private ICommand TabClosedCommand  { set; get; }
        /// <summary>
        ///  Overrides the base window show to inject unity in the main view model.
        /// </summary>
        public new void Show()
        {

            //this.DataContext = new MainWindowViewModel(this._container);
        //    UserAndDefaultConfig.LoadCurrentUserRibbonTabConfig();

            base.Show();
        }
        public IUnityContainer UnityContainer
        {
            get { return _container; }
            set { _container = value; }

        }


        public void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            //Mensaje de ejemplo
            MessageBox.Show("Aquí va nuestro mensaje de ayuda", "Ayuda", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #region RibbonGroup Drag&Drop
        private void RibbonGroup_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            RibbonGroupDragDrop.RibbonGroup_PreviewMouseMove(sender, e);
        }

        private void RibbonGroup_Drop(object sender, DragEventArgs e)
        {
            RibbonGroupDragDrop.RibbonGroup_Drop(sender, e);
        }
        #endregion

    }
}
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Xceed.Wpf.DataGrid;
using DataRow = System.Data.DataRow;

namespace KarveControls
{
    /// <summary>
    /// Interaction logic for DataSearchTextBox.xaml
    /// </summary>

    public partial class DataSearchTextBox : UserControl, INotifyPropertyChanged
    {

        public static readonly RoutedEvent DataSearchTextBoxChangedEvent =
            EventManager.RegisterRoutedEvent(
                "DataSearchTextBoxChanged",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(DataSearchTextBox));

       
        public class DataSearchTextBoxEventArgs : RoutedEventArgs
        {
            private string _fieldData = "";

            public string FieldData
            {
                get { return _fieldData; }
                set { _fieldData = value; }
            }
            public DataSearchTextBoxEventArgs() : base()
            {

            }
            public DataSearchTextBoxEventArgs(RoutedEvent routedEvent) : base(routedEvent)
            {

            }
        }

        public event RoutedEventHandler DataSearchTextBoxChanged
        {
            add { base.AddHandler(DataSearchTextBoxChangedEvent, value); }
            remove { base.RemoveHandler(DataSearchTextBoxChangedEvent, value); }
        }


        #region LabelTextWidth 
        public readonly static DependencyProperty LabelTextWidthDependencyProperty =
            DependencyProperty.Register(
                "LabelTextWidth",
                typeof(string),
                typeof(DataSearchTextBox),
                new PropertyMetadata(string.Empty, OnLabelTextWidthChange));

        public string LabelTextWidth
        {
          
            get { return (string)GetValue(LabelTextWidthDependencyProperty); }
            set { SetValue(LabelTextWidthDependencyProperty, value); }
        }
        private static void OnLabelTextWidthChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataSearchTextBox control = d as DataSearchTextBox;
            if (control != null)
            {
                control.OnPropertyChanged("LabelTextWidth");
                control.OnLabelTextWidthChanged(e);
            }
        }

        private void OnLabelTextWidthChanged(DependencyPropertyChangedEventArgs e)
        {
            double value = Convert.ToDouble(e.NewValue);
            SearchLabel.Width = value;
        }

        #endregion
        #region CommonPart
        public event PropertyChangedEventHandler PropertyChanged;

        protected string _description;
        protected CommonControl.DataType _dataAllowed;
        protected bool _allowedEmpty;
        protected bool _upperCase;
        protected DataTable _itemSource;
        protected string _DataSearchTextBox = string.Empty;
        protected string _tableName = string.Empty;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }



        #region Description
        public static readonly DependencyProperty DescriptionDependencyProperty =
            DependencyProperty.Register(
                "Description",
                typeof(string),
                typeof(DataSearchTextBox),
                new PropertyMetadata(String.Empty));
        #endregion
        #region DataAllowed
        public static readonly DependencyProperty DataAllowedDependencyProperty =
            DependencyProperty.Register(
                "DataAllowed",
                typeof(CommonControl.DataType),
                typeof(DataSearchTextBox),
                new PropertyMetadata(CommonControl.DataType.Any, OnDataAllowedChange));
        public CommonControl.DataType DataAllowed
        {
            get { return (CommonControl.DataType)GetValue(DataAllowedDependencyProperty); }
            set { SetValue(DataAllowedDependencyProperty, value); }
        }
        private static void OnDataAllowedChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataSearchTextBox control = d as DataSearchTextBox;
            if (control != null)
            {
                control.OnPropertyChanged("DataAllowed");
                control.OnDataAllowedChange(e);
            }
        }
        protected virtual void OnDataAllowedChange(DependencyPropertyChangedEventArgs e)
        {
            CommonControl.DataType type = (CommonControl.DataType)Enum.Parse(typeof(CommonControl.DataType), e.NewValue.ToString());
            DataAllowed = type;
            _dataAllowed = type;
        }
        #endregion
        #region ItemSource

        public static DependencyProperty ItemSourceDependencyProperty
            = DependencyProperty.Register(
                "ItemSourceDependencyProperty",
                typeof(DataTable),
                typeof(DataSearchTextBox),
                new PropertyMetadata(new DataTable(), OnItemSourceChanged));

        public DataTable ItemSource
        {
            get { return (DataTable)GetValue(ItemSourceDependencyProperty); }
            set { SetValue(ItemSourceDependencyProperty, value); }
        }
        private static void OnItemSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataSearchTextBox control = d as DataSearchTextBox;
            if (control != null)
            {
                control.OnPropertyChanged("ItemSource");
                control.OnItemSourceChanged(e);
            }
        }
        protected virtual void OnItemSourceChanged(DependencyPropertyChangedEventArgs e)
        {
            DataTable table = e.NewValue as DataTable;
            this._itemSource = table;
        }


        #endregion
        #region TableName
        public static readonly DependencyProperty DBTableNameDependencyProperty =
            DependencyProperty.Register(
                "TableName",
                typeof(string),
                typeof(DataSearchTextBox),
                new PropertyMetadata(string.Empty, OnTableNameChange));
        private static void OnTableNameChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataSearchTextBox control = d as DataSearchTextBox;
            if (control != null)
            {
                control.OnPropertyChanged("TableName");
                control.OnTableNameChanged(e);
            }
        }
        public string TableName
        {
            get { return (string)GetValue(DBTableNameDependencyProperty); }
            set { SetValue(DBTableNameDependencyProperty, value); }
        }
        protected virtual void OnTableNameChanged(DependencyPropertyChangedEventArgs e)
        {
            _tableName = e.NewValue as string;
        }
        #endregion
        #region UpperCaseChange

        public static readonly DependencyProperty UpperCaseDependencyProperty =
            DependencyProperty.Register(
                "UpperCase",
                typeof(bool),
                typeof(DataSearchTextBox),
                new PropertyMetadata(false, OnUpperCaseChange));

        private static void OnUpperCaseChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataSearchTextBox control = d as DataSearchTextBox;
            if (control != null)
            {
                control.OnPropertyChanged("UpperCase");
                control.OnUpperCaseChanged(e);
            }
        }

        private void OnUpperCaseChanged(DependencyPropertyChangedEventArgs e)
        {
            this.SearchText.Text = this.SearchText.Text.ToUpper();
        }

        public bool UpperCase
        {
            get { return (bool)GetValue(UpperCaseDependencyProperty); }
            set { SetValue(UpperCaseDependencyProperty, value); }
        }
        #endregion
        #region IsReadOnly

        public static readonly DependencyProperty IsReadOnlyDependencyProperty =
            DependencyProperty.Register(
                "IsReadOnly",
                typeof(bool),
                typeof(DataSearchTextBox),
                new PropertyMetadata(false, IsReadOnlyDependencyPropertyChange));

        private static void IsReadOnlyDependencyPropertyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataSearchTextBox control = d as DataSearchTextBox;
            if (control != null)
            {
                control.OnPropertyChanged("IsReadOnly");
                control.OnIsReadOnlyChanged(e);
            }
        }

        private void OnIsReadOnlyChanged(DependencyPropertyChangedEventArgs e)
        {
            bool newValue = Convert.ToBoolean(e.NewValue);
            this.SearchText.IsReadOnly = newValue;
        }

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyDependencyProperty); }
            set { SetValue(IsReadOnlyDependencyProperty, value); }
        }
        #endregion
        #region DataField

        public static DependencyProperty DataFieldDependencyProperty =
            DependencyProperty.Register(
                "DataField",
                typeof(string),
                typeof(DataSearchTextBox),
                new PropertyMetadata(string.Empty, OnDataFieldDependencyProperty));

        public string DataField
        {
            get { return (string)GetValue(DataFieldDependencyProperty); }
            set { SetValue(DataFieldDependencyProperty, value); }
        }
        private static void OnDataFieldDependencyProperty(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataSearchTextBox controlDataField = d as DataSearchTextBox;
            if (controlDataField != null)
            {
                controlDataField.OnPropertyChanged("DataSearchTextBox");
                controlDataField.OnDataSearchTextBoxPropertyChanged(e);
            }
        }
        private void OnDataSearchTextBoxPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            _DataSearchTextBox = e.NewValue as string;
        }
        #endregion
        #region LabelText
        public static readonly DependencyProperty LabelTextDependencyProperty =
            DependencyProperty.Register(
                "LabelText",
                typeof(string),
                typeof(DataSearchTextBox),
                new PropertyMetadata(string.Empty, OnLabelTextChange));

        public string LabelText
        {
            get { return (string)GetValue(LabelTextDependencyProperty); }
            set { SetValue(LabelTextDependencyProperty, value); }
        }
        private static void OnLabelTextChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataSearchTextBox control = d as DataSearchTextBox;
            if (control != null)
            {
                control.OnPropertyChanged("LabelText");
                control.OnLabelTextChanged(e);
            }
        }

        private void OnLabelTextChanged(DependencyPropertyChangedEventArgs e)
        {
            string value = e.NewValue as string;
            SearchLabel.Text = value;
        }

        #endregion
        /*
        #region LabelTextWidth 
        public readonly static DependencyProperty LabelTextWidthDependencyProperty =
            DependencyProperty.Register(
                "LabelTextWidth",
                typeof(string),
                typeof(DataSearchTextBox),
                new PropertyMetadata(string.Empty, OnLabelTextWidthChange));

        public string LabelTextWidth
        {
            get { return (string)GetValue(LabelTextWidthDependencyProperty); }
            set { SetValue(LabelTextWidthDependencyProperty, value); }
        }
        private static void OnLabelTextWidthChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataSearchTextBox control = d as DataSearchTextBox;
            if (control != null)
            {
                control.OnPropertyChanged("LabelTextWidth");
                control.OnLabelTextWidthChanged(e);
            }
        }
        #endregion
    */
        #region LabelVisible

        public readonly static DependencyProperty LabelVisibleDependencyProperty =
            DependencyProperty.Register(
                "LabelVisible",
                typeof(bool),
                typeof(DataSearchTextBox),
                new PropertyMetadata(true, OnLabelVisibleChange));

        public bool LabelVisible
        {
            get { return (bool)GetValue(LabelVisibleDependencyProperty); }
            set { SetValue(LabelVisibleDependencyProperty, value); }
        }

        private static void OnLabelVisibleChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataSearchTextBox control = d as DataSearchTextBox;
            if (control != null)
            {
                control.OnPropertyChanged("LabelVisible");
                control.OnLabelVisibleChanged(e);
            }
        }

        #endregion
        #region AllowedEmpty
        public static readonly DependencyProperty AllowedEmptyDependencyProperty =
            DependencyProperty.Register(
                "AllowedEmpty",
                typeof(bool),
                typeof(DataSearchTextBox),
                new PropertyMetadata(false, OnAllowedEmptyChange));
        public bool AllowedEmpty
        {
            get { return (bool)GetValue(AllowedEmptyDependencyProperty); }
            set { SetValue(AllowedEmptyDependencyProperty, value); }
        }
        private static void OnAllowedEmptyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataSearchTextBox control = d as DataSearchTextBox;
            if (control != null)
            {
                control.OnPropertyChanged("AllowedEmpty");
                control.OnAllowedEmptyChange(e);
            }
        }

        private void OnAllowedEmptyChange(DependencyPropertyChangedEventArgs e)
        {
            _allowedEmpty = Convert.ToBoolean(e.NewValue);
           
        }
        #endregion

        #endregion
        public static readonly DependencyProperty AssistNameDependencyProperty =
            DependencyProperty.Register(
                "AssistName",
                typeof(string),
                typeof(DataSearchTextBox), new PropertyMetadata(string.Empty));


        public static readonly DependencyProperty TextContentDependencyProperty =
            DependencyProperty.Register(
                "TextContent",
                typeof(string),
                typeof(DataSearchTextBox), new PropertyMetadata(string.Empty, OnTextContentChange));

        public readonly static DependencyProperty TextContentWidthDependencyProperty =
            DependencyProperty.Register(
                "TextContentWidth",
                typeof(string),
                typeof(DataSearchTextBox), new PropertyMetadata(string.Empty, OnTextContentWidthChange));

        public static readonly DependencyProperty ButtonImageDependencyProperty =
            DependencyProperty.Register(
                "ButtonImage",
                typeof(string),
                typeof(DataSearchTextBox), new PropertyMetadata(string.Empty, OnButtonImageChange));


        public static readonly DependencyProperty LookupDependencyProperty =
            DependencyProperty.Register(
                "Lookup",
                typeof(Boolean),
                typeof(DataSearchTextBox), new PropertyMetadata(false, OnLookupChanged));

        public static readonly DependencyProperty SourceViewDependencyProperty =
            DependencyProperty.Register(
                "SourceView",
                typeof(DataTable),
                typeof(DataSearchTextBox),
                new PropertyMetadata(new DataTable(), OnSourceTableChanged));

        public static readonly RoutedEvent MagnificerPressEvent =
            EventManager.RegisterRoutedEvent(
                "MagnificerPress",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(DataSearchTextBox));

        public static DependencyProperty AuxDataSearchTextBoxDependencyProperty =
            DependencyProperty.Register(
                "AuxDataField",
                typeof(string),
                typeof(DataSearchTextBox), new PropertyMetadata(string.Empty, OnDataSearchTextBoxAuxDataSearchTextBoxChanged));


        private void OnLabelVisibleChanged(DependencyPropertyChangedEventArgs e)
        {
            bool value = Convert.ToBoolean(e.NewValue);
            if (value)
            {
                this.SearchLabel.Visibility = Visibility.Visible;
            }
            else
            {
                this.SearchLabel.Visibility = Visibility.Hidden;
            }
        }
       
        
        public class MagnificerPressEventArgs : RoutedEventArgs
        {

            public MagnificerPressEventArgs() : base()
            {
            }
            public MagnificerPressEventArgs(RoutedEvent routedEvent) : base(routedEvent)
            {
            }
        }

        public event RoutedEventHandler MagnificerPress
        {
            add { AddHandler(MagnificerPressEvent, value); }
            remove { RemoveHandler(MagnificerPressEvent, value); }
        }

        public ICommand SelectedIndexCommand { set; get; }

        public string ButtonImage
        {
            get { return (string)GetValue(ButtonImageDependencyProperty); }
            set { SetValue(ButtonImageDependencyProperty, value); }
        }
        public Boolean Lookup
        {
            get { return (Boolean)GetValue(LookupDependencyProperty); }
            set { SetValue(LookupDependencyProperty, value); }
        }
        public string AssistName
        {
            get { return (string)GetValue(AssistNameDependencyProperty); }
            set { SetValue(AssistNameDependencyProperty, value); }
        }


      
        public static readonly DependencyProperty LabelWidthDependencyProperty =
            DependencyProperty.Register(
                "LabelWidth",
                typeof(string),
                typeof(DataSearchTextBox), new PropertyMetadata(string.Empty, OnLabelWidthChange));

       // private string ImagePath = @"Controls\KarveControls"
        // magnifier pressed or not pressed state
        private int _state = 0;
        // name ofthe the field
        private string _auxDataSearchTextBox = String.Empty;
        private DataTable _sourceView = new DataTable();
        private DataTable _dataTable = new DataTable();
        private DataGridVirtualizingCollectionViewSource _viewData;
      
        public DataSearchTextBox()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
            _viewData = new DataGridVirtualizingCollectionViewSource();
            _viewData.PageSize = 50;
            if (this.SourceView != null)
            {
                _viewData.Source = this.SourceView.DefaultView;
                // _viewData.QueryItems += new EventHandler<QueryItemsEventArgs>(Fetch_QueryItems);
                this.MagnificerGrid.ItemsSource = _viewData.View;
            }
        }
        public string LabelWidth
        {
            get { return (string)GetValue(LabelWidthDependencyProperty); }
            set { SetValue(LabelWidthDependencyProperty, value); }
        }
        public string TextContent
        {
            get { return (string)GetValue(TextContentDependencyProperty); }
            set { SetValue(TextContentDependencyProperty, value); }
        }
        public string TextContentWidth
        {
            get { return (string)GetValue(TextContentWidthDependencyProperty); }
            set { SetValue(TextContentWidthDependencyProperty, value); }
        }
     
        public string AuxDataSearchTextBox
        {
            get { return (string)GetValue(AuxDataSearchTextBoxDependencyProperty); }
            set { SetValue(AuxDataSearchTextBoxDependencyProperty, value); }
        }

        public DataTable SourceView
        {
            get { return (DataTable)GetValue(SourceViewDependencyProperty); }
            set
            {

                SetValue(SourceViewDependencyProperty, value);

                if (_state == 1)
                {
                    this.Popup.IsOpen = true;
                    _state = 0;
                }
            }
        }
        private static void OnSourceTableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataSearchTextBox control = d as DataSearchTextBox;
            if (control != null)
            {
                control.OnPropertyChanged("SourceView");
                control.OnSourceViewPropertyChanged(e);
            }
        }

        private static void OnLookupChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataSearchTextBox control = d as DataSearchTextBox;
            if (control != null)
            {
                control.OnPropertyChanged("Lookup");
                control.OnLookupPropertyChanged(e);
            }
        }

        private void OnLookupPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            bool value = Convert.ToBoolean(e.NewValue);
            if (!value)
            {
                this.PopUpButton.Visibility = Visibility.Hidden;
            }
            else
            {
                this.PopUpButton.Visibility = Visibility.Visible;
            }
        }

        private void OnSourceViewPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                DataTable currentTable = e.NewValue as DataTable;
                _viewData.PageSize = 1000;
                _viewData.Source = currentTable.DefaultView;
                _sourceView = currentTable;
                // _viewData.QueryItems += new EventHandler<QueryItemsEventArgs>(Fetch_QueryItems);
                this.MagnificerGrid.ItemsSource = _viewData.View;
            }
        }

        private static void OnButtonImageChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataSearchTextBox control = d as DataSearchTextBox;
            if (control != null)
            {
                control.OnPropertyChanged("ButtonImage");
                control.OnButtonImageChange(e);
            }
        }
        private void OnButtonImageChange(DependencyPropertyChangedEventArgs e)
        {
            string value = e.NewValue as string;
            if (value != null)
            {
                Uri uriSource = new Uri(value, UriKind.Relative);
                this.PopUpButtonImage.Source = new BitmapImage(uriSource);
            }
        }
        private void RaiseMagnificerPressEvent()
        {
            MagnificerPressEventArgs args = new MagnificerPressEventArgs(MagnificerPressEvent);
            this._state = 1;
            RaiseEvent(args);
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.Popup.IsOpen = false;
        }

        private void PopUpButton_OnClick(object sender, RoutedEventArgs e)
        {
            Binding bind = new Binding();

            bind.Source = this.SourceView;

            MagnificerGrid.SetBinding(Xceed.Wpf.DataGrid.DataGridControl.ItemsSourceProperty, bind);

            this.Popup.IsOpen = true;
            RaiseMagnificerPressEvent();
        }

        
        private static void OnLabelWidthChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataSearchTextBox control = d as DataSearchTextBox;
            if (control != null)
            {
                control.OnPropertyChanged("LabelWidth");
                control.OnLabelTextPropertyChanged(e);
            }
        }


        public void SetDynamicBinding(ref DataTable dta, IList<ValidationRule> rules)
        {
            string field = _DataSearchTextBox.ToUpper();
            if (!string.IsNullOrEmpty(field))
            {
                Binding oBind = new Binding("Text");
                oBind.Source = dta.Columns[field];
                oBind.Mode = BindingMode.TwoWay;
                oBind.ValidatesOnDataErrors = true;
                oBind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                if (rules != null)
                {
                    foreach (ValidationRule rule in rules)
                    {
                        oBind.ValidationRules.Add(rule);
                    }
                }
                this.SearchText.Width = dta.Columns[field].MaxLength;
                SearchText.SetBinding(TextBox.TextProperty, oBind);
            }
        }
        private static void OnTextContentChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataSearchTextBox control = d as DataSearchTextBox;
            if (control != null)
            {
                control.OnPropertyChanged("TextContent");
                control.OnTextContentPropertyChanged(e);
            }

        }

        private static void OnTextContentWidthChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataSearchTextBox control = d as DataSearchTextBox;
            if (control != null)
            {
                control.OnPropertyChanged("TextContentWidth");
                control.OnTextContentWidthPropertyChanged(e);
            }

        }
        private void OnTextContentWidthPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            SearchText.Width = Convert.ToDouble(e.NewValue);
        }
        private void OnTextContentPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            SearchText.Text = e.NewValue as string;
        }
        private void OnLabelTextPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            SearchLabel.Text = e.NewValue as string;
        }

        private static void OnDataSearchTextBoxItemSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataSearchTextBox control = d as DataSearchTextBox;
            if (control != null)
            {
                control.OnPropertyChanged("ItemSource");
                control.OnItemSourcePropertyChanged(e);
            }

        }

        private void OnItemSourcePropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            DataTable dt = e.NewValue as DataTable;
            _dataTable = dt;
        }
        private static void OnDataSearchTextBoxDataSearchTextBoxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataSearchTextBox control = d as DataSearchTextBox;
            if (control != null)
            {
                control.OnPropertyChanged("DataSearchTextBox");
                control.OnDataSearchTextBoxPropertyChanged(e, true);
            }
        }
        private static void OnDataSearchTextBoxAuxDataSearchTextBoxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataSearchTextBox control = d as DataSearchTextBox;
            if (control != null)
            {
                control.OnPropertyChanged("AuxDataSearchTextBox");
                control.OnDataSearchTextBoxPropertyChanged(e, false);
            }
        }

        private void OnDataSearchTextBoxPropertyChanged(DependencyPropertyChangedEventArgs e, bool value)
        {
            if (value)
            {
                this._DataSearchTextBox = e.NewValue as string;
            }
            else
            {

                this._auxDataSearchTextBox = e.NewValue as string;
            }
        }

        private void MagnificerGrid_OnSelectionChanged(object sender, DataGridSelectionChangedEventArgs e)
        {
            Xceed.Wpf.DataGrid.DataGridControl gridControl = sender as Xceed.Wpf.DataGrid.DataGridControl;
            DataRowView currentRowView = gridControl.SelectedItem as DataRowView;
            DataRow row = currentRowView.Row;
            DataColumnCollection cols = row.Table.Columns;
            string columnName = "";
            foreach (DataColumn col in cols)
            {
                if (col.ColumnName == _auxDataSearchTextBox)
                {
                    columnName = _auxDataSearchTextBox;
                    break;
                }
            }
            if (!string.IsNullOrEmpty(columnName))
            {
                this.SearchText.Text = row[columnName] as string;
            }

        }

        
    }
}

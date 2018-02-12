﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using KarveControls.Generic;
using Xceed.Wpf.Toolkit;
using System.Text.RegularExpressions;

namespace KarveControls
{
    [TemplatePart(Name = "PART_AreaTitle", Type = typeof(GroupBox))]
    [TemplatePart(Name = "PART_SearchTerm", Type = typeof(TextBox))]
    [TemplatePart(Name = "PART_TextField", Type = typeof(TextBox))]
    [TemplatePart(Name = "PART_SearchButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_DataAreaText", Type = typeof(TextBlock))]
    [TemplatePart(Name = "PART_EditorText", Type = typeof(MultiLineTextEditor))]
    public class DataArea: TextBox
    {
        private double _dataAreaWidth;
        private string _dataAreaLabel;
        private bool _dataAreaChanged = false;
        private object _itemSource;
        private string _DataArea = string.Empty;
        private string _previousDataArea = string.Empty;
        private MultiLineTextEditor _editorText;
        private Button _searchButton;

        static DataArea()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DataArea), new FrameworkPropertyMetadata(typeof(DataArea)));
        }
        /// <summary>
        /// Routed Event fired when any change in data field happen.
        /// </summary>
        public static readonly RoutedEvent DataAreaChangedEvent =
            EventManager.RegisterRoutedEvent(
                "DataAreaChanged",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(DataArea));

        /// <summary>
        ///  Command to be executed when  a data aread changes
        /// </summary>
        public static DependencyProperty ItemChangedCommandDependencyProperty = DependencyProperty.
            Register("ItemChangedCommand",
                typeof(ICommand),
                typeof(DataArea));

        /// <summary>
        ///  Event triggered when a data field changes.
        /// </summary>
        public ICommand ItemChangedCommand
        {
            get { return (ICommand)GetValue(ItemChangedCommandDependencyProperty); }
            set { SetValue(ItemChangedCommandDependencyProperty, value); }
        }

        /// <summary>
        ///  Event triggered when a data field changes.
        /// </summary>
        public string SearchTerm
        {
            get { return (string)GetValue(SearchTermDependencyProperty); }
            set { SetValue(SearchTermDependencyProperty, value); }
        }
        /// <summary>
        ///  Dependency property used for storing a search term.
        /// </summary>
        public static DependencyProperty SearchTermDependencyProperty = DependencyProperty.
            Register("SearchTerm",
                typeof(string),
                typeof(DataArea));


        /// <summary>
        ///  Data area.
        /// </summary>
        public DataArea()
        {
            
            _editorText = GetTemplateChild("PART_EditorText") as MultiLineTextEditor;
            if (_editorText != null)
            {
                _editorText.LostFocus += EditorTextOnLostFocus;
                _editorText.GotFocus += EditorTextOnGotFocus;
            }
            _searchButton = GetTemplateChild("PART_SearchButton") as Button;
            if (_searchButton != null)
            {
                _searchButton.Click += OnSearchTerm;
            }
        }


        private void OnSearchTerm(object sender, RoutedEventArgs routedEventArgs)
        {
            MultiLineTextEditor editorText = GetTemplateChild("PART_EditorText") as MultiLineTextEditor;
            if (editorText != null)
            {

                var termToFind = GetValue(SearchTermDependencyProperty) as string;
                if (termToFind != null)
                {
                    Match match = Regex.Match(editorText.Text, termToFind);
                    if (match.Success)
                    {
                        
                    }
                }

            }
        }

        /// <summary>
        ///  Event triggered when a data field changes.
        /// </summary>
        public event RoutedEventHandler DataAreaChanged
        {
            add { AddHandler(DataAreaChangedEvent, value); }
            remove { RemoveHandler(DataAreaChangedEvent, value); }
        }
        #region ItemSource
        /// <summary>
        ///  Item source dependency property.
        /// </summary>
        public static DependencyProperty ItemSourceDependencyProperty
            = DependencyProperty.Register(
                "DataSource",
                typeof(object),
                typeof(DataArea),
                new PropertyMetadata(null, OnItemSourceChanged));

        /// <summary>
        ///  Data source for the area.
        /// </summary>
        public object DataSource
        {
            get { return GetValue(ItemSourceDependencyProperty); }
            set { SetValue(ItemSourceDependencyProperty, value); }
        }


        private static void OnItemSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataArea control = d as DataArea;
            if (control != null)
            {
                control.OnItemSourceChanged(e);
            }
        }
        private void OnItemSourceChanged(DependencyPropertyChangedEventArgs e)
        {
            this._itemSource = e.NewValue;
            CheckAndAssignText(DataSource, DataSourcePath);
        }


        #endregion

        #region DataAreaTitle
        public static readonly DependencyProperty DataAreaTitleDependencyProperty =
            DependencyProperty.Register(
                "DataAreaTitle",
                typeof(string),
                typeof(DataArea),
                new PropertyMetadata(string.Empty, OnDataAreaTitleChange));
        private static void OnDataAreaTitleChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataArea control = d as DataArea;
            if (control != null)
            {
                control.OnDateAreaTitleChanged(e);
            }
        }
        /// <summary>
        ///  Title of the data area.
        /// </summary>
        public string DataAreaTitle
        {
            get { return (string)GetValue(DataAreaTitleDependencyProperty); }
            set { SetValue(DataAreaTitleDependencyProperty, value); }
        }
        private void OnDateAreaTitleChanged(DependencyPropertyChangedEventArgs e)
        {
            GroupBox groupBox = GetTemplateChild("PART_AreaTitle") as GroupBox;
            if (groupBox != null)
            {
                groupBox.Header = e.NewValue;
            }
        }

        #endregion

        #region DataArea
        /// <summary>
        ///  Path of the control.
        /// </summary>
        public static DependencyProperty DataAreaDependencyProperty =
            DependencyProperty.Register(
                "DataSourcePath",
                typeof(string),
                typeof(DataArea),
                new PropertyMetadata(string.Empty, OnDataAreaChanged));

        /// <summary>
        ///  Path of an area to be binded.
        /// </summary>
        public string DataSourcePath
        {
            get { return (string)GetValue(DataAreaDependencyProperty); }
            set { SetValue(DataAreaDependencyProperty, value); }
        }
        private static void OnDataAreaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataArea controlDataArea = d as DataArea;
            if (controlDataArea != null)
            {
                controlDataArea.OnDataAreaPropertyChanged(e);
            }
        }
        /// <summary>
        /// CheckAndAssignText.
        /// </summary>
        /// <param name="sourceNew">Property to be assigned</param>
        /// <param name="path">Path of the property.</param>
        private void CheckAndAssignText(object sourceNew, string path)
        {
            // first try without the value part
            string propValue = ComponentUtils.GetPropValue(sourceNew, path) as string;
            if (string.IsNullOrEmpty(propValue))
            {
                path = "Value." + path;
                propValue = ComponentUtils.GetPropValue(sourceNew, path) as string;
                if (!string.IsNullOrEmpty(propValue))
                {
                    TextContent = propValue;
                }
            }
            else
            {
                TextContent = propValue;
            }

        }
        private void OnDataAreaPropertyChanged(DependencyPropertyChangedEventArgs e)
        {

            string path = e.NewValue as string;
            if (!string.IsNullOrEmpty(path))
            {
                CheckAndAssignText(DataSource, path);
            }
        }
        #endregion
        #region LabelText
        /// <summary>
        ///  Label text dependency property.
        /// </summary>
        public static readonly DependencyProperty LabelTextDependencyProperty =
            DependencyProperty.Register(
                "LabelText",
                typeof(string),
                typeof(DataArea),
                new PropertyMetadata(string.Empty, OnLabelTextChange));
        /// <summary>
        ///  Label of the text.
        /// </summary>
        public string LabelText
        {
            get { return (string)GetValue(LabelTextDependencyProperty); }
            set { SetValue(LabelTextDependencyProperty, value); }
        }
        private static void OnLabelTextChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataArea control = d as DataArea;
            if (control != null)
            {
                control.OnLabelTextChanged(e);
            }
        }

        private void OnLabelTextChanged(DependencyPropertyChangedEventArgs e)
        {
            TextBlock block = GetTemplateChild("PART_DataAreaText") as TextBlock;
            if (block != null)
            {
                string value = e.NewValue as string;
                if (value != null)
                {
                    block.Text = value;
                }
            }
        }

        #endregion
        #region LabelTextWidth 
        public readonly static DependencyProperty LabelTextWidthDependencyProperty =
            DependencyProperty.Register(
                "LabelTextWidth",
                typeof(string),
                typeof(DataArea),
                new PropertyMetadata(string.Empty, OnLabelTextWidthChange));
        /// <summary>
        /// Width of text to be present in the area.
        /// </summary>
        public string LabelTextWidth
        {
            get { return (string)GetValue(LabelTextWidthDependencyProperty); }
            set { SetValue(LabelTextWidthDependencyProperty, value); }
        }
        private static void OnLabelTextWidthChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataArea control = d as DataArea;
            if (control != null)
            {
                control.OnLabelTextWidthChanged(e);
            }
        }

        private void OnLabelTextWidthChanged(DependencyPropertyChangedEventArgs e)
        {
            string value = e.NewValue as string;
            if (value != null)
            {
                DataAreaWidth = Convert.ToDouble(value);
                TextBox searchTerm = GetTemplateChild("PART_SearchTerm") as TextBox;
                if (searchTerm != null)
                {
                    searchTerm.Width = DataAreaWidth - 80;
                }
            }
        }
        #endregion
        #region LabelVisible
        /// <summary>
        ///  Set if the label is visible or not.
        /// </summary>
        public readonly static DependencyProperty LabelVisibleDependencyProperty =
            DependencyProperty.Register(
                "LabelVisible",
                typeof(bool),
                typeof(DataArea),
                new PropertyMetadata(true, OnLabelVisibleChange));
        /// <summary>
        /// Label is 
        /// </summary>
        public bool LabelVisible
        {
            get { return (bool)GetValue(LabelVisibleDependencyProperty); }
            set { SetValue(LabelVisibleDependencyProperty, value); }
        }

        private static void OnLabelVisibleChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataArea control = d as DataArea;
            if (control != null)
            {
                control.OnLabelVisibleChanged(e);
            }
        }

        private void OnLabelVisibleChanged(DependencyPropertyChangedEventArgs e)
        {
            bool isVisible = Convert.ToBoolean(e.NewValue);
            TextBlock block = GetTemplateChild("PART_DataAreaText") as TextBlock;
            if (block != null)
            {
                if (isVisible)
                {
                    block.Visibility = Visibility.Visible;
                }
                else
                {
                    block.Visibility = Visibility.Collapsed;
                }
                string value = e.NewValue as string;
                if (value != null)
                {
                    block.Text = value;
                }
            }
      
        }

        #endregion

        #region SearchBoxHeight
        /// <summary>
        ///  SearchBoxHeight dependency property.
        /// </summary>
        public static readonly DependencyProperty SearchBoxHeightDependencyProperty =
            DependencyProperty.Register(
                "SearchBoxHeight",
                typeof(double),
                typeof(DataArea),
                new PropertyMetadata(30D));
        public double SearchBoxHeight
        {
            get { return (double)GetValue(SearchBoxHeightDependencyProperty); }
            set { SetValue(SearchBoxHeightDependencyProperty, value); }
        }
        #endregion

        #region SearchBoxWidth

        public static readonly DependencyProperty SearchBoxWidthDependencyProperty =
            DependencyProperty.Register(
                "SearchBoxWidth",
                typeof(double),
                typeof(DataArea),
                new PropertyMetadata(150D));
        public double SearchBoxWidth
        {
            get { return (double)GetValue(SearchBoxWidthDependencyProperty); }
            set { SetValue(SearchBoxWidthDependencyProperty, value); }
        }
        #endregion

        /// <summary>
        ///  Text content dependency property
        /// </summary>
        #region TextContent Property
        public static readonly DependencyProperty TextContentDependencyProperty =
            DependencyProperty.Register(
                "TextContent",
                typeof(string),
                typeof(DataArea),
                new PropertyMetadata(string.Empty, OnTextContentChange));

        /// <summary>
        ///  Text content dependency property.
        /// </summary>
        public string TextContent
        {
            get { return (string)GetValue(TextContentDependencyProperty); }
            set { SetValue(TextContentDependencyProperty, value); }
        }
        private static void OnTextContentChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataArea control = d as DataArea;
            if (control != null)
            {
                control.OnTextContentPropertyChanged(e);
            }
        }
        private void OnTextContentPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            string value = e.NewValue as string;
            MultiLineTextEditor editorText = GetTemplateChild("PART_EditorText") as MultiLineTextEditor;
            if ((editorText != null) &&  (value!=null))
            {
                editorText.Text = value;
                _DataArea = value;
            }
        }
        #endregion

        
        private void EditorTextOnGotFocus(object sender, RoutedEventArgs routedEventArgs)
        {
            _dataAreaChanged = false;
            RaiseEvent(routedEventArgs);
        }

        private void EditorTextOnLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_previousDataArea) && (string.IsNullOrEmpty(this._DataArea)))
                return;
            if (_editorText == null)
            {
                _editorText = GetTemplateChild("PART_EditorText") as MultiLineTextEditor;
            }
            if (_editorText == null)
                return;

            // ok 
            _dataAreaChanged = _previousDataArea != _editorText.Text;

            if (_dataAreaChanged)
            {
                _previousDataArea = _DataArea;
                _DataArea = _editorText.Text;
                if (_editorText.Text != null)
                {
                    DataAreaFieldEventsArgs ev = new DataAreaFieldEventsArgs(DataAreaChangedEvent);
                    ev.FieldData = _editorText.Text;
                    ComponentFiller filler = new ComponentFiller();
                    var dataObject = DataSource;

                    filler.FillDataObject(_editorText.Text, DataSourcePath, ref dataObject);
                    DataSource = dataObject;

                    IDictionary<string, object> valueDictionary = new Dictionary<string, object>();

                    valueDictionary["Field"] = DataSourcePath;
                    valueDictionary["DataTable"] = DataSource;
                    valueDictionary["DataObject"] = DataSource;
                    valueDictionary["ChangedValue"] = _editorText.Text;

                    ev.ChangedValuesObjects = valueDictionary;

                    if (ItemChangedCommand != null)
                    {
                        ICommand ex = ItemChangedCommand;
                        if (ex.CanExecute(valueDictionary))
                        {
                            ex.Execute(valueDictionary);
                        }
                    }
                    RaiseEvent(ev);
                    _dataAreaChanged = false;
                }
            }
        }
        /// <summary>
        ///  Width do the data area.
        /// </summary>
        public double DataAreaWidth
        {
            get { return _dataAreaWidth; }
            set
            {
                _dataAreaWidth = value;
                _editorText = GetTemplateChild("PART_EditorText") as MultiLineTextEditor;
                TextBox searchTerm = GetTemplateChild("PART_SearchTerm") as TextBox;
                if (_editorText != null)
                {
                    _editorText.Width = _dataAreaWidth;
                }
                if (searchTerm != null)
                {
                    searchTerm.Width = _dataAreaWidth - 80;
                }

                            }
        }
        /// <summary>
        ///  Label of the area
        /// </summary>
        public string DataAreaLabel
        {
            get
            {
                return _dataAreaLabel;
            }
            set
            {
                _dataAreaLabel = value;
                var label = GetTemplateChild("PART_DataAreaText") as TextBlock;
                if (label != null)
                {
                    label.Width = _dataAreaWidth;
                    label.Text = value;
                }
                
            }
        }

    }
}
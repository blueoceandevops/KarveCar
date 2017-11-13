﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KarveCommon.Services;
using KarveControls;
using MasterModule.Common;
using MasterModule.UIObjects.CommissionAgents;

namespace MasterModule.ViewModels
{
    /// <summary>
    /// CommissionAgentInfoViewModel
    /// </summary>
    public partial class CommissionAgentInfoViewModel : MasterViewModuleBase, IEventObserver
    {

        private ObservableCollection<UiDfSearch> _leftSideDualDfSearchBoxes = new ObservableCollection<UiDfSearch>()
        {
            new UiDfSearch("Vendedor")
            {
                AssistTableName = "VENDEDOR",
                AssistDataFieldFirst = "NUM_VENDE",
                AssistDataFieldSecond = "NOMBRE",
                DataField = "VENDE_COMI",
                DataFieldFirst = "VENDE_COMI",
                TextContentFirstWidth = "50",
                TextContentSecondWidth = "30",
                TableName = "COMISIO",
                AssistProperties = "Numero,Nombre",
                SourceView =  new DataTable(),
                LabelVisible = true,
                ButtonImage = MasterModule.ImagePath,
                IsReadOnlyFirst = true
            } ,
            new UiDfSearch("Mercado")
            {
                AssistTableName = "MERCADO",
                AssistDataFieldFirst = "CODIGO",
                AssistDataFieldSecond = "NOMBRE",
                DataAllowed = ControlExt.DataType.Any,
                DataField = "MERCADO",
                TextContentFirstWidth = "50",
                TextContentSecondWidth = "350",
                AssistProperties = "Codigo,Nombre",
                SourceView = new object(),
                DataSource = new object(),
                LabelVisible = true,
                ButtonImage = MasterModule.ImagePath,
                IsReadOnlySecond = true
            },
            new UiDfSearch("Negocio")
            {
                AssistTableName="NEGOCIO",
                AssistDataFieldFirst = "CODIGO",
                AssistDataFieldSecond = "NOMBRE",
                AssistProperties = "Codigo,Negocio",
                DataAllowed = ControlExt.DataType.Any,
                DataField = "NEGOCIO",
                SourceView = new DataTable(),
                DataSource = new object(),
                TextContentFirstWidth = "50",
                TextContentSecondWidth = "350",
                LabelVisible = true,
                ButtonImage = MasterModule.ImagePath,
                IsReadOnlyFirst = true
            },
            new UiDfSearch("Canal")
            {
                DataField = "CANAL",
                AssistTableName = "CANAL",
                AssistDataFieldFirst = "CODIGO",
                AssistDataFieldSecond = "NOMBRE",
                TextContentFirstWidth = "50",
                TextContentSecondWidth = "350",
                AssistProperties = "Canal,Nombre",
                DataAllowed = ControlExt.DataType.Any,
                SourceView = new DataTable(),
                DataSource = new object(),
                LabelVisible = true,
                ButtonImage = MasterModule.ImagePath,
                IsReadOnlySecond = true
            },
            new UiDfSearch("Clave.Ppto")
            {
                DataField = "CLAVEPPTO",
                AssistTableName = "CLAVEPTO",
                AssistDataFieldFirst = "COD_CLAVE",
                AssistDataFieldSecond = "NOMBRE",
                AssistProperties = "Numero,Nombre",
                DataAllowed = ControlExt.DataType.Any,
                TextContentFirstWidth = "50",
                TextContentSecondWidth = "350",
                SourceView = new DataTable(),
                DataSource = new object(),
                LabelVisible = true,
                ButtonImage = MasterModule.ImagePath,
                IsReadOnlySecond = true
            },
            new UiDfSearch("Origen")
            {
                DataField = "ORIGEN_COMI",
                AssistTableName = "ORIGEN",
                AssistDataFieldFirst = "NUM_ORIGEN",
                AssistDataFieldSecond = "NOMBRE",
                TextContentFirstWidth = "50",
                TextContentSecondWidth = "350",
                DataAllowed = ControlExt.DataType.Any,
                AssistProperties = "Numero,Nombre",
                SourceView =  new DataTable(),
                DataSource = new object(),
                LabelVisible = true,
                ButtonImage = MasterModule.ImagePath,
                IsReadOnlySecond = true
            },
            new UiDfSearch("ZonaOfi")
            {
                DataField = "ZONAOFI",
                AssistTableName = "ZONAOFI",
                AssistDataFieldFirst = "COD_ZONAOFI",
                AssistDataFieldSecond = "NOM_ZONA",
                TextContentFirstWidth = "50",
                TextContentSecondWidth = "300",
                AssistProperties = "Codigo,Nombre",
                SourceView = new DataTable(),
                DataSource = new object(),
                ButtonImage = MasterModule.ImagePath,
                LabelVisible = true,
                IsReadOnlySecond = true
            }
        };
    }
}
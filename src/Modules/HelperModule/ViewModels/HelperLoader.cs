﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KarveCommon.Generic;
using KarveDataServices;

namespace HelperModule.ViewModels
{
    public class HelperLoader<Dto, Entity> where Dto: class
                                           where Entity: class
    {
        private IDataServices _services;
        private ObservableCollection<Dto> _helperView;
        private INotifyTaskCompletion<IEnumerable<Dto>> _initializationNotifier;

        public HelperLoader(IDataServices services)
        {
            _services = services;
        }
        /// <summary>
        ///  Load event.
        /// </summary>
        /// <param name="value"></param>
        public void Load(string value)
        {
            _initializationNotifier = NotifyTaskCompletion.Create(LoadDto(value), null);

        }
        /// <summary>
        ///  Load all the entities.
        /// </summary>
        public void LoadAll()
        {
            _initializationNotifier = NotifyTaskCompletion.Create(LoadDtoAll(), null);
        }
        /// <summary>
        ///  Load Data Transfer All.
        /// </summary>
        /// <returns></returns>
        private async Task<IEnumerable<Dto>> LoadDtoAll()
        {
            var listOfVehicles = await _services.GetHelperDataServices().GetMappedAllAsyncHelper<Dto, Entity>();
            if (listOfVehicles == null)
            {
                return null;
            }
            var value = new ObservableCollection<Dto>(listOfVehicles);
            _helperView = value;
            return value;
        }
        /// <summary>
        ///  HelperView.
        /// </summary>
        public ObservableCollection<Dto> HelperView
        {
            set { _helperView = value; }
            get { return _helperView; }
        }
        /// <summary>
        ///  This load a data transfer object.
        /// </summary>
        /// <param name="query">This load a dto.</param>
        /// <returns></returns>
        private async Task<IEnumerable<Dto>> LoadDto(string query) 
        {
            var listOfVehicles = await _services.GetHelperDataServices()
                .GetMappedAsyncHelper<Dto, Entity>(query);
            if (listOfVehicles == null)
            {
                return null;
            }
            var value = new ObservableCollection<Dto>(listOfVehicles);
            _helperView = value;
            return value;
        }
    } 
}
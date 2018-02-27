﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Crud;
using NUnit.Framework;
using KarveDataServices;
using DataAccessLayer;
using KarveDataServices.DataTransferObject;

namespace KarveTest.DAL.Crud.Office
{
    /// <summary>
    ///  TestOfficeCrud. The crud for the office.
    /// </summary>
    [TestFixture]
    class TestOfficeCrud : TestBase
    {
        private IDataServices _dataServices;
        private ISqlExecutor _sqlExecutor;
        private IDataLoader<OfficeDtos> _officeDataLoader;
        private IDataSaver<OfficeDtos> _officeDataSaver;
        private IDataDeleter<OfficeDtos> _officeDataDeleter;
        private CrudFactory _crudFactory;
        [OneTimeSetUp]
        public void SetUp()
        {
            _dataServices = null;
            try
            {
                _sqlExecutor = SetupSqlQueryExecutor();
                _crudFactory = CrudFactory.GetFactory(_sqlExecutor);
                _officeDataLoader = _crudFactory.GetOfficeLoader();
                _officeDataSaver = _crudFactory.GetOfficeSaver();
                _officeDataDeleter = _crudFactory.GetOfficeDeleter();
                Assert.NotNull(_sqlExecutor);

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [Test]
        public async Task Should_Load_HolidaysDates_Correctly()
        {
            /// arrange
            var loaderOffices = await _officeDataLoader.LoadAsyncAll();
            var office = loaderOffices.FirstOrDefault<OfficeDtos>();
            // act
            var holidays = office.HolidayDates;
            // assert.
            Assert.Greater(holidays.Count<HolidayDto>(), 0);
        }

        [Test]
        public async Task Should_Save_HolidayDates_Corrently()
        {
            // arrange
            var loaderOffices = await _officeDataLoader.LoadAsyncAll();
            var office = loaderOffices.FirstOrDefault<OfficeDtos>();
            // act
            var holidays = office.HolidayDates;
            var holidaysList = holidays.ToList<HolidayDto>();
            HolidayDto holidayDto = Craft_Holiday_Date();
            holidaysList.Add(holidayDto);
            office.HolidayDates = holidaysList;
            var result = await _officeDataSaver.SaveAsync(office);
            Assert.IsTrue(result);
            var entity = await _officeDataLoader.LoadValueAsync(office.Codigo);
            var value = entity.HolidayDates.FirstOrDefault<HolidayDto>(x => (
                                                               (x.FESTIVO == holidayDto.FESTIVO) 
                                                            && (x.OFICINA==holidayDto.OFICINA) &&                                                            (x.PARTE_DIA==holidayDto.PARTE_DIA) && 
                                                               (x.HORA_DESDE== holidayDto.HORA_DESDE) && 
                                                               (x.HORA_HASTA ==holidayDto.HORA_HASTA)                                                            ));
            Assert.NotNull(value);
        }
        /// <summary>
        ///  HolidayDto. This craft an holiday data transfer object.
        /// </summary>
        /// <returns></returns>
        private HolidayDto Craft_Holiday_Date()
        {
            HolidayDto dto = new HolidayDto();
            HolidayDto holidayDto = new HolidayDto();
            holidayDto.FESTIVO = new DateTime(2018, 12, 24);
            holidayDto.HORA_DESDE = new TimeSpan(14, 0, 0);
            holidayDto.HORA_HASTA = new TimeSpan(20, 0, 0);
            holidayDto.PARTE_DIA = 1;
            return holidayDto;
        }
        [Test]
        public async Task Should_Create_A_NewOffice_Per_Company()
        {
            // arrange: delete the office if exists
            OfficeDtos dto = new OfficeDtos();
            dto.Codigo = "09";
            var holiday = new List<HolidayDto>();
            holiday.Add(Craft_Holiday_Date());
            dto.HolidayDates = holiday;
            bool value =  await _officeDataDeleter.DeleteAsync(dto);
            Assert.IsTrue(value);
            // act: now we act to receive an office.
            value = await _officeDataSaver.SaveAsync(dto);
            // assert: now we assert to get correctly and office.
            Assert.IsTrue(value);
        }
        [Test]
        public async Task Should_Update_An_Office_Detail()
        {
            // arrange
            var loaderOffices = await _officeDataLoader.LoadAsyncAll();
            var office = loaderOffices.FirstOrDefault<OfficeDtos>();
            office.Nombre = "HPEnterprise";
            // act
            bool value = await _officeDataSaver.SaveAsync(office);
            Assert.IsTrue(value);
            // assert

        }
        [Test]
        public async Task Should_Handle_Exception_Correctly_While_Loading()
        {

        }
    }
}
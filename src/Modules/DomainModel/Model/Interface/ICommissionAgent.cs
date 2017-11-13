﻿using System.Collections.Generic;
using System.Threading.Tasks;
using KarveDataServices.DataTransferObject;

namespace Model.Interface
{
    /// <summary>
    /// The commission interface. It is a wrapper over the pocos.
    /// 
    /// </summary>
    public interface ICommissionAgent
    {
        /// <summary>
        ///   This is a property to verify if the domain object commission agent is correctly loaded.
        /// </summary>
        bool Valid { set; get; }
        /// <summary>
        ///  Data transfer object for province
        /// </summary>
        IEnumerable<ProvinciaDto> ProvinceDto { set; get; }
        /// <summary>
        ///  Data transfer objects for contancts
        /// </summary>
        IEnumerable<ContactsDto> ContactsDto { set; get; }
        /// <summary>
        ///  Data Transfer object for delegations.
        /// </summary>
        IEnumerable<BranchesDto> DelegationDto { set; get; }
        /// <summary>
        /// This return the underlying database object COMISIO.
        /// We use this for avoiding any extensive wrapping of the object COMISIO.
        /// The object COMISIO is generated directly from the table.
        /// We dont use automapper or dto for the value of the table.
        ///  </summary>
        object Value { set; get; }
        /// <summary>
        /// Country Data Transfer Object
        /// </summary>
        IEnumerable<CountryDto> CountryDto { get; set; }
        /// <summary>
        /// Products Data Transfer Object
        /// </summary>
        IEnumerable<ProductsDto> ProductsDto { get; set; }
        /// <summary>
        ///  Language Data Transfer Object
        /// </summary>
        IEnumerable<LanguageDto> LanguageDto { get; set; }
        /// <summary>
        /// Commission Type Dtaa Transfer Object.
        /// </summary>
        IEnumerable<CommissionTypeDto> CommisionTypeDto { get; set; }
        /// <summary>
        /// Clients Data Transfer Object.
        /// </summary>
        
        IEnumerable<VendedorDto> VendedorDto { get; set; }
        /// <summary>
        /// Mercado Data Transfer Object
        /// </summary>
        IEnumerable<MercadoDto> MercadoDto { get; set; }
        /// <summary>
        ///  Negocio Data Transfer Object
        /// </summary>
        IEnumerable<NegocioDto> NegocioDto { get; set; }
        /// <summary>
        ///  Canal Data Transfer Object
        /// </summary>
        IEnumerable<CanalDto> CanalDto { get; set; }
        /// <summary>
        ///  Clave Data Transfer Object
        /// </summary>
        IEnumerable<ClavePtoDto> ClavePptoDto { get; set; }
        /// <summary>
        /// Clientes data transfer object.
        /// </summary>
        IEnumerable<ClientesDto> ClientsDto { get; set; }
        /// <summary>
        /// Origen data transfer object.
        /// </summary>
        IEnumerable<OrigenDto> OrigenDto { get; set; }
        /// <summary>
        /// Cliente data transfer object.
        /// </summary>
        IEnumerable<ZonaOfiDto> ZonaOfiDto { get; set; }
        /// <summary>
        ///  This load the value for the current commission agent.
        /// </summary>
        /// <param name="commissionDictionary">Fields to be loaded</param>
        /// <param name="commissionId">Identifier to be loaded. Primary Key</param>
        /// <returns>Returns the result of loading.</returns>
        Task<bool> LoadValue(IDictionary<string, string> commissionDictionary, string commissionId);
        /// <summary>
        ///  This save a new generated item.
        /// </summary>
        /// <returns>Return true if the new generated item a fresh one is saved.</returns>
        Task<bool> Save();
        /// <summary>
        /// This saves the changes of a commission item. Use this method if it is appropriate to do an update. 
        /// </summary>
        Task<bool> SaveChanges();
        /// <summary>
        ///  This method deletes all data asynchronous for the commission agent.
        /// </summary>
        /// <returns>Return delete or an exception if the data are deleted correctly</returns>
        Task<bool> DeleteAsyncData();
    }
}
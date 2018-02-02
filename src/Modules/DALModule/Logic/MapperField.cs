﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using AutoMapper;
using DataAccessLayer.DataObjects;
using KarveDataServices.DataTransferObject;
using System.Reflection;

namespace DataAccessLayer.Logic
{
    /// <summary>
    /// POCO to Dto converter for the provice domain object
    /// </summary>
    public class ProvinciaConverter : ITypeConverter<PROVINCIA, ProvinciaDto>
    {
        public ProvinciaDto Convert(PROVINCIA source, ProvinciaDto destination, ResolutionContext context)
        {
            ProvinciaDto prov = new ProvinciaDto();
            prov.Code = source.SIGLAS;
            prov.Name = source.PROV;
            prov.Capital = source.CAPITAL;
            prov.Prefix = source.PREFIJO;
            prov.Country = source.PAIS;
            prov.CountryValue = new CountryDto();
            // TODO: avoid this replication
            prov.CountryValue.CountryCode = source.PAIS;
            prov.CountryValue.Code = source.PAIS;
            return prov;
        }
    }

    /// <summary>
    /// Drto converter for the provice domain object
    /// </summary>
    public class ProvinciaConverterToPOCO : ITypeConverter<ProvinciaDto, PROVINCIA>
    {
        /// <summary>
        ///  Provincia converter
        /// </summary>
        /// <param name="source">Convert the provincia</param>
        /// <param name="destination"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public PROVINCIA Convert(ProvinciaDto source, PROVINCIA destination, ResolutionContext context)
        {
            PROVINCIA prov = new PROVINCIA();
            prov.SIGLAS = source.Code;
            prov.PROV = source.Name;
            prov.CAPITAL = source.Capital;
            prov.PREFIJO = source.Prefix;
            prov.PAIS = source.CountryValue.Code;
            return prov;
        }
    }

    /// <summary>
    /// POCO to Dto converter for the country domain object
    /// </summary>
    public class CountryConverter : ITypeConverter<Country, CountryDto>
    {
        public CountryDto Convert(Country source, CountryDto destination, ResolutionContext context)
        {
            CountryDto dto = new CountryDto();
            dto.Code = source.SIGLAS;
            dto.CountryName = source.PAIS;
            dto.Language = source.IDIOMA_PAIS;
            if (source.INTRACO.HasValue)
            {
                var value = source.INTRACO.Value;
                dto.IsIntraco = value == 1;
            }
            return dto;
        }
    }
    /// <summary>
    /// Dto to POCO  converter for the country domain object
    /// </summary>
    public class Country2PocoConverter : ITypeConverter<CountryDto, Country>
    {
        public Country Convert(CountryDto source, Country destination, ResolutionContext context)
        {
            Country dto = new Country();
            dto.SIGLAS = source.Code;
            dto.PAIS = source.CountryName;
            dto.IDIOMA_PAIS = source.Language;
            if (source.IsIntraco)
            {
                dto.INTRACO = 1;
            }
            return dto;
        }
    }
    /// <summary>
    /// POCO to Dto converter for the commission contact domain object
    /// </summary>
    public class ContactsConverter : ITypeConverter<ContactsComiPoco, ContactsDto>
    {
        public ContactsDto Convert(ContactsComiPoco source, ContactsDto destination, ResolutionContext context)
        {
            ContactsDto contactsDto = new ContactsDto();
            contactsDto.ContactsKeyId = source.COMISIO;
            contactsDto.ContactId = source.CONTACTO.ToString();
            contactsDto.ContactName = source.NOM_CONTACTO;
            contactsDto.Nif = source.NIF;
            contactsDto.Responsability = source.percargos.NOMBRE;
            contactsDto.Telefono = source.TELEFONO;
            contactsDto.Movil = source.MOVIL;
            contactsDto.Fax = source.FAX;
            contactsDto.Email = source.EMAIL;
            contactsDto.User = source.USUARIO;
            contactsDto.LastMod = source.ULTMODI;
            contactsDto.CurrentDelegation = source.comiDelega.cldDelegacion;
            return contactsDto;

        }
    }
    /// <summary>
    /// POCO to Dto converter for the commission branches
    /// </summary>
    public class BranchesConverter : ITypeConverter<ComiDelegaPoco, BranchesDto>
    {
        public BranchesDto Convert(ComiDelegaPoco source, BranchesDto destination, ResolutionContext context)
        {
            BranchesDto branchesDto = new BranchesDto();
            branchesDto.BranchId = source.cldIdDelega.ToString();
            branchesDto.Branch = source.cldDelegacion;
            branchesDto.Province.Code = source.PROV.SIGLAS;
            branchesDto.Province.Name = source.PROV.PROV;
            branchesDto.Province.Country = source.PROV.PAIS;
            branchesDto.Address = source.cldDireccion1;
            branchesDto.Address2 = source.cldDireccion2;
            branchesDto.Phone = source.cldTelefono1;
            branchesDto.Phone2 = source.cldTelefono2;
            branchesDto.Email = source.cldEMail;
            branchesDto.City = source.cldPoblacion;
            return branchesDto;
        }
    }

    public class ExtendedProContactsConvert : ITypeConverter<ProContact, ProContactos>
    {
        public ProContactos Convert(ProContact source, ProContactos destination, ResolutionContext context)
        {
            ProContactos proContactos = new ProContactos();
            proContactos.ccoCargo = source.Cargo;
            proContactos.ccoDepto = source.Dipartimento;
            proContactos.ccoFax = source.Fax;
            proContactos.ULTMODI = source.UltimaModifica;
            proContactos.ccoIdCliente = source.ccoIdCliente;
            proContactos.ccoMail = source.Email;
            proContactos.ccoMovil = source.Movil;
            proContactos.ccoFax = source.Fax;
            proContactos.ccoTelefono = source.Telefono;
            return proContactos;
        }
    }
    public class ContactToProContactosConverter : ITypeConverter<ContactsDto, ProContactos>
    {
        public ProContactos Convert(ContactsDto source, ProContactos destination, ResolutionContext context)
        {
            ProContactos contacts = new ProContactos();
            contacts.ccoFax = source.Fax;
            if (source.ContactName == null)
            {
                source.ContactName = String.Empty;
            }
            contacts.ccoContacto = source.ContactName;
            contacts.ccoIdContacto = source.ContactId;
            contacts.ccoIdDelega = source.CurrentDelegation;
            contacts.ccoTelefono = source.Telefono;
            contacts.ccoMail = source.Email;
            contacts.ccoMovil = source.Movil;
            contacts.ccoCargo = source.Responsability;
            contacts.ccoIdCliente = source.ContactsKeyId;

            return contacts;

        }
    }
    public class ProContactsConverter : ITypeConverter<ProContactos, ContactsDto>
    {
        public ContactsDto Convert(ProContactos source, ContactsDto destination, ResolutionContext context)
        {
            ContactsDto contacts = new ContactsDto();
            contacts.ContactId = source.ccoIdContacto;
            contacts.ContactName = source.ccoContacto;

            /*
             branchesDto.BranchId = source.cldIdDelega.ToString();
             branchesDto.Branch = source.cldDelegacion;
             branchesDto.Province.Code = source.PROV.SIGLAS;
             branchesDto.Province.Name = source.PROV.PROV;
             branchesDto.Province.Country = source.PROV.PAIS;
             branchesDto.Address = source.cldDireccion1;
             branchesDto.Address2 = source.cldDireccion2;
             branchesDto.Phone = source.cldTelefono1;
             branchesDto.Phone2 = source.cldTelefono2;
             branchesDto.Email = source.cldEMail;
             branchesDto.City = source.cldPoblacion;
            */
            return contacts;

        }
    }
    /// <summary>
    /// POCO to Dto converter for the office zones domain object
    /// </summary>
    public class ZonaOfiConverter : ITypeConverter<ZONAOFI, ZonaOfiDto>
    {
        public ZonaOfiDto Convert(ZONAOFI source, ZonaOfiDto destination, ResolutionContext context)
        {
            ZonaOfiDto ofiDto = new ZonaOfiDto();
            ofiDto.Codigo = source.COD_ZONAOFI;
            ofiDto.Nombre = source.NOM_ZONA;
            ofiDto.Plaza = source.PLAZA;
            return ofiDto;
        }
    }
    /// <summary>
    /// POCO to Dto converter for the products domain object
    /// </summary>
    public class ProductsConverter : ITypeConverter<PRODUCTS, ProductsDto>
    {

        public ProductsDto Convert(PRODUCTS source, ProductsDto destination, ResolutionContext context)
        {
            ProductsDto destinationDto = new ProductsDto();
            destinationDto.Codigo = source.CODIGO_PRD;
            destinationDto.Nombre = source.NOMBRE_PRD;
            destinationDto.Observacion = source.OBS_PRD;
            return destinationDto;
        }
    }
    /// <summary>
    /// POCO to Dto converter for the market domain object
    /// </summary>
    public class MercadoConverter : ITypeConverter<MERCADO, MercadoDto>
    {

        public MercadoDto Convert(MERCADO source, MercadoDto destination, ResolutionContext context)
        {
            MercadoDto destinationDto = new MercadoDto();
            destinationDto.Code = source.CODIGO;
            destinationDto.Name = source.NOMBRE;
            return destinationDto;
        }
    }
    /// <summary>
    /// POCO to Dto converter for the market domain object
    /// </summary>
    public class Poco2MercadoConverter : ITypeConverter<MercadoDto, MERCADO>
    {

        public MERCADO Convert(MercadoDto source, MERCADO destination, ResolutionContext context)
        {
            MERCADO destinationDto = new MERCADO();
            destinationDto.CODIGO = source.Code;
            destinationDto.NOMBRE = source.Name;
            destinationDto.ULTMODI = source.LastModification;
            destinationDto.USUARIO = source.User;
            return destinationDto;
        }
    }

    /// <summary>
    /// Merge two entities.
    /// </summary>
    public class MergePOCO<Entity> where Entity : class, new()
    {
        public static string EntityName = "POCOToMerge";

        public Entity Convert(Entity source, IDictionary<string, Entity> context)
        {
            Entity entity = new Entity();
            PropertyInfo[] currentProperties = source.GetType().GetProperties();
            foreach (PropertyInfo property in currentProperties)
            {
                var currentSource = source.GetType();
                var propertyInfo = currentSource.GetProperty(property.Name);
                if (propertyInfo != null)
                {
                    var tmpValue = propertyInfo.GetValue(source);
                    if (tmpValue != null)
                    {
                        entity.GetType().GetProperty(property.Name).SetValue(entity, tmpValue);
                    }

                }

            }
            // add the second pococ
            var secondPoco = context[EntityName];
            if (secondPoco == null)
            {
                return entity;
            }
            PropertyInfo[] currentProperties2 = secondPoco.GetType().GetProperties();

            foreach (PropertyInfo property in currentProperties2)
            {
                var currentSource = secondPoco.GetType();
                var propertyInfo = currentSource.GetProperty(property.Name);
                if (property.Name == "ALTA")
                {
                    var v = 1;
                }
                if (propertyInfo != null)
                {
                    var tmpValue = propertyInfo.GetValue(secondPoco);
                    if (tmpValue != null)
                    {
                        entity.GetType().GetProperty(property.Name).SetValue(entity, tmpValue);
                    }

                }

            }
            return entity;
        }
    }

    /// <summary>
        ///  Converter for the the new vehiculo2.
        /// </summary>
        public class GenericBackConverter<Dto, Entity> : ITypeConverter<Dto, Entity> where Entity : class, new()
    {
        public Entity Convert(Dto source, Entity destination, ResolutionContext context)
        {
            Entity e = new Entity();
            PropertyInfo[] currentProperties = e.GetType().GetProperties();
            foreach (PropertyInfo property in currentProperties)
            {
                var currentSource = source.GetType();
                var propertyInfo = currentSource.GetProperty(property.Name);
                if (propertyInfo != null)
                {
                    var tmpValue = propertyInfo.GetValue(source);
                    e.GetType().GetProperty(property.Name).SetValue(e, tmpValue);
                }

            }
            return e;
        }
    }

    /// <summary>
    /// Convert the generic conversion.
    /// </summary>
    public class GenericConverter<Dto, Entity> : ITypeConverter<Dto, Entity> where Entity : class, new()
    {

        public Entity Convert(Dto source, Entity destination, ResolutionContext context)
        {
            Entity e = new Entity();
            Type dtoSourceType = source.GetType();
            PropertyInfo[] currentProperties = e.GetType().GetProperties();
            for (int i = 0; i < currentProperties.Length; ++i)
            {
                // destination property
                var prop = currentProperties[i];
                // source Value
                var sourceValueProperty = dtoSourceType.GetProperty(prop.Name);
                if (sourceValueProperty != null)
                {
                    var destinationProperty = e.GetType().GetProperty(prop.Name);
                    var tmpValue = sourceValueProperty.GetValue(source);
                    if (tmpValue != null)
                    {
                        destinationProperty?.SetValue(e, tmpValue);
                    }
                }
            }
            return e;
        }
    }
    /// POCO to Dto converter for the client domain object to merge both.
    public class ClientToClientes1 : ITypeConverter<CLIENTES1, ClientesDto>
    {
        public ClientesDto Convert(CLIENTES1 source, ClientesDto destination, ResolutionContext context)
        {
            GenericConverter<CLIENTES1, ClientesDto> gc = new GenericConverter<CLIENTES1, ClientesDto>();
            var v = gc.Convert(source, destination, context);
            return v;
        }
    }
    /// POCO to Dto converter for the client domain object to merge both.
    public class ClientToClientes2 : ITypeConverter<CLIENTES2, ClientesDto>
    {
        public ClientesDto Convert(CLIENTES2 source, ClientesDto destination, ResolutionContext context)
        {
            GenericConverter<CLIENTES2, ClientesDto> gc = new GenericConverter<CLIENTES2, ClientesDto>();
            var v = gc.Convert(source, destination, context);
            return v;
        }
    }
    ///
    /// Dto to POCO converter.
    /// 
    public class ClientDtoToClientes1 : ITypeConverter<ClientesDto, CLIENTES1>
    {
        public CLIENTES1 Convert(ClientesDto source, CLIENTES1 destination, ResolutionContext context)
        {
            var entityConverter = new GenericBackConverter<ClientesDto, CLIENTES1>();
            var entity = entityConverter.Convert(source, destination, context);
            return entity;
        }
    }
    ///
    /// Dto to POCO converter.
    /// 
    public class ClientDtoToClientes2 : ITypeConverter<ClientesDto, CLIENTES2>
    {
        public CLIENTES2 Convert(ClientesDto source, CLIENTES2 destination, ResolutionContext context)
        {
            var entityConverter = new GenericBackConverter<ClientesDto, CLIENTES2>();
            var entity = entityConverter.Convert(source, destination, context);
            return entity;
        }
    }

    /// <summary>
    /// POCO to Dto converter for the client domain object
    /// </summary>
    public class ClientesConverter : ITypeConverter<CLIENTES1, ClientesDto>
    {
        public ClientesDto Convert(CLIENTES1 source, ClientesDto destination, ResolutionContext context)
        {
            ClientesDto clientesDto = new ClientesDto();
            clientesDto.Numero = source.NUMERO_CLI;
            clientesDto.Nombre = source.NOMBRE;
            clientesDto.Movil = source.MOVIL;
            return clientesDto;
        }
    }
    /// <summary>
    /// POCO to Dto converter for the canal domain object
    /// </summary>
    public class CanalConverter : ITypeConverter<CANAL, ChannelDto>
    {
        public ChannelDto Convert(CANAL source, ChannelDto destination, ResolutionContext context)
        {
            ChannelDto dto = new ChannelDto();
            dto.Code = source.CODIGO;
            dto.Name = source.NOMBRE;
            return dto;
        }
    }
    /// <summary>
    /// POCO to Dto converter for the business shop domain object
    /// </summary>
    public class NegocioConverter : ITypeConverter<NEGOCIO, BusinessDto>
    {
        public BusinessDto Convert(NEGOCIO source, BusinessDto destination, ResolutionContext context)
        {
            BusinessDto businessDto = new BusinessDto();
            businessDto.Code = source.CODIGO;
            businessDto.Name = source.NOMBRE;
            return businessDto;
        }
    }
    /// <summary>
    /// POCO to Dto converter for the reseller domain object
    /// </summary>
    public class VendedorConverter : ITypeConverter<VENDEDOR, ResellerDto>
    {
        public ResellerDto Convert(VENDEDOR source, ResellerDto destination, ResolutionContext context)
        {
            ResellerDto resellerDto = new ResellerDto();
            resellerDto.Code = source.NUM_VENDE;
            resellerDto.Name = source.NOMBRE;
            resellerDto.Nif = source.NIF;
            resellerDto.City = new CityDto();
            resellerDto.City.Poblacion = source.POBLACION;
            resellerDto.City.Code = source.CP;
            resellerDto.Country = new CountryDto();
            resellerDto.Country.Code = source.NACIOPER;
            resellerDto.Direction = source.DIRECCION;
            resellerDto.Province = new ProvinciaDto();
            resellerDto.Province.Code = source.PROVINCIA;
            resellerDto.Fax = source.FAX;
            resellerDto.Email = source.EMAIL;
            resellerDto.CellPhone = source.MOVIL;
            resellerDto.StartDate = source.FECHALTA;
            resellerDto.LeaveDate = source.FECHABAJA;
            resellerDto.Web = source.WEB;
            return resellerDto;
        }
    }
    /// <summary>
    /// ContactsCOMI and mapper field.
    /// </summary>
    public class ContactsComi : ITypeConverter<ContactsDto, CONTACTOS_COMI>
    {
        public CONTACTOS_COMI Convert(ContactsDto source, CONTACTOS_COMI destination, ResolutionContext context)
        {
            string idComi = "0";
            CONTACTOS_COMI comiContact = new CONTACTOS_COMI();
            IEnumerable<ContactsComiPoco> comiPoco = null;
            if (context.Items.ContainsKey("RefId"))
            {
                idComi = context.Items["RefId"] as string;
            }
            if (context.Items.ContainsKey("POCO"))
            {
                comiPoco = context.Items["POCO"] as IEnumerable<ContactsComiPoco>;
            }
            int contactIdentifier = Int32.Parse(source.ContactId);
            ContactsComiPoco contacts = comiPoco.FirstOrDefault(c => c.CONTACTO == contactIdentifier);
            comiContact.COMISIO = idComi;
            comiContact.NOM_CONTACTO = source.ContactName;
            comiContact.EMAIL = source.Email;
            comiContact.NOM_CONTACTO = source.ContactName;
            comiContact.DELEGA_CC = contacts?.DELEGA_CC;
            comiContact.FAX = source.Fax;
            comiContact.CARGO = contacts?.CARGO;
            comiContact.USUARIO = source.User;
            comiContact.ULTMODI = DateTime.Now.ToString();
            comiContact.NIF = source.Nif;
            comiContact.MOVIL = source.Movil;
            comiContact.FAX = source.Fax;
            comiContact.EMAIL = source.Email;
            return comiContact;
        }
    }
    /// <summary>
    /// POCO to Dto converter for the origen object
    /// </summary>
    public class OrigenConverter : ITypeConverter<ORIGEN, OrigenDto>
    {
        public OrigenDto Convert(ORIGEN source, OrigenDto destination, ResolutionContext context)
        {
            OrigenDto origenDto = new OrigenDto();
            origenDto.Code = source.NUM_ORIGEN;
            origenDto.Name = source.NOMBRE;
            return origenDto;
        }
    }
    /// <summary>
    /// POCO to Dto converter for the language domain object
    /// </summary>
    public class LanguageConverter : ITypeConverter<IDIOMAS, LanguageDto>
    {
        public LanguageDto Convert(IDIOMAS source, LanguageDto destination, ResolutionContext context)
        {
            LanguageDto languageDto = new LanguageDto();
            languageDto.Codigo = source.CODIGO.ToString();
            languageDto.Nombre = source.NOMBRE;

            return languageDto;
        }
    }
    /// <summary>
    /// POCO to Dto converter for the key ppto object
    /// </summary>
    public class ClavePtoConverter : ITypeConverter<CLAVEPTO, ClavePtoDto>
    {
        public ClavePtoDto Convert(CLAVEPTO source, ClavePtoDto destination, ResolutionContext context)
        {
            ClavePtoDto clavePto = new ClavePtoDto();
            clavePto.Numero = source.COD_CLAVE;
            clavePto.Nombre = source.NOMBRE;
            return clavePto;
        }
    }
    /// <summary>
    /// POCO to Dto converter for the commission type object
    /// </summary>
    public class TipoCommisionConverter : ITypeConverter<TIPOCOMI, CommissionTypeDto>
    {
        public CommissionTypeDto Convert(TIPOCOMI source, CommissionTypeDto destination, ResolutionContext context)
        {
            CommissionTypeDto commisiDto = new CommissionTypeDto();
            commisiDto.Codigo = source.NUM_TICOMI;
            commisiDto.Nombre = source.NOMBRE;
            return commisiDto;
        }
    }

    /// <summary>
    ///  Dto to poco converter
    /// </summary>
    public class BranchesToComiDelega : ITypeConverter<BranchesDto, COMI_DELEGA>
    {
        public COMI_DELEGA Convert(BranchesDto source, COMI_DELEGA destination, ResolutionContext context)
        {
            string idComi = "";
            if (context.Items.ContainsKey("RefId"))
            {
                idComi = context.Items["RefId"] as string;
            }

            COMI_DELEGA dest = new COMI_DELEGA();
            dest.cldDelegacion = source.Branch;
            dest.cldDireccion1 = source.Address;
            dest.cldDireccion2 = source.Address2;

            dest.cldEMail = source.Email;
            if (string.IsNullOrEmpty(idComi))
            {
                dest.cldIdCOMI = source.BranchKeyId;
            }
            else
            {
                dest.cldIdCOMI = idComi;
            }
            if (source.Province != null)
            {
                dest.cldIdProvincia = source.Province.Code;
            }
            dest.cldPoblacion = source.City;
            dest.cldIdDelega = int.Parse(source.BranchId);
            return dest;
        }
    }
    /// <summary>
    /// POCO to Dto converter for the visits object
    /// </summary>
    public class VisitaCommissionConverter : ITypeConverter<VisitasComiPoco, VisitsDto>
    {

        public VisitsDto Convert(VisitasComiPoco source, VisitsDto destination, ResolutionContext context)
        {
            VisitsDto visitsDto = new VisitsDto();
            visitsDto.ClientId = source.visIdCliente;
            visitsDto.ContactId = source.visIdContacto;
            visitsDto.ContactName = source.ContactsComi.NOM_CONTACTO;
            visitsDto.Date = source.visFecha;
            visitsDto.SellerId = source.VendedorComi.NOMBRE;
            visitsDto.VisitId = source.visIdVisita;
            visitsDto.VisitType = source.TipoVisitas.NOMBRE_VIS;
            return visitsDto;
        }
    }
    /// <summary>
    ///  ContactosComiConverter
    /// </summary>
    public class ContactosComiConverter : ITypeConverter<ContactsComiPoco, CONTACTOS_COMI>
    {
        public CONTACTOS_COMI Convert(ContactsComiPoco source, CONTACTOS_COMI destination, ResolutionContext context)
        {
            CONTACTOS_COMI comi = new CONTACTOS_COMI();
            comi.COMISIO = source.COMISIO;
            return comi;

        }
    }

    public class ActivityConverter : ITypeConverter<ACTIVI, ActividadDto>
    {
        public ActividadDto Convert(ACTIVI source, ActividadDto destination, ResolutionContext context)
        {
            ActividadDto actividad = new ActividadDto();
            actividad.Codigo = source.NUM_ACTIVI;
            actividad.Nombre = source.NOMBRE;
            return actividad;

        }
    }

    public class BrandVehicle2Poco : ITypeConverter<BrandVehicleDto, MARCAS>
    {
        public MARCAS Convert(BrandVehicleDto source, MARCAS destination, ResolutionContext context)
        {
            MARCAS marcas = new MARCAS();
            marcas.CODIGO = source.Code;
            marcas.NOMBRE = source.Name;
            marcas.CONDICIONES = source.Terms;
            marcas.FBAJA = source.LeaveDate;
            marcas.FECHA = source.CurrentDate;
            marcas.OBS = source.Observation;
            marcas.ULTMODI = source.LastModification;
            marcas.USUARIO = source.User;
            marcas.PROVEE = source.Supplier.NUM_PROVEE;
            return marcas;
        }
    }
    public class Poco2BrandVehicle : ITypeConverter<MARCAS, BrandVehicleDto>
    {
        public BrandVehicleDto Convert(MARCAS source, BrandVehicleDto destination, ResolutionContext context)
        {
            BrandVehicleDto brandVehicleDto = new BrandVehicleDto();
            brandVehicleDto.Code = source.CODIGO;
            brandVehicleDto.CurrentDate = source.FECHA;
            brandVehicleDto.Name = source.NOMBRE;
            brandVehicleDto.Observation = source.OBS;
            brandVehicleDto.LastModification = source.ULTMODI;
            brandVehicleDto.User = source.USUARIO;
            return brandVehicleDto;
        }
    }
    /// <summary>
    ///  Singleton class for the conversion from transfer object to dto.
    /// </summary>
    public static class MapperField
    {
        private static IMapper Instance = null;

        /// <summary>
        ///  Get the map if instantiated otherwise build one.
        /// </summary>
        /// <returns>Map to be built.</returns>
        public static IMapper GetMapper()
        {
            if (Instance == null)
            {
                Instance = BuildMapping();
            }
            return Instance;
        }

        private static IMapper BuildMapping()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TIPOCOMI, CommissionTypeDto>().ConvertUsing(new TipoCommisionConverter());
                cfg.CreateMap<PROVINCIA, ProvinciaDto>().ConvertUsing(new ProvinciaConverter());
                cfg.CreateMap<ProvinciaDto, PROVINCIA>().ConvertUsing(new ProvinciaConverterToPOCO());
                cfg.CreateMap<Country, CountryDto>().ConvertUsing(new CountryConverter());
                cfg.CreateMap<CountryDto, Country>().ConvertUsing(new Country2PocoConverter());
                cfg.CreateMap<OFICINAS, OfficeDtos>().ConvertUsing(new OfficeConverter());
                cfg.CreateMap<ZONAOFI, ZonaOfiDto>().ConvertUsing(new ZonaOfiConverter());
                cfg.CreateMap<PRODUCTS, ProductsDto>().ConvertUsing(new ProductsConverter());
                cfg.CreateMap<MERCADO, MercadoDto>().ConvertUsing(new MercadoConverter());
                cfg.CreateMap<MercadoDto, MERCADO>().ConvertUsing(new Poco2MercadoConverter());
                //cfg.CreateMap<CLIENTES1, ClientesDto>().ConvertUsing(new ClientesConverter());
                cfg.CreateMap<NEGOCIO, BusinessDto>().ConvertUsing(new NegocioConverter());
                cfg.CreateMap<VENDEDOR, ResellerDto>().ConvertUsing(new VendedorConverter());
                cfg.CreateMap<ORIGEN, OrigenDto>().ConvertUsing(new OrigenConverter());
                cfg.CreateMap<CLAVEPTO, ClavePtoDto>().ConvertUsing(new ClavePtoConverter());
                cfg.CreateMap<IDIOMAS, LanguageDto>().ConvertUsing(new LanguageConverter());
                cfg.CreateMap<ContactsDto, ProContactos>().ConvertUsing(new ContactToProContactosConverter());
                cfg.CreateMap<VisitasComiPoco, VisitsDto>().ConvertUsing(new VisitaCommissionConverter());
                cfg.CreateMap<ComiDelegaPoco, BranchesDto>().ConvertUsing(new BranchesConverter());
                cfg.CreateMap<BranchesDto, COMI_DELEGA>().ConvertUsing(new BranchesToComiDelega());
                cfg.CreateMap<ContactsComiPoco, ContactsDto>().ConvertUsing(new ContactsConverter());
                cfg.CreateMap<ContactsDto, CONTACTOS_COMI>().ConvertUsing(new ContactsComi());
                cfg.CreateMap<ACTIVI, ActividadDto>().ConvertUsing(new ActivityConverter());
                cfg.CreateMap<MARCAS, BrandVehicleDto>().ConvertUsing(new Poco2BrandVehicle());
                cfg.CreateMap<CLIENTES1, ClientesDto>().ConvertUsing(new ClientToClientes1());
                cfg.CreateMap<CLIENTES2, ClientesDto>().ConvertUsing(new ClientToClientes2());
                cfg.CreateMap<ClientesDto, CLIENTES1>().ConvertUsing(new ClientDtoToClientes1());
                cfg.CreateMap<ClientesDto, CLIENTES2>().ConvertUsing(new ClientDtoToClientes2());
                cfg.CreateMap<ACTIVI, ActividadDto>().ConvertUsing(new ActivityConverter());
                cfg.CreateMap<BrandVehicleDto, MARCAS>().ConvertUsing(new BrandVehicle2Poco());
                cfg.CreateMap<TIPOCOMI, CommissionTypeDto>().ConvertUsing(src =>
                {
                    var tipoComi = new CommissionTypeDto();
                    tipoComi.Codigo = src.NUM_TICOMI;
                    tipoComi.Nombre = src.NOMBRE;
                    tipoComi.LastModification = src.ULTMODI;
                    tipoComi.User = src.USUARIO;
                    return tipoComi;
                });
                cfg.CreateMap<MARCAS, BrandVehicleDto>().ConvertUsing(src =>
                {
                    var marcas = new BrandVehicleDto
                    {
                        Code = src.CODIGO,
                        Name = src.NOMBRE,
                        LastModification = src.ULTMODI,
                        User = src.USUARIO
                    };
                    return marcas;
                });
                cfg.CreateMap<SITUACION, CurrentSituationDto>().ConvertUsing(src =>
                {
                    var marcas = new CurrentSituationDto()
                    {
                        Code = src.NUMERO,
                        Name = src.NOMBRE,
                        LastModification = src.ULTMODI,
                        User = src.USUARIO
                    };
                    return marcas;
                });
                cfg.CreateMap<CurrentSituationDto, SITUACION>().ConvertUsing(src =>
                {
                    var marcas = new SITUACION()
                    {
                        NUMERO = src.Code,
                        NOMBRE = src.Name,
                        ULTMODI = src.LastModification,
                        USUARIO = src.User
                    };
                    return marcas;
                });

                cfg.CreateMap<MODELO, ModelVehicleDto>().ConvertUsing(src =>
                {
                    var vehicle = new ModelVehicleDto
                    {
                        Codigo = src.CODIGO,
                        Variante = src.VARIANTE,
                        Nombre = src.NOMBRE,
                        Marca = src.MARCA,
                        NomeMarca = src.NOMMARCA,
                        Categoria = src.CATEGORIA
                    };
                    return vehicle;
                });
                cfg.CreateMap<GRUPOS, VehicleGroupDto>().ConvertUsing(
                    src =>
                    {
                        var grupos = new VehicleGroupDto()
                        {
                            Codigo = src.CODIGO,
                            Nombre = src.NOMBRE
                        };
                        return grupos;
                    });
                cfg.CreateMap<TIPOPROVE, SupplierTypeDto>().ConvertUsing(src =>
                {
                    var tipoComi = new SupplierTypeDto();
                    tipoComi.Codigo = src.NUM_TIPROVE;
                    tipoComi.Nombre = src.NOMBRE;
                    return tipoComi;
                });
                cfg.CreateMap<TIPOPROVE, SupplierTypeDto>().ConvertUsing(src =>
                {
                    var model = new SupplierTypeDto();
                    model.Codigo = src.NUM_TIPROVE;
                    model.Nombre = src.NOMBRE;
                    model.UltimaModifica = src.ULTMODI;
                    model.Usuario = src.USUARIO;
                    return model;
                });
                cfg.CreateMap<SupplierTypeDto, TIPOPROVE>().ConvertUsing(src =>
                {
                    var model = new TIPOPROVE();
                    model.NOMBRE = src.Nombre;
                    model.NUM_TIPROVE = src.Codigo;
                    model.USUARIO = src.Usuario;
                    model.ULTMODI = src.UltimaModifica;
                    return model;
                });
                cfg.CreateMap<GRID_SERIALIZATION, GridSettingsDto>().ConvertUsing(src =>
                {
                    var model = new GridSettingsDto();
                    model.GridIdentifier = src.GRID_ID;
                    model.GridName = src.GRID_NAME;
                    if (src.SERILIZED_DATA != null)
                    {
                        model.XmlBase64 = src.SERILIZED_DATA;
                    }
                    return model;
                });
                cfg.CreateMap<GridSettingsDto, GRID_SERIALIZATION>().ConvertUsing(src =>
                {
                    var model = new GRID_SERIALIZATION();
                    model.GRID_ID = src.GridIdentifier;
                    model.GRID_NAME = src.GridName;
                    model.SERILIZED_DATA = src.XmlBase64;
                    return model;
                });

                cfg.CreateMap<VEHI_ACC, VehicleToolDto>().ConvertUsing(src =>
                {
                    var model = new VehicleToolDto();
                    model.Name = src.NOM_ACC;
                    model.Code = src.COD_ACC.ToString();
                    return model;
                });
                cfg.CreateMap<VehicleToolDto, VEHI_ACC>().ConvertUsing(src =>
                {
                    var model = new VEHI_ACC();
                    model.COD_ACC = Convert.ToInt32(src.Code);
                    model.NOM_ACC = src.Name;
                    return model;
                });
                cfg.CreateMap<ACTIVEHI, VehicleActivitiesDto>().ConvertUsing(src =>
                {
                    var model = new VehicleActivitiesDto();
                    model.Code = src.NUM_ACTIVEHI;
                    model.Activity = src.NOMBRE;
                    model.LastModification = src.ULTMODI;
                    model.User = src.USUARIO;
                    model.ActivitySigle = src.SIGLAS_ACT;
                    var c = Convert.ToInt16(src.CALCULO);
                    model.Compute = (c == 0);
                    model.ActityAlquiler = src.ACTIVI_ALQ;
                    model.Assurance = src.SEGURO_ANUAL;

                    return model;
                });
                cfg.CreateMap<ClientPoco, ClientesDto>();

                cfg.CreateMap<CLIENTES1, ClientPoco>().ConvertUsing(src =>
                {
                    var clientPoco = new GenericConverter<CLIENTES1, ClientPoco>();
                    return clientPoco.Convert(src, null, null);
                });
                cfg.CreateMap<CLIENTES2, ClientPoco>().ConvertUsing(src =>
                {
                    var clientPoco = new GenericConverter<CLIENTES2, ClientPoco>();
                    return clientPoco.Convert(src, null, null);
                });
                cfg.CreateMap<VehicleActivitiesDto, ACTIVEHI>().ConvertUsing(src =>
                {
                    var model = new ACTIVEHI();
                    model.NOMBRE = src.Activity;
                    model.CALCULO = src.Compute.ToString();
                    model.ACTIVI_ALQ = src.ActityAlquiler;
                    model.ULTMODI = src.LastModification;
                    model.USUARIO = src.User;
                    model.SEGURO_ANUAL = src.Assurance;
                    model.SIGLAS_ACT = src.ActivitySigle;

                    return model;
                });
                cfg.CreateMap<VehicleToolDto, VEHI_ACC>().ConvertUsing(src =>
                {
                    var model = new VEHI_ACC();
                    model.COD_ACC = Convert.ToInt32(src.Code);
                    model.NOM_ACC = src.Name;
                    return model;
                });

                cfg.CreateMap<COLORFL, ColorDto>().ConvertUsing(src =>
                {
                    var model = new ColorDto();
                    model.Code = src.CODIGO;
                    model.Name = src.NOMBRE;
                    var colorType = src.TIPOCOLOR;
                    model.NoCoating = colorType == "S";
                    model.PowderCoating = colorType == "M";
                    model.TwoTone = colorType == "B";
                    return model;
                });
                cfg.CreateMap<ColorDto, COLORFL>().ConvertUsing(src =>
                {
                    var model = new COLORFL();
                    model.CODIGO = src.Code;
                    model.NOMBRE = src.Name;
                    string type = "";
                    if (src.NoCoating)
                    {
                        type = "S";
                    }
                    if (src.PowderCoating)
                    {
                        type = "M";
                    }
                    if (src.TwoTone)
                    {
                        type = "B";
                    }
                    model.TIPOCOLOR = type;
                    return model;
                });
                cfg.CreateMap<EXTRASVEHI, VehicleExtraDto>().ConvertUsing(src =>
                {
                    var model = new VehicleExtraDto();
                    model.Name = src.NOMBRE;
                    model.Code = src.CODIGO.ToString();
                    model.LastModification = src.ULTMODI;
                    model.User = src.USUARIO;
                    model.Reference = src.REFERENCIA;
                    model.Notes = src.OBS;
                    model.VehicleType = new VehicleTypeDto();
                    model.VehicleType.Code = src.CODIGO.ToString();
                    return model;
                });
                cfg.CreateMap<VehicleExtraDto, EXTRASVEHI>().ConvertUsing(src =>
                {
                    var model = new EXTRASVEHI();
                    model.CODIGO = Convert.ToInt32(src.Code);
                    model.NOMBRE = src.Name;
                    model.ULTMODI = src.LastModification;
                    model.USUARIO = src.User;
                    model.REFERENCIA = src.Reference;
                    model.OBS = src.Notes;
                    model.TIPO_VEHI = src.VehicleType.Code;
                    return model;
                });

                cfg.CreateMap<USO_ALQUILER, RentingUseDto>().ConvertUsing(src =>
                {
                    var model = new RentingUseDto();
                    model.Name = src.NOMBRE;
                    model.Code = src.CODIGO.ToString();
                    model.LastModification = src.ULTMODI;
                    model.User = src.USUARIO;
                    return model;
                });
                cfg.CreateMap<RentingUseDto, USO_ALQUILER>().ConvertUsing(src =>
                {
                    var model = new USO_ALQUILER();
                    model.CODIGO = Convert.ToByte(src.Code);
                    model.NOMBRE = src.Name;
                    model.ULTMODI = src.LastModification;
                    model.USUARIO = src.User;
                    return model;
                });
                cfg.CreateMap<ResellerDto, VENDEDOR>().ConvertUsing(src =>
                {

                    var model = new VENDEDOR();
                    model.NUM_VENDE = src.Code;
                    model.NOMBRE = src.Name;
                    model.ULTMODI = src.LastModification;
                    model.USUARIO = src.User;
                    model.FECHALTA = src.StartDate;
                    model.FECHABAJA = src.LeaveDate;
                    model.CP = src.City.Code;
                    model.POBLACION = src.City.Poblacion;
                    model.PROVINCIA = src.Province.Code;
                    model.EMAIL = src.Email;
                    model.MOVIL = src.CellPhone;
                    model.DIRECCION = src.Direction;
                    model.NIF = src.Nif;
                    model.OBS1 = src.Observation;
                    model.NOTAS = src.Notes;
                    model.FAX = src.Fax;
                    model.WEB = src.Web;

                    return model;
                });
                cfg.CreateMap<OrigenDto, ORIGEN>().ConvertUsing(src =>
                {
                    var model = new ORIGEN();
                    model.NUM_ORIGEN = src.Code;
                    model.NOMBRE = src.Name;
                    model.ULTMODI = src.LastModification;
                    model.USUARIO = src.User;
                    return model;
                });
                cfg.CreateMap<CreditCardDto, TARCREDI>().ConvertUsing(src =>
                {
                    var model = new TARCREDI();
                    model.CODIGO = src.Code;
                    model.NOMBRE = src.Name;
                    model.ULTMODI = src.LastModification;
                    model.USUARIO = src.User;
                    return model;
                });
                cfg.CreateMap<ClientTypeDto, TIPOCLI>().ConvertUsing(src =>
                {
                    var model = new TIPOCLI();
                    model.NUM_TICLI = src.Code;
                    model.NOMBRE = src.Name;
                    model.ULTMODI = src.LastModification;
                    model.USUARIO = src.User;
                    return model;
                });
                cfg.CreateMap<TIPOCLI, ClientTypeDto>().ConvertUsing(src =>
                {
                    var model = new ClientTypeDto();
                    model.Code = src.NUM_TICLI;
                    model.Name = src.NOMBRE;
                    model.User = src.USUARIO;
                    model.LastModification = src.ULTMODI;
                    return model;
                });
                cfg.CreateMap<ClientZoneDto, ZONAS>().ConvertUsing(src =>
                {
                    var model = new ZONAS();
                    model.NUM_ZONA = src.Code;
                    model.NOMBRE = src.Name;
                    model.ULTMODI = src.LastModification;
                    model.USUARIO = src.User;
                    return model;
                });
                cfg.CreateMap<ZONAS, ClientZoneDto>().ConvertUsing(src =>
                {
                    var model = new ClientZoneDto();
                    model.Code = src.NUM_ZONA;
                    model.Name = src.NOMBRE;
                    model.LastModification = src.ULTMODI;
                    model.User = src.USUARIO;
                    return model;
                });
                cfg.CreateMap<VisitTypeDto, TIPOVISITAS>().ConvertUsing(src =>
                {
                    var model = new TIPOVISITAS();
                    model.NOMBRE_VIS = src.Name;
                    model.CODIGO_VIS = src.Code;
                    model.ULTMODI = src.LastModification;
                    model.USUARIO = src.User;
                    return model;
                });
                cfg.CreateMap<TIPOVISITAS, VisitTypeDto>().ConvertUsing(src =>
                {
                    var model = new VisitTypeDto();
                    model.Code = src.CODIGO_VIS;
                    model.Name = src.NOMBRE_VIS;
                    model.User = src.USUARIO;
                    model.LastModification = src.ULTMODI;
                    return model;
                });
                cfg.CreateMap<ContactTypeDto, TIPOCONTACTO_CLI>().ConvertUsing(src =>
                {
                    var model = new TIPOCONTACTO_CLI();
                    model.CODIGO = src.Code;
                    model.NOMBRE = src.Name;
                    model.ULTMODI = src.LastModification;
                    model.USUARIO = src.User;
                    return model;
                });
                cfg.CreateMap<TIPOCONTACTO_CLI, ContactTypeDto>().ConvertUsing(src =>
                {
                    var model = new ContactTypeDto();
                    model.Code = src.CODIGO;
                    model.Name = src.NOMBRE;
                    model.User = src.USUARIO;
                    model.LastModification = src.ULTMODI;
                    return model;
                });
                cfg.CreateMap<TARCREDI, CreditCardDto>().ConvertUsing(src =>
                {
                    var model = new CreditCardDto();
                    model.Code = src.CODIGO;
                    model.Name = src.NOMBRE;
                    model.LastModification = src.ULTMODI;
                    model.User = src.USUARIO;
                    return model;
                });

                cfg.CreateMap<TARJETA_EMP, CompanyCardDto>().ConvertUsing(src =>
                {
                    var model = new CompanyCardDto();
                    model.Code = src.COD_TARJETA;
                    model.Name = src.NOMBRE;
                    model.Conditions = src.CONDICIONES;
                    model.Prefix = src.PREFIJO;
                    model.Price = src.PRECIO;
                    return model;
                });

                cfg.CreateMap<CompanyCardDto, TARJETA_EMP>().ConvertUsing(src =>
                {
                    var model = new TARJETA_EMP();
                    model.COD_TARJETA = src.Code;
                    model.CONDICIONES = src.Conditions;
                    model.PRECIO = src.Price;
                    model.PREFIJO = src.Prefix;
                    model.NOMBRE = src.Name;
                    return model;
                });
                cfg.CreateMap<BANCO, BanksDto>().ConvertUsing(src =>
                 {
                     var model = new BanksDto();
                     model.Code = src.CODBAN;
                     model.Name = src.NOMBRE;
                     model.Swift = src.SWIFT;
                     model.LastModification = src.ULTMODI;
                     return model;
                 });
                cfg.CreateMap<CLAVEPTO, BudgetKeyDto>().ConvertUsing(src =>
                {
                    var model = new BudgetKeyDto();
                    model.Code = src.COD_CLAVE;
                    model.Name = src.NOMBRE;
                    model.LastModification = src.ULTMODI;
                    model.User = src.USUARIO;
                    return model;
                });
                cfg.CreateMap<BudgetKeyDto, CLAVEPTO>().ConvertUsing(src =>
                {
                    var model = new CLAVEPTO();
                    model.COD_CLAVE = src.Code;
                    model.NOMBRE = src.Name;
                    model.ULTMODI = src.LastModification;
                    model.USUARIO = src.User;
                    return model;
                });

                cfg.CreateMap<ChannelDto, CANAL>().ConvertUsing(src =>
                {
                    var model = new CANAL();
                    model.CODIGO = src.Code;
                    model.NOMBRE = src.Name;
                    model.ULTMODI = src.LastModification;
                    model.USUARIO = src.User;
                    return model;
                });
                cfg.CreateMap<CANAL, ChannelDto>().ConvertUsing(src =>
                {
                    var model = new ChannelDto();
                    model.Code = src.CODIGO;
                    model.Name = src.NOMBRE;
                    model.LastModification = src.ULTMODI;
                    model.User = src.USUARIO;
                    return model;
                });
                cfg.CreateMap<TIPO_CARGO, PeoplePositionDto>().ConvertUsing(src =>
                {
                    var model = new PeoplePositionDto();
                    model.Code = src.CODIGO;
                    model.Name = src.NOMBRE;
                    return model;
                });
                cfg.CreateMap<PeoplePositionDto, TIPO_CARGO>().ConvertUsing(src =>
                {
                    var model = new TIPO_CARGO();
                    model.CODIGO = Convert.ToByte(src.Code);
                    model.NOMBRE = src.Name;
                    return model;
                });

                cfg.CreateMap<BLOQUEFAC, InvoiceBlockDto>().ConvertUsing(src =>
                {
                    var model = new InvoiceBlockDto();
                    model.Code = src.CODIGO;
                    model.Name = src.NOMBRE;
                    model.LastModification = src.ULTMODI;
                    model.User = src.USUARIO;
                    return model;
                });
                cfg.CreateMap<InvoiceBlockDto, BLOQUEFAC>().ConvertUsing(src =>
                {
                    var model = new BLOQUEFAC();
                    model.CODIGO = src.Code;
                    model.NOMBRE = src.Name;
                    model.ULTMODI = src.LastModification;
                    model.USUARIO = src.User;
                    return model;
                });
                cfg.CreateMap<BanksDto, BANCO>().ConvertUsing(src =>
                {
                    var model = new BANCO();
                    model.CODBAN = src.Code;
                    model.NOMBRE = src.Name;
                    model.ULTMODI = src.LastModification;
                    model.SWIFT = src.Swift;
                    return model;
                });
                cfg.CreateMap<FORMAS_PEDENT, DeliveringFormDto>().ConvertUsing(src =>
                    {
                        var model = new DeliveringFormDto();
                        model.Codigo = src.CODIGO;
                        model.Nombre = src.NOMBRE;
                        return model;
                    });

                cfg.CreateMap<VehicleProvisionReasonDto, MOT_REPOSTAJE>().ConvertUsing(src =>
                {
                    var model = new MOT_REPOSTAJE();
                    model.COD_MOT = src.Code;
                    model.NOM_MOT = src.Name;
                    return model;
                });


                cfg.CreateMap<CATEGO, VehicleTypeDto>().ConvertUsing(src =>
                {
                    var model = new VehicleTypeDto();
                    model.Code = src.CODIGO;
                    model.Name = src.NOMBRE;
                    model.WebName = src.NOMWEB;
                    model.OfferMargin = src.DIAS_MARGEN;
                    model.LastModification = src.ULTMODI;
                    // model.TerminationDate = src.
                    return model;
                });
                cfg.CreateMap<VehicleTypeDto, CATEGO>().ConvertUsing(src =>
                {
                    var model = new CATEGO();
                    model.CODIGO = src.Code;
                    model.NOMBRE = src.Name;
                    model.NOMWEB = src.WebName;
                    model.DIAS_MARGEN = src.OfferMargin;
                    model.ULTMODI = src.LastModification;
                    return model;
                });

                cfg.CreateMap<DeliveringFormDto, FORMAS_PEDENT>().ConvertUsing(src =>
                {
                    var model = new FORMAS_PEDENT();
                    model.CODIGO = src.Codigo;
                    model.NOMBRE = src.Nombre;
                    return model;
                });
                cfg.CreateMap<PriceConditionDto, TL_CONDICION_PRECIO>().ConvertUsing(src =>
                {
                    var model = new TL_CONDICION_PRECIO();
                    model.CODIGO = src.Codigo;
                    model.NOMBRE = src.Nombre;
                    model.DESCRIPCION = src.Description;
                    return model;
                });
                cfg.CreateMap<SUBLICEN, CompanyDto>().ConvertUsing(src =>
                {
                    var model = new CompanyDto();
                    model.Code = src.CODIGO;
                    model.Name = src.NOMBRE;
                    model.CommercialName = src.NOMCOMER;
                    model.Poblacion = src.POBLACION;
                    model.Nif = src.NIF;
                    return model;
                });
                cfg.CreateMap<DIVISAS, CurrencyDto>().ConvertUsing(src =>
                {
                    var model = new CurrencyDto();
                    model.Codigo = src.CODIGO;
                    model.Nombre = src.NOMBRE;
                    return model;
                });
                cfg.CreateMap<BranchesDto, ProDelega>().ConvertUsing(src =>
                {
                    var model = new ProDelega();
                    model.cldIdCliente = src.BranchKeyId;
                    model.cldDireccion1 = src.Address;
                    model.cldDireccion2 = src.Address2;
                    model.cldEMail = src.Email;
                    model.cldIdDelega = src.BranchId.ToString();
                    model.cldDelegacion = src.Branch;
                    model.cldObservaciones = src.Notes;
                    model.cldPoblacion = src.City;
                    model.cldTelefono1 = src.Phone;
                    model.cldTelefono2 = src.Phone2;
                    if (src.Province != null)
                    {
                        model.cldIdProvincia = src.Province.Code;
                    }
                    return model;
                });
                cfg.CreateMap<ProDelega, BranchesDto>().ConvertUsing(src =>
                {
                    var model = new BranchesDto();
                    model.Branch = src.cldDelegacion;
                    model.BranchId = src.cldIdDelega;
                    model.Address = src.cldDireccion1;
                    model.Address2 = src.cldDireccion2;
                    model.Email = src.cldEMail;
                    model.City = src.cldPoblacion;
                    model.Phone = src.cldTelefono1;
                    model.Phone2 = src.cldTelefono2;
                    model.Notes = src.cldObservaciones;
                    model.BranchKeyId = src.cldIdDelega;
                    model.Province = new ProvinciaDto();
                    model.Province.Code = src.cldIdProvincia;
                    return model;
                });
                cfg.CreateMap<MESES, MonthsDto>().ConstructUsing(src =>
                {
                    var model = new MonthsDto();
                    model.MES = src.MES;
                    model.NUMERO_MES = src.NUMERO_MES;
                    return model;
                });

                cfg.CreateMap<FORMAS, PaymentFormDto>().ConvertUsing(src =>
                {
                    var model = new PaymentFormDto();
                    model.Codigo = src.CODIGO;
                    model.Nombre = src.NOMBRE;
                    model.Cuenta = src.CUENTA;
                    return model;
                });
                cfg.CreateMap<POBLACIONES, CityDto>().ConvertUsing(src =>
                {
                    var model = new CityDto();
                    model.Code = src.CP;
                    model.Poblacion = src.POBLA;
                    model.Pais = src.PAIS;
                    model.Country = new CountryDto();
                    model.Country.Code = src.PAIS;
                    return model;
                });
                cfg.CreateMap<SUBLICEN, CompanyDto>().ConvertUsing(src =>
                {
                    var model = new CompanyDto();
                    model.Code = src.CODIGO;
                    model.Name = src.NOMBRE;
                    model.Nif = src.NIF;
                    model.Poblacion = src.POBLACION;
                    model.CommercialName = src.NOMCOMER;
                    return model;
                });
                cfg.CreateMap<CU1, AccountDto>().ConvertUsing(src =>
                {
                    var model = new AccountDto();
                    model.Codigo = src.CODIGO;
                    model.Description = src.DESCRIP;
                    model.Cuenta = src.CC;
                    return model;
                });
                cfg.CreateMap<BusinessDto, NEGOCIO>().ConvertUsing(src =>
                {
                    var model = new NEGOCIO();
                    model.CODIGO = src.Code;
                    model.NOMBRE = src.Name;
                    model.ULTMODI = src.LastModification;
                    model.USUARIO = src.User;
                    return model;
                });

            });
            var mappingConfig = config.CreateMapper();

            return mappingConfig;
        }
    }


    public class OfficeConverter : ITypeConverter<OFICINAS, OfficeDtos>
    {
        public OfficeDtos Convert(OFICINAS source, OfficeDtos destination, ResolutionContext context)
        {
            OfficeDtos office = new OfficeDtos();
            office.Codigo = source.CODIGO;
            office.Nombre = source.NOMBRE;
            return office;
        }
    }
}


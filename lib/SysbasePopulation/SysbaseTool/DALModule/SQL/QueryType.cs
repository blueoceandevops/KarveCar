﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.SQL
{
    public enum QueryType
    {
        QueryCity,
        QueryMarket,
        QueryCompany,
        QueryLanguage,
        QueryCreditCard,
        QueryZone,
        QueryOfficeZone,
        QuerySeller,
        QueryOffice,
        QueryActivity,
        QueryProvince,
        QueryPaymentForm,
        QueryChannel,
        QueryClientType,
        QueryRentingUse,
        QueryClientSummary,
        QueryClientContacts,
        QueryClient1,
        QueryClient2,
        DeleteClientContacts,
        DeleteClientBranches,
        DeleteClientVisits,
        QueryPagedClient,
        QueryClientPagedSummary,
        QueryPagedCompany,
        QueryCompanySummary,
        QueryCountry,
        QueryOffices,
        QueryOfficeSummary,
        QueryOfficeSummaryByCompany,
        HolidaysByOffice,
        HolidaysDate,
        QueryCurrency,
        QueryVehicleSummary,
        QuerySupplierSummary,
        QueryVehicleSummaryPaged,
        QuerySupplierSummaryPaged,
        QueryBrokerSummary,
        QueryBroker,
        QuerySellerSummary,
        QueryInvoiceSummary,
        QueryInvoiceSummaryExtended,
        QueryInvoiceSummaryPaged,
        QueryClientSummaryExt,
        QueryCommissionAgentSummary,
        QueryBrokerContacts,
        QueryResellerByClient
    };
}

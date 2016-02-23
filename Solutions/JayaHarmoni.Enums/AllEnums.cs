using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JayaHarmoni.Enums
{
    public enum EnumDataStatus
    {
        New,
        Updated,
        Deleted
    }

    public enum EnumInvoiceStatus
    {
        Baru,
        Telah_Dibayar
    }

    public enum EnumReports
    {
        RptMasterCustomer,
        RptMasterEmp,
        RptMasterEquip,
        RptPrintInvoice
    }
}

﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarveModel
{
    /// <summary>
    ///  This model an office.
    /// </summary>
    public class Office : KarveModel.DomainObject
    {
        public override void AcceptChanges()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable GetErrors(string propertyName)
        {
            throw new NotImplementedException();
        }

        public override void RejectChanges()
        {
            throw new NotImplementedException();
        }
    }
}
﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;
using System.Web;
using Develappers.BillomatNet.Mapping;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Queries
{
    internal class InvoiceQueryStringBuilder : QueryStringBuilder<Invoice, Api.Invoice, InvoiceFilter>
    {
        protected internal override string GetFilterStringFor(InvoiceFilter filter)
        {
            if (filter == null)
            {
                return string.Empty;
            }

            var filters = new List<string>();
            if (filter.ClientId.HasValue)
            {
                filters.Add($"client_id={filter.ClientId.Value}");
            }

            if (filter.ContactId.HasValue)
            {
                filters.Add($"contact_id={filter.ContactId.Value}");
            }

            if (!string.IsNullOrEmpty(filter.InvoiceNumber))
            {
                filters.Add($"invoice_number={HttpUtility.UrlEncode(filter.InvoiceNumber)}");
            }

            if ((filter.Status?.Count ?? 0) > 0)
            {
                filters.Add($"status={string.Join(",", filter.Status.Select(MappingHelpers.ToApiValue))}");
            }

            if ((filter.PaymentType?.Count ?? 0) > 0)
            {
                filters.Add($"payment_type={string.Join(",", filter.PaymentType.Select(MappingHelpers.ToApiValue))}");
            }

            if (filter.From.HasValue)
            {
                filters.Add($"from={filter.From.Value:yyyy-MM-dd}");
            }

            if (filter.To.HasValue)
            {
                filters.Add($"from={filter.To.Value:yyyy-MM-dd}");
            }

            if (!string.IsNullOrEmpty(filter.Label))
            {
                filters.Add($"label={HttpUtility.UrlEncode(filter.Label)}");
            }

            if (!string.IsNullOrEmpty(filter.Intro))
            {
                filters.Add($"intro={HttpUtility.UrlEncode(filter.Intro)}");
            }

            if (!string.IsNullOrEmpty(filter.Note))
            {
                filters.Add($"note={HttpUtility.UrlEncode(filter.Note)}");
            }

            if ((filter.Tags?.Count ?? 0) > 0)
            {
                filters.Add($"tags={string.Join(",", filter.Tags.Select(HttpUtility.UrlEncode))}");
            }

            return string.Join("&", filters);
        }
    }
}

﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;
using System.Web;
using Develappers.BillomatNet.Types;

namespace Develappers.BillomatNet.Queries
{
    internal class ClientPropertyQueryStringBuilder : QueryStringBuilder<ClientProperty, Api.ClientProperty, ClientPropertyFilter>
    {
        protected internal override string GetFilterStringFor(ClientPropertyFilter filter)
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

            if (filter.ClientPropertyId.HasValue)
            {
                filters.Add($"client_property_id={filter.ClientPropertyId.Value}");
            }

            if (filter.Value != null)
            {
                string val;
                if (filter.Value is bool)
                {
                    val = filter.Value.ToString();
                }
                else
                {
                    val = (string)filter.Value;
                }

                filters.Add($"value={HttpUtility.UrlEncode(val)}");
            }

            return string.Join("&", filters);
        }
    }

    internal class ClientQueryStringBuilder : QueryStringBuilder<Client, Api.Client, ClientFilter>
    {
        protected internal override string GetFilterStringFor(ClientFilter filter)
        {
            if (filter == null)
            {
                return string.Empty;
            }

            var filters = new List<string>();
            if (!string.IsNullOrEmpty(filter.Name))
            {
                filters.Add($"name={HttpUtility.UrlEncode(filter.Name)}");
            }

            if (!string.IsNullOrEmpty(filter.ClientNumber))
            {
                filters.Add($"client_number={HttpUtility.UrlEncode(filter.ClientNumber)}");
            }

            if (!string.IsNullOrEmpty(filter.Email))
            {
                filters.Add($"email={HttpUtility.UrlEncode(filter.Email)}");
            }

            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                filters.Add($"first_name={HttpUtility.UrlEncode(filter.FirstName)}");
            }

            if (!string.IsNullOrEmpty(filter.LastName))
            {
                filters.Add($"last_name={HttpUtility.UrlEncode(filter.LastName)}");
            }

            if (!string.IsNullOrEmpty(filter.LastName))
            {
                filters.Add($"last_name={HttpUtility.UrlEncode(filter.LastName)}");
            }

            if (!string.IsNullOrEmpty(filter.CountryCode))
            {
                filters.Add($"country_code={HttpUtility.UrlEncode(filter.CountryCode)}");
            }

            if (!string.IsNullOrEmpty(filter.Note))
            {
                filters.Add($"note={HttpUtility.UrlEncode(filter.Note)}");
            }

            if ((filter.InvoiceIds?.Count ?? 0) > 0)
            {
                filters.Add($"invoice_id={string.Join(",", filter.InvoiceIds)}");
            }

            if ((filter.Tags?.Count ?? 0) > 0)
            {
                filters.Add($"tags={string.Join(",", filter.Tags.Select(HttpUtility.UrlEncode))}");
            }

            return string.Join("&", filters);
        }
    }
}

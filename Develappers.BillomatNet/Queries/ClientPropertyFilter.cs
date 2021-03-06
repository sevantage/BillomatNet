﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Develappers.BillomatNet.Queries
{
    /// <summary>
    /// Represents the filter for the article property.
    /// </summary>
    public class ClientPropertyFilter
    {
        public int? ClientId { get; set; }
        public int? ClientPropertyId { get; set; }
        public object Value { get; set; }
    }
}

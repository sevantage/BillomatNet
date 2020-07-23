﻿using System;

namespace Develappers.BillomatNet.Types
{
    /// <summary>
    /// Represents an article.
    /// </summary>
    public class Article
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public string ArticleNumber { get; set; }

        public int? Number { get; set; }

        public string NumberPre { get; set; }

        public int NumberLength { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public float? SalesPrice { get; set; }

        public float? SalesPrice2 { get; set; }

        public float? SalesPrice3 { get; set; }

        public float? SalesPrice4 { get; set; }

        public float? SalesPrice5 { get; set; }

        public string CurrencyCode { get; set; }

        public int? UnitId { get; set; }

        public int? TaxId { get; set; }

        public float? PurchasePrice { get; set; }

        public NetGrossType PurchasePriceNetGross { get; set; }

        public int? SupplierId { get; set; }
    }
}

﻿using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Helpers;
using Develappers.BillomatNet.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Invoice = Develappers.BillomatNet.Types.Invoice;
using InvoiceDocument = Develappers.BillomatNet.Types.InvoiceDocument;
using InvoiceItem = Develappers.BillomatNet.Types.InvoiceItem;

namespace Develappers.BillomatNet
{
    public class InvoiceService : ServiceBase, IEntityService<Invoice, InvoiceFilter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public InvoiceService(Configuration configuration) : base(configuration)
        {
        }


        public Task<Types.PagedList<Invoice>> GetListAsync(CancellationToken token = default(CancellationToken))
        {
            return GetListAsync(null, token);
        }

        public async Task<Types.PagedList<Invoice>> GetListAsync(Query<Invoice, InvoiceFilter> query, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetListAsync<InvoiceListWrapper>("/api/invoices", QueryString.For(query), token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        public async Task<InvoiceDocument> GetPdfAsync(int id, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetItemByIdAsync<InvoiceDocumentWrapper>($"/api/invoices/{id}/pdf", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Returns an invoice by it's id. 
        /// </summary>
        /// <param name="id">The id of the invoice.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The invoice or null if not found.</returns>
        public async Task<Invoice> GetByIdAsync(int id, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetItemByIdAsync<InvoiceWrapper>($"/api/invoices/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Deletes an invoice.
        /// </summary>
        /// <param name="id">The id of the invoice.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        public Task DeleteAsync(int id, CancellationToken token = default(CancellationToken))
        {
            return DeleteAsync($"/api/invoices/{id}", token);
        }

        /// <summary>
        /// Cancels an invoice.
        /// </summary>
        /// <param name="id">The id of the invoice.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        public Task CancelAsync(int id, CancellationToken token = default(CancellationToken))
        {
            return PutAsync<object>($"/api/invoices/{id}/cancel", null, token);
        }

        /// <summary>
        /// Reverses the cancellation of an invoice.
        /// </summary>
        /// <param name="id">The id of the invoice.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        public Task UncancelAsync(int id, CancellationToken token = default(CancellationToken))
        {
            return PutAsync<object>($"/api/invoices/{id}/uncancel", null, token);
        }

        /// <summary>
        /// Completes an invoice.
        /// </summary>
        /// <param name="id">The id of the invoice.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        public Task CompleteAsync(int id, CancellationToken token = default(CancellationToken))
        {
            return CompleteInternalAsync(id, null, token);
        }

        /// <summary>
        /// Completes an invoice.
        /// </summary>
        /// <param name="id">The id of the invoice.</param>
        /// <param name="templateId">The template id.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public Task CompleteAsync(int id, int templateId, CancellationToken token = default(CancellationToken))
        {
            return CompleteInternalAsync(id, templateId, token);
        }

        private async Task CompleteInternalAsync(int id, int? templateId, CancellationToken token)
        {
            var model = new CompleteInvoiceWrapper
            {
                Parameters = new CompleteInvoiceParameters
                {
                    TemplateId = templateId
                }
            };
            await PutAsync<object, CompleteInvoiceWrapper>($"/api/invoices/{id}/complete", model, token).ConfigureAwait(false);
        }

        public async Task<Types.PagedList<InvoiceItem>> GetItemsAsync(int invoiceId, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetListAsync<InvoiceItemListWrapper>("/api/invoice-items", $"invoice_id={invoiceId}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        public async Task<InvoiceItem> GetItemByIdAsync(int id, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetItemByIdAsync<InvoiceItemWrapper>($"/api/invoice-items/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Creates an invoice.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        /// <param name="invoiceItems">The invoice items.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the id of the newly created invoice.
        /// </returns>
        public async Task<Invoice> CreateAsync (Invoice invoice, CancellationToken token = default(CancellationToken))
        {
            var wrappedInvoice = new InvoiceWrapper
            {
                Invoice = invoice.ToApi()
            };
            var result =  await PostAsync("/api/invoices", wrappedInvoice, token);

            return result.ToDomain();
        }

        Task<Invoice> IEntityService<Invoice, InvoiceFilter>.CreateAsync(Invoice model, CancellationToken token = default)
        {
            // TODO: implement implicitly and make public
            throw new System.NotImplementedException();
        }

        Task<Invoice> IEntityService<Invoice, InvoiceFilter>.EditAsync(Invoice model, CancellationToken token = default)
        {
            // TODO: implement implicitly and make public
            throw new System.NotImplementedException();
        }
    }
}
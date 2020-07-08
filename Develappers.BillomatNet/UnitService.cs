﻿using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Helpers;
using Develappers.BillomatNet.Queries;
using System.Threading;
using System.Threading.Tasks;
using Unit = Develappers.BillomatNet.Types.Unit;

namespace Develappers.BillomatNet
{
    public class UnitService : ServiceBase
    {
        public UnitService(Configuration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Retrieves a list of units.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the list of units.
        /// </returns>
        public Task<Types.PagedList<Unit>> GetListAsync(CancellationToken token = default(CancellationToken))
        {
            return GetListAsync(null, token);
        }

        /// <summary>
        /// Retrieves a list of units and applies the filter.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the list of units.
        /// </returns>
        public async Task<Types.PagedList<Unit>> GetListAsync(Query<Unit, UnitFilter> query, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetListAsync<UnitListWrapper>("/api/units", null, token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        /// <summary>
        /// Retrieves a unit item by it's id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the unit.
        /// </returns>
        public async Task<Unit> GetByIdAsync(int id, CancellationToken token = default(CancellationToken))
        {
            var jsonModel = await GetItemByIdAsync<UnitWrapper>($"/api/units/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        public async Task EditAsync(int id, Unit unit, CancellationToken token = default(CancellationToken))
        {
            var wrappedUnit = new UnitWrapper
            {
                Unit = unit.ToApi()
            };
            await PutAsync($"/api/units/{id}", wrappedUnit, token);
        }

        /// <summary>
        /// Creates a unit.
        /// </summary>
        /// <param name="unit">The unit to create.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public async Task CreateAsync(Unit unit, CancellationToken token = default(CancellationToken))
        {
            var wrappedUnit = new UnitWrapper
            {
                Unit = unit.ToApi()
            };
            await PostAsync("/api/units", wrappedUnit, token).ConfigureAwait(false);
        }
    }
}

﻿using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VerasWeb.Models;

namespace VerasWeb.Services
{
    public class CosmosDBService : ICosmosDBService
    {
        private Container _container;

        public CosmosDBService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(Covid19Item item)
        {
            await this._container.CreateItemAsync<Covid19Item>(item, new PartitionKey(item.Id));
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<Covid19Item>(id, new PartitionKey(id));
        }

        public async Task<Covid19Item> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Covid19Item> response = await this._container.ReadItemAsync<Covid19Item>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IEnumerable<Covid19Item>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Covid19Item>(new QueryDefinition(queryString));
            List<Covid19Item> results = new List<Covid19Item>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(string id, Covid19Item item)
        {
            await this._container.UpsertItemAsync<Covid19Item>(item, new PartitionKey(id));
        }
    }
}

using NationBuilder.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NationBuilder
{
    public class NBClient
    {
        private HttpClient _client;
        private string _slug;
        private string _site_slug;
        private string _accessToken;
        private string _endpoint { get { return _endpointCore + "api/v1/"; } set { _endpoint = value; } }
        private string _endpointCore { get { return string.Format("https://{0}.nationbuilder.com/", _slug); } set { _endpointCore = value; } }
        public string SiteSlug { get { return _site_slug; } set { _site_slug = value; } }

        public NBClient(string slug, string accessToken, string siteSlug = "")
        {
            _slug = slug.ToLower();
            _accessToken = accessToken;
            _site_slug = siteSlug.ToLower();

            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            _client.DefaultRequestHeaders.Add("accept", "application/json");
        }

        public async Task<List<T>> GetAllRecords<T>() where T : NBObject
        {
            var NBObject = (NBObject)Activator.CreateInstance(typeof(T));
            var url = _endpoint + NBObject.GetAllEndpoint(_site_slug);

            // Get all records
            var list = new List<T>();
            while (true)
            {
                var response = await _client.GetAsync(url);

                var result = await ReadAsJsonAsync<SegmentResult<T>>(response);
                result.results.ToList().ForEach(r => list.Add(r));

                if (result.next == null) break;

                url = _endpointCore + result.next;
            }
            return list;
        }

        public async Task<T> CreateRecordAsync<T, T2>(T2 recordWrapper) where T : NBObject where T2 : NBWrapper
        {
            var NBObject = (NBObject)Activator.CreateInstance(typeof(T));
            var url = _endpoint + NBObject.CreateEndpoint(_site_slug);
            var content = new StringContent(JsonConvert.SerializeObject(recordWrapper), Encoding.UTF8, "application/json");

            // Create the record
            var response = await _client.PostAsync(url, content);
            var resultWrapper = await ReadAsJsonAsync<T2>(response);

            var newRecord = (T)resultWrapper.Record;
            recordWrapper.Record.Id = newRecord.Id;
            return newRecord;
        }

        public async Task<T> UpdateRecordAsync<T, T2>(long id, T2 recordWrapper) where T : NBObject where T2 : NBWrapper
        {
            recordWrapper.Record.Id = id;

            var NBObject = (NBObject)Activator.CreateInstance(typeof(T));
            var url = _endpoint + NBObject.UpdateEndpoint(_site_slug) + id;
            var content = new StringContent(JsonConvert.SerializeObject(recordWrapper, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }), Encoding.UTF8, "application/json");

            // Update the record
            var response = await _client.PutAsync(url, content);
            var resultWrapper = await ReadAsJsonAsync<T2>(response);
            return (T)resultWrapper.Record;
        }

        public async Task DeleteRecordAsync<T>(long id) where T : NBObject
        {
            var NBObject = (NBObject)Activator.CreateInstance(typeof(T));
            var url = _endpoint + NBObject.DeleteEndpoint(_site_slug) + id;

            // Delete the record
            var response = await _client.DeleteAsync(url);
        }

        private static async Task<T> ReadAsJsonAsync<T>(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("Request failed: " + json);
            }

            return JsonConvert.DeserializeObject<T>(json);
        }

        private class SegmentResult<T>
        {
            public string next { get; set; }
            public string prev { get; set; }
            public T[] results { get; set; }
        }
    }
}

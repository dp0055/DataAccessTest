using DataAccessTest.Models;
using LumenWorks.Framework.IO.Csv;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Caching;

namespace DataAccessTest.DataAccess
{
    public class FileReader : IFileReader
    {
        ObjectCache cache = MemoryCache.Default;
        public async Task<List<FilmData>> LoadCSVData(string fileName)
        {
            List<FilmData> filmDataList = new List<FilmData>();
            if (cache.Contains("filmdata"))
            {
                filmDataList = (List<FilmData>)cache.Get("filmdata");
            }
            else
            {
                //Read CSV file

                using (CsvReader csvReader = new CsvReader(new StreamReader(fileName), true))
                {
                    while (csvReader.ReadNextRecord())
                    {
                        filmDataList.Add(new FilmData { Id = Convert.ToInt32(csvReader[0]), FirstName = csvReader[1], LastName = csvReader[2], Email = csvReader[3], City = csvReader[4], Film = csvReader[5] });
                    }
                }
                CacheItemPolicy cacheItemPolicy = new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddDays(1)
                };
                cache.Set("filmdata", filmDataList, cacheItemPolicy);
            }


            return filmDataList;
        }
        public async Task<List<CarData>> LoadJsonData(string fileName)
        {
            List<CarData> carDataList = new List<CarData>();
            if (cache.Contains("cardata"))
            {
                carDataList = (List<CarData>)cache.Get("cardata");
            }
            else
            {
                //Read Json file
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                using (StreamReader sr = new StreamReader(fs))
                using (JsonTextReader reader = new JsonTextReader(sr))
                {
                    while (reader.TokenType != JsonToken.StartArray)
                        reader.Read();

                    while (reader.Read())
                    {
                        if (reader.TokenType == JsonToken.StartObject)
                        {
                            CarData carData = JObject.Load(reader).ToObject<CarData>();
                            carDataList.Add(carData);
                        }
                    }
                }
                CacheItemPolicy cacheItemPolicy = new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddDays(1)
                };
                cache.Set("cardata", carDataList, cacheItemPolicy);
            }
            return carDataList;
        }
    }
}

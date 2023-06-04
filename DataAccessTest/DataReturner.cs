using DataAccessTest.DataAccess;
using DataAccessTest.Models;

namespace DataAccessTest
{
    public class DataReturner
    {
        //string[] carNames
        //string[] filmsBeginningWithLetters
        //

        IFileReader fileReader;
        public DataReturner(IFileReader _fileReader)
        {
            fileReader = _fileReader;
        }

        public async Task<List<OutputModel>> ReturnData(FilterModel filterModel)
        {
            try
            {               

                string filmfilename = @"C:\Project\DataAccessTest\DataAccessTest\Data\film_data.csv";
                string carfilename = @"C:\Project\DataAccessTest\DataAccessTest\Data\cars.json";

                //Read CSV file
                var filmDataList = await fileReader.LoadCSVData(filmfilename);

                //Read Json file
                var carDataList = await fileReader.LoadJsonData(carfilename);
                var distinctCarNames= carDataList.SelectMany(x=> x.Cars).Select(y => y.Name).Distinct().ToList();

                List<OutputModel> outputList = new List<OutputModel>();
                if (distinctCarNames.Any(x => filterModel.CarNames.Contains(x.ToLower())))
                {
                    //Filtering logic
                    outputList = (from f in filmDataList
                                      join c in carDataList on f.Id equals c.Id
                                      where (filterModel.FilmsBeginningWithLetters.Contains(f.Film.ToLower()[0])
                                      && c.Cars.Any(x => filterModel.CarNames.Contains(x.Name.ToLower())))
                                      select new OutputModel
                                      {
                                          Id = f.Id,
                                          FirstName = f.FirstName,
                                          LastName = f.LastName,
                                          Email = f.Email,
                                          City = f.City,
                                          Film = f.Film,
                                          Cars = c.Cars.Select(x => x.Name).ToList()
                                      }).ToList();
                }
                else
                {
                    outputList = (from f in filmDataList
                                      where (filterModel.FilmsBeginningWithLetters.Contains(f.Film.ToLower()[0]))
                                      select new OutputModel
                                      {
                                          Id = f.Id,
                                          FirstName = f.FirstName,
                                          LastName = f.LastName,
                                          Email = f.Email,
                                          City = f.City,
                                          Film = f.Film,
                                          Cars = new List<string>()
                                      }).ToList();
                }

                return outputList;
            }
            catch(Exception ex)
            {
                //log error
                throw;
            }
        }
    }
}

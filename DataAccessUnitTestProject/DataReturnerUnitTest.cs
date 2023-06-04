using DataAccessTest;
using DataAccessTest.DataAccess;
using DataAccessTest.Models;
using Moq;
using Newtonsoft.Json;

namespace DataAccessUnitTestProject
{
    [TestClass]
    public class DataReturnerUnitTest
    {
        DataReturner dataReturner = null;

        [TestInitialize]
        public void Setup()
        {
            var mockFileReader = new Mock<IFileReader>();
            mockFileReader.Setup(x => x.LoadCSVData(It.IsAny<string>())).Returns(Task.FromResult(GetFilmDatas()));
            mockFileReader.Setup(x => x.LoadJsonData(It.IsAny<string>())).Returns(Task.FromResult(GetCars()));
            dataReturner = new DataReturner(mockFileReader.Object);
        }

        [TestMethod]
        public void ReturnData_WhenFilmsBeginningWithLettersTAndCarNameIsToyota_ReturnFilmStartWithTAndCarnameShouldBeToyota()
        {
            // Arrange
            
            FilterModel filterModel = new FilterModel();
            filterModel.FilmsBeginningWithLetters = new char[] { 's' };
            filterModel.CarNames = new string[] { "toyota" };

            var expectedOutputValue = new List<OutputModel> { new OutputModel { Id = 3, FirstName = "Gail", LastName = "Benneyworth", Email = "gbenneyworth2@fda.gov", City = "Kertahayu", Film = "Skin Game", Cars = new List<string> { "Toyota", "Jeep" } } };

            // Act
            var outputData = dataReturner.ReturnData(filterModel).Result;

            // Assert
            Assert.IsNotNull(outputData);
            Assert.AreEqual(1, outputData.Count);
            Assert.AreEqual(expectedOutputValue[0].Cars.Count, outputData[0].Cars.Count);
            var expectedString = JsonConvert.SerializeObject(expectedOutputValue);
            var actualString = JsonConvert.SerializeObject(outputData);
            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void ReturnData_WhenFilmsBeginningWithLettersZAndCarNameIsUnknown_ShouldReturnNoMatchingData()
        {
            // Arrange

            FilterModel filterModel = new FilterModel();
            filterModel.FilmsBeginningWithLetters = new char[] { 'z' };
            filterModel.CarNames = new string[] { "unknown" };

            var expectedOutputValue = new List<OutputModel> { };

            // Act
            var outputData = dataReturner.ReturnData(filterModel).Result;

            // Assert
            Assert.AreEqual(0, outputData.Count);
            var expectedString = JsonConvert.SerializeObject(expectedOutputValue);
            var actualString = JsonConvert.SerializeObject(outputData);
            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void ReturnData_WhenFilmsBeginningWithLettersSAndCarNameIsUnknown_ShouldReturnFilmStartWithSOnly()
        {
            // Arrange

            FilterModel filterModel = new FilterModel();
            filterModel.FilmsBeginningWithLetters = new char[] { 's' };
            filterModel.CarNames = new string[] { "unknown" };

            var expectedOutputValue = new List<OutputModel> 
            { 
                new OutputModel { Id = 3, FirstName = "Gail", LastName = "Benneyworth", Email = "gbenneyworth2@fda.gov", City = "Kertahayu", Film = "Skin Game", Cars = new List<string> { } },
                new OutputModel { Id = 5, FirstName = "Monah", LastName = "Golby", Email = "mgolby4@bloglovin.com", City = "Sukamaju", Film = "Shocker", Cars = new List<string> { } }
            };

            // Act
            var outputData = dataReturner.ReturnData(filterModel).Result;

            // Assert
            Assert.AreEqual(2, outputData.Count);
            Assert.AreEqual(expectedOutputValue[0].Cars.Count, outputData[0].Cars.Count);
            Assert.AreEqual(expectedOutputValue[1].Cars.Count, outputData[1].Cars.Count);
            var expectedString = JsonConvert.SerializeObject(expectedOutputValue);
            var actualString = JsonConvert.SerializeObject(outputData);
            Assert.AreEqual(expectedString, actualString);
        }

        private List<FilmData> GetFilmDatas()
        {
            return new List<FilmData>
            {
                new FilmData { Id = 1, FirstName = "Tony", LastName = "MacGinlay", Email = "tmacginlay0@furl.net", City = "Zhonglong", Film = "American Carol, An" },
                new FilmData { Id = 2, FirstName = "Natala", LastName = "Keeri", Email = "nkeeri1@cmu.edu", City = "Kinsale", Film = "Incredible Mr. Limpet, The" },
                new FilmData { Id = 3, FirstName = "Gail", LastName = "Benneyworth", Email = "gbenneyworth2@fda.gov", City = "Kertahayu", Film = "Skin Game" },
                new FilmData { Id = 4, FirstName = "Raynell", LastName = "Stigger", Email = "rstigger3@newyorker.com", City = "Stockholm", Film = "Fire Within, The (Feu follet, Le)" },
                new FilmData { Id = 5, FirstName = "Monah", LastName = "Golby", Email = "mgolby4@bloglovin.com", City = "Sukamaju", Film = "Shocker" },
            };
        }

        //Test Data
        private List<CarData> GetCars()
        {
            return new List<CarData>
            {
                new CarData { Id = 1, Cars= new List<Car> { new Car { Name = "Ford" }, new Car { Name = "Dodge" } } },
                new CarData { Id = 2, Cars= new List<Car> { new Car { Name = "Volkswagen" }, new Car { Name = "Cadillac" }, new Car { Name = "Ford" } } },
                new CarData { Id = 3, Cars= new List<Car> { new Car { Name = "Toyota" }, new Car { Name = "Jeep" } } },
                new CarData { Id = 4, Cars= new List<Car> { new Car { Name = "Volkswagen" }, new Car { Name = "Suzuki" } } },
                new CarData { Id = 5, Cars= new List<Car> { } }
            };
        }
    }
}
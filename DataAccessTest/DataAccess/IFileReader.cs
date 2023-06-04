using DataAccessTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessTest.DataAccess
{
    public interface IFileReader
    {
        Task<List<FilmData>> LoadCSVData(string fileName);
        Task<List<CarData>> LoadJsonData(string fileName);

    }
}
